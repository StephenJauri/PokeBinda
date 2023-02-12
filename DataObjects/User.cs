using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    public class User : Account
    {
        public List<UserPokemonCard> PokemonCards { get; set; }
        public List<PokemonCardGroupVM> Groups { get; set; }
        public PokemonCardGroupVM FavoriteGroup { get; set; }
    }
}
