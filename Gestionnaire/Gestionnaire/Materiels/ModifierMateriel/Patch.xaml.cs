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
using System.Text.RegularExpressions;
using MySql.Data.MySqlClient;

namespace Gestionnaire.Materiels.ModifierMateriel
{
    /// <summary>
    /// Logique d'interaction pour Patch.xaml
    /// </summary>
    public partial class Patch : UserControl
    {
        private Materiels.UnMateriel UnMateriel;

        public Patch(Materiels.UnMateriel UnMateriel)
        {
            InitializeComponent();
            this.UnMateriel = UnMateriel;
            InsertTextBox(UnMateriel);
        }

        private BDD.GestionStockBDD bdd;
        private string Nom;
        private string Prix;
        private int Flashcode;
        private int _IN;
        private int _OUT;
        private string DANTE;
        private bool Livrer;
        private int idpatch;


        private void btnPatch_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (InitValue() == true)
                {
                    bdd = new BDD.GestionStockBDD();
                    if (bdd.ModifPatch(Nom, Prix, Flashcode, _IN, _OUT, DANTE, Livrer, idpatch) == true)
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
                Nom = Patch_txtNom.Text;
                Prix = Patch_txtPrix.Text;
                Flashcode = int.Parse(Patch_txtFlashCode.Text);
                _IN = int.Parse(Patch_txtIN.Text);
                _OUT = int.Parse(Patch_txtOUT.Text);
                DANTE = Patch_txtDANTE.Text; ;

                string input = comboLivrer.SelectedItem.ToString();  //"System.Windows.Controls.ComboBoxItem: Cable"
                string Selectioned = input.Replace("System.Windows.Controls.ComboBoxItem: ", ""); //"Oui ou Non"
                if (Selectioned == "Non") { Livrer = false; }
                else { Livrer = true; }


                if (string.IsNullOrEmpty(Nom) || string.IsNullOrEmpty(Prix) || string.IsNullOrEmpty(Flashcode.ToString()) || string.IsNullOrEmpty(_IN.ToString()) || string.IsNullOrEmpty(_OUT.ToString()) || string.IsNullOrEmpty(DANTE) || string.IsNullOrEmpty(Livrer.ToString()))
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

        private void InsertTextBox(Materiels.UnMateriel ModifMateriel)
        {
            bdd = new BDD.GestionStockBDD();
            List<Materiels.Patch> UnPatch = bdd.GetPatch(ModifMateriel.Nom, ModifMateriel.Prix, ModifMateriel.Flashcode, ModifMateriel.Livrer);

            idpatch = Convert.ToInt32(UnPatch[0].Idpatch);
            Patch_txtNom.Text = UnPatch[0].Nom.ToString();
            Patch_txtPrix.Text = UnPatch[0].Prix.ToString();
            Patch_txtFlashCode.Text = UnPatch[0].Flashcode.ToString();
            Patch_txtIN.Text = UnPatch[0].In.ToString();
            Patch_txtOUT.Text = UnPatch[0].Out.ToString();
            Patch_txtDANTE.Text = UnPatch[0].Dante.ToString();

            string Selectioned = UnPatch[0].Livrer.ToString();
            if (Selectioned == "False") { comboLivrer.SelectedIndex = 0; }
            else { comboLivrer.SelectedIndex = 1; }


        }

        private void Patch_txtNom_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+&&[^a-zA-Z]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void Patch_txtPrix_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void Patch_txtFlashCode_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void Patch_txtIN_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void Patch_txtOUT_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void Patch_txtDANTE_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^a-zA-Z]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
