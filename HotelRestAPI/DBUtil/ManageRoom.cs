using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Http;
using ModelLibrary;

namespace HotelRestAPI.DBUtil
{
    public class ManageRoom
    {
        private const string ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=HotelDbtest2;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        private const string GET_ALL = "Select * from room";

        private const string GET_ONE = "Select * from room where Room_No = @id and Hotel_No = @hotelno";

        private const string INSERT = "Insert into room values (@RoomNr,@Hotel_No,@RoomType,@Price)";

        private const string UPDATE = "Update room set Room_No = @RoomNr,  Types = @RoomType, Price = @Price, Hotel_No = @Hotel_No where Room_No = @id and Hotel_No = @hotelno";

        private const string DELETE = "Delete from room where Room_No = @id and Hotel_No = @hotelno";

        public IEnumerable<Room> Get()
        {
            List<Room> liste = new List<Room>();
            SqlConnection conn = new SqlConnection(ConnectionString);
            conn.Open();

            SqlCommand cmd = new SqlCommand(GET_ALL, conn);

            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Room room = readRoom(reader);
                liste.Add(room);
            }

            conn.Close();
            return liste;
        }

        private Room readRoom(SqlDataReader reader)
        {
            Room room = new Room();
            room.RoomNr = reader.GetInt32(0);
            room.Hotel_No = reader.GetInt32(1);
            room.RoomType = reader.GetString(2);
            room.Price = reader.GetDouble(3);
            return room;
        }


        public Room Get(int id, int hotelno)
        {
            Room room = null;
            SqlConnection conn = new SqlConnection(ConnectionString);
            conn.Open();

            SqlCommand cmd = new SqlCommand(GET_ONE, conn);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Parameters.AddWithValue("@hotelno", hotelno);
            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                room = readRoom(reader);
            }

            conn.Close();
            return room;
        }

        public bool Post(Room room)
        {
            try
            {
                bool ok = false;
                SqlConnection conn = new SqlConnection(ConnectionString);
                conn.Open();

                SqlCommand cmd = new SqlCommand(INSERT, conn);
                cmd.Parameters.AddWithValue("@RoomNr", room.RoomNr);
                cmd.Parameters.AddWithValue("@RoomType", room.RoomType);
                cmd.Parameters.AddWithValue("@Price", room.Price);
                cmd.Parameters.AddWithValue("@Hotel_No", room.Hotel_No);


                int noOfRowsAffected = cmd.ExecuteNonQuery();

                ok = noOfRowsAffected == 1 ? true : false;
                conn.Close();
                return ok;
            }

            catch (SqlException e)
            {
                return false;
            }
        }

        public bool Put(int id, int hotelno, Room room)
        {
            try
            {
                bool ok = false;
                SqlConnection conn = new SqlConnection(ConnectionString);
                conn.Open();

                SqlCommand cmd = new SqlCommand(UPDATE, conn);
                cmd.Parameters.AddWithValue("@RoomNr", room.RoomNr);
                cmd.Parameters.AddWithValue("@RoomType", room.RoomType);
                cmd.Parameters.AddWithValue("@Price", room.Price);
                cmd.Parameters.AddWithValue("@Hotel_No", room.Hotel_No);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@hotelno", hotelno);
                int noOfRowsAffected = cmd.ExecuteNonQuery();
                ok = noOfRowsAffected == 1 ? true : false;
                conn.Close();
                return ok;
            }
            catch (SqlException e)
            {
                return false;
            }
        }

        public bool Delete(int id, int hotelno)
        {
            bool ok = false;
            SqlConnection conn = new SqlConnection(ConnectionString);
            conn.Open();
            SqlCommand cmd = new SqlCommand(DELETE, conn);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Parameters.AddWithValue("@hotelno", hotelno);
            int noOfRowsAffected = cmd.ExecuteNonQuery();
            ok = noOfRowsAffected == 1 ? true : false;
            conn.Close();
            return ok;
        }
    }
}