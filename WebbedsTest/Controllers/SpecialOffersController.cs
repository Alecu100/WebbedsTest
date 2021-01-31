using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NSwag;
using NSwag.Annotations;
using WebbedsTest.Models;
using WebbedsTest.Services.Interfaces;

namespace WebbedsTest.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SpecialOffersController : ControllerBase
    {
        private readonly IHotelSpecialOffersSearcher _hotelSpecialOffersSearcher;

        public SpecialOffersController(IHotelSpecialOffersSearcher hotelSpecialOffersSearcher)
        {
            _hotelSpecialOffersSearcher = hotelSpecialOffersSearcher;
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<HotelWithOffers>), statusCode: 200)]
        //Usually a lot of logic starts to get added in controllers methods growing to hundreds of lines of code, there should be a rule and code analyzer set up
        //so that it points to a code smell or flaw if a controller methods has more than 20 lines of code
        //I think just some custom validation and calling 2-3 classes at most in each method
        public async Task<IActionResult> FindBargain([Required]int? destinationId, [Required]int? nights)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            return Ok(await _hotelSpecialOffersSearcher.SearchHotelsForDestination(destinationId.Value, nights.Value));
        }
    }
}
