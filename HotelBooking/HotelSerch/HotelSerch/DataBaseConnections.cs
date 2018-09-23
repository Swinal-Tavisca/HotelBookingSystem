using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HotelSerch.Models;
using System.Data.SqlClient;

namespace HotelSerch
{
    public class DataBaseConnections : IRepository
    {
        public void BookHotels(Book obj)
        {
            string query;
            SqlConnection _connection = new SqlConnection();
            _connection.ConnectionString = "Data Source=.;Initial Catalog=Hotel;User ID=sa;Password=test123!@#";
            Logger.Instance.InputLogDetails("SQL Connection", "Success", "SQL Successfully Connected");
            _connection.Open();
            query = "insert into bookingHotel values(@hotelid,@name,@noofrooms,@cost,@type)";
            SqlCommand cmd = new SqlCommand(query, _connection);
            cmd.Parameters.Add(new SqlParameter("@hotelid", obj.hotelid));
            cmd.Parameters.Add(new SqlParameter("@name", obj.name));
            cmd.Parameters.Add(new SqlParameter("@noofrooms", obj.noofrooms));
            cmd.Parameters.Add(new SqlParameter("@cost", obj.cost));
            cmd.Parameters.Add(new SqlParameter("@type", obj.type));
            cmd.ExecuteNonQuery();
            Logger.Instance.InputLogDetails("Insert data in SQL", "Success", "Data Successfully Inserted");
            _connection.Close();
        }


    }
}