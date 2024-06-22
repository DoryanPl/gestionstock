using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Gestionnaire.Materiels.BDD
{
    class GestionStockBDD : IDisposable
    {
      
        private string connectionString;
        private MySqlConnection _connection;
        
public MySqlConnection Connection { get => _connection; set => _connection = value; }

   
        public GestionStockBDD()
        {
     
            MySqlConnectionStringBuilder builder = new MySqlConnectionStringBuilder();
            builder.Server = Properties.Settings.Default.IP;
            builder.Database = Properties.Settings.Default.Database;
            builder.UserID = Properties.Settings.Default.Username;
            builder.Password = Properties.Settings.Default.Password;
            connectionString = builder.ToString();
            Connection = new MySqlConnection(connectionString);
        }


        public bool IsServerConnected()
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    return true;
                }
                catch (MySqlException ex)
                {
                    string erreur = ex.Message;
                    return false;
                }
            }
        }

        public List<AjouterMateriel.UnMateriel> ListMateriels()
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    string sql =
                        "SELECT 'cable' as table_name, nom, prix, livrer, flashcode FROM cable " +
                        "UNION " +
                        "SELECT 'enceinte' as table_name, nom, prix, livrer, flashcode FROM enceinte " +
                        "UNION " +
                        "SELECT 'video' as table_name, nom, prix, livrer, flashcode FROM video " +
                        "UNION " +
                        "SELECT 'lumiere' as table_name, nom, prix, livrer, flashcode FROM lumiere " +
                        "UNION " +
                        "SELECT 'patch' as table_name, nom, prix, livrer, flashcode FROM patch " +
                        "UNION " +
                        "SELECT 'console' as table_name, nom, prix, livrer, flashcode FROM console " +
                        "UNION " +
                        "SELECT 'structure' as table_name, nom, prix, livrer, flashcode FROM structure;";

                    using (MySqlCommand command = new MySqlCommand(sql, connection))
                    {
                        List<AjouterMateriel.UnMateriel> ListMateriels = new List<AjouterMateriel.UnMateriel>();
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                
                                string categorie = Convert.ToString(reader[0]);
                                string nom = Convert.ToString(reader[1]);
                                string prix = Convert.ToString(reader[2]);
                                bool livrer = Convert.ToBoolean(reader[3]);
                                int flashcode = Convert.ToInt32(reader[4]);
                                AjouterMateriel.UnMateriel UnMateriel2 = new AjouterMateriel.UnMateriel(nom,prix,flashcode,categorie,livrer); 
                                ListMateriels.Add(UnMateriel2);
                            }
                        }
                        
                        return ListMateriels;
                    }
                }
                catch (MySqlException ex)
                {
                    string erreur = ex.Message;
                    return null;
                }
            }
        }

        public bool AddCable(string Nom, string Prix, int Flashcode, int Longueur, string Type, bool Livrer)
        {
            try
            {
                using (Connection)
                {

                    Connection.Open();
                    MySqlTransaction transaction = Connection.BeginTransaction();

                    string sql = "INSERT INTO Cable (NOM, PRIX, FLASHCODE, LONGUEUR, TYPE, LIVRER) VALUES (@Nom, @Prix, @Flashcode, @Longueur, @Type, @Livrer)";
                    MySqlCommand command = new MySqlCommand(sql, Connection);
                    command.Parameters.AddWithValue("@Nom", Nom);
                    command.Parameters.AddWithValue("@Prix", Prix);
                    command.Parameters.AddWithValue("@Flashcode", Flashcode);
                    command.Parameters.AddWithValue("@Longueur", Longueur);
                    command.Parameters.AddWithValue("@Type", Type);
                    command.Parameters.AddWithValue("@Livrer", Livrer);

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
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                if (Connection != null)
                    if (Connection.State == System.Data.ConnectionState.Open)
                        Connection.Close();
            }
        }
        public bool AddConsole(string Nom, string Prix, int Flashcode, int _IN, int _OUT, string DANTE, string AES, string AES50, bool Livrer)
        {
            try
            {
                using (Connection)
                {

                    Connection.Open();
                    MySqlTransaction transaction = Connection.BeginTransaction();

                    string sql = "INSERT INTO Console (NOM, PRIX, FLASHCODE, _IN, _OUT, DANTE, AES, AES50, LIVRER) VALUES (@Nom, @Prix, @Flashcode, @_IN, @_OUT, @DANTE, @AES, @AES50, @Livrer)";
                    MySqlCommand command = new MySqlCommand(sql, Connection);
                    command.Parameters.AddWithValue("@Nom", Nom);
                    command.Parameters.AddWithValue("@Prix", Prix);
                    command.Parameters.AddWithValue("@Flashcode", Flashcode);
                    command.Parameters.AddWithValue("@_IN", _IN);
                    command.Parameters.AddWithValue("@_OUT", _OUT);
                    command.Parameters.AddWithValue("@DANTE", DANTE);
                    command.Parameters.AddWithValue("@AES", AES);
                    command.Parameters.AddWithValue("@AES50", AES50);
                    command.Parameters.AddWithValue("@Livrer", Livrer);

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
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                if (Connection != null)
                    if (Connection.State == System.Data.ConnectionState.Open)
                        Connection.Close();
            }
        }
        public bool AddEnceinte(string Nom, string Prix, int Flashcode, int Puissance, string Bande_Passante, bool Livrer)
        {
            try
            {
                using (Connection)
                {

                    Connection.Open();
                    MySqlTransaction transaction = Connection.BeginTransaction();

                    string sql = "INSERT INTO Enceinte (NOM, PRIX, FLASHCODE, PUISSANCE, BANDE_PASSANTE, LIVRER) VALUES (@Nom, @Prix, @Flashcode, @Puissance, @Bande_Passante, @Livrer)";
                    MySqlCommand command = new MySqlCommand(sql, Connection);
                    command.Parameters.AddWithValue("@Nom", Nom);
                    command.Parameters.AddWithValue("@Prix", Prix);
                    command.Parameters.AddWithValue("@Flashcode", Flashcode);
                    command.Parameters.AddWithValue("@Puissance", Puissance);
                    command.Parameters.AddWithValue("@Bande_Passante", Bande_Passante);
                    command.Parameters.AddWithValue("@Livrer", Livrer);
                    //command.ExecuteNonQuery();

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
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                if (Connection != null)
                    if (Connection.State == System.Data.ConnectionState.Open)
                        Connection.Close();
            }
        }
        public bool AddLumiere(string Nom, string Prix, int Flashcode, bool Livrer)
        {
            try
            {
                using (Connection)
                {

                    Connection.Open();
                    MySqlTransaction transaction = Connection.BeginTransaction();

                    string sql = "INSERT INTO Lumiere (NOM, PRIX, FLASHCODE, LIVRER) VALUES (@Nom, @Prix, @Flashcode, @Livrer)";
                    MySqlCommand command = new MySqlCommand(sql, Connection);
                    command.Parameters.AddWithValue("@Nom", Nom);
                    command.Parameters.AddWithValue("@Prix", Prix);
                    command.Parameters.AddWithValue("@Flashcode", Flashcode);
                    command.Parameters.AddWithValue("@Livrer", Livrer);

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
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                if (Connection != null)
                    if (Connection.State == System.Data.ConnectionState.Open)
                        Connection.Close();
            }
        }
        public bool AddPatch(string Nom, string Prix, int Flashcode, int _IN, int _OUT, string DANTE, bool Livrer)
        {
            try
            {
                using (Connection)
                {

                    Connection.Open();
                    MySqlTransaction transaction = Connection.BeginTransaction();


                    string sql = "INSERT INTO Patch (NOM, PRIX, FLASHCODE, _IN, _OUT, DANTE, LIVRER) VALUES (@Nom, @Prix, @Flashcode, @_IN, @_OUT, @DANTE, @Livrer)";
                    MySqlCommand command = new MySqlCommand(sql, Connection);
                    command.Parameters.AddWithValue("@Nom", Nom);
                    command.Parameters.AddWithValue("@Prix", Prix);
                    command.Parameters.AddWithValue("@Flashcode", Flashcode);
                    command.Parameters.AddWithValue("@_IN", _IN);
                    command.Parameters.AddWithValue("@_OUT", _OUT);
                    command.Parameters.AddWithValue("@DANTE", DANTE);
                    command.Parameters.AddWithValue("@Livrer", Livrer);

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
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                if (Connection != null)
                    if (Connection.State == System.Data.ConnectionState.Open)
                        Connection.Close();
            }
        }
        public bool AddStructure(string Nom, string Prix, int Taille, int Flashcode, bool Livrer)
        {
            try
            {
                using (Connection)
                {

                    Connection.Open();
                    MySqlTransaction transaction = Connection.BeginTransaction();

                    string sql = "INSERT INTO Structure (NOM, PRIX, FLASHCODE, TAILLE, LIVRER) VALUES (@Nom, @Prix, @Flashcode, @Taille, @Livrer)";
                    MySqlCommand command = new MySqlCommand(sql, Connection);
                    command.Parameters.AddWithValue("@Nom", Nom);
                    command.Parameters.AddWithValue("@Prix", Prix);
                    command.Parameters.AddWithValue("@Flashcode", Flashcode);
                    command.Parameters.AddWithValue("@Taille", Taille);
                    command.Parameters.AddWithValue("@Livrer", Livrer);

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
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                if (Connection != null)
                    if (Connection.State == System.Data.ConnectionState.Open)
                        Connection.Close();
            }
        }
        public bool AddVideo(string Nom, string Prix, int Flashcode, bool Livrer)
        {
            try
            {
                using (Connection)
                {

                    Connection.Open();
                    MySqlTransaction transaction = Connection.BeginTransaction();

                    string sql = "INSERT INTO video (NOM, PRIX, FLASHCODE, LIVRER) VALUES (@Nom, @Prix, @Flashcode, @Livrer)";
                    MySqlCommand command = new MySqlCommand(sql, Connection);
                    command.Parameters.AddWithValue("@Nom", Nom);
                    command.Parameters.AddWithValue("@Prix", Prix);
                    command.Parameters.AddWithValue("@Flashcode", Flashcode);
                    command.Parameters.AddWithValue("@Livrer", Livrer);

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
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                if (Connection != null)
                    if (Connection.State == System.Data.ConnectionState.Open)
                        Connection.Close();
            }
        }

        public List<Materiels.Cable> GetCable(string Nom, string Prix, int Flashcode, bool Livrer)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    string sql =
                        "SELECT * FROM cable where nom = @Nom AND prix = @Prix AND flashcode = @Flashcode and livrer = @Livrer;";

                    using (MySqlCommand command = new MySqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@Nom", Nom);
                        command.Parameters.AddWithValue("@Prix", Prix);
                        command.Parameters.AddWithValue("@Flashcode", Flashcode);
                        command.Parameters.AddWithValue("@Livrer", Livrer);

                        List<Materiels.Cable> ListMateriels = new List<Materiels.Cable>();
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int idcable = Convert.ToInt32(reader["idcable"]);
                                string nom = Convert.ToString(reader["Nom"]);
                                int flashcode = Convert.ToInt32(reader["Flashcode"]);
                                int prix = Convert.ToInt32(reader["Prix"]);
                                bool livrer = Convert.ToBoolean(reader["Livrer"]);
                                int longueur = Convert.ToInt32(reader["Longueur"]);
                                string type = Convert.ToString(reader["Type"]);
                                Materiels.Cable UnCable = new Materiels.Cable(idcable, nom, flashcode, prix, livrer, longueur, type);

                                ListMateriels.Add(UnCable);

                            }
                        }

                        return ListMateriels;
                    }
                }
            }
            
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                if (Connection != null)
                    if (Connection.State == System.Data.ConnectionState.Open)
                        Connection.Close();
            }
        }
        public List<Materiels.Console> GetConsole(string Nom, string Prix, int Flashcode, bool Livrer)
        {
           try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    string sql =
                        "SELECT * FROM Console where nom = @Nom AND prix = @Prix AND flashcode = @Flashcode and livrer = @Livrer;";

                    using (MySqlCommand command = new MySqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@Nom", Nom);
                        command.Parameters.AddWithValue("@Prix", Prix);
                        command.Parameters.AddWithValue("@Flashcode", Flashcode);
                        command.Parameters.AddWithValue("@Livrer", Livrer);

                        List<Materiels.Console> ListMateriels = new List<Materiels.Console>();
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int idconsole = Convert.ToInt32(reader["idconsole"]);
                                string nom = Convert.ToString(reader["Nom"]);
                                int flashcode = Convert.ToInt32(reader["Flashcode"]);
                                int prix = Convert.ToInt32(reader["Prix"]);
                                bool livrer = Convert.ToBoolean(reader["Livrer"]);
                                int _IN = Convert.ToInt32(reader["_IN"]);
                                int _OUT = Convert.ToInt32(reader["_OUT"]);
                                string DANTE = Convert.ToString(reader["DANTE"]);
                                string AES = Convert.ToString(reader["AES"]);
                                string AES50 = Convert.ToString(reader["AES50"]);

                                Materiels.Console UneConsole = new Materiels.Console(nom, flashcode, prix, livrer, _IN, _OUT, DANTE, AES, AES50, idconsole);

                                ListMateriels.Add(UneConsole);

                            }
                        }

                        return ListMateriels;
                    }
                }
            
           }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                if (Connection != null)
                    if (Connection.State == System.Data.ConnectionState.Open)
                        Connection.Close();
            }
        }
        public List<Materiels.Enceinte> GetEnceinte(string Nom, string Prix, int Flashcode, bool Livrer)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    string sql =
                        "SELECT * FROM Enceinte where nom = @Nom AND prix = @Prix AND flashcode = @Flashcode and livrer = @Livrer;";

                    using (MySqlCommand command = new MySqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@Nom", Nom);
                        command.Parameters.AddWithValue("@Prix", Prix);
                        command.Parameters.AddWithValue("@Flashcode", Flashcode);
                        command.Parameters.AddWithValue("@Livrer", Livrer);

                        List<Materiels.Enceinte> ListMateriels = new List<Materiels.Enceinte>();
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int idenceinte = Convert.ToInt32(reader["idenceinte"]);
                                string nom = Convert.ToString(reader["Nom"]);
                                int flashcode = Convert.ToInt32(reader["Flashcode"]);
                                int prix = Convert.ToInt32(reader["Prix"]);
                                bool livrer = Convert.ToBoolean(reader["Livrer"]);
                                int puissance = Convert.ToInt32(reader["Puissance"]);
                                string bande_passante = Convert.ToString(reader["Bande_Passante"]);


                                Materiels.Enceinte UneEnceinte = new Materiels.Enceinte(nom, flashcode, prix, livrer, puissance, bande_passante, idenceinte);

                                ListMateriels.Add(UneEnceinte);

                            }
                        }

                        return ListMateriels;
                    }
                }
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                if (Connection != null)
                    if (Connection.State == System.Data.ConnectionState.Open)
                        Connection.Close();
            }
        }
        public List<Materiels.Lumiere> GetLumiere(string Nom, string Prix, int Flashcode, bool Livrer)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    string sql =
                        "SELECT * FROM Lumiere where nom = @Nom AND prix = @Prix AND flashcode = @Flashcode and livrer = @Livrer;";

                    using (MySqlCommand command = new MySqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@Nom", Nom);
                        command.Parameters.AddWithValue("@Prix", Prix);
                        command.Parameters.AddWithValue("@Flashcode", Flashcode);
                        command.Parameters.AddWithValue("@Livrer", Livrer);

                        List<Materiels.Lumiere> ListMateriels = new List<Materiels.Lumiere>();
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int idlumiere = Convert.ToInt32(reader["idlumiere"]);
                                string nom = Convert.ToString(reader["Nom"]);
                                int flashcode = Convert.ToInt32(reader["Flashcode"]);
                                int prix = Convert.ToInt32(reader["Prix"]);
                                bool livrer = Convert.ToBoolean(reader["Livrer"]);


                                Materiels.Lumiere UneLumiere = new Materiels.Lumiere(nom, flashcode, prix, livrer, idlumiere);

                                ListMateriels.Add(UneLumiere);

                            }
                        }

                        return ListMateriels;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                if (Connection != null)
                    if (Connection.State == System.Data.ConnectionState.Open)
                        Connection.Close();
            }
        }
        public List<Materiels.Patch> GetPatch(string Nom, string Prix, int Flashcode, bool Livrer)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {

                    connection.Open();

                    string sql =
                        "SELECT * FROM Patch where nom = @Nom AND prix = @Prix AND flashcode = @Flashcode and livrer = @Livrer;";

                    using (MySqlCommand command = new MySqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@Nom", Nom);
                        command.Parameters.AddWithValue("@Prix", Prix);
                        command.Parameters.AddWithValue("@Flashcode", Flashcode);
                        command.Parameters.AddWithValue("@Livrer", Livrer);

                        List<Materiels.Patch> ListMateriels = new List<Materiels.Patch>();
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int idpatch = Convert.ToInt32(reader["idpatch"]);
                                string nom = Convert.ToString(reader["Nom"]);
                                int flashcode = Convert.ToInt32(reader["Flashcode"]);
                                int prix = Convert.ToInt32(reader["Prix"]);
                                bool livrer = Convert.ToBoolean(reader["Livrer"]);
                                int _IN = Convert.ToInt32(reader["_IN"]);
                                int _OUT = Convert.ToInt32(reader["_OUT"]);
                                string DANTE = Convert.ToString(reader["DANTE"]);



                                Materiels.Patch UnPatch = new Materiels.Patch(nom, flashcode, prix, livrer, _IN, _OUT, DANTE, idpatch);

                                ListMateriels.Add(UnPatch);

                            }
                        }

                        return ListMateriels;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                if (Connection != null)
                    if (Connection.State == System.Data.ConnectionState.Open)
                        Connection.Close();
            }
        }
        public List<Materiels.Structure> GetStructure(string Nom, string Prix, int Flashcode, bool Livrer)
        {
            try 
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    string sql =
                        "SELECT * FROM Structure where nom = @Nom AND prix = @Prix AND flashcode = @Flashcode and livrer = @Livrer;";

                    using (MySqlCommand command = new MySqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@Nom", Nom);
                        command.Parameters.AddWithValue("@Prix", Prix);
                        command.Parameters.AddWithValue("@Flashcode", Flashcode);
                        command.Parameters.AddWithValue("@Livrer", Livrer);

                        List<Materiels.Structure> ListMateriels = new List<Materiels.Structure>();
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int idstructure = Convert.ToInt32(reader["idstructure"]);
                                string nom = Convert.ToString(reader["Nom"]);
                                int flashcode = Convert.ToInt32(reader["Flashcode"]);
                                int prix = Convert.ToInt32(reader["Prix"]);
                                bool livrer = Convert.ToBoolean(reader["Livrer"]);
                                int taille = Convert.ToInt32(reader["Taille"]);

                                Materiels.Structure UneStructure = new Materiels.Structure(nom, flashcode, prix, livrer, taille, idstructure);
                                ListMateriels.Add(UneStructure);

                            }
                        }

                        return ListMateriels;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                if (Connection != null)
                    if (Connection.State == System.Data.ConnectionState.Open)
                        Connection.Close();
            }
        }
        public List<Materiels.Video> GetVideo(string Nom, string Prix, int Flashcode, bool Livrer)
        {
            try { 
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                        connection.Open();

                        string sql =
                            "SELECT * FROM Video where nom = @Nom AND prix = @Prix AND flashcode = @Flashcode and livrer = @Livrer;";

                        using (MySqlCommand command = new MySqlCommand(sql, connection))
                        {
                            command.Parameters.AddWithValue("@Nom", Nom);
                            command.Parameters.AddWithValue("@Prix", Prix);
                            command.Parameters.AddWithValue("@Flashcode", Flashcode);
                            command.Parameters.AddWithValue("@Livrer", Livrer);

                            List<Materiels.Video> ListMateriels = new List<Materiels.Video>();
                            using (MySqlDataReader reader = command.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    int idvideo = Convert.ToInt32(reader["idvideo"]);
                                    string nom = Convert.ToString(reader["Nom"]);
                                    int flashcode = Convert.ToInt32(reader["Flashcode"]);
                                    int prix = Convert.ToInt32(reader["Prix"]);
                                    bool livrer = Convert.ToBoolean(reader["Livrer"]);

                                    Materiels.Video UneVideo = new Materiels.Video(nom, flashcode, prix, livrer, idvideo);
                                    ListMateriels.Add(UneVideo);

                                }
                            }

                            return ListMateriels;
                        }
                    }
                }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                if (Connection != null)
                    if (Connection.State == System.Data.ConnectionState.Open)
                        Connection.Close();
            }

        }

        public bool ModifCable(string Nom, string Prix, int Flashcode, int Longueur, string Type, bool Livrer, int idcable)
        {
            try
            {
                using (Connection)
                {

                    Connection.Open();
                    MySqlTransaction transaction = Connection.BeginTransaction();

                    string sql = "Update Cable set NOM = @Nom, PRIX = @Prix, FLASHCODE = @Flashcode, LONGUEUR = @Longueur, TYPE = @Type, LIVRER = @Livrer where idcable = @idcable;";
                    MySqlCommand command = new MySqlCommand(sql, Connection);
                    command.Parameters.AddWithValue("@Nom", Nom);
                    command.Parameters.AddWithValue("@Prix", Prix);
                    command.Parameters.AddWithValue("@Flashcode", Flashcode);
                    command.Parameters.AddWithValue("@Longueur", Longueur);
                    command.Parameters.AddWithValue("@Type", Type);
                    command.Parameters.AddWithValue("@Livrer", Livrer);
                    command.Parameters.AddWithValue("@idcable", idcable);


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
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                if (Connection != null)
                    if (Connection.State == System.Data.ConnectionState.Open)
                        Connection.Close();
            }
        }
        public bool ModifConsole(string Nom, string Prix, int Flashcode, int _IN, int _OUT, string DANTE, string AES, string AES50, bool Livrer, int idconsole)
        {
            try
            {
                using (Connection)
                {

                    Connection.Open();
                    MySqlTransaction transaction = Connection.BeginTransaction();

                    string sql = "UPDATE Console set NOM = @Nom, PRIX = @Prix, FLASHCODE = @Flashcode, _IN = @_IN, _OUT = @_OUT, DANTE = @DANTE, AES = @AES, AES50 = @AES50, LIVRER = @Livrer where idconsole = @idconsole; ";
                    MySqlCommand command = new MySqlCommand(sql, Connection);
                    command.Parameters.AddWithValue("@Nom", Nom);
                    command.Parameters.AddWithValue("@Prix", Prix);
                    command.Parameters.AddWithValue("@Flashcode", Flashcode);
                    command.Parameters.AddWithValue("@_IN", _IN);
                    command.Parameters.AddWithValue("@_OUT", _OUT);
                    command.Parameters.AddWithValue("@DANTE", DANTE);
                    command.Parameters.AddWithValue("@AES", AES);
                    command.Parameters.AddWithValue("@AES50", AES50);
                    command.Parameters.AddWithValue("@Livrer", Livrer);
                    command.Parameters.AddWithValue("@idconsole", idconsole);


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
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                if (Connection != null)
                    if (Connection.State == System.Data.ConnectionState.Open)
                        Connection.Close();
            }
        }
        public bool ModifEnceinte(string Nom, string Prix, int Flashcode, int Puissance, string Bande_Passante, bool Livrer, int idenceinte)
        {
            try
            {
                using (Connection)
                {

                    Connection.Open();
                    MySqlTransaction transaction = Connection.BeginTransaction();

                    string sql = "UPDATE Enceinte set NOM = @Nom, PRIX = @Prix, FLASHCODE = @Flashcode, PUISSANCE = @Puissance, BANDE_PASSANTE = @Bande_Passante, LIVRER = @Livrer where idenceinte = @idenceinte;";
                    MySqlCommand command = new MySqlCommand(sql, Connection);
                    command.Parameters.AddWithValue("@Nom", Nom);
                    command.Parameters.AddWithValue("@Prix", Prix);
                    command.Parameters.AddWithValue("@Flashcode", Flashcode);
                    command.Parameters.AddWithValue("@Puissance", Puissance);
                    command.Parameters.AddWithValue("@Bande_Passante", Bande_Passante);
                    command.Parameters.AddWithValue("@Livrer", Livrer);
                    command.Parameters.AddWithValue("@idenceinte", idenceinte);

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
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                if (Connection != null)
                    if (Connection.State == System.Data.ConnectionState.Open)
                        Connection.Close();
            }
        }
        public bool ModifLumiere(string Nom, string Prix, int Flashcode, bool Livrer, int idlumiere)
        {
            try
            {
                using (Connection)
                {

                    Connection.Open();
                    MySqlTransaction transaction = Connection.BeginTransaction();

                    string sql = "UPDATE Lumiere set NOM = @Nom, PRIX = @Prix, FLASHCODE = @Flashcode, LIVRER = @Livrer where idlumiere = @idlumiere ;";
                    MySqlCommand command = new MySqlCommand(sql, Connection);
                    command.Parameters.AddWithValue("@Nom", Nom);
                    command.Parameters.AddWithValue("@Prix", Prix);
                    command.Parameters.AddWithValue("@Flashcode", Flashcode);
                    command.Parameters.AddWithValue("@Livrer", Livrer);
                    command.Parameters.AddWithValue("@idlumiere", idlumiere);

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
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                if (Connection != null)
                    if (Connection.State == System.Data.ConnectionState.Open)
                        Connection.Close();
            }
        }
        public bool ModifPatch(string Nom, string Prix, int Flashcode, int _IN, int _OUT, string DANTE, bool Livrer, int idpatch)
        {
            try
            {
                using (Connection)
                {

                    Connection.Open();
                    MySqlTransaction transaction = Connection.BeginTransaction();


                    string sql = "UPDATE Patch set NOM = @Nom, PRIX = @Prix, FLASHCODE = @Flashcode, _IN = @_IN, _OUT = @_OUT, DANTE = @DANTE, LIVRER = @Livrer where idpatch = @idpatch";
                    MySqlCommand command = new MySqlCommand(sql, Connection);
                    command.Parameters.AddWithValue("@Nom", Nom);
                    command.Parameters.AddWithValue("@Prix", Prix);
                    command.Parameters.AddWithValue("@Flashcode", Flashcode);
                    command.Parameters.AddWithValue("@_IN", _IN);
                    command.Parameters.AddWithValue("@_OUT", _OUT);
                    command.Parameters.AddWithValue("@DANTE", DANTE);
                    command.Parameters.AddWithValue("@Livrer", Livrer);
                    command.Parameters.AddWithValue("@idpatch", idpatch);


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
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                if (Connection != null)
                    if (Connection.State == System.Data.ConnectionState.Open)
                        Connection.Close();
            }
        }
        public bool ModifStructure(string Nom, string Prix, int Taille, int Flashcode, bool Livrer, int idstructure)
        {
            try
            {
                using (Connection)
                {

                    Connection.Open();
                    MySqlTransaction transaction = Connection.BeginTransaction();

                    string sql = "UPDATE Structure set NOM = @Nom, PRIX = @Prix, FLASHCODE = @Flashcode, TAILLE = @Taille, LIVRER = @Livrer where idstructure = @idstructure";
                    MySqlCommand command = new MySqlCommand(sql, Connection);
                    command.Parameters.AddWithValue("@Nom", Nom);
                    command.Parameters.AddWithValue("@Prix", Prix);
                    command.Parameters.AddWithValue("@Flashcode", Flashcode);
                    command.Parameters.AddWithValue("@Taille", Taille);
                    command.Parameters.AddWithValue("@Livrer", Livrer);
                    command.Parameters.AddWithValue("@idstructure", idstructure);


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
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                if (Connection != null)
                    if (Connection.State == System.Data.ConnectionState.Open)
                        Connection.Close();
            }
        }
        public bool ModifVideo(string Nom, string Prix, int Flashcode, bool Livrer, int idvideo)
        {
            try
            {
                using (Connection)
                {

                    Connection.Open();
                    MySqlTransaction transaction = Connection.BeginTransaction();

                    string sql = "UPDATE video set NOM = @Nom, PRIX = @Prix, FLASHCODE = @Flashcode, LIVRER = @Livrer where idvideo = @idvideo";
                    MySqlCommand command = new MySqlCommand(sql, Connection);
                    command.Parameters.AddWithValue("@Nom", Nom);
                    command.Parameters.AddWithValue("@Prix", Prix);
                    command.Parameters.AddWithValue("@Flashcode", Flashcode);
                    command.Parameters.AddWithValue("@Livrer", Livrer);
                    command.Parameters.AddWithValue("@idvideo", idvideo);


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
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                if (Connection != null)
                    if (Connection.State == System.Data.ConnectionState.Open)
                        Connection.Close();
            }
        }

        public bool DeleteCable(int idcable)
        {
            try
            {
                using (Connection)
                {

                    Connection.Open();
                    MySqlTransaction transaction = Connection.BeginTransaction();

                    string sql = "DELETE from cable where idcable = @idcable";
                    MySqlCommand command = new MySqlCommand(sql, Connection);
                    command.Parameters.AddWithValue("@idcable", idcable);

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
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                if (Connection != null)
                    if (Connection.State == System.Data.ConnectionState.Open)
                        Connection.Close();
            }
        }
        public bool DeleteConsole(int idconsole)
        {
            try
            {
                using (Connection)
                {

                    Connection.Open();
                    MySqlTransaction transaction = Connection.BeginTransaction();

                    string sql = "DELETE from console where idconsole = @idconsole";
                    MySqlCommand command = new MySqlCommand(sql, Connection);
                    command.Parameters.AddWithValue("@idconsole", idconsole);

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
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                if (Connection != null)
                    if (Connection.State == System.Data.ConnectionState.Open)
                        Connection.Close();
            }
        }
        public bool DeleteEnceinte(int idenceinte)
        {
            try
            {
                using (Connection)
                {

                    Connection.Open();
                    MySqlTransaction transaction = Connection.BeginTransaction();

                    string sql = "DELETE from enceinte where idenceinte = @idenceinte";
                    MySqlCommand command = new MySqlCommand(sql, Connection);
                    command.Parameters.AddWithValue("@idenceinte", idenceinte);

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
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                if (Connection != null)
                    if (Connection.State == System.Data.ConnectionState.Open)
                        Connection.Close();
            }
        }
        public bool DeleteLumiere(int idlumiere)
        {
            try
            {
                using (Connection)
                {

                    Connection.Open();
                    MySqlTransaction transaction = Connection.BeginTransaction();

                    string sql = "DELETE from lumiere where idlumiere = @idlumiere";
                    MySqlCommand command = new MySqlCommand(sql, Connection);
                    command.Parameters.AddWithValue("@idlumiere", idlumiere);

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
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                if (Connection != null)
                    if (Connection.State == System.Data.ConnectionState.Open)
                        Connection.Close();
            }
        }
        public bool DeletePatch(int idpatch)
        {
            try
            {
                using (Connection)
                {

                    Connection.Open();
                    MySqlTransaction transaction = Connection.BeginTransaction();

                    string sql = "DELETE from patch where idpatch = @idpatch";
                    MySqlCommand command = new MySqlCommand(sql, Connection);
                    command.Parameters.AddWithValue("@idpatch", idpatch);

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
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                if (Connection != null)
                    if (Connection.State == System.Data.ConnectionState.Open)
                        Connection.Close();
            }
        }
        public bool DeleteStrucutre(int idstructure)
        {
            try
            {
                using (Connection)
                {

                    Connection.Open();
                    MySqlTransaction transaction = Connection.BeginTransaction();

                    string sql = "DELETE from structure where idstructure = @idstructure";
                    MySqlCommand command = new MySqlCommand(sql, Connection);
                    command.Parameters.AddWithValue("@idstructure", idstructure);

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
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                if (Connection != null)
                    if (Connection.State == System.Data.ConnectionState.Open)
                        Connection.Close();
            }
        }
        public bool DeleteVideo(int idvideo)
        {
            try
            {
                using (Connection)
                {

                    Connection.Open();
                    MySqlTransaction transaction = Connection.BeginTransaction();

                    string sql = "DELETE from video where idvideo = @idvideo";
                    MySqlCommand command = new MySqlCommand(sql, Connection);
                    command.Parameters.AddWithValue("@idvideo", idvideo);

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
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                if (Connection != null)
                    if (Connection.State == System.Data.ConnectionState.Open)
                        Connection.Close();
            }
        }

        public List<AjouterMateriel.UnMateriel> ListCable()
        {
            try 
            { 
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    string sql =
                    "SELECT 'cable' as table_name, nom, prix, livrer, flashcode FROM cable ";

                    using (MySqlCommand command = new MySqlCommand(sql, connection))
                    {

                        List<AjouterMateriel.UnMateriel> ListMateriels = new List<AjouterMateriel.UnMateriel>();
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string categorie = Convert.ToString(reader[0]);
                                string nom = Convert.ToString(reader[1]);
                                string prix = Convert.ToString(reader[2]);
                                bool livrer = Convert.ToBoolean(reader[3]);
                                int flashcode = Convert.ToInt32(reader[4]);
                                AjouterMateriel.UnMateriel UnMateriel2 = new AjouterMateriel.UnMateriel(nom, prix, flashcode, categorie, livrer);
                                ListMateriels.Add(UnMateriel2);

                            }
                        }

                        return ListMateriels;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                if (Connection != null)
                    if (Connection.State == System.Data.ConnectionState.Open)
                        Connection.Close();
            }
        }
        public List<AjouterMateriel.UnMateriel> ListConsole()
        {
            try { 
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    string sql =
                    "SELECT 'console' as table_name,id nom, prix, livrer, flashcode FROM console ";


                    using (MySqlCommand command = new MySqlCommand(sql, connection))
                    {
                        List<AjouterMateriel.UnMateriel> ListMateriels = new List<AjouterMateriel.UnMateriel>();

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string categorie = Convert.ToString(reader[0]);
                                string nom = Convert.ToString(reader[1]);
                                string prix = Convert.ToString(reader[2]);
                                bool livrer = Convert.ToBoolean(reader[3]);
                                int flashcode = Convert.ToInt32(reader[4]);
                                AjouterMateriel.UnMateriel UnMateriel2 = new AjouterMateriel.UnMateriel(nom, prix, flashcode, categorie, livrer);
                                ListMateriels.Add(UnMateriel2);


                            }
                        }

                        return ListMateriels;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                if (Connection != null)
                    if (Connection.State == System.Data.ConnectionState.Open)
                        Connection.Close();
            }
        }
        public List<AjouterMateriel.UnMateriel> ListEnceinte()
        {
            try 
            { 
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    string sql =
                    "SELECT 'enceinte' as table_name, nom, prix, livrer, flashcode FROM enceinte ";


                    using (MySqlCommand command = new MySqlCommand(sql, connection))
                    {

                        List<AjouterMateriel.UnMateriel> ListMateriels = new List<AjouterMateriel.UnMateriel>();

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string categorie = Convert.ToString(reader[0]);
                                string nom = Convert.ToString(reader[1]);
                                string prix = Convert.ToString(reader[2]);
                                bool livrer = Convert.ToBoolean(reader[3]);
                                int flashcode = Convert.ToInt32(reader[4]);
                                AjouterMateriel.UnMateriel UnMateriel2 = new AjouterMateriel.UnMateriel(nom, prix, flashcode, categorie, livrer);
                                ListMateriels.Add(UnMateriel2);
                            }
                        }

                        return ListMateriels;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                if (Connection != null)
                    if (Connection.State == System.Data.ConnectionState.Open)
                        Connection.Close();
            }
        }
        public List<AjouterMateriel.UnMateriel> ListLumiere()
        {
            try 
            { 
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    string sql =
                    "SELECT 'lumiere' as table_name, nom, prix, livrer, flashcode FROM lumiere ";


                    using (MySqlCommand command = new MySqlCommand(sql, connection))
                    {
                        List<AjouterMateriel.UnMateriel> ListMateriels = new List<AjouterMateriel.UnMateriel>();

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string categorie = Convert.ToString(reader[0]);
                                string nom = Convert.ToString(reader[1]);
                                string prix = Convert.ToString(reader[2]);
                                bool livrer = Convert.ToBoolean(reader[3]);
                                int flashcode = Convert.ToInt32(reader[4]);
                                AjouterMateriel.UnMateriel UnMateriel2 = new AjouterMateriel.UnMateriel(nom, prix, flashcode, categorie, livrer);
                                ListMateriels.Add(UnMateriel2);

                            }
                        }

                        return ListMateriels;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                if (Connection != null)
                    if (Connection.State == System.Data.ConnectionState.Open)
                        Connection.Close();
            }
        }
        public List<AjouterMateriel.UnMateriel> ListPatch()
        {
            try 
            { 
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    string sql =
                    "SELECT 'patch' as table_name, nom, prix, livrer, flashcode FROM patch ";


                    using (MySqlCommand command = new MySqlCommand(sql, connection))
                    {

                        List<AjouterMateriel.UnMateriel> ListMateriels = new List<AjouterMateriel.UnMateriel>();

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string categorie = Convert.ToString(reader[0]);
                                string nom = Convert.ToString(reader[1]);
                                string prix = Convert.ToString(reader[2]);
                                bool livrer = Convert.ToBoolean(reader[3]);
                                int flashcode = Convert.ToInt32(reader[4]);
                                AjouterMateriel.UnMateriel UnMateriel2 = new AjouterMateriel.UnMateriel(nom, prix, flashcode, categorie, livrer);
                                ListMateriels.Add(UnMateriel2);

                            }
                        }

                        return ListMateriels;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                if (Connection != null)
                    if (Connection.State == System.Data.ConnectionState.Open)
                        Connection.Close();
            }

        }
        public List<AjouterMateriel.UnMateriel> ListStructure()
        {
            try 
            { 
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {

                    connection.Open();

                    string sql =
                    "SELECT 'structure' as table_name, nom, prix, livrer, flashcode FROM structure;";


                    using (MySqlCommand command = new MySqlCommand(sql, connection))
                    {

                        List<AjouterMateriel.UnMateriel> ListMateriels = new List<AjouterMateriel.UnMateriel>();

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string categorie = Convert.ToString(reader[0]);
                                string nom = Convert.ToString(reader[1]);
                                string prix = Convert.ToString(reader[2]);
                                bool livrer = Convert.ToBoolean(reader[3]);
                                int flashcode = Convert.ToInt32(reader[4]);
                                AjouterMateriel.UnMateriel UnMateriel2 = new AjouterMateriel.UnMateriel(nom, prix, flashcode, categorie, livrer);
                                ListMateriels.Add(UnMateriel2);

                            }
                        }

                        return ListMateriels;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                if (Connection != null)
                    if (Connection.State == System.Data.ConnectionState.Open)
                        Connection.Close();
            }
        }
        public List<AjouterMateriel.UnMateriel> ListVideo()
        {
            try 
            { 
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    string sql =
                    "SELECT 'video' as table_name, nom, prix, livrer, flashcode FROM video ";


                    using (MySqlCommand command = new MySqlCommand(sql, connection))
                    {

                        List<AjouterMateriel.UnMateriel> ListMateriels = new List<AjouterMateriel.UnMateriel>();
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string categorie = Convert.ToString(reader[0]);
                                string nom = Convert.ToString(reader[1]);
                                string prix = Convert.ToString(reader[2]);
                                bool livrer = Convert.ToBoolean(reader[3]);
                                int flashcode = Convert.ToInt32(reader[4]);
                                AjouterMateriel.UnMateriel UnMateriel2 = new AjouterMateriel.UnMateriel(nom, prix, flashcode, categorie, livrer);
                                ListMateriels.Add(UnMateriel2);

                            }
                        }

                        return ListMateriels;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                if (Connection != null)
                    if (Connection.State == System.Data.ConnectionState.Open)
                        Connection.Close();
            }
        }

        public void Dispose()
        {
            if (Connection != null)
            {
                Connection.Dispose();
                Connection = null;
            }
        }


    }
}
