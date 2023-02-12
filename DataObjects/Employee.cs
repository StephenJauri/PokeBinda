using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    public class Employee : Account
    {
        public List<string> Roles { get; set; }
        public string RolesString => string.Join(", ", Roles);
    }
}
