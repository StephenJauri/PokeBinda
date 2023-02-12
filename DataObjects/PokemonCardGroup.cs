using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    public class PokemonCardGroup
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public bool Favorite { get; set; }
        public bool Active { get; set; }
    }
}
