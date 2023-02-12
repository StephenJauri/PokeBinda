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
using DataObjects;
using LogicLayer;
using LogicLayerInterfaces;

namespace PresentationLayer
{
    /// <summary>
    /// Interaction logic for ViewAllAbilitiesUC.xaml
    /// </summary>
    public partial class ViewAllAbilitiesUC : UserControl, IRefreshUserControl
    {
        private IAbilityManager _abilityManager = null;
        private Window _owner;
        private bool _selectionMode;
        public Ability SelectedAbility {get; set;}
        public ViewAllAbilitiesUC(IAbilityManager abilityManager, Window owner, bool selectionMode = false)
        {
            InitializeComponent();
            this._owner = owner;
            btnEditAbility.Visibility = Visibility.Hidden;
            _abilityManager = abilityManager;
            _selectionMode = selectionMode;
            RefreshDataGrid();
        }
        
        private void RefreshDataGrid()
        {
            try
            {
                AbilityGrid.ItemsSource = _abilityManager.GetAllAbilities();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n\n" + ex.InnerException.Message);
            }
        }

        private void AbilityGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if ((SelectedAbility = (Ability)AbilityGrid.SelectedItem) != null)
            {
                if (!_selectionMode)
                {
                    btnEditAbility_Click(sender, e);
                }
                else
                {
                    btnSelectAbility_Click(sender, e);
                }
            }
        }

        private void AbilityGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (AbilityGrid.SelectedItem != null)
            {
                if (_selectionMode)
                {
                    btnSelectAbility.Visibility = Visibility.Visible;
                }
                btnEditAbility.Visibility = Visibility.Visible;
            }
            else
            {
                if (_selectionMode)
                {
                    btnSelectAbility.Visibility = Visibility.Hidden;
                }
                btnEditAbility.Visibility = Visibility.Hidden;
            }
        }

        private void btnEditAbility_Click(object sender, RoutedEventArgs e)
        {
            ViewAbilityWPF viewAbility = new ViewAbilityWPF((Ability)AbilityGrid.SelectedItem, _abilityManager);
            viewAbility.Owner = this._owner;
            viewAbility.ShowDialog();
            if (viewAbility.DialogResult == true)
            {
                RefreshDataGrid();
            }
        }

        private void btnAddAbility_Click(object sender, RoutedEventArgs e)
        {
            ViewAbilityWPF viewAbility = new ViewAbilityWPF(_abilityManager);
            viewAbility.Owner = this._owner;
            viewAbility.ShowDialog();
            if (viewAbility.DialogResult == true)
            {
                RefreshDataGrid();
            }
        }

        private void btnSelectAbility_Click(object sender, RoutedEventArgs e)
        {
            if ((SelectedAbility = (Ability)AbilityGrid.SelectedItem) != null)
            {
                _owner.DialogResult = true;
                _owner.Close();
            }
        }

        public void Refresh()
        {
            RefreshDataGrid();
        }
    }
}
