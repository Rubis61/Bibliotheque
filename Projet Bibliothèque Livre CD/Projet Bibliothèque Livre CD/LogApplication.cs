using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Projet_Bibliothèque_Livre_CD
{
    public class LogApplication
    {
        FileStream stream;
        public bool CréerUnFichierTexte(string path)
        {
            if (File.Exists(path)) return true;

            try
            {
                stream = File.Create(path);
                stream.Close();
                return true;
            }
            catch(Exception)
            {
                Console.WriteLine("Accès refusé ou chemin d'accès non valide !");
                Console.ReadKey();
           
                return false;
            }
        }
        public void WriteMessage(string message)
        {
               using (FileStream stream = File.Open(Menu.pathOfLog, FileMode.Append, FileAccess.Write))
              {
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    writer.WriteLine(message);
                    writer.Close();
                }
              }
        }
    }
}

