using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Model
{
    public class Contact
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }

        public override string ToString()
        {
            return $"{Name} {LastName} - {Email}";
        }
    }
}
