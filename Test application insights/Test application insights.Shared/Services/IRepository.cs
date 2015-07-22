using System.Collections.Generic;
using System.Threading.Tasks;
using Test_application_insights.Model;

namespace Test_application_insights.Services
{
    public interface IRepository
    {
        Task<IList<Contact>> GetAllContacts();
        Task<Contact> GetContactById(int id);
        Task<Contact> GetNextContact();
    }
}
