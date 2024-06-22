using System;
using System.Windows;
using System.Windows.Media;


namespace Gestionnaire.Interface
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

        private BDD.GestionStockBDD BDD = new BDD.GestionStockBDD();
      

        private void btnMateriel_Click(object sender, RoutedEventArgs e)
        {
            Materiels.Materiel materiel = new Materiels.Materiel();
            materiel.ReloadGrid();
            PanelAfficher.Children.Clear();
            if (!PanelAfficher.Children.Contains(materiel))
            {
                PanelAfficher.Children.Add(materiel);
                materiel.BringIntoView();
            }
            else
            {
                materiel.BringIntoView();
            }
        }

        private void btnSetting_Click(object sender, RoutedEventArgs e)
        {
            Settings settings = new Settings();
            PanelAfficher.Children.Clear();
            if (!PanelAfficher.Children.Contains(settings))
            {
                PanelAfficher.Children.Add(settings);
                settings.BringIntoView();
            }
            else
            {
                settings.BringIntoView();
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
           start();
        }


        private void btnUserSettings_Click(object sender, RoutedEventArgs e)
        {
            LoginUser.LoginUser loginUser = new LoginUser.LoginUser();
            loginUser.ShowDialog();

        }

        public void start()
        {
            Accueil accueil = new Accueil();
            PanelAfficher.Children.Clear();
            if (!PanelAfficher.Children.Contains(accueil))
            {
                PanelAfficher.Children.Add(accueil);
                accueil.BringIntoView();
            }
            else
            {
                accueil.BringIntoView();
            }

            Properties.Settings.Default.User = "DEFAULT_VALUE";
            Properties.Settings.Default.Permission = "DEFAULT_VALUE";


            string[] args = Environment.GetCommandLineArgs();

            if (args.Length > 1)
            {
                Properties.Settings.Default.IP = args[1];
                Properties.Settings.Default.Database = args[2];
                Properties.Settings.Default.BDDUsername = args[3];
                Properties.Settings.Default.BDDPassword = args[4];
                Properties.Settings.Default.Permission = args[5];
                Properties.Settings.Default.User = args[6];
            }


            Properties.Settings.Default.UrlQrCode = "http://projet.2brou.fr/materiel/";
            Properties.Settings.Default.Save();

            LoginUser.LoginUser loginUser = new LoginUser.LoginUser();

            btnClient.IsEnabled = false;
            btnEmploye.IsEnabled = false;
            btnMateriel.IsEnabled = false;

            if (BDD.IsServerConnected() == true)
            {

                if (Properties.Settings.Default.User == "DEFAULT_VALUE")
                {
                    txtConnected.Text = "\n Déconnecté";
                    Led.Fill = Brushes.Red;

                  

                    loginUser.ShowDialog();

                }
                else 
                { 
                   loginUser.VerifPermission();
                }
            }
            else
            {
                btnSetting_Click(this, new RoutedEventArgs());
            }
        }


        private void btnDisconnect_Click(object sender, RoutedEventArgs e)
        {
            if (Properties.Settings.Default.User != "DEFAULT_VALUE")
            {
                txtConnected.Text = "\n Déconnecté";
                Led.Fill = Brushes.Red;

                btnClient.IsEnabled = false;
                btnEmploye.IsEnabled = false;
                btnMateriel.IsEnabled = false;

                Properties.Settings.Default.Permission = "DEFAULT_VALUE";
                Properties.Settings.Default.User = "DEFAULT_VALUE";
                Properties.Settings.Default.Save();


                if (BDD.IsServerConnected() == true)
                {
                    LoginUser.LoginUser loginUser = new LoginUser.LoginUser();
                    loginUser.ShowDialog();
                }
                else
                {
                    btnSetting_Click(this, new RoutedEventArgs());
                }
            }
        }

        private void btnEmploye_Click(object sender, RoutedEventArgs e)
        {
            Employe.Employe employe = new Employe.Employe();
            employe.ReloadGrid();
            PanelAfficher.Children.Clear();
            if (!PanelAfficher.Children.Contains(employe))
            {
                PanelAfficher.Children.Add(employe);
                employe.BringIntoView();
            }
            else
            {
                employe.BringIntoView();
            }
        }

        private void btnClient_Click(object sender, RoutedEventArgs e)
        {
            Client.Client client = new Client.Client();
            client.ReloadGrid();
            PanelAfficher.Children.Clear();
            if (!PanelAfficher.Children.Contains(client))
            {
                PanelAfficher.Children.Add(client);
                client.BringIntoView();
            }
            else
            {
                client.BringIntoView();
            }
        }

        
    }
}
