using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace WcfHotel
{
    public class Hotel
    {
        [DataMember]
        public int hotelId { get; set; }
        [DataMember]
        public string name { get; set; }
        [DataMember]
        public string address { get; set; }
        [DataMember]
        public int pincode { get; set; }
        [DataMember]
        public int rating { get; set; }
    }
}