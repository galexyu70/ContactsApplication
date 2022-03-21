using ContactsApp.Models;
using Dapper;

namespace ContactsApp.Database
{
    public class SqlHelper
    {
       //check if table Contacts exists
        public static readonly string sqlTblCount = @"SELECT COUNT(*) FROM sqlite_master WHERE type='table' AND name='Contacts'";

        //create table Contacts
        public static readonly string sqlCreateTbl = @"CREATE TABLE 'Contacts' (
                                    'id'    INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE,
	                                'Name'  TEXT NOT NULL,
	                                'JobTitle'  TEXT NOT NULL,
	                                'Company'   TEXT NOT NULL,
	                                'Address'   TEXT NOT NULL,
	                                'PhoneNumber'   TEXT NOT NULL,
	                                'Email' TEXT NOT NULL,
	                                'LastDateContacted' TEXT NOT NULL,
	                                'Comments'  TEXT ); ";

        //insert record into Contacts
        public static readonly string sqlInsertWithId = @"INSERT INTO Contacts (id, Name, JobTitle, Company, Address, PhoneNumber, Email, LastDateContacted, Comments)
                                                        VALUES(@id, @Name, @JobTitle, @Company, @Address, @PhoneNumber, @Email, @LastDateContacted, @Comments)";

        //Select all records from Contacts
        public static readonly string sqlSelectAll = @"SELECT id, Name, JobTitle, Company, Address, PhoneNumber, Email, LastDateContacted, Comments FROM Contacts";

        public static readonly string sqlSelectAllForId = @"SELECT id, Name, JobTitle, Company, Address, PhoneNumber, Email, LastDateContacted, Comments FROM Contacts WHERE id = @id";

        // //statement to create record 
        public static readonly string sqlInsert = @"INSERT INTO Contacts (Name, JobTitle, Company, Address, PhoneNumber, Email, LastDateContacted, Comments)
                                                  VALUES(@Name, @JobTitle, @Company, @Address, @PhoneNumber, @Email, @LastDateContacted, @Comments)";

        // //statement to update record if exists, otherwise creates one
        public static readonly string sqlUpdate = @"REPLACE INTO Contacts (id, Name, JobTitle, Company, Address, PhoneNumber, Email, LastDateContacted, Comments)
                                                  VALUES(@id, @Name, @JobTitle, @Company, @Address, @PhoneNumber, @Email, @LastDateContacted, @Comments)";

        //sample contact used to create first record in Contacts table
        public static readonly ContactModel SampleContact = new ContactModel()
        {
            Id = 0,
            Name = "Jon Doe",
            JobTitle = "Manager",
            Company = "MNP",
            Address = "123 Main St",
            PhoneNumber = "604-456-7890",
            Email = "Jon.Doe@mnp.com",
            LastDateContacted = DateTime.Now,
            Comments = "NA"
        };

        /// <summary>
        /// Creates Dynamic Parameters to be executed with sql query
        /// </summary>
        /// <param name="contact"></param>
        /// <returns>DynamicParameters</returns>
        public static DynamicParameters CreateDynamicParametersInsert(ContactModel contact)
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();

            if(contact != null)
                dict = new Dictionary<string, object>
                {
                    { "Name", contact.Name },
                    { "JobTitle", contact.JobTitle },
                    { "Company", contact.Company },
                    { "Address", contact.Address },
                    { "PhoneNumber", contact.PhoneNumber },
                    { "Email", contact.Email },
                    { "LastDateContacted", contact.LastDateContacted },
                    { "Comments", contact.Comments }
                };

            DynamicParameters parameters = new DynamicParameters(dict);
          
            return parameters;
        }

        /// <summary>
        /// Creates Dynamic Parameters to be executed with sql query
        /// </summary>
        /// <param name="contact"></param>
        /// <returns>DynamicParameters</returns>
        public static DynamicParameters CreateDynamicParametersUpdate(ContactModel contact)
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();

            if (contact != null)
                dict = new Dictionary<string, object>
                {
                    { "id", contact.Id },
                    { "Name", contact.Name },
                    { "JobTitle", contact.JobTitle  },
                    { "Company", contact.Company },
                    { "Address", contact.Address },
                    { "PhoneNumber", contact.PhoneNumber },
                    { "Email", contact.Email },
                    { "LastDateContacted", contact.LastDateContacted },
                    { "Comments", contact.Comments }
                };

            DynamicParameters parameters = new DynamicParameters(dict);

            return parameters;
        }

        public static string GetTestConfigString()
        {
            string configString = string.Empty;

            try
            {
                string dir = Directory.GetCurrentDirectory();
                dir = dir.Substring(0, dir.IndexOf("\\ContactsApp.Test\\") + "ContactsApp.Test\\".Length);
                IConfiguration configuration = new ConfigurationBuilder()
                    .SetBasePath(dir)
                    .AddJsonFile("appsettingsTest.json")
                    .Build();
                configString = configuration.GetConnectionString("SqliteConnectionString");
                configString = configString.Replace("~", dir);
            }
            catch (Exception ex)
            {
                configString = string.Empty;
            }

            return configString;
        }
    }
}
