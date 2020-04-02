using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using ModelLibrary;

namespace HotelRestAPI.DBUtil
{
    public class ManageBooking
    {
        private const string ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=HotelDbtest2;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        private const string GET_ALL = "Select * from booking";

        private const string GET_ONE = "Select * from booking where Booking_id = @id";

        private const string INSERT = "Insert into booking values (@Hotel_No,@Guest_No,@Date_From,@Date_To,@Room_No)";

        private const string UPDATE = "Update booking set Hotel_No = @Hotel_No, Date_From = @Date_From, Date_To = @Date_To where Booking_id = @id";

        private const string DELETE = "Delete from booking where Booking_id = @id";

        public IEnumerable<Booking> Get()
        {
            List<Booking> liste = new List<Booking>();
            SqlConnection conn = new SqlConnection(ConnectionString);
            conn.Open();

            SqlCommand cmd = new SqlCommand(GET_ALL, conn);

            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Booking booking = readBooking(reader);
                liste.Add(booking);
            }

            conn.Close();
            return liste;
        }

        private Booking readBooking(SqlDataReader reader)
        {
            Booking booking = new Booking();
            booking.Booking_id = reader.GetInt32(0);
            booking.Hotel_No = reader.GetInt32(1);
            int guest_No  = reader.GetInt32(2);
            booking.Guest = new ManageGuest().Get(guest_No);
            booking.Date_From = reader.GetDateTime(3);
            booking.Date_To = reader.GetDateTime(4);
            int room_No = reader.GetInt32(5);
            
            booking.room = new ManageRoom().Get(room_No, booking.Hotel_No);
            return booking;
        }


        public Booking Get(int id)
        {
            Booking booking = null;
            SqlConnection conn = new SqlConnection(ConnectionString);
            conn.Open();

            SqlCommand cmd = new SqlCommand(GET_ONE, conn);
            cmd.Parameters.AddWithValue("@id", id);
            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                booking = readBooking(reader);
            }

            conn.Close();
            return booking;
        }


        public bool Post(Booking booking)
        {
            try
            {
                bool ok = false;
                SqlConnection conn = new SqlConnection(ConnectionString);
                conn.Open();

                SqlCommand cmd = new SqlCommand(INSERT, conn);
                cmd.Parameters.AddWithValue("@Hotel_No", booking.Hotel_No);
                cmd.Parameters.AddWithValue("@Date_From", booking.Date_From);
                cmd.Parameters.AddWithValue("@Date_To", booking.Date_To);



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


        public bool Put(int id, Booking booking)
        {
            try
            {
                bool ok = false;
                SqlConnection conn = new SqlConnection(ConnectionString);
                conn.Open();

                SqlCommand cmd = new SqlCommand(UPDATE, conn);
                cmd.Parameters.AddWithValue("@Booking_id", booking.Booking_id);
                cmd.Parameters.AddWithValue("@Hotel_No", booking.Hotel_No);
                cmd.Parameters.AddWithValue("@Date_From", booking.Date_From);
                cmd.Parameters.AddWithValue("@Date_To", booking.Date_To);
                cmd.Parameters.AddWithValue("@id", id);
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

        public bool Delete(int id)
        {
            bool ok = false;
            SqlConnection conn = new SqlConnection(ConnectionString);
            conn.Open();
            SqlCommand cmd = new SqlCommand(DELETE, conn);
            cmd.Parameters.AddWithValue("@id", id);
            int noOfRowsAffected = cmd.ExecuteNonQuery();
            ok = noOfRowsAffected == 1 ? true : false;
            conn.Close();
            return ok;
        }
    }
}