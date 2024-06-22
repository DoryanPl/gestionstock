

namespace Gestionnaire.Client
{
    public class UnClient
    {
        private int id_client;
        private string nom_client;
        private string prenom_client;
        private string adresse_client;
        private string code_postale_client;
        private string ville_client;
        private string telephone_client;
        private string email_client;

        public int Id_client { get => id_client; set => id_client = value; }
        public string Nom_client { get => nom_client; set => nom_client = value; }
        public string Prenom_client { get => prenom_client; set => prenom_client = value; }
        public string Adresse_client { get => adresse_client; set => adresse_client = value; }
        public string Code_postale_client { get => code_postale_client; set => code_postale_client = value; }
        public string Ville_client { get => ville_client; set => ville_client = value; }
        public string Telephone_client { get => telephone_client; set => telephone_client = value; }
        public string Email_client { get => email_client; set => email_client = value; }

        public UnClient(int id_client, string nom_client, string prenom_client, string adresse_client, string code_postale_client, string ville_client, string telephone_client, string email_client)
        {
            this.Id_client = id_client;
            this.Nom_client = nom_client;
            this.Prenom_client = prenom_client;
            this.Adresse_client = adresse_client;
            this.Code_postale_client = code_postale_client;
            this.Ville_client = ville_client;
            this.Telephone_client = telephone_client;
            this.Email_client = email_client;
        }
    }
}
