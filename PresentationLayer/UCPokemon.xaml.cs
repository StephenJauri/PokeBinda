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
    /// Interaction logic for UCPokemon.xaml
    /// </summary>
    public partial class UCPokemon : UserControl
    {
        public bool FourthColumnVisible
        {
            get
            {
                return Math.Abs(50 - mainGrid.ColumnDefinitions[3].Width.Value) > 0.1;
            }
            set
            {
                mainGrid.ColumnDefinitions[3].Width = value ? new GridLength(50) : new GridLength(0);
            }
        }
        public UCPokemon()
        {
            InitializeComponent();
        }
    }
}
