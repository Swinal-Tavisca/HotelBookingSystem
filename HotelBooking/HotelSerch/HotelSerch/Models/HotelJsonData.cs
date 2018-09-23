using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelSerch.Models
{
    public class HotelJsonData
    {
        public int hotelid { get; set; }
        public string name { get; set; }
        public string address { get; set; }
        public string HotelDescription { get; set; }
        public string Amenities { get; set; }
        public int HotelContactNo { get; set;}
        public string HotelImageURL { get; set; }
    }
}
 