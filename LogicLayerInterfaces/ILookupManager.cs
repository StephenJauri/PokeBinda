using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace LogicLayerInterfaces
{
    public interface ILookupManager
    {
        List<ComboBoxRelationItem<string>> GetTypes(string nullValue = "Any Type");
        List<ComboBoxRelationItem<int?>> GetGens(string nullValue = "Any Gen");
        List<ComboBoxRelationItem<Pokemon>> GetPokemon(string nullValue = "Any Pokemon");
        List<ComboBoxRelationItem<string>> GetTags(string nullValue = "Any Tag");
        List<ComboBoxRelationItem<string>> GetStatuses(string nullValue = "Any Status");
        List<string> GetAllTypes();
        List<string> GetAllTags();
    }
}
