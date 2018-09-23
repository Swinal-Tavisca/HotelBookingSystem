using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WcfHotel
{
    public class Room
    {
        public int hotelid { get; set; }
        public string roomtype{get; set;}
        public int cost { get; set;}
        public int noofrooms { get; set; }
    }
}