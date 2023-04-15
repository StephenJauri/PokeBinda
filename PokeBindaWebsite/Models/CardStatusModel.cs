using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PokeBindaWebsite.Models
{
    public class CardStatusModel
    {
        public string SelectedStatus { get; set; }
        public IEnumerable<string> Statuses { get; set; }
        public int? Card { get; set; }
    }
}