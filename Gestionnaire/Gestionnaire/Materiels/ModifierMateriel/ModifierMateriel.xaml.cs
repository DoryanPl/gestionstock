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

namespace Gestionnaire.Materiels.ModifierMateriel
{
    /// <summary>
    /// Logique d'interaction pour ModifierMateriel.xaml
    /// </summary>
    public partial class ModifierMateriel : Window
    {
        private BDD.GestionStockBDD bdd;
        private string Reference;
        private string Categorie;
        private string Caracteristique;
        private double Prix_HT;
        private int id_materiel;

        public ModifierMateriel(int id_materiel)
        {
            InitializeComponent();
            this.id_materiel = id_materiel;
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
                    if (bdd.ModifMateriel(id_materiel, Reference, Caracteristique, Categorie, Prix_HT) == true)
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
                Reference = txtReference.Text;
                Categorie = txtCategorie.Text;
                Caracteristique = txtCaracteristique.Text;
                Prix_HT = Convert.ToDouble(txtPrixHT.Text);

                if (string.IsNullOrEmpty(Reference) || string.IsNullOrEmpty(Categorie) || string.IsNullOrEmpty(Prix_HT.ToString()) || string.IsNullOrEmpty(Caracteristique))
                {
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        private void InsertTextBox()
        {
            bdd = new BDD.GestionStockBDD();
            UnMateriel unMateriel = bdd.GetMateriel(id_materiel);

            txtReference.Text = unMateriel.Reference.ToString();
            txtCategorie.Text = unMateriel.Categorie.ToString();
            txtCaracteristique.Text = unMateriel.Caracteristique.ToString();
            txtPrixHT.Text = unMateriel.Prix_ht.ToString();
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
                btnModifier_Click(sender, e);
            }
        }
    }
}
