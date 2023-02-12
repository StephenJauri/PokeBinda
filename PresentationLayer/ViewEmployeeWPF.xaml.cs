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
    /// Interaction logic for ViewEmployeeWPF.xaml
    /// </summary>
    public partial class ViewEmployeeWPF : Window
    {
        private Employee _account = null;
        private bool _newAccount;
        private IEmployeeManager _employeeManager = null;
        private static readonly BitmapImage _deleteAccount = new BitmapImage(new Uri("/images/icons/trashicon.png", UriKind.Relative));
        private static readonly BitmapImage _enableAccount = new BitmapImage(new Uri("/images/icons/plusicon.png", UriKind.Relative));
        public ViewEmployeeWPF(Employee account, IEmployeeManager employeeManager)
        {
            _newAccount = false;
            _account = account;
            _employeeManager = employeeManager;
            InitializeComponent();
            UpdateAccountInformation();
            AddEvents();
            btnSave.IsDefault = true;
            btnCancel.IsCancel = true;
        }
        public ViewEmployeeWPF(IEmployeeManager employeeManager)
        {
            InitializeComponent();
            _newAccount = true;
            _account = new Employee()
            {
                Active = true,
                Birthday = DateTime.Now,
                CreationDate = DateTime.Now,
                Email = "emailaddress@company.com",
                FamilyName = "Family Name",
                GivenName = "Given Name",
                Roles = new List<string>()
            };
            btnDeleteAccount.Visibility = Visibility.Hidden;
            btnChangePassword.Visibility = Visibility.Hidden;
            pbPassword.IsEnabled = true;
            _employeeManager = employeeManager;
            UpdateAccountInformation();
            AddEvents();
            btnSave.IsDefault = true;
            btnCancel.IsCancel = true;
        }

        private void AddEvents()
        {
            txtBoxFamilyName.TextChanged += TxtBoxFamilyName_TextChanged;
            txtBoxGivenName.TextChanged += TxtBoxGivenName_TextChanged;
            txtBoxEmail.TextChanged += TxtBoxEmail_TextChanged;
            pbPassword.PasswordChanged += PassBoxPassword_PasswordChanged;
        }
        private void UpdateAccountInformation()
        {
            if (_newAccount)
            {
                txtBlockWelcome.Text = "Create Account";
            }
            else
            {
                txtBlockWelcome.Text = "Modify Account";
            }
            txtBoxBirthday.Text = _account.Birthday.ToString("MMMM d, yyyy");
            txtBoxEmail.Text = _account.Email;
            txtBoxGivenName.Text = _account.GivenName;
            txtBoxFamilyName.Text = _account.FamilyName;
            datePickerBirthday.SelectedDate = _account.Birthday;
            imgDeleteAccount.Source = _account.Active ? _deleteAccount : _enableAccount;
            txtActive.Text = _account.Active.ToString();
            UpdateRoleInfo();
        }
        private void UpdateRoleInfo()
        {

            grdRoles.Children.Clear();
            grdRoles.RowDefinitions.Clear();
            _account.Roles.ForEach(AddRole);
        }
        private void AddRole(string role)
        {
            grdRoles.RowDefinitions.Add(new RowDefinition());
            TextWithRemoveUC roleUC = new TextWithRemoveUC();
            roleUC.txtBlockItem.Text = role;
            roleUC.btnRemoveItem.Click += (obj, args) => RemoveRole(obj, args, role);
            Grid.SetRow(roleUC, grdRoles.RowDefinitions.Count - 1);
            grdRoles.Children.Add(roleUC);
        }
        private void RemoveRole(object sender, RoutedEventArgs args, string role)
        {
            _account.Roles.Remove(role);
            UpdateRoleInfo();
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
                _account.Birthday = datePickerBirthday.SelectedDate.Value;
                txtBoxBirthday.Text = datePickerBirthday.SelectedDate.Value.ToString("MMMM d, yyyy");
            }
        }
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (validateInputs())
            {
                _account.GivenName = txtBoxGivenName.Text;
                _account.FamilyName = txtBoxFamilyName.Text;
                _account.Email = txtBoxEmail.Text;
                try
                {
                    if (_newAccount)
                    {
                       _employeeManager.CreateEmployee(_account,pbPassword.Password);
                    }
                    else
                    {
                       _employeeManager.ChangeEmployeeAdmin(_account);
                    }
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
            if (_newAccount && pbPassword.Password.Length < 8)
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
            return success;
        }
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
        private void btnDeleteAccount_Click(object sender, RoutedEventArgs e)
        {
            if (_account.Active)
            {
                if (MessageBox.Show("Disable this account?", "Disable Account?", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    try
                    {
                        _account.Active = false;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message + "\n\n" + ex.InnerException.Message);
                    }
                }
            }
            else
            {
                if (MessageBox.Show("Enable this account?", "Enable Account?", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    try
                    {
                        _account.Active = true;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message + "\n\n" + ex.InnerException.Message);
                    }
                }
            }
            imgDeleteAccount.Source = _account.Active ? _deleteAccount : _enableAccount;
            txtActive.Text = _account.Active.ToString();
        }
        private void btnChangePassword_Click(object sender, RoutedEventArgs e)
        {
            ConfirmPasswordPrompt newPassword = new ConfirmPasswordPrompt();
            newPassword.Owner = this;
            newPassword.PromptText = "Enter new password";
            newPassword.Title = "Enter Password";
            newPassword.ShowDialog();
            if (newPassword.Password.Length < 8)
            {
                MessageBox.Show("Password must be at least 8 characters long!");
                return;
            }
            if (newPassword.DialogResult == true)
            {
                try
                {
                    _employeeManager.ChangeEmployeePasswordAdmin(_account, newPassword.Password);
                    MessageBox.Show("Password Changed!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex + "\n\n" + ex.InnerException.Message);
                }
            }
        }

        private void btnAddRole_Click(object sender, RoutedEventArgs e)
        {
            SelectRoleWPF selectedRoleWindow = new SelectRoleWPF(_employeeManager);
            selectedRoleWindow.Owner = this;
            selectedRoleWindow.ShowDialog();
            if (selectedRoleWindow.DialogResult == true)
            {
                if (_account.Roles.Contains(selectedRoleWindow.SelectedRole))
                {
                    MessageBox.Show("User is already assigned this role!");
                    return;
                }
                else
                {
                    _account.Roles.Add(selectedRoleWindow.SelectedRole);
                    _account.Roles.Sort();
                    UpdateRoleInfo();
                    return;
                }
            }
        }
    }
}
