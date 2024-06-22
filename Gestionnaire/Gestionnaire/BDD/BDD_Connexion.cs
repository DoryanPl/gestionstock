
using MySql.Data.MySqlClient;


namespace Gestionnaire.BDD
{
    class GestionStockBDD_Connexion
    {

         
        private MySqlConnection connection;
        public MySqlConnection Connection { get => connection; set => connection = value; }

        public GestionStockBDD_Connexion()
        {
     
            MySqlConnectionStringBuilder builder = new MySqlConnectionStringBuilder();
            builder.Server = Properties.Settings.Default.IP;
            builder.Database = Properties.Settings.Default.Database;
            builder.UserID = Properties.Settings.Default.BDDUsername;
            builder.Password = Properties.Settings.Default.BDDPassword;
            string connectionString = builder.ToString();
            Connection = new MySqlConnection(connectionString);
        }


        public bool IsServerConnected()
        {
            using (Connection)
            {
                try
                {
                    Connection.Open();
                    return true;
                }
                catch 
                {
                    return false;
                }
            }
        }


       /*private void connexionBdd()
        {
            try
            {
                Mabdd = new BddPatients();
                if (Properties.Settings.Default.Login == "" || Properties.Settings.Default.Mdp == "")
                {
                    ParametresBdd param = new ParametresBdd();
                    if (param.ShowDialog() == true)
                    {
                        Properties.Settings.Default.NomBdd = param.txtBdd.Text;
                        Properties.Settings.Default.Serveur = param.txtServeur.Text;
                        Properties.Settings.Default.Login = param.txtLogin.Text;
                        Properties.Settings.Default.Mdp = param.PassBox.Password;
                        Properties.Settings.Default.MdpAdmin = param.PsswAdminBdd.Password;
                        Properties.Settings.Default.Admin = param.txtAdminBdd.Text;
                        Properties.Settings.Default.Save();
                        MessageBox.Show("Paramètres de connexion enregistrés");
                    }
                    else
                        MessageBox.Show("Paramètres de connexion non enregistrés");
                }
                droit = Mabdd.VerifLoginMdp(Properties.Settings.Default.Login, Properties.Settings.Default.Mdp);
                if (droit == 0)
                {
                    MenugestionUtilisateurs.IsEnabled = false;
                }
                else
                {
                    MenugestionUtilisateurs.IsEnabled = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }*/
    }
}
