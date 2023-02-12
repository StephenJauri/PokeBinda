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
using LogicLayerInterfaces;
using LogicLayer;

namespace PresentationLayer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class Login : Window
    {
        private const string SWITCH_TO_EMPLOYEE_MODE_EMAIL = "Employee";
        private const string SWITCH_TO_EMPLOYEE_MODE_PASSWORD = "Password";
        private bool _employeeMode = false;
        private ImageCollection _imageCollection = null;
        public Login()
        {
            InitializeComponent();
            StaticInformation.IconImagePath = Environment.CurrentDirectory + "/images/icons/";
            StaticInformation.CardImagePath = Environment.CurrentDirectory + "/images/cards/";
            _imageCollection = new ImageCollection();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            // trigger a switch between user modes
            if (Keyboard.IsKeyDown(Key.LeftAlt) || Keyboard.IsKeyDown(Key.RightAlt))
            {
                if (tbEmail.Text == SWITCH_TO_EMPLOYEE_MODE_EMAIL && pbPassword.Password == SWITCH_TO_EMPLOYEE_MODE_PASSWORD)
                {
                    _employeeMode = !_employeeMode;
                    UpdateModeButtons();
                    return;
                }
            }

            if (_employeeMode)
            {

                Employee employee = null;
                IEmployeeManager manager = new EmployeeManager();

                string email = tbEmail.Text.ToLower();
                string password = pbPassword.Password;

                if (!ValidateInputs())
                {
                    return;
                }

                try
                {
                    employee = manager.LoginEmployee(email, password);
                    PokeBinda binderBrowser = new PokeBinda(employee, manager,_imageCollection);
                    Close();
                    binderBrowser.Show();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + "\n\n" + ex.InnerException?.Message);
                    return;
                }
            }
            else
            {
                User user = null;
                ICardManager cardManager = new CardManager();
                IUserManager manager = new UserManager(cardManager);

                string email = tbEmail.Text.ToLower();
                string password = pbPassword.Password;
                
                if (!ValidateInputs())
                {
                    return;
                }

                try
                {
                    user = manager.LoginUser(email, password);
                    PokeBinda binderBrowser = new PokeBinda(user, manager,_imageCollection);
                    this.Close();
                    binderBrowser.Show();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + "\n\n" + ex.InnerException.Message);
                    return;
                }
            }
        }

        private bool ValidateInputs()
        {
            if (tbEmail.Text.Length < 6)
            {
                MessageBox.Show("Invalid email address.");
                tbEmail.Text = "";
                tbEmail.Focus();
                return false;
            }
            if (pbPassword.Password == "")
            {
                MessageBox.Show("You must enter a password.");
                pbPassword.Focus();
                return false;
            }
            return true;

        }

        private void UpdateModeButtons()
        {
            if (_employeeMode)
            {
                Grid.SetRowSpan(txtTitle, 1);
                txtSubTitle.Visibility = Visibility.Visible;
                txtTitle.VerticalAlignment = VerticalAlignment.Bottom;
                btnCreateAccount.Visibility = Visibility.Hidden;
            }
            else
            {
                Grid.SetRowSpan(txtTitle, 2);
                txtTitle.VerticalAlignment = VerticalAlignment.Center;
                txtSubTitle.Visibility = Visibility.Collapsed;
                btnCreateAccount.Visibility = Visibility.Visible;
            }
            tbEmail.Clear();
            pbPassword.Clear();
            tbEmail.Focus();
        }
        private void LoginForm_Loaded(object sender, RoutedEventArgs e)
        {
            btnLogin.IsDefault = true;
            tbEmail.Focus();
        }

        private void btnCreateAccount_Click(object sender, RoutedEventArgs e)
        {
            CreateAccountWPF newAccount = new CreateAccountWPF();
            newAccount.Owner = this;
            newAccount.ShowDialog();
        }
    }
}
