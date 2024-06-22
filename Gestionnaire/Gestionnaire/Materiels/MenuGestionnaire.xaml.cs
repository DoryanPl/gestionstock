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

namespace Gestionnaire.Materiels
{
    /// <summary>
    /// Logique d'interaction pour MenuGestionnaire.xaml
    /// </summary>
    public partial class MenuGestionnaire : Window
    {
        public MenuGestionnaire()
        {
            InitializeComponent();
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;

        }

        private void btnMateriel_Click(object sender, RoutedEventArgs e)
        {
            PanelAfficher.Children.Clear();
            if (!PanelAfficher.Children.Contains(Materiel.Instance))
            {
                PanelAfficher.Children.Add(Materiel.Instance);
                // Materiel.Instance.Dock = DockStyle.Fill;
                Materiel.Instance.BringIntoView();
            }
            else
            {
                Materiel.Instance.BringIntoView();
            }
        }

        private void btnSetting_Click(object sender, RoutedEventArgs e)
        {
            Settings settings = new Settings();
            PanelAfficher.Children.Clear();
            if (!PanelAfficher.Children.Contains(settings))
            {
                PanelAfficher.Children.Add(settings);
                // Materiel.Instance.Dock = DockStyle.Fill;
                settings.BringIntoView();
            }
            else
            {
                settings.BringIntoView();
            }
        }
    }
}
