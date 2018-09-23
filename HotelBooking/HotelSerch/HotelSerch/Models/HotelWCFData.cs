using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelSerch.Models
{
    public class HotelWCFData
    {
        public int hotelid { get; set; }
        public string name { get; set; }
        public string address { get; set; }
        public int pincode { get; set; }
        public int rating { get; set; }
        
    }
}