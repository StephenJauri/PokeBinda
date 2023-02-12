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

namespace PresentationLayer
{
    /// <summary>
    /// Interaction logic for SelectTagWPF.xaml
    /// </summary>
    public partial class SelectTagWPF : Window
    {
        public string SelectedTag { get; set; }
        private ILookupManager _lookupManager = null;
        public SelectTagWPF(ILookupManager lookupManager)
        {
            InitializeComponent();
            btnSelect.Visibility = Visibility.Hidden;
            _lookupManager = lookupManager;
            TypeGrid.ItemsSource = _lookupManager.GetAllTags();
            btnSelect.IsDefault = true;
            btnCancel.IsCancel = true;
        }

        private void TagGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            btnSelect_Click(sender, e);
        }

        private void TagGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
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
            if ((SelectedTag = (string)TypeGrid.SelectedItem) != null)
            {
                this.DialogResult = true;
                SelectedTag = (string)TypeGrid.SelectedItem;
                this.Close();
            }
        }
    }
}
