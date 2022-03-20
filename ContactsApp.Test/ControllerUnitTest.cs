using ContactsApp.Controllers;
using ContactsApp.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.IO;

namespace ContactsApp.Test
{

    [TestClass]
    public class ControllerUnitTest
    {

        string mocTmpData = string.Empty;

        [TestMethod]
        public void IndexView_Test()
        {
            //Arrange
            ContactController mocController = TestHelper.GetContactController(mocTmpData);

            //Act
            ViewResult? result = mocController.Index() as ViewResult;

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Index", result.ViewName);
        }

        [TestMethod]
        public void NewView_Test()
        {
            //Arrange
            ContactController mocController = TestHelper.GetContactController(mocTmpData);

            //Act
            ViewResult? result = mocController.New() as ViewResult;

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("New", result.ViewName);
        }

        [TestMethod]
        public void NewView_ValidData_Test()
        {
            //Arrange
            ContactController mocController = TestHelper.GetContactController(mocTmpData);

            //Act
            var result = (RedirectToActionResult)mocController.New(TestData.Contact_Valid);

            //Assert
            Assert.AreEqual("Index", result.ActionName);

        }

        [TestMethod]
        public void NewView_EmptyData_Test()
        {
            //Arrange
            ContactController mocController = TestHelper.GetContactController(mocTmpData);

            //Act
            ViewResult? result = mocController.New(TestData.Contact_Null) as ViewResult;

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("New", result.ViewName);

        }

        [TestMethod]
        public void NewView_ModelError_Test()
        {
            //Arrange
            ContactController mocController = TestHelper.GetContactController(mocTmpData);

            //Act
            mocController.ModelState.AddModelError("Test Error", "Test Error");
            ViewResult? result = mocController.New(new ContactModel()) as ViewResult;

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("New", result.ViewName);

        }

        [TestMethod]
        public void EditViewValidId_Test()
        {
            //Arrange
            ContactController mocController = TestHelper.GetContactController(mocTmpData);

            //Act
            ViewResult? result = mocController.Edit(0) as ViewResult;

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Edit", result.ViewName);
        }

        [TestMethod]
        public void EditViewNotValidId_Test()
        {
            //Arrange
            mocTmpData = "Contact not found";
            ContactController mocController = TestHelper.GetContactController(mocTmpData);

            //Act
            ViewResult? result = mocController.Edit(9999) as ViewResult;

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Index", result.ViewName);
            Assert.AreEqual(mocTmpData, result.TempData["Error"]);
        }

        [TestMethod]
        public void EditView_ValidData_Test()
        {
            //Arrange
            ContactController mocController = TestHelper.GetContactController(mocTmpData);

            //Act
            var result = (RedirectToActionResult)mocController.Edit(TestData.Contact_Valid);

            //Assert
            Assert.AreEqual("Index", result.ActionName);

        }

        [TestMethod]
        public void EditView_EmptyData_Test()
        {
            //Arrange
            ContactController mocController = TestHelper.GetContactController(mocTmpData);
            //Act
            ViewResult? result = mocController.Edit(TestData.Contact_Null) as ViewResult;

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Edit", result.ViewName);

        }


    }
}