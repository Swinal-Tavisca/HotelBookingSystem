using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Cassandra;
namespace HotelSerch
{
    public class Logger
    {
        public static Logger _logger;
        public static Logger Instance
        {
            get
            {
                if (_logger == null)
                    _logger = new Logger();

                return _logger;
            }
        }

        public void InputLogDetails(string Request, string Response, string Comment)
        {
            var cluster = Cluster.Builder().AddContactPoints("127.0.0.1").Build();

            var session = cluster.Connect("hotel_info");

            string query = "Insert into  logger  (id, Request, Response, Comment, Time) values (uuid(),'" + Request + "', '" + Response + "', '" + Comment + "', dateof(now()))";

            var res = session.Execute(query);

        }
    }
}