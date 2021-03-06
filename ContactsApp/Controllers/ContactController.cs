using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ContactsApp.Models;
using ContactsApp.Database;
using Microsoft.Extensions.Configuration;

namespace ContactsApp.Controllers
{

    public class ContactController : Controller
    {
        private readonly SQLiteDBAccess _sqliteDBAccess;    //access to SQLite database
        private readonly IConfiguration _configuration;  //to get configuration properties
        private readonly IWebHostEnvironment _environment; //to get environment where app is running

 
        /// <summary>
        /// Constructor used to get Configuration sting
        /// </summary>
        /// <param name="Config"></param>
        /// <param name="Env"></param>
        public ContactController(IConfiguration Config, IWebHostEnvironment Env)
        {
            string ConfigurationString = string.Empty;

            //regular case when running app
            if(Config != null & Env != null)
            {
                _configuration = Config;
                _environment = Env;
                ConfigurationString = LoadConfigString();
            }
            else//testing case when we are passing nulls for Config and Env
            {
                //accessing location of appsettingsTest.json for testing 
                ConfigurationString = SqlHelper.GetTestConfigString(); 
            }

            _sqliteDBAccess = new SQLiteDBAccess(ConfigurationString);

        }

        /// <summary>
        /// Creates Configuartion String for DB access
        /// </summary>
        /// <returns>configString</returns>
        public string LoadConfigString()
        {
            //get Configuration string from appsetings.json
            string configString = _configuration.GetConnectionString("SqliteConnectionString");
            //get path where the app is running
            string contentRootPath = _environment.ContentRootPath;
            configString = configString.Replace("~", contentRootPath);

            return configString;
        }

        // GET: Contact/Index
        /// <summary>
        /// Gets list of all Contacts
        /// </summary>
        /// <returns>Index View</returns>
        public ActionResult Index()
        {
            //load all Contacts from database
            List<ContactModel>  contacts = _sqliteDBAccess.LoadContacts();
            TempData["Error"] = _sqliteDBAccess.Error;

            if (!TempData["Error"].Equals(string.Empty))
            {
                ModelState.AddModelError("", "Error occured: " + TempData["Error"]);
            }

            return View("Index", contacts);
        }


        // GET: Contact/New
        /// <summary>
        /// Shows New View
        /// </summary>
        /// <returns>New View</returns>
        [Route("Contact/New")]
        public ActionResult New()
        {
            //set date to today's date for the new contact
            ContactModel contact = new ContactModel();
            contact.LastDateContacted = DateTime.Now;

            return View("New", contact);
        }

        // POST: Contact/New
        /// <summary>
        /// Saves Contact to database
        /// </summary>
        /// <param name="contact"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Contact/New")]
        public ActionResult New(ContactModel contact)
        {

            if (ModelState.IsValid)
            {
                //save contact to database
                int ret = _sqliteDBAccess.SaveContact(contact);

                TempData["Error"] = _sqliteDBAccess.Error;

                if (ret > 0 && TempData["Error"].Equals(string.Empty))
                {
                    TempData["Success"] = "Contact saved.";
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Error saving contact: " + TempData["Error"]);
                    return View("New", contact);

                }
            }
            else
            {
                ModelState.AddModelError("", "Please fill mandatory fileds.");
                return View("New", contact);
            }

        }

        // GET: Contact/Edit/5
        /// <summary>
        /// Gets Contact for the given id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("Contact/{id}")]
        [Route("Contact/Edit")]
        public ActionResult Edit(int id)
        {
            //load contact for given id 
            ContactModel contact = _sqliteDBAccess.GetContact(id);
            TempData["Error"] = _sqliteDBAccess.Error;

            if (contact != null && TempData["Error"].Equals(string.Empty))
                return View("Edit", contact);
            else
            {
                ModelState.AddModelError("", $"Could not find contact id: {id} ; Error: " + TempData["Error"]);
                return View("Index");
            }
        }

        // POST: Contact/Edit
        /// <summary>
        /// Updates Contact to database
        /// </summary>
        /// <param name="contact"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Contact/Edit")]
        public ActionResult Edit(ContactModel contact)
        {
  
            if (ModelState.IsValid)
            {
                //save contact 
                int ret = _sqliteDBAccess.UpdateContact(contact);

                TempData["Error"] = _sqliteDBAccess.Error;

                if (ret > 0 && TempData["Error"].Equals(string.Empty))
                {
                    TempData["Success"] = "Contact updated.";
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Error saving contact: " + TempData["Error"]);
                    return View("Edit", contact);

                }
            }
            else
            {
                ModelState.AddModelError("", "Please fill mandatory fields.");
                return View("Edit", contact);
            }
        }


    }
}
