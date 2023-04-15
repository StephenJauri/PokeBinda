using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PokeBindaWebsite.Models
{
    public class TabModel
    {
        public string Name { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
        public string Image { get; set; }
        public Dictionary<string, object> Hiddens { get; set; } = new Dictionary<string, object>();
    }
}