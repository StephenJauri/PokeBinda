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
    /// Interaction logic for UCAbility.xaml
    /// </summary>
    public partial class UCAbility : UserControl
    {
        public bool RemoveColumnVisible
        {
            get
            {
                return Math.Abs(50 - abilityGrid.ColumnDefinitions[1].Width.Value) > 0.1;
            }
            set
            {
                abilityGrid.ColumnDefinitions[1].Width = value ? new GridLength(50) : new GridLength(0);
            }
        }
        public UCAbility()
        {
            InitializeComponent();
        }
    }
}
