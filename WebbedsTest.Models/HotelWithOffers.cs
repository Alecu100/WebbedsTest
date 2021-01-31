using System;
using System.Collections.Generic;
using System.Text;
using WebbedsTest.Services;

namespace WebbedsTest.Models
{
    public class HotelWithOffers
    {
        public string Name { get; set; }

        public int PropertyId { get; set; }

        public int GeoId { get; set; }

        public short Rating { get; set; }

        public List<Offer> Offers { get; set; }
    }
}
