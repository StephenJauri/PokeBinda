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
    /// Interaction logic for ViewAllPokemonUC.xaml
    /// </summary>
    public partial class ViewAllPokemonUC : UserControl, IRefreshUserControl
    {
        private IPokemonManager _pokemonManager = null;
        private Window _owner;
        private bool _selectionMode;
        public Pokemon SelectedPokemon { get; set; }
        public ViewAllPokemonUC(IPokemonManager pokemonManager, Window owner, bool selectionMode = false)
        {
            InitializeComponent();
            this._owner = owner;
            _selectionMode = selectionMode;
            btnEditPokemon.Visibility = Visibility.Hidden;
            _pokemonManager = pokemonManager;
            RefreshDataGrid();
        }
        private void RefreshDataGrid()
        {
            try
            {
                PokemonGrid.ItemsSource = _pokemonManager.GetAllPokemon();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex + "\n\n" + ex.InnerException);
            }
        }

        private void PokemonGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if ((SelectedPokemon = (Pokemon)PokemonGrid.SelectedItem) != null)
            {
                if (!_selectionMode)
                {
                    btnEditPokemon_Click(sender, e);
                }
                else
                {
                    btnSelect_Click(sender, e);
                }
            }
        }

        private void PokemonGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (PokemonGrid.SelectedItem != null)
            {
                btnEditPokemon.Visibility = Visibility.Visible;
                if (_selectionMode)
                {
                    btnSelect.Visibility = Visibility.Visible;
                }
            }
            else
            {
                btnEditPokemon.Visibility = Visibility.Hidden;
                if (_selectionMode)
                {
                    btnSelect.Visibility = Visibility.Hidden;
                }
            }
        }

        private void btnEditPokemon_Click(object sender, RoutedEventArgs e)
        {
            ViewPokemonWPF viewPokemon = new ViewPokemonWPF((Pokemon)PokemonGrid.SelectedItem,_pokemonManager);
            viewPokemon.Owner = this._owner;
            viewPokemon.ShowDialog();
            if (viewPokemon.DialogResult == true)
            {
                RefreshDataGrid();
            }
        }

        private void btnAddPokemon_Click(object sender, RoutedEventArgs e)
        {
            ViewPokemonWPF viewPokemon = new ViewPokemonWPF(_pokemonManager);
            viewPokemon.Owner = this._owner;
            viewPokemon.ShowDialog();
            if (viewPokemon.DialogResult == true)
            {
                RefreshDataGrid();
            }
        }

        private void btnSelect_Click(object sender, RoutedEventArgs e)
        {
            if ((SelectedPokemon = (Pokemon)PokemonGrid.SelectedItem) != null)
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
