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
    public partial class SelectRoleWPF : Window
    {
        public string SelectedRole { get; set; }
        private IEmployeeManager _employeeManager = null;
        public SelectRoleWPF(IEmployeeManager employeeManager)
        {
            InitializeComponent();
            btnSelect.Visibility = Visibility.Hidden;
            _employeeManager = employeeManager;
            TypeGrid.ItemsSource = _employeeManager.GetAllRoles();
            btnSelect.IsDefault = true;
            btnCancel.IsCancel = true;
        }

        private void TypeGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            btnSelect_Click(sender, e);
        }

        private void TypeGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (TypeGrid.SelectedItem != null)
            {
                btnSelect.Visibility = Visibility.Visible;
            }
            else
            {
                btnSelect.Visibility = Visibility.Hidden;
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        private void btnSelect_Click(object sender, RoutedEventArgs e)
        {
            if ((SelectedRole = (string)TypeGrid.SelectedItem) != null)
            {
                this.DialogResult = true;
                this.Close();
            }
        }
    }
}
