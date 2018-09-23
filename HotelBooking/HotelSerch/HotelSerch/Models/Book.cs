using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelSerch.Models
{
    public class Book
    {
        public int hotelid { get; set;}
        public String name { get; set; }
        public int noofrooms { get; set; }
        public String type { get; set; }
        public int cost { get; set; }
    }
}