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
    public class RoomsController : ApiController
    {
        private static ManageRoom manager = new ManageRoom();

        // GET: api/Room
        public IEnumerable<Room> Get()
        {
            return manager.Get();
        }

        // Get: api/rooms/1/2
        [HttpGet]
        [Route("api/rooms/{id}/{hotelno}")]

        public Room Get(int id,int hotelno)
        {
            return manager.Get(id, hotelno);
        }

        public bool Post([FromBody]Room room)
        {
            return manager.Post(room);
        }

        [HttpPut]
        [Route("api/rooms/{id}/{hotelno}")]
        public bool Put(int id, int hotelno, [FromBody]Room room)
        {
            return manager.Put(id, hotelno, room);
        }

        [HttpDelete]
        [Route("api/rooms/{id}/{hotelno}")]
        public bool Delete(int id, int hotelno)
        {
            return manager.Delete(id, hotelno);
        }
    }
}
