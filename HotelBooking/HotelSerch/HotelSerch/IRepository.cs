using HotelSerch.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelSerch
{
    interface IRepository
    {
        void BookHotels(Book obj);
    }
}
