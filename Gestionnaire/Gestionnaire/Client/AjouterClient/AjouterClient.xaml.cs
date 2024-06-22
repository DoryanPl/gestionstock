using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;
using System.Text.RegularExpressions;
using MySql.Data.MySqlClient;

namespace Gestionnaire.Client.AjouterClient
{
    /// <summary>
    /// Logique d'interaction pour AjouterEmploye.xaml
    /// </summary>
    public partial class AjouterClient : Window
    {
        private BDD.GestionStockBDD bdd;
        private string Nom;
        private string Prenom;
        private string Adresse;
        private string Code_postale;
        private string Ville;
        private string Email;
        private string Telephone;

        public AjouterClient()
        {
            InitializeComponent();
        }

        private void btnAjouter_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (InitValue() == true)
                {
                    bdd = new BDD.GestionStockBDD();

                    if (bdd.AddClient(Nom, Prenom, Adresse, Code_postale, Ville, Telephone, Email) == true)
                    {
                        SuccessDialog success = new SuccessDialog();
                        success.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                        success.ShowDialog();
                        ClearTextBox();
                    }
                    else
                    {
                        FailedDialog failed = new FailedDialog();
                        failed.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                        failed.ShowDialog();
                    }
                }

                else
                {
                    FailedDialog failed = new FailedDialog();
                    failed.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                    failed.ShowDialog();
                }
            }

            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
                FailedDialog failed = new FailedDialog();
                failed.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                failed.ShowDialog();

            }
        }

        private bool InitValue()
        {

            try
            {
                Nom = txtNom.Text;
                Prenom = txtPrenom.Text;
                Adresse = txtAdresse.Text;
                Code_postale = txtCP.Text;
                Ville = txtVille.Text;
                Email = txtEmail.Text;
                Telephone = txtTel.Text;


                if (string.IsNullOrEmpty(Nom) || string.IsNullOrEmpty(Prenom) || string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(Telephone) || string.IsNullOrEmpty(Adresse) || string.IsNullOrEmpty(Code_postale) || string.IsNullOrEmpty(Ville))
                {
                    return false;
                }

                string pattern = @"^0[1-9][0-9]{8}$";
                Regex regex = new Regex(pattern);

                string emailPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
                Regex regex2 = new Regex(emailPattern);

                if (!regex.IsMatch(Telephone) || !regex2.IsMatch(Email))
                {
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                //  MessageBox.Show(ex.Message);
                return false;
            }
        }

        public void ClearTextBox()
        {
            txtNom.Text = string.Empty;
            txtPrenom.Text = string.Empty;
            txtEmail.Text = string.Empty;
            txtTel.Text = string.Empty;
            txtAdresse.Text = string.Empty;
            txtCP.Text = string.Empty;
            txtVille.Text = string.Empty;



        }

        private void txtChiffre_Lettre_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+&&[^a-zA-Z]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void txtChiffre_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");

            if (txtTel != null && txtTel.Text.Length == 0)
            {
                if (e.Text != "0")
                {
                    e.Handled = true;
                }
            }
            else
            {
                e.Handled = regex.IsMatch(e.Text);
            }
        }

        private void txtLettre_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^a-zA-Z]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                btnAjouter_Click(sender, e);
            }
        }

        private void txtTel_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtTel.Text.Length > 10)
            {
                txtTel.Text = txtTel.Text.Substring(0, 10);
                txtTel.CaretIndex = 10; // Positionne le curseur à la fin du texte
            }
        }

    }
}
