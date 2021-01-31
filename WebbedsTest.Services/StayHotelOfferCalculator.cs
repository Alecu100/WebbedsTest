using WebbedsTest.Models;
using WebbedsTest.Services.Interfaces;

namespace WebbedsTest.Services
{
    public class StayHotelOfferCalculator : IHotelOfferCalculator
    {
        public ERateTypes RateType => ERateTypes.Stay;

        public Offer CalculateOffer(Rate rate, int numberOfNights)
        {
            return new Offer
            {
                BoardType = rate.BoardType,
                Price = rate.Value
            };
        }
    }
}
