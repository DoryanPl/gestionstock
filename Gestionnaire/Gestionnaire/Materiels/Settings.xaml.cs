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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Security.Cryptography;
using System.Configuration;
using System.IO;



namespace Gestionnaire.Materiels
{
    /// <summary>
    /// Logique d'interaction pour Settings.xaml
    /// </summary>
    public partial class Settings : UserControl
    {

        public Settings()
        {
            InitializeComponent();

            Settings_txtIp.Text = Properties.Settings.Default.IP;
            Settings_txtDatabase.Text = Properties.Settings.Default.Database;
            Settings_txtUser.Text = Properties.Settings.Default.Username;
            Settings_txtPassword.Password = Properties.Settings.Default.Password;
            Properties.Settings.Default.Save();


            /*
            using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
            {
                // Charger la clé privée à partir du fichier
                string privateKeyXml = File.ReadAllText("private.xml");
                rsa.FromXmlString(privateKeyXml);
                

                // Utiliser la clé privée pour déchiffrer les données
                //byte[] decryptedData = rsa.Decrypt(Encoding.UTF8.GetBytes(Settings_txtPassword.Password), false);

            
            }
            */
        }


        private void btnConnexion_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Properties.Settings.Default.IP = Settings_txtIp.Text;
                Properties.Settings.Default.Database = Settings_txtDatabase.Text;
                Properties.Settings.Default.Username = Settings_txtUser.Text;
                Properties.Settings.Default.Password = Settings_txtPassword.Password;


                /*
                EncryptPassword.CreateKey();
                using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
                {
                   // Charger la clé publique à partir du fichier
                string publicKeyXml = File.ReadAllText("public.xml");
                    rsa.FromXmlString(publicKeyXml);

                    // Utiliser la clé publique pour chiffrer les données
                    byte[] encryptedData = rsa.Encrypt(Encoding.UTF8.GetBytes(Settings_txtPassword.Password), true);
                    Properties.Settings.Default.Password = Convert.ToBase64String(encryptedData);

                }

                */
                Properties.Settings.Default.Save();

                Etat_connection();

            }

            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }



        }

        private void btnAnnuler_Click(object sender, RoutedEventArgs e)
        {
            Settings_txtIp.Text = Properties.Settings.Default.IP;
            Settings_txtDatabase.Text = Properties.Settings.Default.Database;
            Settings_txtUser.Text = Properties.Settings.Default.Username;
            Settings_txtPassword.Password = Properties.Settings.Default.Password;
        }

        public void Etat_connection() //Connexion à la BDD
        {
            try
            {
                BDD.GestionStockBDD testco = new BDD.GestionStockBDD();
                if (testco.IsServerConnected() == true)
                {
                    BDD.SuccessDialog success = new BDD.SuccessDialog();
                    success.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                    success.ShowDialog();
                }
                else
                {
                    BDD.FailedDialog failed = new BDD.FailedDialog();
                    failed.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                    failed.ShowDialog();
                }
            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message); 
            }
        }



    }

}
