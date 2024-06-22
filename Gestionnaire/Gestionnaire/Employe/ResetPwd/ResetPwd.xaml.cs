using System;
using System.Windows;


namespace Gestionnaire.Employe.ResetPwd
{
    /// <summary>
    /// Logique d'interaction pour DeleteEmploye.xaml
    /// </summary>
    public partial class ResetPwd : Window
    {
        private int id_employe;

        public ResetPwd(int id_employe)
        {
            this.id_employe = id_employe;
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            InitializeComponent();
        }

        private void btnOui_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                BDD.GestionStockBDD bdd = new BDD.GestionStockBDD();
               if( bdd.ResetPwd(id_employe))
                {
                    SuccessDialog sucess = new SuccessDialog();
                    sucess.ShowDialog();

                    this.Close();
                }
                else
                {
                    FailedDialog fail = new FailedDialog();
                    fail.ShowDialog();
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
