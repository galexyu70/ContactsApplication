using ContactsApp.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ContactsApp.Models;
using ContactsApp.Database;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace ContactsApp.Test
{
    public class TestHelper
    {
        public static ContactController GetContactController(string mocTmpData)
        {
            var httpContext = new DefaultHttpContext();
            var mockTempData = new TempDataDictionary(httpContext, Mock.Of<ITempDataProvider>());
            mockTempData["Error"] = mocTmpData;
            ContactController _mocController = new ContactController(null, null)
            {
                TempData = mockTempData
            };

            return _mocController;
        }


        public static string GetConfigurationString()
        {
            string dir = Directory.GetCurrentDirectory();
            dir = dir.Substring(0, dir.IndexOf("\\ContactsApp.Test\\") + "ContactsApp.Test\\".Length);
            IConfiguration configuration = new ConfigurationBuilder()
                .SetBasePath(dir)
                .AddJsonFile("appsettingsTest.json")
                .Build();
            string configString = configuration.GetConnectionString("SqliteConnectionString");
            configString = configString.Replace("~", dir);

            return configString;

        }
    }
}
