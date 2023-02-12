﻿using System;
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

namespace PresentationLayer
{
    /// <summary>
    /// Interaction logic for ConfirmPasswordPrompt.xaml
    /// </summary>
    public partial class ConfirmPasswordPrompt : Window
    {
        public ConfirmPasswordPrompt()
        {
            InitializeComponent();
        }


        public string PromptText
        {
            get
            {
                return this.tbkText.Text;
            }
            set
            {
                this.tbkText.Text = value;
            }
        }

        public string Password
        {
            get
            {
                return tbxInput.Password;
            }
            set
            {
                tbxInput.Password = value;
            }
        }

        private void OK_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            this.Close();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            btnOK.IsDefault = true;
            btnCancel.IsCancel = true;
            tbxInput.Focus();
            tbxInput.SelectAll();
        }
    }
}
