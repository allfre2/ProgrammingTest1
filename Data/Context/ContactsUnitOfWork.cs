using Data.Interfaces;
using Data.Model;
using System.Threading.Tasks;

namespace Data.Context
{
    public class ContactsUnitOfWork : UnitOfWork<ContactsContext>, IContactsUnitOfWork
    {
        public ContactsUnitOfWork() : base()
        {
            _contacts = new Repository<Contact, ContactsContext>(Context);
            InsertMockData().GetAwaiter().GetResult();
        }

        readonly Repository<Contact, ContactsContext> _contacts;
        public IRepository<Contact, ContactsContext> Contacts { get => _contacts; }

        /// <summary>
        /// Fill DbContext With Mock Data for the Programming Test
        /// </summary>
        private async Task InsertMockData()
        {
            await Contacts.Add(SeedData.GetPermissions());
            await Complete();
        }
    }
}
