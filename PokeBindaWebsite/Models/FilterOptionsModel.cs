using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PokeBindaWebsite.Models
{
    public class FilterOptionsModel
    {
        public string Tag { get; set; }
        public int? Pokemon { get; set; }
        public string Type { get; set; }
        public List<Pokemon> SelectablePokemon {get ;set; }
        public List<string> Tags { get; set; }
        public List<string> Types { get; set; }


    }
}