using WebbedsTest.Models;

namespace WebbedsTest.Services.Interfaces
{
    /// <summary>
    /// This way, we can easily extend how prices are calculated for each rate by just adding a new offer calculator for a specific rate type
    /// </summary>
    public interface IHotelOfferCalculator
    {
        ERateTypes RateType { get; }

        Offer CalculateOffer(Rate rate, int numberOfNights);
    }
}
