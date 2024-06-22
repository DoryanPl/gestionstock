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
using System.Text.RegularExpressions;
using MySql.Data.MySqlClient;



namespace Gestionnaire.Employe.ModifierEmploye
{
    /// <summary>
    /// Logique d'interaction pour ModifierEmploye.xaml
    /// </summary>
    public partial class ModifierEmploye : Window
    {
        private BDD.GestionStockBDD bdd;
        private string Nom;
        private string Prenom;
        private string Email;
        private string Telephone;
        private string User;
        private bool Permission;
        private int id_employe;

        public ModifierEmploye(int id_employe)
        {
            InitializeComponent();
            this.id_employe = id_employe;
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
                    if (bdd.ModifEmploye(id_employe, Nom, Prenom, Email, Telephone, Permission, User) == true)
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
                Email = txtEmail.Text;
                Telephone = txtTel.Text;
                User = txtUser.Text;
                if (cbPerm.SelectedIndex == 0)
                {
                    Permission = true;
                }
                else { Permission = false; }


                if (string.IsNullOrEmpty(Nom) || string.IsNullOrEmpty(Prenom) || string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(Telephone) || string.IsNullOrEmpty(Permission.ToString()) || string.IsNullOrEmpty(User))
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
            List<UnEmploye> UnEmploye = bdd.GetEmploye(id_employe);

            txtNom.Text = UnEmploye[0].Nom.ToString();
            txtPrenom.Text = UnEmploye[0].Prenom.ToString();
            txtEmail.Text = UnEmploye[0].Email.ToString();
            txtTel.Text = UnEmploye[0].Telephone.ToString();
            txtUser.Text = UnEmploye[0].User.ToString();

            if (UnEmploye[0].Permission == true)
            {
                cbPerm.SelectedIndex = 0;
            }
            else { cbPerm.SelectedIndex = 1; }

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
