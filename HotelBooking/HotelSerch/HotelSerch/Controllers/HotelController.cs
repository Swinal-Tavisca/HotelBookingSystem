using HotelSerch.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace HotelSerch.Controllers
{
    public class HotelController : ApiController
    {
        public async System.Threading.Tasks.Task<List<HotelWCFData>> GetDetailsFromWCF()
        {
            
            var _client = new HttpClient();
            var _response = await _client.GetAsync("http://localhost:53538/HotelService.svc/hotel");
            List<HotelWCFData> _content = new List<HotelWCFData>();
            if (_response.StatusCode == HttpStatusCode.OK)
            {
                _content = await _response.Content.ReadAsAsync<List<HotelWCFData>>();

            }
            Logger.Instance.InputLogDetails("Get Data from WCF", "Success", "Data successfully retrived from WCF");
            return _content;
        }
        public List<HotelJsonData> GetDetailsFromJSON()
        {
            
            string _filepath = "C:/Users/swinal/source/repos/HotelSerch/Hotels.JSON";
            string _result = string.Empty;
            List<HotelJsonData> HotelList = new List<HotelJsonData>();
            Logger.Instance.InputLogDetails("Get Data from JSON", "Success", "Data Succesfully retrived from JSON");
            using (StreamReader reader = new StreamReader(_filepath))
            {
                var json = reader.ReadToEnd();
                HotelList = JsonConvert.DeserializeObject<List<HotelJsonData>>(json);
                return HotelList;
            }
        }
        [HttpGet]
        [Route("api/hotel/getBothDetails")]
        public async System.Threading.Tasks.Task<List<MergedJson_Wcf>> GetCombinedDetailsFromJSON_WCF()
        {
            List<MergedJson_Wcf> _hotelList = new List<MergedJson_Wcf>();
            List<HotelJsonData> _HotelListFromJson = new List<HotelJsonData>();
            List<HotelWCFData> _HotelListFromWcf = new List<HotelWCFData>();
            System.Threading.Tasks.Task<List<HotelWCFData>> HotelListOfWcfTask = GetDetailsFromWCF();
            _HotelListFromWcf = await HotelListOfWcfTask;
            _HotelListFromJson = GetDetailsFromJSON();
            for (int i = 0; i < _HotelListFromWcf.Count; i++)
            {
                MergedJson_Wcf hotel = new MergedJson_Wcf();
                hotel.hotelid = _HotelListFromWcf[i].hotelid;
                hotel.name = _HotelListFromWcf[i].name;
                hotel.rating = _HotelListFromWcf[i].rating;
                hotel.pincode = _HotelListFromWcf[i].pincode;
                hotel.address = _HotelListFromWcf[i].address;
                hotel.HotelDescription = _HotelListFromJson[i].HotelDescription;
                hotel.HotelContactNo = _HotelListFromJson[i].HotelContactNo;
                hotel.Amenities = _HotelListFromJson[i].Amenities;
                hotel.HotelImageURL = _HotelListFromJson[i].HotelImageURL;
                _hotelList.Add(hotel);
            }
            Logger.Instance.InputLogDetails("Combine data WCF-JSON", "Success", "Data successfully combined");
            return _hotelList;
        }

        [HttpGet]
        [Route("api/HotelService/Room/{hotelname}")]
        public async System.Threading.Tasks.Task<List<Room>> getRoomsOfAHotel(string _hotelname)
        {
            
            List<Room> _roomsList = new List<Room>();
            string filepath = "C:/Users/swinal/source/repos/HotelSerch/Hotels.JSON";
            string result = string.Empty;
            List<HotelJsonData> HotelList = new List<HotelJsonData>();
            using (StreamReader streamreader = new StreamReader(filepath))
            {
                var json = streamreader.ReadToEnd();
                HotelList = JsonConvert.DeserializeObject<List<HotelJsonData>>(json);

            }
            string hotelid = null;
            foreach (var hotel in HotelList)
            {
                if (hotel.name == _hotelname)
                {
                    hotelid = hotel.hotelid.ToString();
                    break;
                }
            }
            var client = new HttpClient();
            string url = "http://localhost:53538/HotelService.svc/Room/" + hotelid;
            HttpResponseMessage response = await client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                _roomsList = await response.Content.ReadAsAsync<List<Room>>();

            }
            Logger.Instance.InputLogDetails("Get Rooms Of Hotel", "Success", "Data Successfully retrived");
            return _roomsList;

        }
    }
}
