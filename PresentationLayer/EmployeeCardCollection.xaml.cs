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
    /// Interaction logic for EmployeeCardCollection.xaml
    /// </summary>
    public partial class EmployeeCardCollection : UserControl, IRefreshUserControl
    {

        private Window _owner = null;
        private ILookupManager _lookupManager = null;
        private IEmployeeManager _employeeManager = null;
        private ICardManager _cardManager = null;
        private IPokemonManager _pokemonManager = null;
        private IAbilityManager _abilityManager = null;
        private List<PokemonCard> _pokemonCards { get; set; }
        private ImageCollection _imageCollection = null;
        private const string NEW_CARD_IMAGE_NAME = "default.png";

        public EmployeeCardCollection(IEmployeeManager employeeManager, ILookupManager lookupManager, Window owner, ICardManager cardManager, IPokemonManager pokemonManager, IAbilityManager abilityManager, ImageCollection imageCollection)
        {
            InitializeComponent();

            _cardManager = cardManager;
            _lookupManager = lookupManager;
            _pokemonManager = pokemonManager;
            _abilityManager = abilityManager;
            _owner = owner;
            _imageCollection = imageCollection;
            _employeeManager = employeeManager;
            this.DataContext = this;

            try
            {
                _pokemonCards = _cardManager.LoadAllPokemonCards();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message + "\n\n" + ex.InnerException.Message);
            }
            PopulateComboBoxes();
            ClearAndPopulateGrid();
        }
        private void PopulateComboBoxes()
        {

            PokemonComboBox.ItemsSource = _lookupManager.GetPokemon();
            TypeComboBox.ItemsSource = _lookupManager.GetTypes();
            TagComboBox.ItemsSource = _lookupManager.GetTags();
            GenComboBox.ItemsSource = _lookupManager.GetGens();

            PokemonComboBox.SelectedIndex = 0;
            TypeComboBox.SelectedIndex = 0;
            GenComboBox.SelectedIndex = 0;
            TagComboBox.SelectedIndex = 0;
            ReleasedComboBox.SelectedIndex = 2;
            ActiveComboBox.SelectedIndex = 0;
            OrderByComboBox.SelectedIndex = 0;


            PokemonComboBox.SelectionChanged += ComboBox_SelectionChanged;
            TypeComboBox.SelectionChanged += ComboBox_SelectionChanged;
            GenComboBox.SelectionChanged += ComboBox_SelectionChanged;
            TagComboBox.SelectionChanged += ComboBox_SelectionChanged;
            ActiveComboBox.SelectionChanged += ComboBox_SelectionChanged;
            ReleasedComboBox.SelectionChanged += ComboBox_SelectionChanged;
            OrderByComboBox.SelectionChanged += ComboBox_SelectionChanged;
        }
        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ClearAndPopulateGrid();
        }
        private void ClearAndPopulateGrid()
        {
            ClearGrid();

            var filters = CreateFilters();
            var displayedCards = FilterPokemonCards(_pokemonCards, filters);
            displayedCards = OrderPokemonCards(displayedCards);
            AddFakeCard();
            displayedCards.ForEach(AddCardItem);
        }
        private List<PokemonCard> FilterPokemonCards(List<PokemonCard> pokemonCards, List<Func<PokemonCard, bool>> filters)
        {
            List<PokemonCard> displayedCards = pokemonCards;

            foreach (Func<PokemonCard, bool> filter in filters)
            {
                displayedCards = displayedCards.Where(filter).ToList();
            }
            return displayedCards;
        }
        private void ClearGrid()
        {
            CardSection.Children.Clear();
            CardSection.RowDefinitions.Clear();
            CardSection.RowDefinitions.Add(new RowDefinition());
        }
        private List<PokemonCard> OrderPokemonCards(List<PokemonCard> pokemonCards)
        {
            List<PokemonCard> sortedPokemonCards = pokemonCards.ToList();
            if ((string)((ComboBoxItem)OrderByComboBox.SelectedItem).Content == "Pokedex #")
            {
                sortedPokemonCards = sortedPokemonCards.OrderBy((card) => card.Pokemon.Count > 0 ? card.Pokemon[0].PokedexNumber : -1).ToList();
            }
            else if ((string)((ComboBoxItem)OrderByComboBox.SelectedItem).Content == "Card Name")
            {
                sortedPokemonCards = sortedPokemonCards.OrderBy((card) => card.Name.ToLower()).ToList();
            }
            else if ((string)((ComboBoxItem)OrderByComboBox.SelectedItem).Content == "Pokemon Name")
            {
                sortedPokemonCards = sortedPokemonCards.OrderBy((card) => card.Pokemon.Count > 0 ? card.Pokemon[0].Name.ToLower() : null).ToList();
            }
            else if ((string)((ComboBoxItem)OrderByComboBox.SelectedItem).Content == "Type")
            {
                sortedPokemonCards = sortedPokemonCards.OrderBy((card) => card.Types.Count > 0 ? card.Types[0].ToLower() : null).ToList();
            }
            else if ((string)((ComboBoxItem)OrderByComboBox.SelectedItem).Content == "Release Date")
            {
                pokemonCards = pokemonCards.OrderBy((card) => card.ReleaseYear).ToList();
            }
            if (chkDescending.IsChecked == true)
            {
                sortedPokemonCards.Reverse();
            }
            return sortedPokemonCards;
        }
        private List<Func<PokemonCard, bool>> CreateFilters()
        {

            List<Func<PokemonCard, bool>> filters = new List<Func<PokemonCard, bool>>();
            Pokemon pokemonFilter = ((ComboBoxRelationItem<Pokemon>)PokemonComboBox.SelectedItem).RelatedItem;
            string tagFilter = ((ComboBoxRelationItem<string>)TagComboBox.SelectedItem).RelatedItem;
            string typeFilter = ((ComboBoxRelationItem<string>)TypeComboBox.SelectedItem).RelatedItem;
            int? genFilter = ((ComboBoxRelationItem<int?>)GenComboBox.SelectedItem).RelatedItem;
            string activeFilter = (string)((ComboBoxItem)ActiveComboBox.SelectedItem).Content;
            string releasedFilter = (string)((ComboBoxItem)ReleasedComboBox.SelectedItem).Content;

            if (pokemonFilter != null)
            {
                filters.Add((card) => card.Pokemon.Exists((poke) => pokemonFilter.ID == poke.ID));
            }
            if (tagFilter != null)
            {
                filters.Add((card) => card.Tags.Contains(tagFilter));
            }
            if (typeFilter != null)
            {
                filters.Add((card) => card.Types.Contains(typeFilter));
            }
            if (genFilter != null)
            {
                filters.Add((card) => card.Pokemon.Count > 0 && card.Pokemon[0].Gen == genFilter);
            }
            if (releasedFilter != "Both")
            {
                filters.Add((card) => card.Released == (releasedFilter == "Released"));
            }
            if (activeFilter != "Both")
            {
                filters.Add((card) => card.Active == (activeFilter == "Active"));
            }

            return filters;
        }
        private void PokemonCard_Click(object sender, RoutedEventArgs e, PokemonCard card)
        {
            CreateAndEditCardWPF displayCard = new CreateAndEditCardWPF(card, _cardManager, _lookupManager, _pokemonManager,_abilityManager,_imageCollection);
            displayCard.Owner = this._owner;
            displayCard.ShowDialog();
            if (displayCard.DialogResult == true)
            {
                ClearAndPopulateGrid();
            }
        }
        private void NewPokemonCard_Click(object sender, RoutedEventArgs e)
        {
            PokemonCard _newPokemonCard = new PokemonCard()
            {
                Abilities = new List<Ability>(),
                Active = true,
                HP = null,
                ImageName = "default.png",
                Name = "Card Name",
                Note = "",
                Pokemon = new List<Pokemon>(),
                Released = false,
                ReleaseYear = DateTime.Now,
                SetNumber = "0/0",
                Tags = new List<string>(),
                Types = new List<string>()
            };

            CreateAndEditCardWPF displayCard = new CreateAndEditCardWPF(_newPokemonCard, _cardManager, _lookupManager,_pokemonManager,_abilityManager,_imageCollection, true);
            displayCard.Owner = this._owner;
            displayCard.ShowDialog();

            if (displayCard.DialogResult == true)
            {
                ClearAndPopulateGrid();
            }

        }
        public void AddFakeCard()
        {
            PokeCardButton pokeCard = new PokeCardButton()
            {
                CardText = "New Card"
            };
            pokeCard.CardImage.Source = _imageCollection.GetCardImage(NEW_CARD_IMAGE_NAME);
            Grid.SetRow(pokeCard, CardSection.Children.Count / 4);
            Grid.SetColumn(pokeCard, CardSection.Children.Count % 4);
            pokeCard.ButtonMask.Click += (obj, arg) => NewPokemonCard_Click(obj, arg);
            CardSection.Children.Add(pokeCard);
        }
        public void AddCardItem(PokemonCard pokemonCard)
        {
            if (CardSection.RowDefinitions.Count <= CardSection.Children.Count / 4)
            {
                CardSection.RowDefinitions.Add(new RowDefinition());
            }
            PokeCardButton pokeCard = new PokeCardButton()
            {
                CardText = pokemonCard.Name,
            };
            pokeCard.CardImage.Source = _imageCollection.GetCardImage(pokemonCard.ImageName);
            Grid.SetRow(pokeCard, CardSection.Children.Count / 4);
            Grid.SetColumn(pokeCard, CardSection.Children.Count % 4);
            pokeCard.ButtonMask.Click += (obj, arg) => PokemonCard_Click(obj, arg, pokemonCard);
            CardSection.Children.Add(pokeCard);
        }
        private void ScrollViewer_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            ScrollViewer scrollviewer = sender as ScrollViewer;
            if (e.Delta > 0)
                scrollviewer.LineLeft();
            else
                scrollviewer.LineRight();
            e.Handled = true;
        }
        private void chkDescending_Click(object sender, RoutedEventArgs e)
        {
            ClearAndPopulateGrid();
        }

        public void Refresh()
        {
            try
            {
                _pokemonCards = _cardManager.LoadAllPokemonCards();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n\n" + ex.InnerException.Message);
            }
            ClearAndPopulateGrid();
        }
    }

}
