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
using System.Windows.Shapes;
using DataObjects;
using LogicLayerInterfaces;

namespace PresentationLayer
{
    /// <summary>
    /// Interaction logic for ViewAllAbilitiesWPF.xaml
    /// </summary>
    public partial class ViewAllAbilitiesWPF : Window
    {
        private ViewAllAbilitiesUC _abilitySelector = null;
        public Ability SelectedAbility => _abilitySelector.SelectedAbility;
        public ViewAllAbilitiesWPF(IAbilityManager abilityManager)
        {
            InitializeComponent();
            abilitySelectionGrid.Children.Add(_abilitySelector = new ViewAllAbilitiesUC(abilityManager, this, true));
        }
    }
}
