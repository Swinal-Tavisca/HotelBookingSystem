using HotelSerch.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace HotelSerch.Controllers
{
    public class BookingController : ApiController
    {
        public void BookRoom([FromBody] Book bookingObject)
        {
            
            Task.Run(() => {
                string _filepath = "C:/Users/swinal/source/repos/HotelSerch/Hotels.JSON";
                string _result = string.Empty;
                List<HotelJsonData> HotelList = new List<HotelJsonData>();
                using (StreamReader streamreader = new StreamReader(_filepath))
                {
                    var _json = streamreader.ReadToEnd();
                    HotelList = JsonConvert.DeserializeObject<List<HotelJsonData>>(_json);
                }
                foreach (var _hotel in HotelList)
                {
                    if (_hotel.name == bookingObject.name)
                    {
                        bookingObject.hotelid = _hotel.hotelid;
                    }
                }
                DataBaseConnections sqlObject = new DataBaseConnections();
                sqlObject.BookHotels(bookingObject);
            });
            Logger.Instance.InputLogDetails("Booking Room", "Success", "Booking Details added in Database");
        }


        [HttpPut]
        public async void updateCassendra([FromBody] Book bookObj)
        {
            
            string _filepath = "C:/Users/swinal/source/repos/HotelSerch/Hotels.JSON";
            string result = string.Empty;
            List<HotelJsonData> HotelList = new List<HotelJsonData>();
            using (StreamReader streamreader = new StreamReader(_filepath))
            {
                var _json = streamreader.ReadToEnd();
                HotelList = JsonConvert.DeserializeObject<List<HotelJsonData>>(_json);

            }

            foreach (var _hotel in HotelList)
            {
                if (_hotel.name == bookObj.name)
                {
                    bookObj.hotelid = _hotel.hotelid;
                }
            }
            HttpResponseMessage response = null;

            using (var client = new HttpClient())
            {
                string url = "http://localhost:53538/HotelService.svc/hotelUpdate";
                response = await client.PutAsJsonAsync(url, bookObj);
            }
            Logger.Instance.InputLogDetails("Updating Cassendra", "Success", "Cassendra Updated Successfully");
        }

    }
}
