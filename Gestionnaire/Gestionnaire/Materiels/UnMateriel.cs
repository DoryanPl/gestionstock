using System;
using System.Collections.Generic;
using System.Windows;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;


namespace Gestionnaire.Materiels
{
    public class UnMateriel 
    {
        private string categorie;
        private string reference;
        private string caracteristique;
        private double prix_ht;
        private string flashcode;
        private int id_materiel;

        public string Categorie { get => categorie; set => categorie = value; }
        public string Reference { get => reference; set => reference = value; }
        public string Caracteristique { get => caracteristique; set => caracteristique = value; }
        public double Prix_ht { get => prix_ht; set => prix_ht = value; }
        public string Flashcode { get => flashcode; set => flashcode = value; }
        public int Id_materiel { get => id_materiel; set => id_materiel = value; }

        public UnMateriel(int id_materiel, string categorie, string reference, string caracteristique, double prix_ht, string flashcode)
        {
            this.Categorie = categorie;
            this.Reference = reference;
            this.Caracteristique = caracteristique;
            this.Flashcode = flashcode;
            this.Prix_ht = prix_ht;
            this.Id_materiel = id_materiel;

        }

        public UnMateriel()
        {

        }

    }
}
