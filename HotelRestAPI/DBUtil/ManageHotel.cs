using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Sockets;
using System.Web;
using System.Web.Http;
using ModelLibrary;

namespace HotelRestAPI.DBUtil
{
    public class ManageHotel
    {

        private const string ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=HotelDbtest2;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        private const string GET_ALL = "Select * from hotel";

        private const string GET_ONE = "Select * from hotel where Hotel_No = @Id";

        private const string INSERT = "Insert into hotel values (@Id, @Name, @Address)";

        private const string UPDATE = "Update hotel set Hotel_No = @HotelId, Name = @Name, Address = @Address where Hotel_No = @Id";

        private const string DELETE = "Delete from hotel where Hotel_No = @Id";


        public IEnumerable<Hotel> Get()
        {
            List<Hotel> liste = new List<Hotel>();
            SqlConnection conn = new SqlConnection(ConnectionString);
            conn.Open();

            SqlCommand cmd = new SqlCommand(GET_ALL, conn);

            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Hotel hotel = readHotel(reader);
                liste.Add(hotel);
            }

            conn.Close();
            return liste;
        }

        private Hotel readHotel(SqlDataReader reader)
        {
            Hotel hotel = new Hotel();

            hotel.Id = reader.GetInt32(0);
            hotel.Name = reader.GetString(1);
            hotel.Address = reader.GetString(2);


            return hotel;
        }

        
        public Hotel Get(int id)
        {
            Hotel hotel = null;
            SqlConnection conn = new SqlConnection(ConnectionString);
            conn.Open();

            SqlCommand cmd = new SqlCommand(GET_ONE, conn);
            cmd.Parameters.AddWithValue("@Id", id);
            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                hotel = readHotel(reader);
            }

            conn.Close();
            return hotel;
        }

        public bool Post(Hotel hotel)
        {
            try
            {
                bool ok = false;
                SqlConnection conn = new SqlConnection(ConnectionString);
                conn.Open();

                SqlCommand cmd = new SqlCommand(INSERT, conn);
                cmd.Parameters.AddWithValue("@Id", hotel.Id);
                cmd.Parameters.AddWithValue("@Name", hotel.Name);
                cmd.Parameters.AddWithValue("@Address", hotel.Address);
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

      
        public bool Put(int id, Hotel hotel)
        {
            try
            {
                bool ok = false;
                SqlConnection conn = new SqlConnection(ConnectionString);
                conn.Open();


                SqlCommand cmd = new SqlCommand(UPDATE, conn);
                cmd.Parameters.AddWithValue("@HotelId", hotel.Id);
                cmd.Parameters.AddWithValue("@Name", hotel.Name);
                cmd.Parameters.AddWithValue("@Address", hotel.Address);
                cmd.Parameters.AddWithValue("@Id", id);
                int noOfRowsAffected = cmd.ExecuteNonQuery();

                ok = noOfRowsAffected == 1 ? true : false;
                conn.Close();
                return ok;
            }
            catch (SqlException)
            {
                return false;
            }
        }

     
        public bool Delete(int id)
        {
            bool ok = false;
            SqlConnection conn = new SqlConnection(ConnectionString);
            conn.Open();

            SqlCommand cmd = new SqlCommand(DELETE, conn);
            cmd.Parameters.AddWithValue("@Id", id);
            int noOfRowsAffected = cmd.ExecuteNonQuery();

            ok = noOfRowsAffected == 1 ? true : false;
            conn.Close();
            return ok;
        }
    }
}