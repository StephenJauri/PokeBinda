using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PokeBindaWebsite.Models
{
    public class GroupModel
    {
        public PokemonCardGroupVM CardGroup { get; set; }
        public FilterOptionsModel Options { get; set; }
    }
}