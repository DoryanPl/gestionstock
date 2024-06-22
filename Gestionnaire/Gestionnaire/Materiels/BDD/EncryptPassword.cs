using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography.X509Certificates;



namespace Gestionnaire.Materiels.BDD
{
    class EncryptPassword
    {
        private static string path;

        public static string Path { get => path; set => path = value; }

        public static void CreateKey()
        {
           Path = "D:/PC/Documents/Cours/2e_BTS/gestionstock/wpf-client/Gestionnaire/Gestionnaire/Gestionnaire_login/";

            using (var rsa = new RSACryptoServiceProvider(2048))
            {
                // Sauvegarde des clés dans des fichiers XML
               File.WriteAllText("private.xml", rsa.ToXmlString(true));
                File.WriteAllText("public.xml", rsa.ToXmlString(false));
            }
        }
    }
}
