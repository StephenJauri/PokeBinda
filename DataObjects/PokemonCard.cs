using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    public class PokemonCard
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Note { get; set; }
        public int? HP { get; set; }
        public DateTime ReleaseYear { get; set; }
        public string SetNumber { get; set; }
        public string ImageName { get; set; }
        public bool Released { get; set; }
        public bool Active { get; set; }
        public List<string> Types { get; set; }
        public List<string> Tags { get; set; }
        public List<Pokemon> Pokemon { get; set; }
        public List<Ability> Abilities { get; set; }
    }
}
