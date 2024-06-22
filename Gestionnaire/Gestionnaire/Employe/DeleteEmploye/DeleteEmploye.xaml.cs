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

namespace Gestionnaire.Employe.DeleteEmploye
{
    /// <summary>
    /// Logique d'interaction pour DeleteEmploye.xaml
    /// </summary>
    public partial class DeleteEmploye : Window
    {
        private BDD.GestionStockBDD bdd;
        private int id_employe;

        public DeleteEmploye(int id_employe)
        {
            this.id_employe = id_employe;
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            InitializeComponent();
        }

        private void btnOui_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bdd = new BDD.GestionStockBDD();

                if (bdd.DeleteEmploye(id_employe) == true)
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
