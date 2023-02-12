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
    /// Interaction logic for ChangePasswordWPF.xaml
    /// </summary>
    public partial class ChangePasswordWPF : Window
    {
        private IUserManager _userManager = null;
        private IEmployeeManager _employeeManager = null;
        private Account _user = null;
        private bool _employeeMode;
        public ChangePasswordWPF(IUserManager userManager, Account user)
        {
            _userManager = userManager;
            _user = user;
            _employeeMode = false;
            InitializeComponent();
            pbOldPassword.Focus();
            btnSave.IsDefault = true;
            btnCancel.IsCancel = true;
        }

        public ChangePasswordWPF(IEmployeeManager employeeManager, Account user)
        {
            _employeeManager = employeeManager;
            _user = user;
            _employeeMode = true;
            InitializeComponent();
            pbOldPassword.Focus();
            btnSave.IsDefault = true;
            btnCancel.IsCancel = true;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (ValidatePassword())
            {
                if (!_employeeMode)
                {
                    try
                    {
                        _userManager.ResetPassword(_user, pbOldPassword.Password, pbNewPassword.Password);
                        MessageBox.Show("Password Changed!");
                        this.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message + "\n\n" + ex.InnerException.Message);
                        pbOldPassword.Password = string.Empty;
                        pbOldPassword.Focus();
                    }
                }
                else
                {
                    try
                    {
                        _employeeManager.ResetPassword(_user, pbOldPassword.Password, pbNewPassword.Password);
                        MessageBox.Show("Password Changed!");
                        this.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message + "\n\n" + ex.InnerException.Message);
                        pbOldPassword.Password = string.Empty;
                        pbOldPassword.Focus();
                    }
                }
            }

        }

        private bool ValidatePassword()
        {
            if (pbNewPassword.Password != pbConfirmPassword.Password)
            {
                MessageBox.Show("Your passwords do not match");
                return false;
            }
            if (pbNewPassword.Password.Length < 8)
            {
                MessageBox.Show("Your password must be at least 8 characters long");
                return false;
            }
            return true;
        }
    }
}
