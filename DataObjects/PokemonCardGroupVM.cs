﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    public class PokemonCardGroupVM : PokemonCardGroup
    {
        public List<UserPokemonCard> Cards { get; set; }
    }
}