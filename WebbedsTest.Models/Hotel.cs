using System.Collections.Generic;

namespace WebbedsTest.Models
{
    public class Hotel
    {
        public int PropertyId { get; set; }

        public string Name { get; set; }

        public int GeoId { get; set; }

        public short Rating { get; set; }

        public List<Rate> Rates { get; set; }
    }
}
