using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.SQLite;
using System.Configuration;

namespace Projet_Bibliothèque_Livre_CD
{
    class BDD
    {
        static readonly string connectionString = ConfigurationManager.ConnectionStrings["BibliothequeDBConnectionString"].ConnectionString;
        SQLiteConnection dbConnection = new SQLiteConnection(connectionString);

        public BDD() 
        { }

        public IEnumerable<Livre> getLivres()
        {
            dbConnection.Open();
            List<Livre> livres = new List<Livre>();

            try
            {
                string sql_selectAllLivres = "SELECT * FROM Livre";
                SQLiteCommand command = new SQLiteCommand(sql_selectAllLivres, dbConnection);
                SQLiteDataReader reader = command.ExecuteReader();

                while(reader.Read())
                {
                    int id = (reader["Id"] as int?).HasValue ? (reader["Id"] as int?).Value : -1;
                    string titre = reader["Titre"] as string;
                    int nbr = (reader["Nombre"] as int?).Value;
                    string auteur = reader["Auteur"] as string;
                    string ISBN = reader["NumeroISBN"] as string;
                    GenreDuLivre genre;
                    Enum.TryParse(reader["Genre"] as string, true, out genre);

                    Livre livre = new Livre(id, titre, nbr, ISBN, auteur, genre);
                    livres.Add(livre);
                }

                reader.Close();
            }
            catch (Exception e)
            {
                livres = null;
            }

            dbConnection.Close();

            return livres;
        }
    }
}