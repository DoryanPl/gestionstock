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
    /// Logique d'interaction pour ConfirmDelete.xaml
    /// </summary>
    public partial class MultipleConfirmDelete : Window
    {
        private BDD.GestionStockBDD bdd;
        private List<int> id_employe = new List<int>();

        public MultipleConfirmDelete(List<int> id_employe)
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
                foreach (int id in id_employe) {

                    if (bdd.DeleteEmploye(id) == false)
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
