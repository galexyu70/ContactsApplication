using ContactsApp.Models;
using Dapper;
using System.Data;
using System.Data.SQLite;

namespace ContactsApp.Database
{
    public class SQLiteDBAccess
    {

        public string Error { get; private set; } = string.Empty;
        private readonly string _connectionString;
        
        /// <summary>
        /// Constructor getting Connection string from Contacts Controller
        /// Creates database and Contacts table if it does not exists
        /// Adds first record to the Contacts table
        /// </summary>
        /// <param name="_connectionString"></param>
        public SQLiteDBAccess(string ConnectionString)
        {
            //get connection string from Controller
            _connectionString = ConnectionString;
            try
            {
                //creates database if it does not exists
                using (IDbConnection cnn = new SQLiteConnection(_connectionString))
                {
                    //check if table Contacts exists
                    var rows = cnn.ExecuteScalar(SqlHelper.sqlTblCount);

                    //create table if does not exists and add one redord
                    if (Convert.ToInt32(rows) == 0)
                    {
                        //create dynamic parameters to insert first sample record
                        DynamicParameters parameters = SqlHelper.CreateDynamicParametersUpdate(SqlHelper.SampleContact);

                        var ret = cnn.Execute(SqlHelper.sqlCreateTbl);
                        ret = cnn.Execute(SqlHelper.sqlInsertWithId, parameters);
                    }
 
                }
            }
            catch (Exception ex)
            {
                Error = ex.Message.ToString();
            }

        }


        /// <summary>
        /// Returns list of all contacts
        /// </summary>
        /// <returns></returns>
        public List<ContactModel> LoadContacts()
        {
            List<ContactModel> contacts = new List<ContactModel>();
            try
            {
                using (IDbConnection cnn = new SQLiteConnection(_connectionString))
                {
                    var output = cnn.Query<ContactModel>(SqlHelper.sqlSelectAll);
                    if (output != null)
                        contacts = output.ToList();
                }
            }
            catch (Exception ex)
            {
                Error = ex.Message;
            }
            return contacts;
        }

        /// <summary>
        /// Update given contact
        /// </summary>
        /// <param name="contact"></param>
        /// <returns></returns>
        public int UpdateContact(ContactModel contact)
        {
            int rows = 0;

            //create dynamic parameters using contact to update record
            DynamicParameters parameters = SqlHelper.CreateDynamicParametersUpdate(contact);

            try
            {
                using (IDbConnection cnn = new SQLiteConnection(_connectionString))
                {
                    rows = cnn.Execute(SqlHelper.sqlUpdate, parameters);
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.ToLower().Contains("not null constraint failed"))
                    Error = "Constraint failed";
                else
                    Error = ex.Message;
            }
            return rows;
        }

 

        /// <summary>
        /// Inserts new contact
        /// </summary>
        /// <param name="contact"></param>
        /// <returns></returns>
        public int SaveContact(ContactModel contact)
        {
            int rows = 0;

            //create dynamic parameters using contact to insert new record
            DynamicParameters parameters = SqlHelper.CreateDynamicParametersInsert(contact);

            try
            {
                using (IDbConnection cnn = new SQLiteConnection(_connectionString))
                {
                    rows = cnn.Execute(SqlHelper.sqlInsert, parameters);
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.ToLower().Contains("not null constraint failed"))
                    Error = "Constraint failed";
                else
                    Error = ex.Message;
            }
            return rows;
        }

        /// <summary>
        /// Returns contact for given id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ContactModel GetContact(int id)
        {
            ContactModel contact = new ContactModel();

            var dictionary = new Dictionary<string, object>
            {
                { "id", id }
            };
            var parameters = new DynamicParameters(dictionary);

            try
            {
                using (IDbConnection cnn = new SQLiteConnection(_connectionString))
                {
                    var output = cnn.Query<ContactModel>(SqlHelper.sqlSelectAllForId, parameters);
                    if (output != null && output.Count() > 0)
                        contact = output.FirstOrDefault();
                    else
                        Error = "Contact not found";

                }
            }
            catch (Exception ex)
            {
                Error = ex.Message;
            }
            return contact;

        }

    }
}
