using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessInterfaces;

namespace DataAccesslayer
{
    public class ImageAccessor : IImageAccessor
    {
        public bool CopyImageToDirectory(string fullPath, string fileName)
        {
            bool result = false;
            string newImagePath = Path.Combine(Environment.CurrentDirectory, "images/cards/", fileName);
            try
            {
                if (File.Exists(newImagePath))
                {
                    result = false;
                }
                else
                {
                    File.Copy(fullPath, newImagePath);
                    result = true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }
    }
}
