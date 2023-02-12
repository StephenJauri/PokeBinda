using System;
using System.Collections.Generic;
using System.Linq;
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
    /// Interaction logic for ViewAbilityWPF.xaml
    /// </summary>
    public partial class ViewAbilityWPF : Window
    {
        private Ability _ability = null;
        private bool _newAbility;
        private IAbilityManager _abilityManager = null;
        public ViewAbilityWPF(Ability ability, IAbilityManager abilityManager)
        {
            _newAbility = false;
            _ability = ability;
            _abilityManager = abilityManager;
            InitializeComponent();
            btnSave.IsDefault = true;
            btnCancel.IsCancel = true;
            tbName.Text = ability.Name;
            tbDescription.Text = ability.Description;
            tbName.Focus();
        }
        public ViewAbilityWPF(IAbilityManager abilityManager)
        {
            _ability = new Ability();
            _newAbility = true;
            _abilityManager = abilityManager;
            InitializeComponent();
            btnSave.IsDefault = true;
            btnCancel.IsCancel = true;
            this.Title = "Create Ability";
            this.lblTitle.Text = "Create Ability";
        }


        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (!ValidateInputs())
            {
                return;
            }    
            try
            {
                this.DialogResult = true;
                _ability.Name = tbName.Text;
                _ability.Description = tbDescription.Text;
                if (_newAbility)
                {
                    _abilityManager.CreateAbility(_ability);
                }
                else
                {
                    _abilityManager.UpdateAbility(_ability);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n\n" + ex.InnerException.Message);
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
        private bool ValidateInputs()
        {
            if (tbName.Text.Length > 32)
            {
                MessageBox.Show("Name cannot be longer than 32 characters");
                return false;
            }
            if (tbName.Text.Length > 512)
            {
                MessageBox.Show("Description cannot be longer than 32 characters");
                return false;
            }
            if (tbName.Text.Trim() == "" && tbDescription.Text.Trim() == "")
            {
                MessageBox.Show("You must provide either a name or a description");
                return false;
            }
            return true;
        }
    }

}
