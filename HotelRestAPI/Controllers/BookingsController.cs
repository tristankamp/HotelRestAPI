using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using HotelRestAPI.DBUtil;
using ModelLibrary;

namespace HotelRestAPI.Controllers
{
    public class BookingsController : ApiController
    {
        private static ManageBooking manager = new ManageBooking();

      
        public IEnumerable<Booking> Get()
        {
            return manager.Get();
        }



        public Booking Get(int id)
        {
            return manager.Get(id);
        }

        public bool Post([FromBody]Booking booking)
        {
            return manager.Post(booking);
        }

        public bool Put(int id, [FromBody]Booking booking)
        {
            return manager.Put(id, booking);
        }

        public bool Delete(int id)
        {
            return manager.Delete(id);
        }
    }
}
