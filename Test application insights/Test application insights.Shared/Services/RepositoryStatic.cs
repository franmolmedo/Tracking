using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Test_application_insights.Model;

namespace Test_application_insights.Services
{
    public class RepositoryStatic : IRepository
    {
        private IList<Contact> _contacts;

        public RepositoryStatic()
        {
            _contacts = new List<Contact>
            {
                new Contact
                {
                    Id = 1,
                    Name = "User name 1",
                    Surname = "Surname 1",
                    Address = "Address 1",
                    PhoneNumber="606000040"
                }
            };
        }

        public Task<IList<Contact>> GetAllContacts()
        {
            return Task.FromResult(_contacts);
        }

        public Task<Contact> GetContactById(int id)
        {
            var contact = _contacts.FirstOrDefault(x => x.Id == id);
            return Task.FromResult(contact);
        }

        public Task<Contact> GetNextContact()
        {
            var lastContact = _contacts.Last();
            var newContact = new Contact
            {
                Id = lastContact.Id + 1,
                Name = "User Name " + (lastContact.Id +1),
                Surname = "Surname " + (lastContact.Id + 1),
                Address = "Address " + (lastContact.Id + 1),
                PhoneNumber = GenerateRandomNumberString()
            };
            _contacts.Insert(_contacts.Count,newContact);
            return Task.FromResult(newContact);
        }

        #region Private Methods

        private string GenerateRandomNumberString()
        {
            const string chars = "0123456789";
            var random = new Random();
            return new string(
                Enumerable.Repeat(chars, 8)
                          .Select(s => s[random.Next(s.Length)])
                          .ToArray());
        }
        #endregion
    }
}
