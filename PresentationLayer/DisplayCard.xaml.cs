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
    /// <summary>
    /// Interaction logic for DisplayCard.xaml
    /// </summary>
    public partial class DisplayCard : Window
    {
        private UserPokemonCard _userPokemonCard = null;
        private PokemonCard _pokemonCard = null;
        private User _user = null;
        private IUserManager _userManager = null;
        private const string FAVORITE_ICON_NAME = "favoriteicon.png";
        private const string NOT_FAVORITE_ICON_NAME = "notfavoriteicon.png";
        private ImageCollection _imageCollection = null;
        private bool _isInFavoriteGroup;
        private bool _newCard = false;
        private ILookupManager _lookupManager = null;

        public DisplayCard(UserPokemonCard userCard, User user, IUserManager userManager, ILookupManager lookupManager,ImageCollection imageCollection,bool newCard = false)
        {
            _lookupManager = lookupManager;
            _userPokemonCard = userCard;
            _pokemonCard = userCard;
            _user = user;
            _userManager = userManager;
            _newCard = newCard;
            _imageCollection = imageCollection;
            InitializeComponent();
            SetDetailsFromUserPokemonCard();


            // hide add to collection button
            btnAddToCollection.IsEnabled = false;
            btnAddToCollection.Visibility = Visibility.Hidden;

            // change favorite icon
            if (_isInFavoriteGroup = _userPokemonCard.Groups.Contains(_user.FavoriteGroup))
            {
                imgFavorite.Source = _imageCollection.GetIconImage(FAVORITE_ICON_NAME);
            }

            if (newCard)
            {
                btnRemoveFromCollection.Visibility = Visibility.Hidden;
            }
            else
            {
                btnSaveCard.Visibility = Visibility.Hidden;
            }
        }
        public DisplayCard(PokemonCard card, ILookupManager lookupManager, User user, IUserManager userManager, ImageCollection imageCollection)
        {
            _pokemonCard = card;
            _user = user;
            _userManager = userManager;
            _lookupManager = lookupManager;
            _imageCollection = imageCollection;
            InitializeComponent();
            SetDetailsFromPokemonCard();

            // hide user card stuff
            btnAddToFavoriteGroup.Visibility = Visibility.Hidden;
            btnRemoveFromCollection.Visibility = Visibility.Hidden;
            btnRemoveFromGroup.Visibility = Visibility.Hidden;
            btnAddToGroup.Visibility = Visibility.Hidden;
            grdStatus.Visibility = Visibility.Hidden;
            txtBlockStatus.Visibility = Visibility.Hidden;
            btnSaveCard.Visibility = Visibility.Hidden;
        }



        private void SetDetailsFromPokemonCard()
        {
            txtBlockCardName.Text = _pokemonCard.Name + " " + _pokemonCard.HP;
            if (_pokemonCard.Tags.Count > 1)
            {
                txtBlockTags.Text = "Tags";
            }
            txtBlockTagDetails.Text = string.Join(", ", _pokemonCard.Tags);
            _pokemonCard.Pokemon.ForEach(AddPokemon);
            if (_pokemonCard.Types.Count > 1)
            {
                txtBlockElement.Text = "Types";
            }
            txtBlockElementDetails.Text = string.Join(", ", _pokemonCard.Types);
            if (_pokemonCard.Abilities.Count > 1)
            {
                txtBlockAbility.Text = "Abilities";
            }
            _pokemonCard.Abilities.ForEach(AddAbility);
            txtBlockSetNumberDetails.Text = _pokemonCard.SetNumber;
            txtBlockReleaseDateDetails.Text = _pokemonCard.ReleaseYear.ToString("MM/dd/yyyy");
            imgCardImage.Source = _imageCollection.GetCardImage(_pokemonCard.ImageName);
        }

        private void SetDetailsFromUserPokemonCard()
        {
            txtBlockStatusDetails.Text = _userPokemonCard.Status;
            SetDetailsFromPokemonCard();
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
            Grid.SetRow(ucPokemon, grdPokemon.RowDefinitions.Count - 1);
            grdPokemon.Children.Add(ucPokemon);
        }

        private void AddAbility(Ability ability)
        {
            grdAbilities.RowDefinitions.Add(new RowDefinition());
            UCAbility ucAbility = new UCAbility();
            ucAbility.txtBlockAbilityName.Text = ability.Name;
            ucAbility.txtBlockAbilityDescription.Text = ability.Description;
            Grid.SetRow(ucAbility, grdAbilities.RowDefinitions.Count - 1);
            grdAbilities.Children.Add(ucAbility);
        }

        private void btnAddToFavoriteGroup_Click(object sender, RoutedEventArgs e)
        {
            if (_isInFavoriteGroup)
            {
                RemoveFromFavoriteGroup();
            }
            else
            {
                AddToFavoriteGroup();
            }

        }

        private void RemoveFromFavoriteGroup()
        {
            try
            {
                _userManager.RemoveCardFromGroup(_userPokemonCard, _user.FavoriteGroup, _newCard);
                _isInFavoriteGroup = !_isInFavoriteGroup;
                imgFavorite.Source = _imageCollection.GetIconImage(NOT_FAVORITE_ICON_NAME);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n\n" + ex.InnerException.Message);
                return;
            }
        }
        private void AddToFavoriteGroup()
        {
            try
            {
                _userManager.AddCardToGroup(_userPokemonCard, _user.FavoriteGroup, _newCard);
                _isInFavoriteGroup = !_isInFavoriteGroup;
                imgFavorite.Source = _imageCollection.GetIconImage(FAVORITE_ICON_NAME);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n\n" + ex.InnerException.Message);
                return;
            }
        }

        private void btnEditStatus_Click(object sender, RoutedEventArgs e)
        {
            ContextMenu menu = new ContextMenu();
            _lookupManager.GetStatuses(null).ForEach((item) => {
                MenuItem menuItem = new MenuItem()
                {
                    Header = item.DisplayText
                };
                menuItem.Click += (obj, arg) => ChangeStatus_Click(obj, arg, item.RelatedItem);
                menu.MaxHeight = 300;
                menu.Items.Add(menuItem);
            });
            menu.IsOpen = true;
        }

        private void ChangeStatus_Click(object sender, RoutedEventArgs e, string newStatus)
        {
            try
            {
                _userManager.UpdateUserPokemonCardStatus(_userPokemonCard, newStatus, _newCard);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n\n" + ex.InnerException.Message);
                return;
            }
            txtBlockStatusDetails.Text = _userPokemonCard.Status;
        }

        private void btnAddToCollection_Click(object sender, RoutedEventArgs e)
        {
            UserPokemonCard newCard = _userManager.CreateUserPokemonCard(_pokemonCard);
            DisplayCard addNewCardDisplay = new DisplayCard(newCard, _user, _userManager,_lookupManager,_imageCollection,true);
            addNewCardDisplay.Owner = this;
            addNewCardDisplay.ShowDialog();
        }

        private void btnSaveCard_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _userManager.AddPokemonCardToCollection(_userPokemonCard, _user);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n\n" + ex.InnerException.Message);
                return;
            }
            DisplayCard viewSavedCard = new DisplayCard(_userPokemonCard, _user, _userManager, _lookupManager,_imageCollection);
            viewSavedCard.Owner = this;
            viewSavedCard.ShowDialog();
            this.Close();
            this.Owner.Close();

        }

        private void btnRemoveFromCollection_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to remove " + _userPokemonCard.Name + " from your collection?","Confirm?",MessageBoxButton.YesNo,MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                try
                {
                    _userManager.DeleteUserPokemonCard(_userPokemonCard, _user);
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + "\n\n" + ex.InnerException.Message);
                }
            }
        }

        private void btnRemoveFromGroup_Click(object sender, RoutedEventArgs e)
        {
            ContextMenu menu = new ContextMenu();
            _userPokemonCard.Groups.ForEach((grp) =>
            {
                MenuItem item = new MenuItem()
                {
                    Header = grp.Name
                };
                item.Click += (obj, args) => btnRemoveGroupMenuItem_Click(obj, args, grp);
                menu.Items.Add(item);
            });
            if (menu.Items.Count == 0)
            {
                menu.Items.Add(new MenuItem()
                {
                    Header = "Empty"
                });
            }
            menu.IsOpen = true;
        }

        private void btnRemoveGroupMenuItem_Click(object sender, RoutedEventArgs e, PokemonCardGroupVM group)
        {
            if (group == _user.FavoriteGroup)
            {
                RemoveFromFavoriteGroup();
            }
            else
            {
                try
                {
                    _userManager.RemoveCardFromGroup(_userPokemonCard, group, _newCard);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + "\n\n" + ex.InnerException.Message);
                    return;
                }
            }
        }

        private void btnAddGroupMenuItem_Click(object sender, RoutedEventArgs e, PokemonCardGroupVM group)
        {
            if (group == _user.FavoriteGroup)
            {
                AddToFavoriteGroup();
            }
            else
            {
                try
                {
                    _userManager.AddCardToGroup(_userPokemonCard, group, _newCard);
                    if (group == _user.FavoriteGroup)
                    {

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + "\n\n" + ex.InnerException.Message);
                    return;
                }
            }
        }

        private void btnAddToGroup_Click(object sender, RoutedEventArgs e)
        {
            ContextMenu menu = new ContextMenu();
            if (!_userPokemonCard.Groups.Contains(_user.FavoriteGroup))
            {
                MenuItem item = new MenuItem()
                {
                    Header = _user.FavoriteGroup.Name
                };
                item.Click += (obj, args) => btnAddGroupMenuItem_Click(obj, args, _user.FavoriteGroup);
                menu.Items.Add(item);
            }
            foreach (PokemonCardGroupVM group in _user.Groups)
            {
                if (!_userPokemonCard.Groups.Contains(group))
                {
                    MenuItem item = new MenuItem()
                    {
                        Header = group.Name
                    };
                    item.Click += (obj, args) => btnAddGroupMenuItem_Click(obj, args, group);
                    menu.Items.Add(item);
                }
            }
            if (menu.Items.Count == 0)
            {
                menu.Items.Add(new MenuItem()
                {
                    Header = "Empty"
                });
            }
            menu.IsOpen = true;
        }


    }
}
