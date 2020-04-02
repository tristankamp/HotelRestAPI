using System;
using System.Collections.Generic;
using System.Linq;
using HotelRestAPI.DBUtil;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ModelLibrary;

namespace UnitTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestManageHotelPost()
        {
            //Arrange
            ManageHotel manageHotel = new ManageHotel();
            Hotel newHotel = new Hotel(602,"TestHotel","TestAdresse");


            //Act

            bool ok = manageHotel.Post(newHotel);
            bool okdelete = manageHotel.Delete(602);

            //Assert
            Assert.IsTrue(ok);

        }

        [TestMethod]
        public void TestManageHotelAdd()
        {
            //Arrange
            Hotel aHotel = new Hotel(601, "TestOpret", "TestOpret");
            ManageHotel manageHotel = new ManageHotel();
            int numberBefore = (from h in manageHotel.Get()
                select h.Id).Count();

            //Act

            bool ok = manageHotel.Post(aHotel);
            int numbersAfter = (from h in manageHotel.Get()
                select h.Id).Count();
            bool okdelete = manageHotel.Delete(601);

            //Assert
            Assert.AreEqual(numberBefore+1, numbersAfter);
           

        }

        [TestMethod]
        public void TestManageHotelDelete()
        {
            //Arrange
            ManageHotel manageHotel = new ManageHotel();
            Hotel aHotel = new Hotel(603, "Testdelete", "Testdelete");
            bool ok = manageHotel.Post(aHotel);
            int numberBefore = (from h in manageHotel.Get()
                select h.Id).Count();

            //Act

            bool okdelete = manageHotel.Delete(603);
            int numbersAfter = (from h in manageHotel.Get()
                select h.Id).Count();

            //Assert
            Assert.AreEqual(numberBefore-1, numbersAfter);


        }

        [TestMethod]
        public void TestManageHotelPut()
        {
            //Arrange
            ManageHotel manageHotel = new ManageHotel();
            bool okbefore = manageHotel.Post(new Hotel(604,"BeforeHotel","BeforeHotel"));


            //Act
            Hotel putHotel = new Hotel(604,"PutHotel","PutHotel");
            bool ok = manageHotel.Put(604, putHotel);
            List<Hotel> listHotel = manageHotel.Get().ToList();
            Hotel hotelInList = manageHotel.Get(604);
            bool okdelete = manageHotel.Delete(604);


            //Assert
            
            Assert.AreEqual(putHotel, hotelInList);


        }

    }
}
