using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using FolderBrowserDialog = System.Windows.Forms.FolderBrowserDialog;
using DialogResult = System.Windows.Forms.DialogResult;


namespace Gestionnaire.Interface
{
    /// <summary>
    /// Logique d'interaction pour Settings.xaml
    /// </summary>
    public partial class Settings : UserControl
    {
        //private byte [] salt = BDD.EncryptPassword.CreateSalt();
        //private byte[] salt;

        public Settings()
        {
            InitializeComponent();

            Settings_txtIp.Text = Properties.Settings.Default.IP;
            Settings_txtDatabase.Text = Properties.Settings.Default.Database;
            Settings_txtUser.Text = Properties.Settings.Default.BDDUsername;
            Settings_txtPassword.Password = Properties.Settings.Default.BDDPassword;
            btnPath.Content = Properties.Settings.Default.Path;
        }
        private BDD.GestionStockBDD connectBDD = new BDD.GestionStockBDD();


        private void btnConnexion_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Properties.Settings.Default.IP = Settings_txtIp.Text;
                Properties.Settings.Default.Database = Settings_txtDatabase.Text;
                Properties.Settings.Default.BDDUsername = Settings_txtUser.Text;
                Properties.Settings.Default.BDDPassword = Settings_txtPassword.Password;
                Properties.Settings.Default.Save();

                Etat_connection();
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnAnnuler_Click(object sender, RoutedEventArgs e)
        {
            Settings_txtIp.Text = Properties.Settings.Default.IP;
            Settings_txtDatabase.Text = Properties.Settings.Default.Database;
            Settings_txtUser.Text = Properties.Settings.Default.BDDUsername;
            Settings_txtPassword.Password = Properties.Settings.Default.BDDPassword;
        }

        public void Etat_connection() //Connexion à la BDD
        {
            try
            {
                if (connectBDD.IsServerConnected() == true)
                {
                    BDD.SuccessDialog success = new BDD.SuccessDialog();
                    success.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                    success.ShowDialog();

                    if( Properties.Settings.Default.User == "DEFAULT_VALUE")
                    {
                        Interface.MenuGestionnaire menu = Application.Current.Windows.OfType<Interface.MenuGestionnaire>().FirstOrDefault();
                        menu.start();
                    }
                }
                else
                {
                    BDD.FailedDialog failed = new BDD.FailedDialog();
                    failed.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                    failed.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void UserControl_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                btnConnexion_Click(sender, e);
            }
        }

        private void btnChangePwd_Click(object sender, RoutedEventArgs e)
        {
            
            try
            {
                string OldPwd = Settings_txtOldPwd.Password;
                string NewPwd = Settings_txtNewPwd.Password;

                if (connectBDD.ChangePwd(OldPwd, NewPwd))
                {
                    BDD.SuccessDialog success = new BDD.SuccessDialog();
                    success.Cable_lblNom.Text = "Changement de mot de passe réussi";
                    success.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                    success.ShowDialog();
                }
                else
                {
                    BDD.FailedDialog failed = new BDD.FailedDialog();
                    failed.lblNom.Text = "Changement de mot de passe échoué";

                    failed.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                    failed.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnPath_Click(object sender, RoutedEventArgs e)
        {
            var folderBrowserDialog = new FolderBrowserDialog();

            DialogResult result = folderBrowserDialog.ShowDialog();

            if(result == DialogResult.OK)
            {
                string Path = folderBrowserDialog.SelectedPath;
                btnPath.Content = Path;

                Properties.Settings.Default.Path = Path;
                Properties.Settings.Default.Save();
            }
        }
    }

}
