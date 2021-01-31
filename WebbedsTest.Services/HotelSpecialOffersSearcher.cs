using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using RestSharp;
using WebbedsTest.Models;
using WebbedsTest.Services.Interfaces;
using WebbedsTest.Services.Settings;

namespace WebbedsTest.Services
{
    public class HotelSpecialOffersSearcher : IHotelSpecialOffersSearcher
    {
        private readonly IRestClient _restClient;

        private readonly IJsonSerializer _jsonSerializer;

        /// <summary>
        /// We can add new ways of calculating hotel prices without changing this code, just by using dependency injection
        /// We can register multiple hotel offers calculators in the service collection and they will all be passed by the di container as a IEnumerable
        /// </summary>
        private readonly List<IHotelOfferCalculator> _hotelOfferCalculators;

        private readonly IOptions<ApiSettings> _apiSettings;

        public HotelSpecialOffersSearcher(IEnumerable<IHotelOfferCalculator> stayPriceCalculators, IRestClient restClient, IJsonSerializer jsonSerializer, IOptions<ApiSettings> apiSettings)
        {
            _hotelOfferCalculators = stayPriceCalculators.ToList();
            _restClient = restClient;
            _jsonSerializer = jsonSerializer;
            _apiSettings = apiSettings;
            _restClient.BaseUrl = new Uri(_apiSettings.Value.BaseUrl, UriKind.Absolute);
        }


        public async Task<List<HotelWithOffers>> SearchHotelsForDestination(int destinationId, int numberOfNights)
        {
            var request = new RestRequest(_apiSettings.Value.FindBargainUri, Method.GET);

            request.AddQueryParameter(Constants.DestinationId, destinationId.ToString());
            request.AddQueryParameter(Constants.Nights, numberOfNights.ToString());
            request.AddQueryParameter(Constants.Code, _apiSettings.Value.AuthCode);

            var response = await _restClient.ExecuteAsync<string>(request);

            var foundHotels = _jsonSerializer.Deserialize<List<Hotel>>(response.Data);

            var foundHotelsWithOffers = new List<HotelWithOffers>();

            foreach (var hotel in foundHotels)
            {
                var hotelWithOffers = new HotelWithOffers
                {
                    GeoId = hotel.GeoId,
                    Name = hotel.Name,
                    PropertyId = hotel.PropertyId,
                    Rating = hotel.Rating,
                    Offers = new List<Offer>()
                };

                foreach (var rate in hotel.Rates)
                {
                    var stayPriceCalculator = _hotelOfferCalculators.FirstOrDefault(calculator => calculator.RateType == rate.RateType);

                    if (stayPriceCalculator == null)
                    {
                        continue;
                    }


                    hotelWithOffers.Offers.Add(stayPriceCalculator.CalculateOffer(rate, numberOfNights));
                }

                foundHotelsWithOffers.Add(hotelWithOffers);
            }

            return foundHotelsWithOffers;
        }
    }
}
