using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    public class Pokemon
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public byte? Gen { get; set; }
        public short? PokedexNumber { get; set; }
        public bool Released { get; set; }
        public bool Active { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }

    
}
