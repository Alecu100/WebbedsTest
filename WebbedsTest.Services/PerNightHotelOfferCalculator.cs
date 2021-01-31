using WebbedsTest.Models;
using WebbedsTest.Services.Interfaces;

namespace WebbedsTest.Services
{
    public class PerNightHotelOfferCalculator : IHotelOfferCalculator
    {
        public ERateTypes RateType => ERateTypes.PerNight;

        public Offer CalculateOffer(Rate rate, int numberOfNights)
        {
            return new Offer
            {
                BoardType = rate.BoardType,
                Price = numberOfNights * rate.Value
            };
        }
    }
}
