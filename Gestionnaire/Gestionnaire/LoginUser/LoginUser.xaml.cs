
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System;


namespace Gestionnaire.LoginUser
{
    /// <summary>
    /// Logique d'interaction pour LoginUser.xaml
    /// </summary>
    public partial class LoginUser : Window
    {
        public LoginUser()
        {
            InitializeComponent();  
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;

        }
        private BDD.GestionStockBDD connectBDD = new BDD.GestionStockBDD();

        private void btnConnexion_Click(object sender, RoutedEventArgs e)
        {

            if (connectBDD.IsServerConnected() == true)
            {
                if (Verification() == true)
                {
                    if (connectBDD.VerifUser(Settings_txtUser.Text, Settings_txtPassword.Password))
                    {
                        SuccessDialog success = new SuccessDialog();
                        success.ShowDialog();
                        VerifPermission();
                        this.Close();
                    }
                    else
                    {
                        FailedDialog failed = new FailedDialog();
                        failed.ShowDialog();
                    }
                }
            }
            else
            {
                FailedDialog failed = new FailedDialog();
                failed.lblNom.Text = "Définir les paramètres de connexion \n à la base de données";
                failed.ShowDialog();
            }
        }

        public void VerifPermission()
        {
            Interface.MenuGestionnaire menu = Application.Current.Windows.OfType<Interface.MenuGestionnaire>().FirstOrDefault();

            bool enabled = Convert.ToBoolean(Properties.Settings.Default.Permission);

                menu.btnClient.IsEnabled = enabled;
                menu.btnEmploye.IsEnabled = enabled;
                menu.btnMateriel.IsEnabled = enabled;
                menu.txtConnected.Text = "Connecté avec : \n" + Properties.Settings.Default.User;
                menu.Led.Fill = Brushes.Green;

        }


        private bool Verification()
        {
            if(Settings_txtUser.Text == "" || Settings_txtUser.Text == "Username")
            {
                FailedDialog failed = new FailedDialog();
                failed.lblNom.Text = "Insérer un username";
                failed.ShowDialog();
                return false;
            }

            if(Settings_txtPassword.Password == "" || Settings_txtPassword.Password == "Password")
            {
                FailedDialog failed = new FailedDialog();
                failed.lblNom.Text = "Insérer un mot de passe";
                failed.ShowDialog();
                return false;
            }

            return true;

        }


        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                btnConnexion_Click(sender, e);
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            string user = Properties.Settings.Default.User;

            if (user != "DEFAULT_VALUE")
            {
                SuccessDialog succes = new SuccessDialog();
                succes.lblNom.Text = "Vous êtes connectée en tant que : \n " + user.ToUpper();
                succes.ShowDialog();
                this.Close();

            }
        }

        private void Settings_txtUser_GotFocus(object sender, RoutedEventArgs e)
        {
            if (Settings_txtUser.Text == "Username")
            {
                Settings_txtUser.Text = "";
            }
        }

        private void Settings_txtUser_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Settings_txtUser.Text))
            {
                Settings_txtUser.Text = "Username";
            }
        }

        private void Settings_txtPassword_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Settings_txtPassword.Password))
            {
                Settings_txtPassword.Password = "Password";
            }
           
        }

        private void Settings_txtPassword_GotFocus(object sender, RoutedEventArgs e)
        {
            if (Settings_txtPassword.Password == "Password")
            {
                Settings_txtPassword.Password = "";
            }
        }
    }
}
