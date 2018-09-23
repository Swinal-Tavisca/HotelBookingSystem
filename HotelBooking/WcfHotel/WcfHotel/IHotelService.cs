using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace WcfHotel
{
    [ServiceContract]
    public interface IHotelService
    {

        [OperationContract]
        [WebGet(UriTemplate = "/hotel", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        List<Hotel> getAllHotels();


        [OperationContract]
        [WebGet(UriTemplate = "/hotelID/{id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        List<Room> getHotelById(string id);


        [OperationContract]
        [WebInvoke(UriTemplate = "/hotelUpdate", Method = "PUT", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        void update(Room bookObject);

    }
}
