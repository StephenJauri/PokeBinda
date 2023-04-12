using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PokeBindaWebsite.Models
{
    public class CollectionModel
    {
        public FilterOptionsModel Options { get; set; }
        public List<UserPokemonCard> Cards = new List<UserPokemonCard>();
    }
}