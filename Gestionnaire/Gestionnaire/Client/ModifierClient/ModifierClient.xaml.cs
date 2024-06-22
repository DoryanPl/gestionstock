using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Text.RegularExpressions;
using MySql.Data.MySqlClient;



namespace Gestionnaire.Client.ModifierClient
{
    /// <summary>
    /// Logique d'interaction pour ModifierEmploye.xaml
    /// </summary>
    public partial class ModifierClient : Window
    {
        private BDD.GestionStockBDD bdd;
        private int id_client;
        private string Nom;
        private string Prenom;
        private string Adresse;
        private string Code_postale;
        private string Ville;
        private string Email;
        private string Telephone;

        public ModifierClient(int id_client)
        {
            InitializeComponent();
            this.id_client = id_client;
            InsertTextBox();
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            //MessageBox.Show(ModifMateriel.Nom + " " + ModifMateriel.Prix + " "+ Convert.ToString(ModifMateriel.Flashcode) + " " + ModifMateriel.Livrer + " "+ ModifMateriel.Categorie);
        }

        private void btnModifier_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (InitValue() == true)
                {
                    bdd = new BDD.GestionStockBDD();
                    if (bdd.ModifClient(id_client, Nom, Prenom, Adresse, Code_postale, Ville, Telephone, Email) == true)
                    {
                        SuccessDialog success = new SuccessDialog();
                        success.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                        success.ShowDialog();
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

        private void InsertTextBox()
        {
            bdd = new BDD.GestionStockBDD();
            List<UnClient> UnClient = bdd.GetClient(id_client);

            txtNom.Text = UnClient[0].Nom_client.ToString();
            txtPrenom.Text = UnClient[0].Prenom_client.ToString();
            txtEmail.Text = UnClient[0].Email_client.ToString();
            txtTel.Text = UnClient[0].Telephone_client.ToString();
            txtAdresse.Text = UnClient[0].Adresse_client.ToString();
            txtCP.Text = UnClient[0].Code_postale_client.ToString();
            txtVille.Text = UnClient[0].Ville_client.ToString();

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

        private void txtEmail_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            string emailPattern = @"^[^^a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            Regex regex = new Regex(emailPattern);
            if (!regex.IsMatch(txtEmail.Text + e.Text))
            {
                e.Handled = true;
            }
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                btnModifier_Click(sender, e);
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
