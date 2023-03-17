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
using Microsoft.Win32;

namespace PresentationLayer
{
    /// <summary>
    /// Interaction logic for CreateAndEditCardWPF.xaml
    /// </summary>
    public partial class CreateAndEditCardWPF : Window
    {
        private PokemonCard _pokemonCard = null;
        private PokemonCard _newPokemonCard = null;
        private ICardManager _cardManager = null;
        private ILookupManager _lookupManager = null;
        private IPokemonManager _pokemonManager = null;
        private IAbilityManager _abilityManager = null;
        private ImageCollection _imageCollection = null;
        private bool _newCard = false;

        private static readonly Regex _disallowedCharacters = new Regex("[^0-9]+");
        private const string CHECKMARK_IMAGE_NAME = "checkselectedicon.png";
        private const string XMARK_IMAGE_NAME = "xselectedicon.png";

        public CreateAndEditCardWPF(PokemonCard card, ICardManager cardManager, ILookupManager lookupManager,IPokemonManager pokemonManager,IAbilityManager abilityManager,ImageCollection imageCollection, bool newCard = false)
        {
            _lookupManager = lookupManager;
            _pokemonManager = pokemonManager;
            _abilityManager = abilityManager;
            _cardManager = cardManager;
            _newCard = newCard;
            _imageCollection = imageCollection;


            InitializeComponent();
            pokeHeader.FourthColumnVisible = true;

            if (!_newCard)
            {
                _pokemonCard = card;
                SetupCardCopy();
            }
            else
            {
                _pokemonCard = card;
                _newPokemonCard = card;
                this.Title = "Create Card";
                btnChangeActive.Visibility = Visibility.Hidden;

            }
            SetDetailsFromPokemonCard();
            btnSaveCard.IsDefault = true;
            btnCancel.IsCancel = true;
        }

        private void SetupCardCopy()
        {
            _newPokemonCard = new PokemonCard()
            {
                ID = _pokemonCard.ID,
                Abilities = new List<Ability>(),
                Active = _pokemonCard.Active,
                HP = _pokemonCard.HP,
                ImageName = _pokemonCard.ImageName,
                Name = _pokemonCard.Name,
                Note = _pokemonCard.Note,
                Pokemon = new List<Pokemon>(),
                Released = _pokemonCard.Released,
                ReleaseYear = _pokemonCard.ReleaseYear,
                SetNumber = _pokemonCard.SetNumber,
                Tags = new List<string>(),
                Types = new List<string>()
            };
            _pokemonCard.Abilities.ForEach(_newPokemonCard.Abilities.Add);
            _pokemonCard.Pokemon.ForEach(_newPokemonCard.Pokemon.Add);
            _pokemonCard.Tags.ForEach(_newPokemonCard.Tags.Add);
            _pokemonCard.Types.ForEach(_newPokemonCard.Types.Add);
        }
        private void SetDetailsFromPokemonCard()
        {
            txtBoxCardName.Text = _pokemonCard.Name;
            txtBoxHP.Text = _pokemonCard.HP?.ToString();
            if (_pokemonCard.Tags.Count > 1)
            {
                txtBlockTags.Text = "Tags";
            }
            _pokemonCard.Tags.ForEach(AddTag);
            _pokemonCard.Pokemon.ForEach(AddPokemon);
            if (_pokemonCard.Types.Count > 1)
            {
                txtBlockElement.Text = "Types";
            }
            _pokemonCard.Types.ForEach(AddType);
            if (_pokemonCard.Abilities.Count > 1)
            {
                txtBlockAbility.Text = "Abilities";
            }
            _pokemonCard.Abilities.ForEach(AddAbility);
            txtBoxSetNumber.Text = _pokemonCard.SetNumber;
            txtBlockReleaseDateDetails.Text = _pokemonCard.ReleaseYear.ToString("MM/dd/yyyy");
            datePickerReleaseDate.SelectedDate = _pokemonCard.ReleaseYear;
            imgActive.Source = _imageCollection.GetIconImage(_pokemonCard.Active ? CHECKMARK_IMAGE_NAME : XMARK_IMAGE_NAME);
            tbNotes.Text = _pokemonCard.Note;
            imgReleased.Source = _imageCollection.GetIconImage(_pokemonCard.Released ? CHECKMARK_IMAGE_NAME : XMARK_IMAGE_NAME);
            imgCardImage.Source = _imageCollection.GetCardImage(_pokemonCard.ImageName);
        }
        private void UpdateDetailsFromNewPokemonCard()
        {
            if (_newPokemonCard.Tags.Count > 1)
            {
                txtBlockTags.Text = "Tags";
            }
            else
            {
                txtBlockTags.Text = "Tag";
            }
            grdTags.Children.Clear();
            grdTags.RowDefinitions.Clear();
            _newPokemonCard.Tags.ForEach(AddTag);
            grdPokemon.Children.Clear();
            grdPokemon.RowDefinitions.Clear();
            grdPokemon.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(40)});
            grdPokemon.Children.Add(pokeHeader);
            _newPokemonCard.Pokemon.ForEach(AddPokemon);
            if (_newPokemonCard.Types.Count > 1)
            {
                txtBlockElement.Text = "Types";
            }
            else
            {
                txtBlockElement.Text = "Type";
            }
            grdTypes.Children.Clear();
            grdTypes.RowDefinitions.Clear();
            _newPokemonCard.Types.ForEach(AddType);
            grdAbilities.Children.Clear();
            grdAbilities.RowDefinitions.Clear();
            if (_newPokemonCard.Abilities.Count > 1)
            {
                txtBlockAbility.Text = "Abilities";
            }
            else
            {
                txtBlockAbility.Text = "Ability";
            }
            _newPokemonCard.Abilities.ForEach(AddAbility);
            txtBlockReleaseDateDetails.Text = _newPokemonCard.ReleaseYear.ToString("MM/dd/yyyy");
            imgCardImage.Source = _imageCollection.GetCardImage(_newPokemonCard.ImageName);
        }
        private void AddPokemon(Pokemon pokemon)
        {

            grdPokemon.RowDefinitions.Add(new RowDefinition());
            UCPokemon ucPokemon = new UCPokemon();
            ucPokemon.txtBlockGen.Text = pokemon.Gen.ToString();
            ucPokemon.txtBlockPokedex.Text = pokemon.PokedexNumber.ToString();
            ucPokemon.txtBlockName.Text = pokemon.Name;
            ucPokemon.txtBlockName.FontSize = 15;
            ucPokemon.txtBlockPokedex.FontSize = 15;
            ucPokemon.txtBlockGen.FontSize = 15;
            ucPokemon.FourthColumnVisible = true;
            ucPokemon.btnRemovePokemon.Visibility = Visibility.Visible;
            ucPokemon.btnRemovePokemon.Click += (obj, args) => RemovePokemon(obj, args, pokemon);
            Grid.SetRow(ucPokemon, grdPokemon.RowDefinitions.Count - 1);
            grdPokemon.Children.Add(ucPokemon);
        }
        private void AddType(string type)
        {
            grdTypes.RowDefinitions.Add(new RowDefinition());
            TextWithRemoveUC textWithRemove = new TextWithRemoveUC();
            textWithRemove.txtBlockItem.Text = type;
            textWithRemove.btnRemoveItem.Click += (obj, args) => RemoveType(obj, args, type);
            Grid.SetRow(textWithRemove, grdTypes.RowDefinitions.Count - 1);
            grdTypes.Children.Add(textWithRemove);
        }
        private void AddTag(string tag)
        {
            grdTags.RowDefinitions.Add(new RowDefinition());
            TextWithRemoveUC textWithRemove = new TextWithRemoveUC();
            textWithRemove.txtBlockItem.Text = tag;
            textWithRemove.btnRemoveItem.Click += (obj, args) => RemoveTag(obj, args, tag);
            Grid.SetRow(textWithRemove, grdTags.RowDefinitions.Count - 1);
            grdTags.Children.Add(textWithRemove);
        }
        private void RemoveType(object sender, RoutedEventArgs e, string type)
        {
            _newPokemonCard.Types.Remove(type);
            UpdateDetailsFromNewPokemonCard();
        }
        private void RemoveTag(object sender, RoutedEventArgs e, string tag)
        {
            _newPokemonCard.Tags.Remove(tag);
            UpdateDetailsFromNewPokemonCard();
        }
        private void RemovePokemon(object sender, RoutedEventArgs e, Pokemon pokemon)
        {
            _newPokemonCard.Pokemon.Remove(_newPokemonCard.Pokemon.Find((poke) => pokemon.ID == poke.ID));
            UpdateDetailsFromNewPokemonCard();
        }
        private void AddAbility(Ability ability)
        {
            grdAbilities.RowDefinitions.Add(new RowDefinition());
            UCAbility ucAbility = new UCAbility();
            ucAbility.txtBlockAbilityName.Text = ability.Name;
            ucAbility.txtBlockAbilityDescription.Text = ability.Description;
            ucAbility.RemoveColumnVisible = true;
            ucAbility.btnRemoveAbility.Visibility = Visibility.Visible;
            ucAbility.btnRemoveAbility.Click += (obj, args) => RemoveAbility(obj, args, ability);
            Grid.SetRow(ucAbility, grdAbilities.RowDefinitions.Count - 1);
            grdAbilities.Children.Add(ucAbility);
        }
        private void RemoveAbility(object sender, RoutedEventArgs e, Ability ability)
        {
            _newPokemonCard.Abilities.Remove(_newPokemonCard.Abilities.Find((abili) => ability.ID == abili.ID));
            UpdateDetailsFromNewPokemonCard();
        }
        private void btnChangeActive_Click(object sender, RoutedEventArgs e)
        {
            _newPokemonCard.Active = !_newPokemonCard.Active;
            imgActive.Source = _imageCollection.GetIconImage(_newPokemonCard.Active ? CHECKMARK_IMAGE_NAME : XMARK_IMAGE_NAME);
        }
        private void btnChangeReleased_Click(object sender, RoutedEventArgs e)
        {
            _newPokemonCard.Released = !_newPokemonCard.Released;
            imgReleased.Source = _imageCollection.GetIconImage(_newPokemonCard.Released ? CHECKMARK_IMAGE_NAME : XMARK_IMAGE_NAME);
        }
        private void txtBoxHP_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text);
        }
        private static bool IsTextAllowed(string text)
        {
            return !_disallowedCharacters.IsMatch(text);
        }
        private void HPTextPasted(object sender, DataObjectPastingEventArgs e)
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
        private void datePickerReleaseDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            _newPokemonCard.ReleaseYear = datePickerReleaseDate.SelectedDate.Value;
            txtBlockReleaseDateDetails.Text = _newPokemonCard.ReleaseYear.ToString("MM/dd/yyyy");
        }
        private void btnAddPokemon_Click(object sender, RoutedEventArgs e)
        {
            ViewAllPokemonWPF selectPokemonWindow = new ViewAllPokemonWPF(_pokemonManager);
            selectPokemonWindow.Owner = this;
            selectPokemonWindow.ShowDialog();
            if (selectPokemonWindow.DialogResult == true)
            {
                if (_newPokemonCard.Pokemon.Exists((poke) => poke.ID == selectPokemonWindow.SelectedPokemon.ID))
                {
                    MessageBox.Show("Card already contains this pokemon!");
                    return;
                }
                else
                {
                    _newPokemonCard.Pokemon.Add(selectPokemonWindow.SelectedPokemon);
                    _newPokemonCard.Pokemon.Sort((poke1,poke2) => poke1.ID - poke2.ID);
                    UpdateDetailsFromNewPokemonCard();
                    return;
                }
            }
        }
        private void btnAddAbility_Click(object sender, RoutedEventArgs e)
        {
            ViewAllAbilitiesWPF selectedAbilityWindow = new ViewAllAbilitiesWPF(_abilityManager);
            selectedAbilityWindow.Owner = this;
            selectedAbilityWindow.ShowDialog();
            if (selectedAbilityWindow.DialogResult == true)
            {
                if (_newPokemonCard.Abilities.Exists((ability) => ability.ID == selectedAbilityWindow.SelectedAbility.ID))
                {
                    MessageBox.Show("Card already contains this ability!");
                    return;
                }
                else
                {
                    _newPokemonCard.Abilities.Add(selectedAbilityWindow.SelectedAbility);
                    _newPokemonCard.Abilities.Sort((ability1, ability2) => ability1.ID - ability2.ID);
                    UpdateDetailsFromNewPokemonCard();
                    return;
                }
            }
        }
        private void btnEditTags_Click(object sender, RoutedEventArgs e)
        {
            SelectTagWPF selectedTagWindow = new SelectTagWPF(_lookupManager);
            selectedTagWindow.Owner = this;
            selectedTagWindow.ShowDialog();
            if (selectedTagWindow.DialogResult == true)
            {
                if (_newPokemonCard.Tags.Contains(selectedTagWindow.SelectedTag))
                {
                    MessageBox.Show("Card already contains this tag!");
                    return;
                }
                else
                {
                    _newPokemonCard.Tags.Add(selectedTagWindow.SelectedTag);
                    _newPokemonCard.Tags.Sort();
                    UpdateDetailsFromNewPokemonCard();
                    return;
                }
            }
        }
        private void btnEditTypes_Click(object sender, RoutedEventArgs e)
        {
            SelectElementWPF selectedTypeWPF = new SelectElementWPF(_lookupManager);
            selectedTypeWPF.Owner = this;
            selectedTypeWPF.ShowDialog();
            if (selectedTypeWPF.DialogResult == true)
            {
                if (_newPokemonCard.Types.Contains(selectedTypeWPF.SelectedElement))
                {
                    MessageBox.Show("Card already contains this type!");
                    return;
                }
                else
                {
                    _newPokemonCard.Types.Add(selectedTypeWPF.SelectedElement);
                    _newPokemonCard.Types.Sort();
                    UpdateDetailsFromNewPokemonCard();
                    return;
                }
            }
        }
        private void btnSaveCard_Click(object sender, RoutedEventArgs e)
        {
            if(ValidateInputs())
            {
                if (txtBoxHP.Text.Trim() == "")
                {
                    _newPokemonCard.HP = null;
                }
                else
                {
                    _newPokemonCard.HP = int.Parse(txtBoxHP.Text);
                }
                _newPokemonCard.Name = txtBoxCardName.Text;
                _newPokemonCard.Note = tbNotes.Text;
                _newPokemonCard.SetNumber = txtBoxSetNumber.Text;
                try
                {
                    if (_newCard)
                    {
                        _cardManager.CreatePokemonCard(_newPokemonCard);
                        MessageBox.Show("Card Created");
                    }
                    else
                    {
                        _cardManager.UpdatePokemonCard(_newPokemonCard);
                        MessageBox.Show("Card Updated");
                    }
                    this.DialogResult = true;
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + "\n\n" + ex.InnerException.Message);
                }
            }
        }
        private bool ValidateInputs()
        {
            if (txtBoxCardName.Text.Length < 3)
            {
                MessageBox.Show("Card Name must be at least 3 characters long!");
                return false;
            }
            int hp;
            if (txtBoxHP.Text.Trim() != "")
            {
                if (int.TryParse(txtBoxHP.Text, out hp))
                {
                    if (hp < 10)
                    {
                        MessageBox.Show("Pokemon must have at least 10 HP or be empty");
                        return false;
                    }
                    if (hp > 1000)
                    {
                        MessageBox.Show("HP cannot be greater than 1000");
                        return false;
                    }
                }
                else
                {
                    MessageBox.Show("HP cannot be greater than 1000");
                    return false;
                }
            }
            if (tbNotes.Text.Length > 64)
            {
                MessageBox.Show("Notes cannot be longer than 64 characters");
                return false;
            }
            if (txtBoxSetNumber.Text.Length < 1)
            {
                MessageBox.Show("Set number must have at least 1 character");
                return false;
            }
            if (txtBoxSetNumber.Text.Length > 32)
            {
                MessageBox.Show("Set number cannot be longer than 32 characters");
                return false;
            }
            return true;
        }
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        private void btnEditImage_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog selectImage = new OpenFileDialog();
            selectImage.Filter = "image files (*.png, *.gif, *.jpeg, *.jpg, *.webp)|*.png;*.gif;*.jpeg;*.jpg;*.webp";
            selectImage.Title = "Select Image";
            selectImage.Multiselect = false;
            if (selectImage.ShowDialog() == true)
            {
                try
                {
                    _cardManager.CopyImageIntoApplication(selectImage.FileName, selectImage.SafeFileName);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + "\n\n" + ex.InnerException?.Message);
                    return;
                }
                _newPokemonCard.ImageName = selectImage.SafeFileName;
                UpdateDetailsFromNewPokemonCard();

            }
        }
    }
}
