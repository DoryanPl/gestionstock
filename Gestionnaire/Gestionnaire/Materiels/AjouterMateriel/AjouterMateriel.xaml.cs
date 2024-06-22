using System;
using System.Windows;
using System.Windows.Input;
using System.Text.RegularExpressions;
using MySql.Data.MySqlClient;

namespace Gestionnaire.Materiels.AjouterMateriel
{
    /// <summary>
    /// Logique d'interaction pour AjouterMateriel.xaml
    /// </summary>
    public partial class AjouterMateriel : Window
    {
        private BDD.GestionStockBDD bdd;
        private string Reference;
        private string Categorie;
        private string Caracteristique;
        private double Prix_HT;
        private int Quantite;

        public AjouterMateriel()
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

                    if (bdd.AddMateriel(Reference, Caracteristique, Categorie, Prix_HT, "", Quantite)== true)
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
                Reference = txtReference.Text;
                Categorie = txtCategorie.Text;
                Caracteristique = txtCaracteristique.Text;
                Prix_HT = Convert.ToDouble(txtPrixHT.Text);
                Quantite = Convert.ToInt32(txtQuantite.Text);


                if (string.IsNullOrEmpty(Reference) || string.IsNullOrEmpty(Categorie) || string.IsNullOrEmpty(Prix_HT.ToString()) || string.IsNullOrEmpty(Caracteristique) || string.IsNullOrEmpty(Quantite.ToString()))
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
            txtReference.Text = string.Empty;
            txtCategorie.Text = string.Empty;
            txtCaracteristique.Text = string.Empty;
            txtPrixHT.Text = string.Empty;
            txtQuantite.Text = string.Empty;

        }

        private void txtChiffre_Lettre_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+&&[^a-zA-Z]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void txtChiffre_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
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

    }

}

