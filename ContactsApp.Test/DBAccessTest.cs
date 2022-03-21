using ContactsApp.Models;
using ContactsApp.Database;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactsApp.Test
{
    [TestClass]
    public class DBAccessTest
    {

        [TestMethod]
        public void GetTestConfigString_Test()
        {
            //Arrange

            //Act
            string ConfigString = SqlHelper.GetTestConfigString();

            //Assert
            Assert.AreNotEqual(ConfigString, string.Empty);
        }

        [TestMethod]
        public void LoadContacts_Test()
        {
            //Arrange
            string ConfigString = SqlHelper.GetTestConfigString();
            SQLiteDBAccess sqliteDBAccess = new SQLiteDBAccess(ConfigString);

            //Act
            List<ContactModel> contactList = sqliteDBAccess.LoadContacts();

            //Assert
            Assert.AreEqual(string.Empty, sqliteDBAccess.Error);
            Assert.AreNotEqual(0, contactList.Count);
        }

        [TestMethod]
        public void GetContact_ValidId_Test()
        {
            //Arrange
            string ConfigString = SqlHelper.GetTestConfigString();
            SQLiteDBAccess sqliteDBAccess = new SQLiteDBAccess(ConfigString);

            //Act
            ContactModel contact = sqliteDBAccess.GetContact(0);

            //Assert
            Assert.AreEqual(string.Empty, sqliteDBAccess.Error);
            Assert.AreEqual(contact.Name, "Jon Doe");
        }

        [TestMethod]
        public void GetContact_NotValidId_Test()
        {
            //Arrange
            string ConfigString = SqlHelper.GetTestConfigString();
            SQLiteDBAccess sqliteDBAccess = new SQLiteDBAccess(ConfigString);

            //Act
            ContactModel contact = sqliteDBAccess.GetContact(9999999);

            //Assert
            Assert.AreEqual("Contact not found", sqliteDBAccess.Error);
        }

        [TestMethod]
        public void UpdateContact_ValidModel_Test()
        {
            //Arrange
            string ConfigString = SqlHelper.GetTestConfigString();
            SQLiteDBAccess sqliteDBAccess = new SQLiteDBAccess(ConfigString);

            //Act
            int ret = sqliteDBAccess.UpdateContact(TestData.Contact_Valid);

            //Assert
            Assert.AreEqual(ret, 1);
            Assert.AreEqual(string.Empty, sqliteDBAccess.Error);
        }

        [TestMethod]
        public void UpdateContact_NotValidModel_Test()
        {
            //Arrange
            string ConfigString = SqlHelper.GetTestConfigString();
            SQLiteDBAccess sqliteDBAccess = new SQLiteDBAccess(ConfigString);

            //Act
            int ret = sqliteDBAccess.UpdateContact(TestData.Contact_Null);

            //Assert
            Assert.AreEqual(ret, 0);
            Assert.AreEqual(sqliteDBAccess.Error, "Constraint failed");
        }

        [TestMethod]
        public void UpdateContact_NullModel_Test()
        {
            //Arrange
            string ConfigString = SqlHelper.GetTestConfigString();
            SQLiteDBAccess sqliteDBAccess = new SQLiteDBAccess(ConfigString);

            //Act
            int ret = sqliteDBAccess.UpdateContact(null);

            //Assert
            Assert.AreEqual(ret, 0);
            Assert.AreNotEqual(sqliteDBAccess.Error, string.Empty);
        }

        [TestMethod]
        public void SaveContact_ValidModel_Test()
        {
            //Arrange
            string ConfigString = SqlHelper.GetTestConfigString();
            SQLiteDBAccess sqliteDBAccess = new SQLiteDBAccess(ConfigString);

            //Act
            int ret = sqliteDBAccess.SaveContact(TestData.Contact_Valid);

            //Assert
            Assert.AreEqual(ret, 1);
            Assert.AreEqual(string.Empty, sqliteDBAccess.Error);
        }

        [TestMethod]
        public void SaveContact_NotValidModel_Test()
        {
            //Arrange
            string ConfigString = SqlHelper.GetTestConfigString();
            SQLiteDBAccess sqliteDBAccess = new SQLiteDBAccess(ConfigString);

            //Act
            int ret = sqliteDBAccess.SaveContact(TestData.Contact_Null);

            //Assert
            Assert.AreEqual(ret, 0);
            Assert.AreEqual(sqliteDBAccess.Error, "Constraint failed");
        }

        [TestMethod]
        public void SaveContact_NullModel_Test()
        {
            //Arrange
            string ConfigString = SqlHelper.GetTestConfigString();
            SQLiteDBAccess sqliteDBAccess = new SQLiteDBAccess(ConfigString);

            //Act
            int ret = sqliteDBAccess.SaveContact(null);

            //Assert
            Assert.AreEqual(ret, 0);
            Assert.AreNotEqual(sqliteDBAccess.Error, string.Empty);
        }

    }
}
