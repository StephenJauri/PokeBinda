using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Objects
{
    public class User
    {
        public int ID {get; private set;}
        public string Email { get; set; }
        public string GivenName { get; set; }
        public string FamilyName { get; set; }
        public DateTime Birthday { get; set; }
        public bool Active { get; set; }
    }
}
