using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace PokeBindaWebsite.Models
{
    public class RenameGroupModel
    {
        [Required]
        public int? Group { get; set; }
        [Required]
        [MinLength(3, ErrorMessage = "Must be 3 or more characters")]
        [MaxLength(16, ErrorMessage = "Must be 16 or less characters")]
        public string Name { get; set; }

        public bool Favorite { get; set; }
    }
}