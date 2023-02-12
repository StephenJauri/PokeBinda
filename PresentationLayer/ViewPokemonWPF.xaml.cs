using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
using LogicLayer;
using LogicLayerInterfaces;

namespace PresentationLayer
{
    /// <summary>
    /// Interaction logic for ViewPokemonWPF.xaml
    /// </summary>

    public partial class ViewPokemonWPF : Window
    {
        private static readonly Regex _disallowedCharacters = new Regex("[^0-9]+");
        private Pokemon _pokemon = null;
        private bool _newPokemon;
        private IPokemonManager _pokemonManager = null;
        public ViewPokemonWPF(Pokemon pokemon, IPokemonManager pokemonManager)
        {
            _newPokemon = false;
            _pokemon = pokemon;
            _pokemonManager = pokemonManager;
            InitializeComponent();
            btnSave.IsDefault = true;
            btnCancel.IsCancel = true;
            tbGen.Text = pokemon.Gen.ToString();
            tbPokedex.Text = pokemon.PokedexNumber.ToString();
            tbName.Text = pokemon.Name;
            chkActive.IsChecked = pokemon.Active;
            chkReleased.IsChecked = pokemon.Released;
            tbPokedex.Focus();
        }
        public ViewPokemonWPF(IPokemonManager pokemonManager)
        {
            _pokemon = new Pokemon()
            {
                Released = false,
                Active = true
            };
            _newPokemon = true;
            _pokemonManager = pokemonManager;
            InitializeComponent();
            btnSave.IsDefault = true;
            btnCancel.IsCancel = true;
            this.Title = "Create Pokemon";
            this.lblTitle.Text = "Create Pokemon";
            chkActive.IsChecked = _pokemon.Active;
            chkActive.IsEnabled = false;
            chkReleased.IsChecked = _pokemon.Released;
        }


        private void NumberBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text);
        }
        private static bool IsTextAllowed(string text)
        {
            return !_disallowedCharacters.IsMatch(text);
        }

        private void CatchTextPasted(object sender, DataObjectPastingEventArgs e)
        {
            if (e.DataObject.GetDataPresent(typeof(string)))
            {
                string text = (string)e.DataObject.GetData(typeof(string));
                if (!IsTextAllowed(text))
                {
                    e.CancelCommand();
                }
            }
            else
            {
                e.CancelCommand();
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if(!ValidateInputs())
            {
                return;
            }
            if (tbGen.Text == "")
            {
                _pokemon.Gen = null;
            }
            else
            {
                _pokemon.Gen = byte.Parse(tbGen.Text);
            }
            if (tbPokedex.Text == "")
            {
                _pokemon.PokedexNumber = null;
            }
            else
            {
                _pokemon.PokedexNumber = short.Parse(tbPokedex.Text);
            }
            _pokemon.Name = tbName.Text;
            _pokemon.Released = chkReleased.IsChecked == true;
            _pokemon.Active = chkActive.IsChecked == true;

            try
            {
                this.DialogResult = true;
                if (_newPokemon)
                {
                    _pokemonManager.CreatePokemon(_pokemon);
                }
                else
                {
                    _pokemonManager.UpdatePokemon(_pokemon);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n\n" + ex.InnerException.Message);
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
        private bool ValidateInputs()
        {
            if (tbGen.Text != "")
            {
                byte tinyint;
                if (!byte.TryParse(tbGen.Text, out tinyint))
                {
                    MessageBox.Show("Gen must be empty or less than 255");
                    return false;
                }
            }
            if (tbPokedex.Text != "")
            {
                short smallint;
                if (!short.TryParse(tbPokedex.Text,out smallint))
                {
                    MessageBox.Show("Pokedex # must be empty or less than 32,767");
                    return false;
                }
                else if (smallint < 0)
                {
                    MessageBox.Show("Pokedex # must be empty or positive");
                    return false;
                }
            }
            if (tbName.Text.Length < 3)
            {
                MessageBox.Show("Name must be at least 3 characters long");
                return false;
            }
            if (tbName.Text.Length > 16)
            {
                MessageBox.Show("Name can't be longer than 16 characters");
                return false;
            }
            return true;
        }
    }

}
