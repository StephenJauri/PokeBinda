using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicLayerInterfaces;
using DataAccessInterfaces;
using DataAccesslayer;
using DataObjects;

namespace LogicLayer
{
    public class LookupManager : ILookupManager
    {
        private ILookupAccessor _lookupAccessor = null;
        private List<Pokemon> allPokemon;
        private List<int> allGens;
        private List<string> allTypes;
        private List<string> allTags;
        private List<string> allStatuses;
        public LookupManager()
        {
            _lookupAccessor = new LookupAccessor();
            LoadLookups();
        }
        public void LoadLookups()
        {
            try
            {
                allPokemon = _lookupAccessor.SelectAllActiveReleasedPokemon();
                allGens = _lookupAccessor.SelectAllPokemonGens();
                allTypes = _lookupAccessor.SelectAllCardTypes();
                allTags = _lookupAccessor.SelectAllTags();
                allStatuses = _lookupAccessor.SelectAllStatuses();
            } catch (Exception ex)
            {
                throw new ApplicationException("Failed to load filters", ex);
            }
        }
        public List<ComboBoxRelationItem<int?>> GetGens(string nullValue = "Any Gen")
        {
            List<ComboBoxRelationItem<int?>> gens = new List<ComboBoxRelationItem<int?>>();
            gens.Add(new ComboBoxRelationItem<int?>()
            {
                DisplayText = nullValue,
                RelatedItem = null
            });
            allGens.ForEach((gen) => gens.Add(new ComboBoxRelationItem<int?>()
            {
                DisplayText = gen.ToString(),
                RelatedItem = gen
            }));
            return gens;
        }
        public List<ComboBoxRelationItem<Pokemon>> GetPokemon(string nullValue = "Any Pokemon")
        {
            List<ComboBoxRelationItem<Pokemon>> pokemonCBItems = new List<ComboBoxRelationItem<Pokemon>>();
            pokemonCBItems.Add(new ComboBoxRelationItem<Pokemon>()
            {
                DisplayText = nullValue,
                RelatedItem = null
            });
            allPokemon.ForEach((pokemon) => pokemonCBItems.Add(
                new ComboBoxRelationItem<Pokemon>()
                {
                    DisplayText = pokemon.Name,
                    RelatedItem = pokemon
                })
            );
            return pokemonCBItems;
        }
        public List<ComboBoxRelationItem<string>> GetTypes(string nullValue = "Any Type")
        {
            List<ComboBoxRelationItem<string>> types = new List<ComboBoxRelationItem<string>>();
            types.Add(new ComboBoxRelationItem<string>()
            {
                DisplayText = nullValue,
                RelatedItem = null
            });
            allTypes.ForEach((type) => types.Add(new ComboBoxRelationItem<string>()
            {
                DisplayText = type,
                RelatedItem = type
            }));

            return types;
        }
        public List<ComboBoxRelationItem<string>> GetStatuses(string nullValue = "Any Status")
        {
            List<ComboBoxRelationItem<string>> statuses = new List<ComboBoxRelationItem<string>>();
            if (nullValue != null)
            {
                statuses.Add(new ComboBoxRelationItem<string>()
                {
                    DisplayText = nullValue,
                    RelatedItem = null
                });
            }
            allStatuses.ForEach((status) => statuses.Add(new ComboBoxRelationItem<string>()
            {
                DisplayText = status,
                RelatedItem = status
            }));

            return statuses;
        }
        public List<ComboBoxRelationItem<string>> GetTags(string nullValue = "Any Tag")
        {
            List<ComboBoxRelationItem<string>> tags = new List<ComboBoxRelationItem<string>>();
            tags.Add(new ComboBoxRelationItem<string>()
            {
                DisplayText = nullValue,
                RelatedItem = null
            });
            allTags.ForEach((tag) => tags.Add(new ComboBoxRelationItem<string>()
            {
                DisplayText = tag,
                RelatedItem = tag
            }));

            return tags;
        }
        public List<string> GetAllTypes()
        {
            return this.allTypes;
        }
        public List<string> GetAllTags()
        {
            return this.allTags;
        }
    }
}
