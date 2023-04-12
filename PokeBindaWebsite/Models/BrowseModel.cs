using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PokeBindaWebsite.Models
{
    public class BrowseModel
    {
        public FilterOptionsModel Options { get; set; }
        public List<PokemonCard> Cards = new List<PokemonCard>();
    }
}