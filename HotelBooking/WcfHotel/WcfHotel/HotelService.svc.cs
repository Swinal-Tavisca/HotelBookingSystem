using Cassandra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WcfHotel
{
    public class HotelService : IHotelService
    {
        Cluster cluster = Cluster.Builder().AddContactPoint("127.0.0.1").Build();
        List<Hotel> _hotelList = new List<Hotel>();
        List<Room> _roomList = new List<Room>();
        public List<Hotel> getAllHotels()
        {
            ISession _session = cluster.Connect("hotel_info");
            string query = "SELECT * FROM  hotel_info.hotel";
            var res = _session.Execute(query);
            foreach (var row in res)
            {
                int id = row.GetValue<int>("hotelid");
                string hoteladdress = row.GetValue<string>("address");
                string hotelname = row.GetValue<string>("name");
                int pincode = row.GetValue<int>("pincode");
                int rating = row.GetValue<int>("rating");
                _hotelList.Add(new Hotel { hotelId = id, address = hoteladdress, name = hotelname, pincode = pincode, rating = rating });
            }
            return _hotelList;
        }

        public List<Room> getHotelById(string hotelID)
        {
            ISession _session = cluster.Connect("hotel_info");
            int id = Int32.Parse(hotelID);
            string query = "SELECT * FROM  hotel_info.rooms WHERE hotelid=" + id;
            var res = _session.Execute(query);
            foreach (var row in res)
            {
                int hotelid = row.GetValue<int>("hotelid");
                string roomtype = row.GetValue<string>("roomtype");
                int roomavailabilty = row.GetValue<int>("noofrooms");
                int roomprice = row.GetValue<int>("cost");

                _roomList.Add(new Room { hotelid = id, roomtype = roomtype, noofrooms = roomavailabilty, cost = roomprice });
            }
            return _roomList;
        }
        public void update(Room bookObject)
        {
            ISession _session = cluster.Connect("hotel_info");
            int availablerooms = 0;
            int noRooms = bookObject.noofrooms;
            string query = "SELECT * FROM  hotel_info.rooms where roomtype= '" + bookObject.roomtype + "' AND hotelid=" + bookObject.hotelid;
            var res = _session.Execute(query);
            foreach (var row in res)
            {
                availablerooms = int.Parse(row.GetValue<int>("noofrooms").ToString());
            }
            availablerooms = availablerooms - noRooms;
            query = "Update hotel_info.rooms Set noofrooms =  " + availablerooms + " Where roomtype = '" + bookObject.roomtype + "' AND hotelid=" + bookObject.hotelid;
            _session.Execute(query);
        }
    }
}
