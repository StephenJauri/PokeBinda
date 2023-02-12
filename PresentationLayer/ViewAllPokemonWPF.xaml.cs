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
using LogicLayerInterfaces;
using DataObjects;

namespace PresentationLayer
{
    /// <summary>
    /// Interaction logic for ViewAllPokemonWPF.xaml
    /// </summary>
    public partial class ViewAllPokemonWPF : Window
    {
        private ViewAllPokemonUC _pokemonSelector = null;
        public Pokemon SelectedPokemon => _pokemonSelector.SelectedPokemon;
        public ViewAllPokemonWPF(IPokemonManager pokemonManager)
        {
            InitializeComponent();
            pokemonSelectorGrid.Children.Add(_pokemonSelector = new ViewAllPokemonUC(pokemonManager, this, true));
        }
    }
}
