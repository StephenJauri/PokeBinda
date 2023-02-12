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
    /// Interaction logic for ViewAllEmployeesUC.xaml
    /// </summary>
    public partial class ViewAllEmployeesUC : UserControl, IRefreshUserControl
    {
        private Window _owner = null;
        private IEmployeeManager _employeeManager = null;
        private Employee _viewer = null;
        public ViewAllEmployeesUC(IEmployeeManager employeeManager, Window owner, Employee viewer)
        {
            InitializeComponent();
            _owner = owner;
            _employeeManager = employeeManager;
            _viewer = viewer;
            RefreshDataGrid();
        }

        private void EmployeeGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if ((Employee)EmployeeGrid.SelectedItem != null)
            {
                btnEdit_Click(sender, e);
            }
        }

        private void EmployeeGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if ((Employee)EmployeeGrid.SelectedItem != null)
            {
                btnEdit.Visibility = Visibility.Visible;
            }
            else
            {
                btnEdit.Visibility = Visibility.Hidden;
            }
        }


        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            ViewEmployeeWPF viewEmployee = new ViewEmployeeWPF(_employeeManager);
            viewEmployee.Owner = this._owner;
            viewEmployee.ShowDialog();
            RefreshDataGrid();
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            Employee selectedEmployee = (Employee)EmployeeGrid.SelectedItem;
            if (selectedEmployee.ID == _viewer.ID)
            {
                MessageBox.Show("Cannot edit your own account!");
                return;
            }
            ViewEmployeeWPF viewEmployee = new ViewEmployeeWPF(selectedEmployee,_employeeManager);
            viewEmployee.Owner = this._owner;
            viewEmployee.ShowDialog();
            RefreshDataGrid();
        }
        private void RefreshDataGrid()
        {
            try
            {
                EmployeeGrid.ItemsSource = _employeeManager.GetAllEmployees();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex + "\n\n" + ex.InnerException.Message);
            }
        }

        public void Refresh()
        {
            RefreshDataGrid();
        }
    }
}
