using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PokeBindaWebsite.Models
{
    public class AddRemoveGroupsModel
    {
        public IEnumerable<DataObjects.PokemonCardGroup> Groups { get; set; }
        public int Card { get; set; }
    }
}