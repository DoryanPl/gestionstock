using System;
using System.Collections.Generic;
using System.Text;
using MySql.Data.MySqlClient;
using System.Windows;
using System.Security.Cryptography;



namespace Gestionnaire.BDD 
{ 
    class GestionStockBDD : GestionStockBDD_Connexion
    {

        //-------------- MATERIEL ----------------//

        public List<Materiels.UnMateriel> ListMateriels()
        {
            using (Connection)
            {
                try
                {
                    Connection.Open();

                    string sql =
                        "SELECT * FROM materiels ";

                    using (MySqlCommand command = new MySqlCommand(sql, Connection))
                    {
                        List<Materiels.UnMateriel> ListMateriels = new List<Materiels.UnMateriel>();
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int id_materiel = Convert.ToInt32(reader["id_materiel"]);
                                string categorie = Convert.ToString(reader["Categorie"]);
                                string reference = Convert.ToString(reader["Reference"]);
                                string caracteristique = Convert.ToString(reader["Caracteristique"]);
                                double prix_ht = Convert.ToDouble(reader["Prix_ht"]);
                                string  flashcode = Convert.ToString(reader["Qrcode"]);

                                Materiels.UnMateriel UnMateriel2 = new Materiels.UnMateriel(id_materiel, categorie, reference, caracteristique, prix_ht, flashcode); 
                                ListMateriels.Add(UnMateriel2);
                            }
                        }
                        
                        return ListMateriels;
                    }
                }
                catch (MySqlException ex)
                {
                    switch (ex.Number)
                    {
                        case 0:
                            MessageBox.Show("Impossible de se connecter au serveur.");
                            break;
                        case 1045:
                            MessageBox.Show("Nom d'utilisateur ou mot de passe incorrect.");
                            break;
                        default:
                            MessageBox.Show("Erreur de connexion : " + ex.Number + " - " + ex.Message);
                            break;
                    }
                    return null;
                }
                finally
                {
                    if (Connection != null)
                        if (Connection.State == System.Data.ConnectionState.Open)
                            Connection.Close();
                }
            }
        }

        public bool AddMateriel(string Reference, string Caracteristique, string Categorie, double Prix_ht, string Flashcode, int Quantite)
        {
            try
            {
                using (Connection)
                {

                    Connection.Open();
                    MySqlTransaction transaction = Connection.BeginTransaction();

                    string sqlID = "SELECT MAX(id_materiel) FROM materiels";
                    MySqlCommand commandID = new MySqlCommand(sqlID, Connection);

                    int ID;
                    if (commandID.ExecuteScalar() != null)
                    {
                        ID = Convert.ToInt32(commandID.ExecuteScalar());

                        ID++;
                        string url = Properties.Settings.Default.UrlQrCode + ID;

                        string sql = "INSERT INTO materiels (id_materiel, REFERENCE, CARACTERISTIQUE, CATEGORIE, PRIX_HT, QRCODE) VALUES (@ID, @Reference, @Caracteristique, @Categorie, @Prix_ht, @QrCode)";
                        MySqlCommand command = new MySqlCommand(sql, Connection);
                        command.Parameters.AddWithValue("@ID", ID);
                        command.Parameters.AddWithValue("@Reference", Reference);
                        command.Parameters.AddWithValue("@Caracteristique", Caracteristique);
                        command.Parameters.AddWithValue("@Categorie", Categorie);
                        command.Parameters.AddWithValue("@Prix_ht", Prix_ht);
                        command.Parameters.AddWithValue("@QrCode", url);


                        for (int i = 0; i < Quantite; i++)
                        {
                            if (command.ExecuteNonQuery() != 1)
                            {
                                transaction.Rollback();
                                return false;
                            }
                        }

                        transaction.Commit();
                        return true;
                    }
                    return false;


                }
            }
            catch (MySqlException ex)
            {
                switch (ex.Number)
                {
                    case 0:
                        MessageBox.Show("Impossible de se connecter au serveur.");
                        break;
                    case 1045:
                        MessageBox.Show("Nom d'utilisateur ou mot de passe incorrect.");
                        break;
                    default:
                        MessageBox.Show("Erreur de connexion : " + ex.Number + " - " + ex.Message);
                        break;
                }
                return false;
            }
            finally
            {
                if (Connection != null)
                    if (Connection.State == System.Data.ConnectionState.Open)
                        Connection.Close();
            }
        }

        public Materiels.UnMateriel GetMateriel(int id_materiel)
        {
            try
            {
                using (Connection)
                {

                    Connection.Open();

                    string sql =
                        "SELECT * FROM materiels where id_materiel = @id_materiel;";

                    using (MySqlCommand command = new MySqlCommand(sql, Connection))
                    {
                        command.Parameters.AddWithValue("@id_materiel", id_materiel);
                        Materiels.UnMateriel unMateriel = new Materiels.UnMateriel();

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string categorie = Convert.ToString(reader["Categorie"]);
                                string reference = Convert.ToString(reader["Reference"]);
                                string caracteristique = Convert.ToString(reader["Caracteristique"]);
                                double prix_ht = Convert.ToDouble(reader["Prix_ht"]);
                                string flashcode = Convert.ToString(reader["Qrcode"]);

                                unMateriel = new Materiels.UnMateriel(id_materiel, categorie, reference, caracteristique, prix_ht, flashcode);

                            }
                        }
                        return unMateriel;
                    }
                }
            }
            catch (MySqlException ex)
            {
                switch (ex.Number)
                {
                    case 0:
                        MessageBox.Show("Impossible de se connecter au serveur.");
                        break;
                    case 1045:
                        MessageBox.Show("Nom d'utilisateur ou mot de passe incorrect.");
                        break;
                    default:
                        MessageBox.Show("Erreur de connexion : " + ex.Number + " - " + ex.Message);
                        break;
                }
                return null;
            }
            finally
            {
                if (Connection != null)
                    if (Connection.State == System.Data.ConnectionState.Open)
                        Connection.Close();
            }
        }

        public bool ModifMateriel(int id_materiel, string Reference, string Caracteristique, string Categorie, double Prix_ht)
        {

            try
            {
                using (Connection)
                {

                    Connection.Open();
                    MySqlTransaction transaction = Connection.BeginTransaction();


                    string sql = "UPDATE materiels set Reference = @Reference, Caracteristique = @Caracteristique, Categorie = @Categorie, Prix_ht = @Prix_ht where id_materiel = @id_materiel";
                    MySqlCommand command = new MySqlCommand(sql, Connection);
                    command.Parameters.AddWithValue("@Reference", Reference);
                    command.Parameters.AddWithValue("@Caracteristique", Caracteristique);
                    command.Parameters.AddWithValue("@Categorie", Categorie);
                    command.Parameters.AddWithValue("@Prix_ht", Prix_ht);
                    command.Parameters.AddWithValue("@id_materiel", id_materiel);


                    if (command.ExecuteNonQuery() != 1)
                    {
                        transaction.Rollback();
                        return false;
                    }
                    else
                    {
                        transaction.Commit();
                        return true;
                    }
                }
            }
            catch (MySqlException ex)
            {
                switch (ex.Number)
                {
                    case 0:
                        MessageBox.Show("Impossible de se connecter au serveur.");
                        break;
                    case 1045:
                        MessageBox.Show("Nom d'utilisateur ou mot de passe incorrect.");
                        break;
                    default:
                        MessageBox.Show("Erreur de connexion : " + ex.Number + " - " + ex.Message);
                        break;
                }
                return false;
            }
            finally
            {
                if (Connection != null)
                    if (Connection.State == System.Data.ConnectionState.Open)
                        Connection.Close();
            }
        }

        public bool DeleteMateriel(int id_materiel)
        {
            try
            {
                using (Connection)
                {

                    Connection.Open();
                    MySqlTransaction transaction = Connection.BeginTransaction();

                    string sql = "DELETE from materiels where id_materiel = @id_materiel";
                    MySqlCommand command = new MySqlCommand(sql, Connection);
                    command.Parameters.AddWithValue("@id_materiel", id_materiel);

                    if (command.ExecuteNonQuery() != 1)
                    {
                        transaction.Rollback();
                        return false;
                    }
                    else
                    {
                        transaction.Commit();
                        return true;
                    }
                }
            }
            catch (MySqlException ex)
            {
                switch (ex.Number)
                {
                    case 0:
                        MessageBox.Show("Impossible de se connecter au serveur.");
                        break;
                    case 1045:
                        MessageBox.Show("Nom d'utilisateur ou mot de passe incorrect.");
                        break;
                    default:
                        MessageBox.Show("Erreur de connexion : " + ex.Number + " - " + ex.Message);
                        break;
                }
                return false;
            }
            finally
            {
                if (Connection != null)
                    if (Connection.State == System.Data.ConnectionState.Open)
                        Connection.Close();
            }
        }


        //-------------- EMPLOYE ----------------//

        public List<Employe.UnEmploye> ListEmployes()
        {
            using (Connection)
            {
                try
                {
                    Connection.Open();

                    string sql =
                        "SELECT * FROM employes ";

                    using (MySqlCommand command = new MySqlCommand(sql, Connection))
                    {
                        List<Employe.UnEmploye> ListEmployes = new List<Employe.UnEmploye>();
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int id_employe = Convert.ToInt32(reader["id_employe"]);
                                string nom = Convert.ToString(reader["nom_employe"]);
                                string prenom = Convert.ToString(reader["prenom_employe"]);
                                string email = Convert.ToString(reader["email_employe"]);
                                string telephone = Convert.ToString(reader["telephone_employe"]);
                                bool permission = Convert.ToBoolean(reader["permission"]);
                                string user = Convert.ToString(reader["identifiant"]);
                                string password = Convert.ToString(reader["password"]);

                                Employe.UnEmploye Employe = new Employe.UnEmploye(id_employe, nom, prenom, email, telephone, permission, user, password) ;
                                ListEmployes.Add(Employe);
                            }
                        }

                        return ListEmployes;
                    }
                }
                catch (MySqlException ex)
                {
                    switch (ex.Number)
                    {
                        case 0:
                            MessageBox.Show("Impossible de se connecter au serveur.");
                            break;
                        case 1045:
                            MessageBox.Show("Nom d'utilisateur ou mot de passe incorrect.");
                            break;
                        default:
                            MessageBox.Show("Erreur de connexion : " + ex.Number + " - " + ex.Message);
                            break;
                    }
                    return null;
                }
                finally
                {
                    if (Connection != null)
                        if (Connection.State == System.Data.ConnectionState.Open)
                            Connection.Close();
                }
            }
        }

        public bool AddEmploye(string Nom, string Prenom, string Email, string Telephone, bool Permission, string User)
        {
            try
            {
                using (Connection)
                {

                    Connection.Open();
                    MySqlTransaction transaction = Connection.BeginTransaction();

                        string sql = "INSERT INTO employes (nom_employe, prenom_employe, email_employe, telephone_employe, PERMISSION, IDENTIFIANT, PASSWORD) VALUES (@Nom, @Prenom, @Email, @Telephone, @Permission, @User, @Password)";
                        MySqlCommand command = new MySqlCommand(sql, Connection);

                    string Password =  CreatePassword("Password123@");
                    if (Password != null)
                    {
                        command.Parameters.AddWithValue("@Nom", Nom);
                        command.Parameters.AddWithValue("@Prenom", Prenom);
                        command.Parameters.AddWithValue("@Email", Email);
                        command.Parameters.AddWithValue("@Telephone", Telephone);
                        command.Parameters.AddWithValue("@Permission", Permission);
                        command.Parameters.AddWithValue("@User", User);
                        command.Parameters.AddWithValue("@Password", Password);
                    }
                    else
                    {
                        return false;
                    }

                    if (command.ExecuteNonQuery() != 1)
                    {
                        transaction.Rollback();
                        return false;
                    }

                        transaction.Commit();
                        return true;
                }
            }
            catch (MySqlException ex)
            {
                switch (ex.Number)
                {
                    case 0:
                        MessageBox.Show("Impossible de se connecter au serveur.");
                        break;
                    case 1045:
                        MessageBox.Show("Nom d'utilisateur ou mot de passe incorrect.");
                        break;
                    default:
                        MessageBox.Show("Erreur de connexion : " + ex.Number + " - " + ex.Message);
                        break;
                }
                return false;
            }
            finally
            {
                if (Connection != null)
                    if (Connection.State == System.Data.ConnectionState.Open)
                        Connection.Close();
            }
        }

        public List<Employe.UnEmploye> GetEmploye(int id_employe)
        {
            try
            {
                using (Connection)
                {

                    Connection.Open();

                    string sql =
                        "SELECT * FROM employes where id_employe = @id_employe;";

                    using (MySqlCommand command = new MySqlCommand(sql, Connection))
                    {
                        command.Parameters.AddWithValue("@id_employe", id_employe);

                        List<Employe.UnEmploye> ListEmploye = new List<Employe.UnEmploye>();
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string nom = Convert.ToString(reader["nom_employe"]);
                                string prenom = Convert.ToString(reader["prenom_employe"]);
                                string email = Convert.ToString(reader["email_employe"]);
                                string telephone = Convert.ToString(reader["telephone_employe"]);
                                bool permission = Convert.ToBoolean(reader["permission"]);
                                string user = Convert.ToString(reader["identifiant"]);
                                string password = Convert.ToString(reader["password"]);

                                Employe.UnEmploye Employe = new Employe.UnEmploye(id_employe, nom, prenom, email, telephone, permission, user, password);
                                ListEmploye.Add(Employe);

                            }
                        }

                        return ListEmploye;
                    }
                }
            }
            catch (MySqlException ex)
            {
                switch (ex.Number)
                {
                    case 0:
                        MessageBox.Show("Impossible de se connecter au serveur.");
                        break;
                    case 1045:
                        MessageBox.Show("Nom d'utilisateur ou mot de passe incorrect.");
                        break;
                    default:
                        MessageBox.Show("Erreur de connexion : " + ex.Number + " - " + ex.Message);
                        break;
                }
                return null;
            }
            finally
            {
                if (Connection != null)
                    if (Connection.State == System.Data.ConnectionState.Open)
                        Connection.Close();
            }
        }

        public bool ModifEmploye(int id_employe, string Nom, string Prenom, string Email, string Telephone, bool Permission, string User)
        {

            try
            {
                using (Connection)
                {

                    Connection.Open();
                    MySqlTransaction transaction = Connection.BeginTransaction();


                    string sql = "UPDATE employes set Nom_employe = @Nom, Prenom_employe = @Prenom, Email_employe = @Email, Telephone_employe = @Telephone, Permission = @Permission, Identifiant = @User where id_employe = @id_employe";
                    MySqlCommand command = new MySqlCommand(sql, Connection);
                    command.Parameters.AddWithValue("@Nom", Nom);
                    command.Parameters.AddWithValue("@Prenom", Prenom);
                    command.Parameters.AddWithValue("@Email", Email);
                    command.Parameters.AddWithValue("@Telephone", Telephone);
                    command.Parameters.AddWithValue("@Permission", Permission);
                    command.Parameters.AddWithValue("@User", User);
                    command.Parameters.AddWithValue("@id_employe", id_employe);


                    if (command.ExecuteNonQuery() != 1)
                    {
                        transaction.Rollback();
                        return false;
                    }
                    else
                    {
                        transaction.Commit();
                        return true;
                    }
                }
            }
            catch (MySqlException ex)
            {
                switch (ex.Number)
                {
                    case 0:
                        MessageBox.Show("Impossible de se connecter au serveur.");
                        break;
                    case 1045:
                        MessageBox.Show("Nom d'utilisateur ou mot de passe incorrect.");
                        break;
                    default:
                        MessageBox.Show("Erreur de connexion : " + ex.Number + " - " + ex.Message);
                        break;
                }
                return false;
            }
            finally
            {
                if (Connection != null)
                    if (Connection.State == System.Data.ConnectionState.Open)
                        Connection.Close();
            }
        }

        public bool DeleteEmploye(int id_employe)
        {
            try
            {
                using (Connection)
                {

                    Connection.Open();
                    MySqlTransaction transaction = Connection.BeginTransaction();

                    string sql = "DELETE from employes where id_employe = @id_employe";
                    MySqlCommand command = new MySqlCommand(sql, Connection);
                    command.Parameters.AddWithValue("@id_employe", id_employe);

                    if (command.ExecuteNonQuery() != 1)
                    {
                        transaction.Rollback();
                        return false;
                    }
                    else
                    {
                        transaction.Commit();
                        return true;
                    }
                }
            }
            catch (MySqlException ex)
            {
                switch (ex.Number)
                {
                    case 0:
                        MessageBox.Show("Impossible de se connecter au serveur.");
                        break;
                    case 1045:
                        MessageBox.Show("Nom d'utilisateur ou mot de passe incorrect.");
                        break;
                    default:
                        MessageBox.Show("Erreur de connexion : " + ex.Number + " - " + ex.Message);
                        break;
                }
                return false;
            }
            finally
            {
                if (Connection != null)
                    if (Connection.State == System.Data.ConnectionState.Open)
                        Connection.Close();
            }
        }

        public bool ResetPwd(int id_employe){ 
            try
            {
                using (Connection)
                {

                    Connection.Open();
                    MySqlTransaction transaction = Connection.BeginTransaction();


                    string sql = "UPDATE employes set password=@Password where id_employe = @id_employe";
                    MySqlCommand command = new MySqlCommand(sql, Connection);

                    string password = CreatePassword("Password123@");
                    command.Parameters.AddWithValue("@Password", password);
                    command.Parameters.AddWithValue("@id_employe", id_employe);

                    if (command.ExecuteNonQuery() != 1)
                    {
                        transaction.Rollback();
                        return false;
                    }
                    else
                    {
                        transaction.Commit();
                        return true;
                    }
                }
            }
            catch (MySqlException ex)
            {
                switch (ex.Number)
                {
                    case 0:
                        MessageBox.Show("Impossible de se connecter au serveur.");
                        break;
                    case 1045:
                        MessageBox.Show("Nom d'utilisateur ou mot de passe incorrect.");
                        break;
                    default:
                        MessageBox.Show("Erreur de connexion : " + ex.Number + " - " + ex.Message);
                        break;
                }
                return false;
            }
            finally
            {
                if (Connection != null)
                    if (Connection.State == System.Data.ConnectionState.Open)
                        Connection.Close();
            }
        }


        //-------------- CLIENT ----------------//

        public List<Client.UnClient> ListClients()
        {
            using (Connection)
            {
                try
                {
                    Connection.Open();

                    string sql =
                        "SELECT * FROM clients ";

                    using (MySqlCommand command = new MySqlCommand(sql, Connection))
                    {
                        List<Client.UnClient> ListClients = new List<Client.UnClient>();
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int id_client = Convert.ToInt32(reader["id_client"]);
                                string nom = Convert.ToString(reader["nom_client"]);
                                string prenom = Convert.ToString(reader["prenom_client"]);
                                string adresse = Convert.ToString(reader["adresse_client"]);
                                string code_postale = Convert.ToString(reader["code_postale_client"]);
                                string ville = Convert.ToString(reader["ville_client"]);
                                string telephone = Convert.ToString(reader["telephone_client"]);
                                string mail = Convert.ToString(reader["mail_client"]);

                                Client.UnClient Client = new Client.UnClient(id_client, nom, prenom, adresse, code_postale, ville, telephone, mail);
                                ListClients.Add(Client);
                            }
                        }

                        return ListClients;
                    }
                }
                catch (MySqlException ex)
                {
                    switch (ex.Number)
                    {
                        case 0:
                            MessageBox.Show("Impossible de se connecter au serveur.");
                            break;
                        case 1045:
                            MessageBox.Show("Nom d'utilisateur ou mot de passe incorrect.");
                            break;
                        default:
                            MessageBox.Show("Erreur de connexion : " + ex.Number + " - " + ex.Message);
                            break;
                    }
                    return null;
                }
                finally
                {
                    if (Connection != null)
                        if (Connection.State == System.Data.ConnectionState.Open)
                            Connection.Close();
                }
            }
        }

        public bool AddClient(string Nom, string Prenom, string Adresse, string Code_postale, string Ville, string Telephone, string Email)
        {
            try
            {
                using (Connection)
                {

                    Connection.Open();
                    MySqlTransaction transaction = Connection.BeginTransaction();

                    string sql = "INSERT INTO clients (nom_client, prenom_client, adresse_client, code_postale_client, ville_client, telephone_client, mail_client) VALUES (@Nom, @Prenom, @Adresse, @Code_postale, @Ville, @Telephone, @Email)";
                    MySqlCommand command = new MySqlCommand(sql, Connection);
                    command.Parameters.AddWithValue("@Nom", Nom);
                    command.Parameters.AddWithValue("@Prenom", Prenom);
                    command.Parameters.AddWithValue("@Adresse", Adresse);
                    command.Parameters.AddWithValue("@Code_postale", Code_postale);
                    command.Parameters.AddWithValue("@Ville", Ville);
                    command.Parameters.AddWithValue("@Telephone", Telephone);
                    command.Parameters.AddWithValue("@Email", Email);


                    if (command.ExecuteNonQuery() != 1)
                    {
                        transaction.Rollback();
                        return false;
                    }

                    transaction.Commit();
                    return true;
                }
            }
            catch (MySqlException ex)
            {
                switch (ex.Number)
                {
                    case 0:
                        MessageBox.Show("Impossible de se connecter au serveur.");
                        break;
                    case 1045:
                        MessageBox.Show("Nom d'utilisateur ou mot de passe incorrect.");
                        break;
                    default:
                        MessageBox.Show("Erreur de connexion : " + ex.Number + " - " + ex.Message);
                        break;
                }
                return false;
            }
            finally
            {
                if (Connection != null)
                    if (Connection.State == System.Data.ConnectionState.Open)
                        Connection.Close();
            }
        }

        public List<Client.UnClient> GetClient(int id_client)
        {
            try
            {
                using (Connection)
                {

                    Connection.Open();

                    string sql =
                        "SELECT * FROM clients where id_client = @id_client;";

                    using (MySqlCommand command = new MySqlCommand(sql, Connection))
                    {
                        command.Parameters.AddWithValue("@id_client", id_client);

                        List<Client.UnClient> ListClients = new List<Client.UnClient>();
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string nom = Convert.ToString(reader["nom_client"]);
                                string prenom = Convert.ToString(reader["prenom_client"]);
                                string adresse = Convert.ToString(reader["adresse_client"]);
                                string code_postale = Convert.ToString(reader["code_postale_client"]);
                                string ville = Convert.ToString(reader["ville_client"]);
                                string telephone = Convert.ToString(reader["telephone_client"]);
                                string mail = Convert.ToString(reader["mail_client"]);

                                Client.UnClient Client = new Client.UnClient(id_client, nom, prenom, adresse, code_postale, ville, telephone, mail);
                                ListClients.Add(Client);

                            }
                        }

                        return ListClients;
                    }
                }
            }
            catch (MySqlException ex)
            {
                switch (ex.Number)
                {
                    case 0:
                        MessageBox.Show("Impossible de se connecter au serveur.");
                        break;
                    case 1045:
                        MessageBox.Show("Nom d'utilisateur ou mot de passe incorrect.");
                        break;
                    default:
                        MessageBox.Show("Erreur de connexion : " + ex.Number + " - " + ex.Message);
                        break;
                }
                return null;
            }
            finally
            {
                if (Connection != null)
                    if (Connection.State == System.Data.ConnectionState.Open)
                        Connection.Close();
            }
        }

        public bool ModifClient(int id_client, string Nom, string Prenom, string Adresse, string Code_postale, string Ville, string Telephone, string Email)
        {

            try
            {
                using (Connection)
                {

                    Connection.Open();
                    MySqlTransaction transaction = Connection.BeginTransaction();


                    string sql = "UPDATE clients set Nom_client = @Nom, Prenom_client = @Prenom, Adresse_client = @Adresse, Code_postale_client = @Code_postale, Ville_client = @Ville, Telephone_client = @Telephone, mail_client = @Email where id_client = @id_client";
                    MySqlCommand command = new MySqlCommand(sql, Connection);
                    command.Parameters.AddWithValue("@id_client", id_client);
                    command.Parameters.AddWithValue("@Nom", Nom);
                    command.Parameters.AddWithValue("@Prenom", Prenom);
                    command.Parameters.AddWithValue("@Adresse", Adresse);
                    command.Parameters.AddWithValue("@Code_postale", Code_postale);
                    command.Parameters.AddWithValue("@Ville", Ville);
                    command.Parameters.AddWithValue("@Telephone", Telephone);
                    command.Parameters.AddWithValue("@Email", Email);


                    if (command.ExecuteNonQuery() != 1)
                    {
                        transaction.Rollback();
                        return false;
                    }
                    else
                    {
                        transaction.Commit();
                        return true;
                    }
                }
            }
            catch (MySqlException ex)
            {
                switch (ex.Number)
                {
                    case 0:
                        MessageBox.Show("Impossible de se connecter au serveur.");
                        break;
                    case 1045:
                        MessageBox.Show("Nom d'utilisateur ou mot de passe incorrect.");
                        break;
                    default:
                        MessageBox.Show("Erreur de connexion : " + ex.Number + " - " + ex.Message);
                        break;
                }
                return false;
            }
            finally
            {
                if (Connection != null)
                    if (Connection.State == System.Data.ConnectionState.Open)
                        Connection.Close();
            }
        }

        public bool DeleteClient(int id_client)
        {
            try
            {
                using (Connection)
                {

                    Connection.Open();
                    MySqlTransaction transaction = Connection.BeginTransaction();

                    string sql = "DELETE from clients where id_client = @id_client";
                    MySqlCommand command = new MySqlCommand(sql, Connection);
                    command.Parameters.AddWithValue("@id_client", id_client);

                    if (command.ExecuteNonQuery() != 1)
                    {
                        transaction.Rollback();
                        return false;
                    }
                    else
                    {
                        transaction.Commit();
                        return true;
                    }
                }
            }
            catch (MySqlException ex)
            {
                switch (ex.Number)
                {
                    case 0:
                        MessageBox.Show("Impossible de se connecter au serveur.");
                        break;
                    case 1045:
                        MessageBox.Show("Nom d'utilisateur ou mot de passe incorrect.");
                        break;
                    default:
                        MessageBox.Show("Erreur de connexion : " + ex.Number + " - " + ex.Message);
                        break;
                }
                return false;
            }
            finally
            {
                if (Connection != null)
                    if (Connection.State == System.Data.ConnectionState.Open)
                        Connection.Close();
            }
        }

        //-------------- USER ----------------//

        /* public bool VerifUser(string Username, string Password)
         {
             try
             {
                 byte[] HashPasswordByte = BDD.EncryptPassword.HashPassword(Password, salt);
                 string HashPasswordString = Convert.ToBase64String(HashPasswordByte);

                 using (Connection)
                 {

                     Connection.Open();
                     MySqlTransaction transaction = Connection.BeginTransaction();


                     string sql = "select permission, identifiant, password from employe where identifiant = @Username AND Password = @Password";
                     MySqlCommand command = new MySqlCommand(sql, Connection);
                     command.Parameters.AddWithValue("@Username", Username);
                     command.Parameters.AddWithValue("@Password", HashPasswordString);



                     if (command.ExecuteScalar() != null)
                     {
                         string permission = command.ExecuteScalar().ToString();
                         Properties.Settings.Default.Permission = permission;
                         Properties.Settings.Default.User = Username;
                         Properties.Settings.Default.Save();

                         transaction.Commit();
                         return true;
                     }
                     else
                     {
                         Properties.Settings.Default.Permission = "False";
                         Properties.Settings.Default.Save();
                         transaction.Rollback();
                         return false;
                     }

                 }
             }
             catch (MySqlException ex)
             {
                 switch (ex.Number)
                 {
                     case 0:
                         MessageBox.Show("Impossible de se connecter au serveur.");
                         break;
                     case 1045:
                         MessageBox.Show("Nom d'utilisateur ou mot de passe incorrect.");
                         break;
                     default:
                         MessageBox.Show("Erreur de connexion : " + ex.Number + " - " + ex.Message);
                         break;
                 }
                 Properties.Settings.Default.Permission = "False";
                 Properties.Settings.Default.Save();
                 return false;
             }
             finally
             {
                 if (Connection != null)
                     if (Connection.State == System.Data.ConnectionState.Open)
                         Connection.Close();
             }
         }*/

        public bool VerifUser(string username, string password)
        {
            try
            {
                string Hashpassword = CreatePassword(password);

                using (Connection)
                {

                    Connection.Open();
                    MySqlTransaction transaction = Connection.BeginTransaction();

                    string sql = "select permission, identifiant from employes where password = @Password AND identifiant = @Username";
                    
                    using (MySqlCommand command = new MySqlCommand(sql, Connection))
                    {
                        command.Parameters.AddWithValue("@Username", username);
                        command.Parameters.AddWithValue("@Password", Hashpassword);

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                reader.Read();
                                Properties.Settings.Default.Permission = Convert.ToString(reader["permission"]);
                                Properties.Settings.Default.User = Convert.ToString(reader["identifiant"]);
                                return true;
                            }
                            return false;
                        }
                    }
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        public string CreatePassword(string password)
        {
            try
            {
                SHA256 sha256 = SHA256.Create();
                byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
                byte[] hashBytes = sha256.ComputeHash(passwordBytes);

                StringBuilder sb = new StringBuilder();
                foreach (byte b in hashBytes)
                {
                    sb.Append(b.ToString("x2"));
                }
                return sb.ToString();
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }

        }

        public bool ChangePwd(string OldPwd, string NewPwd)
        {
            try
            {
                string Username = Properties.Settings.Default.User;

                if ( VerifUser(Username, OldPwd))
                {
                    string OldPwdEncrypt = CreatePassword(OldPwd);
                    string NewPwdEncrypt = CreatePassword(NewPwd);

                    using (Connection)
                    {

                        Connection.Open();
                        MySqlTransaction transaction = Connection.BeginTransaction();

                        string sql = "UPDATE employes set password = @NewPassword where identifiant = @Username and password=@OldPwd";
                        MySqlCommand command = new MySqlCommand(sql, Connection);
                        command.Parameters.AddWithValue("@Username", Username);
                        command.Parameters.AddWithValue("@NewPassword", NewPwdEncrypt);
                        command.Parameters.AddWithValue("@OldPwd", OldPwdEncrypt);

                        // command.Parameters.AddWithValue("@OldPassword", HashOldPasswordString);


                        if (command.ExecuteNonQuery() != 1)
                        {
                            transaction.Rollback();
                            return false;
                        }
                        else
                        {
                            transaction.Commit();
                            return true;
                        }

                    }
                }
                return false;
            }
            catch (MySqlException ex)
            {
                switch (ex.Number)
                {
                    case 0:
                        MessageBox.Show("Impossible de se connecter au serveur.");
                        break;
                    case 1045:
                        MessageBox.Show("Nom d'utilisateur ou mot de passe incorrect.");
                        break;
                    default:
                        MessageBox.Show("Erreur de connexion : " + ex.Number + " - " + ex.Message);
                        break;
                }
                return false;
            }
            finally
            {
                if (Connection != null)
                    if (Connection.State == System.Data.ConnectionState.Open)
                        Connection.Close();
            }
        }
       



    }
}
