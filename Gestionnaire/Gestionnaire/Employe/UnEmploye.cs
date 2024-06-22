
namespace Gestionnaire.Employe
{
    class UnEmploye
    {
        private int id_employe;
        private string nom;
        private string prenom;
        private string email;
        private string telephone;
        private bool permission;
        private string user;
        private string password;

        public string Nom { get => nom; set => nom = value; }
        public string Prenom { get => prenom; set => prenom = value; }
        public string Email { get => email; set => email = value; }
        public string Telephone { get => telephone; set => telephone = value; }
        public bool Permission { get => permission; set => permission = value; }
        public string User { get => user; set => user = value; }
        public string Password { get => password; set => password = value; }
        public int Id_employe { get => id_employe; set => id_employe = value; }

        public UnEmploye(int id_employe, string nom, string prenom, string email, string telephone, bool permission, string user, string password)
        {
            this.Id_employe = id_employe;
            this.Nom = nom;
            this.Prenom = prenom;
            this.Email = email;
            this.Telephone = telephone;
            this.Permission = permission;
            this.User = user;
            this.Password = password;
        }
    }
}
