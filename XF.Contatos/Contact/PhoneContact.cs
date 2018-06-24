using System;
using System.Collections.Generic;

namespace XF.Contatos.Contact
{
    public class PhoneContact
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string PhotoUri { get; set; }

        public string Name { get => $"{FirstName} {LastName}"; }

    }

    public interface IContactList
    {
        IEnumerable<PhoneContact> GetAllContacts();
    }
}
