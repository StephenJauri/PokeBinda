using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessInterfaces
{
    public interface IImageAccessor
    {
        bool CopyImageToDirectory(string fullPath, string fileName);
    }
}
