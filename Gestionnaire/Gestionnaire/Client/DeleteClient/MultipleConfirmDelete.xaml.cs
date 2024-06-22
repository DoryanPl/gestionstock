using System;
using System.Collections.Generic;
using System.Windows;

namespace Gestionnaire.Client.DeleteClient
{
    /// <summary>
    /// Logique d'interaction pour ConfirmDelete.xaml
    /// </summary>
    public partial class MultipleConfirmDelete : Window
    {
        private BDD.GestionStockBDD bdd;
        private List<int> id_client = new List<int>();

        public MultipleConfirmDelete(List<int> id_client)
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
                foreach (int id in id_client) {

                    if (bdd.DeleteClient(id) == false)
                    {
                        FailedDialog failed = new FailedDialog();
                        this.Close();
                    }
                }
                SuccessDialog success = new SuccessDialog();
                success.ShowDialog();
                this.Close();
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
