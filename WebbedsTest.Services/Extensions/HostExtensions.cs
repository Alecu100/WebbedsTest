using Microsoft.Extensions.DependencyInjection;
using RestSharp;
using WebbedsTest.Services.Interfaces;
using WebbedsTest.Services.Settings;

namespace WebbedsTest.Services.Extensions
{
    public static class HostExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<IHotelSpecialOffersSearcher, HotelSpecialOffersSearcher>();
            serviceCollection.AddSingleton<IHotelOfferCalculator, PerNightHotelOfferCalculator>();
            serviceCollection.AddSingleton<IHotelOfferCalculator, StayHotelOfferCalculator>();
            serviceCollection.AddOptions<ApiSettings>();
            serviceCollection.AddSingleton<IJsonSerializer, JsonSerializer>();
            serviceCollection.AddSingleton<IRestClient>(new RestClient());

            return serviceCollection;
        }
    }
}
