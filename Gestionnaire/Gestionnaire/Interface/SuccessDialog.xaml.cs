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

namespace Gestionnaire.Interface
{
    /// <summary>
    /// Logique d'interaction pour SuccessDialog.xaml
    /// </summary>
    public partial class SuccessDialog : Window
    {
        public SuccessDialog()
        {
            InitializeComponent();
        }


        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            this.Close();

        }
    }
}
