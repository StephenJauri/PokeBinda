using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    public class Account
    {
        public int ID { get; set; }
        public string Email { get; set; }
        public string GivenName { get; set; }
        public string FamilyName { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime Birthday { get; set; }
        public bool Active { get; set; }
    }
}
