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
    /// Logique d'interaction pour DeleteMaterielEmpty.xaml
    /// </summary>
    public partial class DeleteEmployeEmpty : Window
    {
        public DeleteEmployeEmpty()
        {
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            InitializeComponent();
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
