using System.Collections.Generic;
using System.Threading.Tasks;
using WebbedsTest.Models;

namespace WebbedsTest.Services.Interfaces
{
    public interface IHotelSpecialOffersSearcher
    {
        Task<List<HotelWithOffers>> SearchHotelsForDestination(int destinationId, int numberOfNights);
    }
}
