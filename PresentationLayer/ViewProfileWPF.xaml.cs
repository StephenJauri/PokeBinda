using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
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
    /// Interaction logic for ViewProfileWPF.xaml
    /// </summary>
    public partial class ViewProfileWPF : Window
    {
        private Account _account = null;
        private bool _employeeAccount;
        private DateTime? _changedBirthday = null;
        private IUserManager _userManager = null;
        private IEmployeeManager _employeeManager = null;
        public ViewProfileWPF(Account account, IUserManager userManager)
        {
            _account = account;
            _employeeAccount = false;
            _userManager = userManager;
            InitializeComponent();
            UpdateAccountInformation();
            AddEvents();
            btnSave.IsDefault = true;
        }
        public ViewProfileWPF(Account account, IEmployeeManager employeeManager)
        {
            _account = account;
            _employeeAccount = true;
            _employeeManager = employeeManager;
            InitializeComponent();
            UpdateAccountInformation();
            AddEvents();
            btnSave.IsDefault = true;
        }

        private void AddEvents()
        {
            txtBoxFamilyName.TextChanged += TxtBoxFamilyName_TextChanged;
            txtBoxGivenName.TextChanged += TxtBoxGivenName_TextChanged;
        }
        private void UpdateAccountInformation()
        {
            txtBlockWelcome.Text = "Welcome " + _account.FamilyName + ", " + _account.GivenName;
            txtBoxBirthday.Text = _account.Birthday.ToString("MMMM d, yyyy");
            txtBoxEmail.Text = _account.Email;
            txtBoxGivenName.Text = _account.GivenName;
            txtBoxFamilyName.Text = _account.FamilyName;
        }

        private void btnEditProfile_Click(object sender, RoutedEventArgs e)
        {
            EnterEditMode();
        }
        private void EnterEditMode()
        {
            txtBoxFamilyName.BorderThickness = new Thickness(1);
            txtBoxFamilyName.IsReadOnly = false;
            txtBoxGivenName.BorderThickness = new Thickness(1);
            txtBoxGivenName.IsReadOnly = false;
            txtBoxBirthday.BorderThickness = new Thickness(1);
            datePickerBirthday.Visibility = Visibility.Visible;
            btnSave.Visibility = Visibility.Visible;
            btnCancel.Visibility = Visibility.Visible;
            if (!_employeeAccount)
            {
                btnDeleteAccount.Visibility = Visibility.Visible;
                btnDeleteAccount.IsEnabled = true;
            }
            btnEditProfile.Visibility = Visibility.Hidden;
            btnChangePassword.Visibility = Visibility.Visible;
            txtBoxPassword.BorderThickness = new Thickness(1);
            btnSave.IsEnabled = true;
            btnCancel.IsEnabled = true;
            this.Title = "Edit Profile";
        }
        private void ExitEditMode()
        {
            txtBoxFamilyName.BorderThickness = new Thickness(0);
            txtBoxFamilyName.IsReadOnly = true;
            txtBoxGivenName.BorderThickness = new Thickness(0);
            txtBoxGivenName.IsReadOnly = true;
            txtBoxBirthday.BorderThickness = new Thickness(0);
            datePickerBirthday.Visibility = Visibility.Hidden;
            btnSave.Visibility = Visibility.Hidden;
            btnCancel.Visibility = Visibility.Hidden;
            if (!_employeeAccount)
            {
                btnDeleteAccount.Visibility = Visibility.Hidden;
                btnDeleteAccount.IsEnabled = false;
            }
            btnEditProfile.Visibility = Visibility.Visible;
            btnChangePassword.Visibility = Visibility.Hidden;
            txtBoxPassword.BorderThickness = new Thickness(0);
            btnSave.IsEnabled = false;
            btnCancel.IsEnabled = false;
            this.Title = "View Profile";
        }
        private void TxtBoxGivenName_TextChanged(object sender, TextChangedEventArgs e)
        {
            lblValidationGivenName.Visibility = Visibility.Hidden;
        }

        private void TxtBoxFamilyName_TextChanged(object sender, TextChangedEventArgs e)
        {
            lblValidationFamilyName.Visibility = Visibility.Hidden;
        }

        private void datePickerBirthday_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (datePickerBirthday.SelectedDate != null)
            {
                _changedBirthday = datePickerBirthday.SelectedDate.Value;
                txtBoxBirthday.Text = datePickerBirthday.SelectedDate.Value.ToString("MMMM d, yyyy");
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (validateInputs())
            {
                Account _updatedAccount = new Account();
                _updatedAccount.GivenName = txtBoxGivenName.Text;
                _updatedAccount.FamilyName = txtBoxFamilyName.Text;
                _updatedAccount.Birthday = _changedBirthday.GetValueOrDefault(_account.Birthday);
                try
                {
                    if (_employeeAccount)
                    {
                        _employeeManager.ChangeEmployeeInformation(_account, _updatedAccount);
                    }
                    else
                    {
                        _userManager.ChangeUserInformation(_account, _updatedAccount);
                    }
                    ExitEditMode();
                    UpdateAccountInformation();
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
                lblValidationFamilyName.Content = "Family name cannont be longer than 64 characters!";
                lblValidationFamilyName.Visibility = Visibility.Visible;
            }
            return success;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            ExitEditMode();
            UpdateAccountInformation();
        }

        private void btnChangePassword_Click(object sender, RoutedEventArgs e)
        {
            ChangePasswordWPF changePasswordWPF = null;
            if (_employeeAccount)
            {
                changePasswordWPF = new ChangePasswordWPF(_employeeManager, _account);
            }
            else
            {
                changePasswordWPF = new ChangePasswordWPF(_userManager, _account);
            }
            changePasswordWPF.Owner = this;
            changePasswordWPF.ShowDialog();
        }

        private void btnDeleteAccount_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Delete your account?" + "\n\n" + "All data will be lost","Delete Account?",MessageBoxButton.YesNo,MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                ConfirmPasswordPrompt prompt = new ConfirmPasswordPrompt()
                {
                    PromptText = "Enter your password to confirm!"
                };
                prompt.Title = "Confirm";
                prompt.Owner = this;
                prompt.ShowDialog();
                if (prompt.DialogResult == true)
                {
                    try
                    {
                        _userManager.DeleteAccount(_account,prompt.Password);
                        MessageBox.Show("Account Deleted!");
                        this.DialogResult = true;
                        this.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message + "\n\n" + ex.InnerException.Message);
                    }
                }
            }
        }
    }
}
