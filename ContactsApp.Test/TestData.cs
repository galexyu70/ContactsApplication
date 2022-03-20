using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ContactsApp.Models;

namespace ContactsApp.Test
{
    internal class TestData
    {
        public static ContactModel Contact_Null = new ContactModel()
        {
            Id = 1,
            Name = null,
            JobTitle = null,
            Company = "HP",
            Address = null,
            PhoneNumber = null,
            Email = null,
            LastDateContacted = DateTime.Now
        };

        public static ContactModel Contact_Valid = new ContactModel()
        {
            Id=1,
            Name = "Sara Jones",
            JobTitle = "Developer",
            Company = "MNP",
            Address = "456 Main St",
            PhoneNumber = "604-123-456",
            Email = "Sara.Jones@mnp.com",
            LastDateContacted = DateTime.Now
        };
    }
}
