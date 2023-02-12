using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using DataObjects;

namespace PresentationLayer
{
    public class ImageCollection
    {
        private Dictionary<string, BitmapImage> _images = null;
        private Dictionary<string, BitmapImage> _icons = null;
        private string _brokenImageName = "brokenImage.gif";
        public ImageCollection()
        {
            _images = new Dictionary<string, BitmapImage>();
            _icons = new Dictionary<string, BitmapImage>();
        }
        public BitmapImage GetCardImage(string imageName)
        {
            BitmapImage cardImage = null;
            if (!_images.TryGetValue(imageName, out cardImage))
            {
                Uri imageUri = new Uri(StaticInformation.CardImagePath + imageName);
                try
                {
                    _images.Add(imageName, cardImage = new BitmapImage(imageUri));
                }
                catch
                {
                    return GetIconImage(_brokenImageName);
                }
            }
            return cardImage;
        }
        public BitmapImage GetIconImage(string imageName)
        {
            BitmapImage cardImage = null;
            if (!_icons.TryGetValue(imageName, out cardImage))
            {
                Uri imageUri = new Uri(StaticInformation.IconImagePath + imageName);
                _icons.Add(imageName, cardImage = new BitmapImage(imageUri));
            }
            return cardImage;
        }
    }
}
