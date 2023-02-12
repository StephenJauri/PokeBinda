using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    public class UserPokemonCard : PokemonCard
    {
        public int UserCardID { get; set; }
        public string Status { get; set; }
        public DateTime DateAdded { get; set; }
        public List<PokemonCardGroupVM> Groups { get; set; }
        public bool UserCardActive { get; set; }

        public override string ToString()
        {
            return Name + " " + Note + " " + HP + " " + SetNumber + " " + String.Join(", ",Types) + " " + String.Join(", ", Pokemon) + " " + String.Join(", ", Abilities) + " " + String.Join(", ", Tags);
        }
    }
}
