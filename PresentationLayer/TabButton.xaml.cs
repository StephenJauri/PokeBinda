using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PresentationLayer
{
    /// <summary>
    /// Interaction logic for TabButton.xaml
    /// </summary>
    public partial class TabButton : UserControl
    {
        public TabButton()
        {
            InitializeComponent();
            this.DataContext = this;
        }

        public string TabLabel { get; set; }
        public BitmapImage TabImage { get; set; }
        public Brush TextColor { get; set; } = new SolidColorBrush(Color.FromRgb(255, 193, 142));
        public Button Button { 
            get
            {
                return this.Clickable;
            }
            private set
            {
                this.Button = value;
            }
        }
    }
}
