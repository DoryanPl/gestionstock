using System;
using System.Windows;
using System.Windows.Input;


namespace Gestionnaire.Employe.ResetPwd
{
    /// <summary>
    /// Logique d'interaction pour SuccessDialog.xaml
    /// </summary>
    public partial class SuccessDialog : Window
    {
        public SuccessDialog()
        {
            InitializeComponent();
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;

        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            this.Close();

        }

        private void Window_Closed(object sender, EventArgs e)
        {
            Employe employe = new Employe();
            employe.ReloadGrid();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                btnOk_Click(sender, e);
            }
        }
    }
}
