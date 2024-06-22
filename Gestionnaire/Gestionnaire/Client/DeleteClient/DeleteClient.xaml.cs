using System;
using System.Windows;


namespace Gestionnaire.Client.DeleteClient
{
    /// <summary>
    /// Logique d'interaction pour DeleteEmploye.xaml
    /// </summary>
    public partial class DeleteClient : Window
    {
        private BDD.GestionStockBDD bdd;
        private int id_client;

        public DeleteClient(int id_client)
        {
            this.id_client = id_client;
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            InitializeComponent();
        }

        private void btnOui_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bdd = new BDD.GestionStockBDD();

                if (bdd.DeleteClient(id_client) == true)
                {
                    SuccessDialog success = new SuccessDialog();
                    success.ShowDialog();
                    this.Close();

                }
                else
                {
                    FailedDialog failed = new FailedDialog();
                    this.Close();
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnNon_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
