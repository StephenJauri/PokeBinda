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
using LogicLayer;
using LogicLayerInterfaces;

namespace PresentationLayer
{
    public partial class PokeBinda : Window
    {
        // user stuff
        private User _user = null;
        private IUserManager _userManager = null;

        // both stuff
        private List<PokemonCard> _pokemonCards = null;
        private ILookupManager _lookupManager = null;
        private ICardManager _cardManager = null;
        private bool _employeeMode = false;
        private ImageCollection _imageCollection = null;
        private Dictionary<string, UserControl> _tabCollectionUC = new Dictionary<string, UserControl>();
        private Dictionary<string, IRefreshUserControl> _tabCollectionIR = new Dictionary<string, IRefreshUserControl>();
        private string _currentlyDisplayedTab = "";

        // employee stuff

        private Employee _employee = null;
        private IEmployeeManager _empoyeeManager = null;
        private IPokemonManager _pokemonManager = null;
        private IAbilityManager _abilityManager = null;

        // tab images
        private const string BINDER_IMAGE_NAME = "bindericon.png";
        private const string FAVORITE_IMAGE_NAME = "favoritegroupicon.png";
        private const string BROWSE_IMAGE_NAME = "browseicon.png";
        private const string COLLECTION_IMAGE_NAME = "collectionicon.png";
        private const string NEW_IMAGE_NAME = "plusicon.png";
        private const string EMPLOYEE_IMAGE_NAME = "employeesicon.png";
        private const string POKEMON_IMAGE_NAME = "pokemonicon.png";
        private const string ABILITY_IMAGE_NAME = "abilityicon.png";

        public PokeBinda(User user, IUserManager userManager, ImageCollection imageCollection)
        {
            InitializeComponent();
            _user = user;
            _userManager = userManager;
            _employeeMode = false;
            _lookupManager = new LookupManager();
            _cardManager = new CardManager();
            _pokemonCards = _cardManager.LoadAllActiveReleasedCards();
            _imageCollection = imageCollection;
            SetupUserTabs();
        }
        public PokeBinda(Employee employee, IEmployeeManager employeeManager, ImageCollection imageCollection)
        {
            InitializeComponent();
            _employee = employee;
            _empoyeeManager = employeeManager;
            _lookupManager = new LookupManager();
            _pokemonManager = new PokemonManager();
            _abilityManager = new AbilityManager();
            _employeeMode = true;
            _cardManager = new CardManager();
            _imageCollection = imageCollection;
            SetupEmployeeTabs();
        }
        public void SetupUserTabs()
        {
            ClearTabs();

            // favorite tab
            addTab(_user.FavoriteGroup.Name, _imageCollection.GetIconImage(FAVORITE_IMAGE_NAME), (sender, args) => GroupButton_Click(sender, args, _user.FavoriteGroup), (sender, args) => GroupButton_RightClick(sender, args, _user.FavoriteGroup));


            // collection tab and browse tab
            addTab("Collection", _imageCollection.GetIconImage(COLLECTION_IMAGE_NAME), BrowseCollection_Click, null);
            addTab("Browse Cards", _imageCollection.GetIconImage(BROWSE_IMAGE_NAME), BrowseCards_Click, null);

            // other user groups alphabetically 
            foreach (PokemonCardGroupVM group in _user.Groups)
            {
                addTab(group.Name, _imageCollection.GetIconImage(BINDER_IMAGE_NAME), (sender, args) => GroupButton_Click(sender, args, group), (sender, args) => GroupButton_RightClick(sender, args, group));
            }

            // new group tab button and removed cards tab
            addTab("New Group", _imageCollection.GetIconImage(NEW_IMAGE_NAME), NewGroupButton_Click, null);
        }
        private void SetupEmployeeTabs()
        {
            ClearTabs();
            if (_employee.Roles.Contains("Employee"))
            {
                addTab("Cards", _imageCollection.GetIconImage(COLLECTION_IMAGE_NAME), BrowseCardsEmployee_Click, null);
                addTab("Abilities", _imageCollection.GetIconImage(ABILITY_IMAGE_NAME), ViewAllAbilities, null);
                addTab("Pokemon", _imageCollection.GetIconImage(POKEMON_IMAGE_NAME), ViewAllPokemon, null);
            }
            if (_employee.Roles.Contains("Admin"))
            {
                addTab("Employees", _imageCollection.GetIconImage(EMPLOYEE_IMAGE_NAME), ViewAllEmployees, null);
                // admin tabs
            }
        }
        private void ViewAllPokemon(object sender, RoutedEventArgs e)
        {
            string pokemonKey = "pokemon";
            InformationSection.Children.Clear();
            if (DictionaryContainsKey(pokemonKey))
            {
                DisplayAndRefreshExistingTab(pokemonKey);
            }
            else
            {
                ViewAllPokemonUC pokemonPanel = new ViewAllPokemonUC(_pokemonManager, this);
                AddToDictionary(pokemonKey, pokemonPanel);
                _currentlyDisplayedTab = pokemonKey;
                InformationSection.Children.Add(pokemonPanel);
            }
        }
        private void ViewAllAbilities(object sender, RoutedEventArgs e)
        {
            string abilityKey = "ability";
            InformationSection.Children.Clear();
            if (DictionaryContainsKey(abilityKey))
            {
                DisplayAndRefreshExistingTab(abilityKey);
            }
            else
            {
                ViewAllAbilitiesUC abilityPanel = new ViewAllAbilitiesUC(_abilityManager, this);
                AddToDictionary(abilityKey, abilityPanel);
                _currentlyDisplayedTab = abilityKey;
                InformationSection.Children.Add(abilityPanel);
            }
        }
        private void AddToDictionary<T>(string key, T refreshableUserControl) where T : UserControl, IRefreshUserControl
        {
            _tabCollectionIR.Add(key, refreshableUserControl);
            _tabCollectionUC.Add(key, refreshableUserControl);
        }
        private void DisplayAndRefreshExistingTab(string key)
        {
            _currentlyDisplayedTab = key;
            InformationSection.Children.Add(_tabCollectionUC[key]);
            _tabCollectionIR[key].Refresh();
        }
        private bool DictionaryContainsKey(string key)
        {
            return _tabCollectionIR.ContainsKey(key) && _tabCollectionUC.ContainsKey(key);
        }
        private void RemoveFromDictionary(string key)
        {
            _tabCollectionUC.Remove(key);
            _tabCollectionIR.Remove(key);
        }
        public void ViewAllEmployees(object sender, RoutedEventArgs e)
        {
            string employeesKey = "employees";
            InformationSection.Children.Clear();
            if (DictionaryContainsKey(employeesKey))
            {
                DisplayAndRefreshExistingTab(employeesKey);
            }
            else
            {
                ViewAllEmployeesUC employeePanel = new ViewAllEmployeesUC(_empoyeeManager, this, _employee);
                AddToDictionary(employeesKey, employeePanel);
                _currentlyDisplayedTab = employeesKey;
                InformationSection.Children.Add(employeePanel);
            }
        }
        public void ClearTabs()
        {
            gridTabButtons.Children.Clear();
            gridTabButtons.RowDefinitions.Clear();
            gridTabButtons.ColumnDefinitions.Clear();
        }
        public void addTab(string buttonText, BitmapImage tabImage, Action<object, RoutedEventArgs> clickAction, Action<object, RoutedEventArgs> rightClickAction)
        {
            RowDefinition newTabRow = new RowDefinition();
            newTabRow.Height = new GridLength(115);
            TabButton newTab = new TabButton()
            {
                TabLabel = buttonText,
                TabImage = tabImage
            };
            if (rightClickAction != null)
            {
                newTab.Button.MouseRightButtonDown += rightClickAction.Invoke;
            }
            newTab.Button.Click += clickAction.Invoke;
            gridTabButtons.RowDefinitions.Add(newTabRow);

            Grid.SetRow(newTab, gridTabButtons.RowDefinitions.Count - 1);
            gridTabButtons.Children.Add(newTab);
        }
        private void NewGroupButton_Click(object sender, RoutedEventArgs e)
        {
            TextPrompt prompt = new TextPrompt();
            prompt.PromptText = "Enter Group Name";
            prompt.Title = "New Group";
            prompt.Owner = this;
            prompt.WindowStartupLocation = WindowStartupLocation.CenterOwner;

            bool? result = prompt.ShowDialog();


            if (result == true)
            {
                string newName = prompt.Input;
                if (newName.Length < 3 || newName.Length > 16)
                {
                    MessageBox.Show("Group name must be between 3 and 16 characters");
                    return;
                }
                try
                {
                    _userManager.CreateGroup(_user, newName);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + "\n\n" + ex.InnerException.Message);
                    return;
                }
                SetupUserTabs();
            }
        }
        private void BrowseCollection_Click(object sender, RoutedEventArgs e)
        {
            BrowseCollection();
        }
        private void BrowseCollection()
        {
            string browseCollectionKey = "browse_collection";
            InformationSection.Children.Clear();
            if (DictionaryContainsKey(browseCollectionKey))
            {
                DisplayAndRefreshExistingTab(browseCollectionKey);
            }
            else
            {
                UserCardCollection collection = new UserCardCollection(_lookupManager, _userManager, _user, this, _user.PokemonCards, _imageCollection);
                AddToDictionary(browseCollectionKey,collection);
                _currentlyDisplayedTab = browseCollectionKey;
                InformationSection.Children.Add(collection);
            }
        }
        private void BrowseCards_Click(object sender, RoutedEventArgs e)
        {
            string browseCardsUserKey = "browse_cards_user";
            InformationSection.Children.Clear();
            if (DictionaryContainsKey(browseCardsUserKey))
            {
                DisplayAndRefreshExistingTab(browseCardsUserKey);
            }
            else
            {
                UserCardCollection collection = new UserCardCollection(_userManager, _lookupManager, _user, this, _pokemonCards, _imageCollection);
                AddToDictionary(browseCardsUserKey, collection);
                InformationSection.Children.Add(collection);
                _currentlyDisplayedTab = browseCardsUserKey;
            }
        }
        private void BrowseCardsEmployee_Click(object sender, RoutedEventArgs e)
        {
            string browseCardsEmployeeKey = "browse_cards_employee";
            InformationSection.Children.Clear();
            if (DictionaryContainsKey(browseCardsEmployeeKey))
            {
                DisplayAndRefreshExistingTab(browseCardsEmployeeKey);
            }
            else
            {
                EmployeeCardCollection collection = new EmployeeCardCollection(_empoyeeManager, _lookupManager, this, _cardManager, _pokemonManager, _abilityManager, _imageCollection);
                AddToDictionary(browseCardsEmployeeKey, collection);
                InformationSection.Children.Add(collection);
                _currentlyDisplayedTab = browseCardsEmployeeKey;
            }
        }
        private void GroupButton_Click(object sender, RoutedEventArgs e, PokemonCardGroupVM group)
        {
            string browseGroupKey = "group" + group.ID;
            InformationSection.Children.Clear();
            if (DictionaryContainsKey(browseGroupKey))
            {
                DisplayAndRefreshExistingTab(browseGroupKey);
            }
            else
            {
                UserCardCollection collection = new UserCardCollection(_lookupManager, _userManager, _user, this, group.Cards, _imageCollection);
                AddToDictionary(browseGroupKey, collection);
                InformationSection.Children.Add(collection);
                _currentlyDisplayedTab = browseGroupKey;
            }
        }
        private void GroupButton_RightClick(object sender, RoutedEventArgs e, PokemonCardGroupVM group)
        {
            ContextMenu contextMenu = new ContextMenu();
            MenuItem setFavorite = new MenuItem();
            MenuItem rename = new MenuItem();
            MenuItem remove = new MenuItem();
            setFavorite.Header = "Set Favorite";
            rename.Header = "Rename";
            remove.Header = "Remove";

            remove.Click += (obj, args) => MenuRemove_Click(obj, args, group);
            rename.Click += (obj, args) => MenuRename_Click(obj, args, group);
            setFavorite.Click += (obj, args) => MenuSetFavorite_Click(obj, args, group);

            contextMenu.Items.Add(rename);

            if(!group.Favorite)
            {
                contextMenu.Items.Add(setFavorite);
                contextMenu.Items.Add(remove);
            }

            contextMenu.IsOpen = true;
        }
        private void MenuRemove_Click(object sender, RoutedEventArgs e, PokemonCardGroupVM group)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure you want to delete " + group.Name + "?", "Confirm", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result == MessageBoxResult.Yes)
            {
                string groupKey = "group" + group.ID;
                _userManager.DeleteGroup(_user, group);
                SetupUserTabs();
                if (_currentlyDisplayedTab == groupKey)
                {
                    RemoveFromDictionary(groupKey);
                    BrowseCollection();
                }
            }
        }
        private void MenuRename_Click(object sender, RoutedEventArgs e, PokemonCardGroupVM group)
        {
            TextPrompt prompt = new TextPrompt();
            prompt.PromptText = "Rename: " + group.Name;
            prompt.Input = group.Name;
            prompt.Title = "Rename";
            prompt.Owner = this;
            prompt.WindowStartupLocation = WindowStartupLocation.CenterOwner;

            bool? result = prompt.ShowDialog();


            if (result == true)
            {
                string newName = prompt.Input;
                if (newName.Length < 3 || newName.Length > 16)
                {
                    MessageBox.Show("Group name must be between 3 and 16 characters");
                    return;
                }
                try
                {
                    _userManager.RenameGroup(_user, group, newName);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + "\n\n" + ex.InnerException.Message);
                    return;
                }
                SetupUserTabs();
            }
        }
        private void MenuSetFavorite_Click(object sender, RoutedEventArgs e, PokemonCardGroupVM group)
        {
            try
            {
                _userManager.SetFavoriteGroup(_user, group);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n\n" + ex.InnerException.Message);
            }
            SetupUserTabs();
        }
        private void btnProfile_Click(object sender, RoutedEventArgs e)
        {
            ContextMenu userContext = new ContextMenu();
            MenuItem viewProfile = new MenuItem();
            viewProfile.Header = "Profile";
            MenuItem logout = new MenuItem();
            logout.Header = "Log Out";

            logout.Click += MenuLogout_Click;
            viewProfile.Click += ViewProfile_Click;

            userContext.Items.Add(viewProfile);
            userContext.Items.Add(logout);
            userContext.IsOpen = true;
        }
        private void ViewProfile_Click(object sender, RoutedEventArgs e)
        {
            ViewProfileWPF viewProfileWindow = null;
            if (_employeeMode)
            {
                viewProfileWindow = new ViewProfileWPF(_employee, _empoyeeManager);
            }
            else
            {
                viewProfileWindow = new ViewProfileWPF(_user, _userManager);
            }
            viewProfileWindow.Owner = this;
            viewProfileWindow.ShowDialog();
            if (viewProfileWindow.DialogResult == true)
            {
                Logout();
            }
        }
        private void MenuLogout_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure you want to log out?", "Log Out", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result == MessageBoxResult.Yes)
            {
                Logout();
            }
        }
        private void Logout()
        {
            Login login = new Login();
            login.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            this.Close();
            login.Show();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
        }
    }
}
