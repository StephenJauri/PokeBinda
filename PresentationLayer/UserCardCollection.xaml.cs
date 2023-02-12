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
    /// Interaction logic for UserCardCollection.xaml
    /// </summary>
    public partial class UserCardCollection : UserControl, IRefreshUserControl
    {
        private Window _owner = null;
        private User _user = null;
        private ILookupManager _lookupManager = null;
        private IUserManager _userManager = null;
        private bool _userCardMode;
        private ImageCollection _imageCollection = null;
        public List<UserPokemonCard> cards { get; set; }
        public List<PokemonCard> pokemonCards { get; set; }
        public UserCardCollection(ILookupManager lookupManager,IUserManager userManager, User user, Window owner, List<UserPokemonCard> collectionCards, ImageCollection imageCollection)
        {
            InitializeComponent();
            _userCardMode = true;
            _lookupManager = lookupManager;
            _user = user;
            _userManager = userManager;
            _imageCollection = imageCollection;
            this.DataContext = this;
            cards = collectionCards;

            PopulateComboBoxes();
            ClearAndPopulateGrid();
        }
        public UserCardCollection(IUserManager userManager,ILookupManager lookupManager, User user, Window owner, List<PokemonCard> collectionCards, ImageCollection imageCollection)
        {
            InitializeComponent();
            _userCardMode = false;
            _lookupManager = lookupManager;
            _owner = owner;
            _user = user;
            _imageCollection = imageCollection;
            this._userManager = userManager;
            this.DataContext = this;
            pokemonCards = collectionCards;

            PopulateComboBoxes(false);
            ClearAndPopulateGrid();
        }
        private void PopulateComboBoxes(bool includeStatus = true)
        {

            PokemonComboBox.ItemsSource = _lookupManager.GetPokemon();
            TypeComboBox.ItemsSource = _lookupManager.GetTypes();
            TagComboBox.ItemsSource = _lookupManager.GetTags();
            GenComboBox.ItemsSource = _lookupManager.GetGens();

            if (includeStatus)
            {
                StatusComboBox.ItemsSource = _lookupManager.GetStatuses();
                StatusComboBox.SelectedIndex = 0;
                StatusComboBox.SelectionChanged += ComboBox_SelectionChanged;
            }
            else
            {
                StatusComboBox.Visibility = Visibility.Hidden;
                FilterBar.ColumnDefinitions[4].Width = new GridLength(0);
            }

            PokemonComboBox.SelectedIndex = 0;
            TypeComboBox.SelectedIndex = 0;
            GenComboBox.SelectedIndex = 0;
            TagComboBox.SelectedIndex = 0;
            OrderByComboBox.SelectedIndex = 0;


            PokemonComboBox.SelectionChanged += ComboBox_SelectionChanged;
            TypeComboBox.SelectionChanged += ComboBox_SelectionChanged;
            GenComboBox.SelectionChanged += ComboBox_SelectionChanged;
            TagComboBox.SelectionChanged += ComboBox_SelectionChanged;
            OrderByComboBox.SelectionChanged += ComboBox_SelectionChanged;
        }
        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ClearAndPopulateGrid();
        }
        private void ClearAndPopulateGrid()
        {
            ClearGrid();

            if (_userCardMode)
            {
                var filters = CreateGenericFilters<UserPokemonCard>();
                string statusFilter = ((ComboBoxRelationItem<string>)StatusComboBox.SelectedItem).RelatedItem;
                if (statusFilter != null)
                {
                    filters.Add((card) => card.Status == statusFilter);
                }
                var displayedCards = FilterPokemonCards(cards,filters);
                displayedCards = OrderPokemonCards(displayedCards);
                displayedCards.ForEach(AddUserCardItem);
            }
            else
            {
                var filters = CreateGenericFilters<PokemonCard>();
                var displayedCards = FilterPokemonCards(pokemonCards, filters);
                displayedCards = OrderPokemonCards(displayedCards);
                displayedCards.ForEach(AddCardItem);
            }
        }
        private void ClearGrid()
        {
            CardSection.Children.Clear();
            CardSection.RowDefinitions.Clear();
            CardSection.RowDefinitions.Add(new RowDefinition());
        }
        private List<T> FilterPokemonCards<T>(List<T> pokemonCards, List<Func<T,bool>> filters) where T:PokemonCard
        {
            List<T> displayedCards = pokemonCards;

            foreach (Func<T, bool> filter in filters)
            {
                displayedCards = displayedCards.Where(filter).ToList();
            }
            return displayedCards;
        }
        private List<T> OrderPokemonCards<T>(List<T> pokemonCards) where T : PokemonCard
        {
            List<T> sortedCards = pokemonCards.ToList();
            if ((string)((ComboBoxItem)OrderByComboBox.SelectedItem).Content == "Pokedex #")
            {
                sortedCards = sortedCards.OrderBy((card) => card.Pokemon.Count > 0 ? card.Pokemon[0].PokedexNumber : -1).ToList();
            }
            else if ((string)((ComboBoxItem)OrderByComboBox.SelectedItem).Content == "Card Name")
            {
                sortedCards = sortedCards.OrderBy((card) => card.Name.ToLower()).ToList();
            }
            else if ((string)((ComboBoxItem)OrderByComboBox.SelectedItem).Content == "Pokemon Name")
            {
                sortedCards = sortedCards.OrderBy((card) => card.Pokemon.Count > 0 ? card.Pokemon[0].Name.ToLower() : null).ToList();
            }
            else if ((string)((ComboBoxItem)OrderByComboBox.SelectedItem).Content == "Type")
            {
                sortedCards = sortedCards.OrderBy((card) => card.Types.Count > 0 ? card.Types[0].ToLower() : null).ToList();
            }
            else if ((string)((ComboBoxItem)OrderByComboBox.SelectedItem).Content == "Release Date")
            {
                sortedCards = sortedCards.OrderBy((card) => card.ReleaseYear).ToList();
            }
            if (chkDescending.IsChecked == true)
            {
                sortedCards.Reverse();
            }
            return sortedCards;
        }
        private List<Func<T, bool>> CreateGenericFilters<T>() where T: PokemonCard
        {

            List<Func<T, bool>> filters = new List<Func<T, bool>>();
            Pokemon pokemonFilter = ((ComboBoxRelationItem<Pokemon>)PokemonComboBox.SelectedItem).RelatedItem;
            string tagFilter = ((ComboBoxRelationItem<string>)TagComboBox.SelectedItem).RelatedItem;
            string typeFilter = ((ComboBoxRelationItem<string>)TypeComboBox.SelectedItem).RelatedItem;
            int? genFilter = ((ComboBoxRelationItem<int?>)GenComboBox.SelectedItem).RelatedItem;

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

            return filters;
        }
        private void PokemonCard_Click(object sender, RoutedEventArgs e, PokemonCard card)
        {
            DisplayCard displayCard = new DisplayCard(card,_lookupManager, _user, _userManager,_imageCollection);
            displayCard.Owner = this._owner;
            displayCard.ShowDialog();
            ClearAndPopulateGrid();
        }
        private void UserPokemonCard_Click(object sender, RoutedEventArgs e, UserPokemonCard card)
        {
            DisplayCard displayCard = new DisplayCard(card, _user, _userManager,_lookupManager,_imageCollection);
            displayCard.Owner = this._owner;
            displayCard.ShowDialog();
            ClearAndPopulateGrid();
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
        public void AddUserCardItem(UserPokemonCard pokemonCard)
        {
            if (CardSection.RowDefinitions.Count <= CardSection.Children.Count / 4)
            {
                CardSection.RowDefinitions.Add(new RowDefinition());
            }
            PokeCardButton pokeCard = new PokeCardButton()
            {
                CardText = pokemonCard.Name
            };
            pokeCard.CardImage.Source = _imageCollection.GetCardImage(pokemonCard.ImageName);
            Grid.SetRow(pokeCard, CardSection.Children.Count / 4);
            Grid.SetColumn(pokeCard, CardSection.Children.Count % 4);
            pokeCard.ButtonMask.Click += (obj, arg) => UserPokemonCard_Click(obj, arg, pokemonCard);
            CardSection.Children.Add(pokeCard);
        }
        private void ScrollViewer_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            ScrollViewer scrollviewer = sender as ScrollViewer;
            if (e.Delta > 0)
            {
                scrollviewer.LineLeft();
            }
            else
            {
                scrollviewer.LineRight();
            }
            e.Handled = true;
        }
        private void chkDescending_Click(object sender, RoutedEventArgs e)
        {
            ClearAndPopulateGrid();
        }
        public void Refresh()
        {
            ClearAndPopulateGrid();
        }
    }

}
