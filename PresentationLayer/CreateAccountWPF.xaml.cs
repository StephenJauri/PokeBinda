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
using LogicLayer;
using LogicLayerInterfaces;
using DataObjects;

namespace PresentationLayer
{
    /// <summary>
    /// Interaction logic for CreateAccountWPF.xaml
    /// </summary>
    public partial class CreateAccountWPF : Window
    {
        private IUserManager _userManager = null;
        public CreateAccountWPF()
        {
            _userManager = new UserManager();
            InitializeComponent();
            AddEvents();
            btnSave.IsDefault = true;
            btnCancel.IsCancel = true;
            TimeSpan time = new TimeSpan(365 * 13,0,0,0);
            datePickerBirthday.SelectedDate = DateTime.Now - time;
        }

        private void AddEvents()
        {
            txtBoxFamilyName.TextChanged += TxtBoxFamilyName_TextChanged;
            txtBoxGivenName.TextChanged += TxtBoxGivenName_TextChanged;
            txtBoxEmail.TextChanged += TxtBoxEmail_TextChanged;
            pbPassword.PasswordChanged += PassBoxPassword_PasswordChanged;
        }
        private void TxtBoxGivenName_TextChanged(object sender, TextChangedEventArgs e)
        {
            lblValidationGivenName.Visibility = Visibility.Hidden;
        }
        private void TxtBoxEmail_TextChanged(object sender, TextChangedEventArgs e)
        {
            lblValidationEmail.Visibility = Visibility.Hidden;
        }
        private void PassBoxPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            lblValidationPassword.Visibility = Visibility.Hidden;
        }
        private void TxtBoxFamilyName_TextChanged(object sender, TextChangedEventArgs e)
        {
            lblValidationFamilyName.Visibility = Visibility.Hidden;
        }
        private void datePickerBirthday_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (datePickerBirthday.SelectedDate != null)
            {
                txtBoxBirthday.Text = datePickerBirthday.SelectedDate.Value.ToString("MMMM d, yyyy");
            }
        }
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (validateInputs())
            {
                User _account = new User();
                _account.GivenName = txtBoxGivenName.Text;
                _account.FamilyName = txtBoxFamilyName.Text;
                _account.Email = txtBoxEmail.Text;
                _account.Birthday = datePickerBirthday.SelectedDate.Value;
                try
                {
                    _userManager.CreateUserAccount(_account, pbPassword.Password);
                    MessageBox.Show("Account Created!");
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + "\n\n" + ex.InnerException.Message);
                }
            }
        }
        private bool validateInputs()
        {
            bool success = true;
            if (txtBoxGivenName.Text.Length < 3)
            {
                success = false;
                lblValidationGivenName.Content = "Given name cannot be shorter than 3 characters!";
                lblValidationGivenName.Visibility = Visibility.Visible;
            }
            else if (txtBoxGivenName.Text.Length > 64)
            {
                success = false;
                lblValidationGivenName.Content = "Given name cannont be longer than 64 characters!";
                lblValidationGivenName.Visibility = Visibility.Visible;
            }
            if (txtBoxFamilyName.Text.Length < 3)
            {
                success = false;
                lblValidationFamilyName.Content = "Family name cannot be shorter than 3 characters!";
                lblValidationFamilyName.Visibility = Visibility.Visible;
            }
            else if (txtBoxFamilyName.Text.Length > 64)
            {
                success = false;
                lblValidationFamilyName.Content = "Family name cannot be longer than 64 characters!";
                lblValidationFamilyName.Visibility = Visibility.Visible;
            }
            if (pbPassword.Password.Length < 8)
            {
                success = false;
                lblValidationPassword.Content = "Password must be at least 8 characters!";
                lblValidationPassword.Visibility = Visibility.Visible;
            }
            if (txtBoxEmail.Text.Length < 8)
            {
                success = false;
                lblValidationEmail.Content = "Email must be at least 8 characters!";
                lblValidationEmail.Visibility = Visibility.Visible;
            }
            if (txtBoxEmail.Text.Length > 128)
            {
                success = false;
                lblValidationEmail.Content = "Email cannot be larger than 128 characters!";
                lblValidationEmail.Visibility = Visibility.Visible;
            }
            if (datePickerBirthday.SelectedDate == null)
            {
                MessageBox.Show("Please select a birthday");
                success = false;
            }
            
            return success;
        }
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

    }
}
