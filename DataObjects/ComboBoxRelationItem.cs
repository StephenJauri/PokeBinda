using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    public class ComboBoxRelationItem<ObjectType>
    {
        public string DisplayText { get; set; }
        public ObjectType RelatedItem { get; set; }

        public override string ToString()
        {
            return DisplayText;
        }
    }
}
