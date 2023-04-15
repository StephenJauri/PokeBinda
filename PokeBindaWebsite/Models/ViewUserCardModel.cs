using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PokeBindaWebsite.Models
{
    public class ViewUserCardModel
    {
        public bool IsFavorite { get; set; }
        public List<string> Statuses { get; set; }
        public UserPokemonCard Card { get; set; }
        public IEnumerable<PokemonCardGroup> AddGroups { get; set; }
        public IEnumerable<PokemonCardGroup> RemoveGroups { get; set; }
    }
}