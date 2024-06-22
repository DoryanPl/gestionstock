using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;



namespace Gestionnaire.Materiels.AjouterMateriel
{
    public class UnMateriel : INotifyPropertyChanged
    {
        private string categorie;
        private string nom;
        private string prix;
        private bool livrer;
        private int flashcode;

        public string Categorie { get => categorie; set { categorie = value; NotifyPropertyChanged("categorie"); } }
        public string Nom { get => nom; set { nom = value; NotifyPropertyChanged("Nom"); } }
        public string Prix { get => prix; set { prix = value; NotifyPropertyChanged("Prix"); } }
    public bool Livrer { get => livrer; set  {livrer = value; NotifyPropertyChanged("Livrer"); } }
        public int Flashcode { get => flashcode; set {flashcode = value; NotifyPropertyChanged("Flashcode"); } }

        public UnMateriel(string nom, string prix, int flashcode,  string categorie, bool livrer)
        {
            this.categorie = categorie;
            this.nom = nom;
            this.prix = prix;
            this.flashcode = flashcode;
            this.livrer = livrer;
        }

        public override string ToString()
        {
            try
            {
                //return "Numero: " + numero + " Nom: " + nom + " Poids: " + poids + " Couleur: " + couleur;
                return categorie + "\t" + nom + "\t" + prix + "\t" + flashcode + "\t" + livrer; 

            }
            catch (Exception ex)
            {
                throw new Exception("erreur ToString", ex);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(string propertyName)
        {
            try
            {
                if (this.PropertyChanged != null)
                {
                    this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erreur Notify", ex);
            }
        }


    }
}
