using Data.Context;
using Data.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Interfaces
{
    public interface IContactsUnitOfWork : IUnitOfWork
    {
        IRepository<Contact, ContactsContext> Contacts { get; }
    }
}
