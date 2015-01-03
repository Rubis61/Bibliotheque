using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.SQLite;
using System.Configuration;

namespace Projet_Bibliothèque_Livre_CD
{
    public class BDD
    {
        private static readonly string connectionString = ConfigurationManager.ConnectionStrings["BibliothequeDBConnectionString"].ConnectionString;
        private static SQLiteConnection dbConnection = new SQLiteConnection(connectionString);

        private static SQLiteCommand command;

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
                    int id = reader.GetInt32(0);//.HasValue ? reader.GetInt32(0).Value : -1;
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
            catch (Exception)
            {
                livres = null;
            }

            dbConnection.Close();

            return livres;
        }

        public bool ajouterLivre(Livre livre)
        {
            dbConnection.Open();

            string sqlInsert_Livre = "INSERT INTO Livre(Titre, Nombre, Auteur, NumeroISBN, Genre) VALUES(@Titre, @Nombre, @Auteur, @ISBN, @Genre);";
            SQLiteCommand command = new SQLiteCommand(sqlInsert_Livre, dbConnection);
            command.Parameters.Add(new SQLiteParameter("@Titre", livre.Titre));
            command.Parameters.Add(new SQLiteParameter("@Nombre", livre.NombreEnStock));
            command.Parameters.Add(new SQLiteParameter("@Auteur", livre.AuteurDuLivre));
            command.Parameters.Add(new SQLiteParameter("@ISBN", livre.NumeroISBN));
            command.Parameters.Add(new SQLiteParameter("@Genre", livre.Genre.ToString()));

            bool result = false;
            try
            {
                int nbrRowsInserted = command.ExecuteNonQuery();

                result = (nbrRowsInserted>=1);
            }
            catch (Exception e)
            {
                result = false;
                throw;
            }

            dbConnection.Close();

            return result;
        }

        public bool setNombreEnStock_Livre(string titre, int nombre)
        {
            dbConnection.Open();

            string sqlInsert_Livre = "UPDATE Livre SET Nombre = @Nombre";// WHERE Titre = @Titre";
            SQLiteCommand command = new SQLiteCommand(sqlInsert_Livre, dbConnection);
            command.Parameters.Add(new SQLiteParameter("@Titre", titre));
            command.Parameters.Add(new SQLiteParameter("@Nombre", nombre));

            int result = command.ExecuteNonQuery();

            dbConnection.Close();

            return (result >= 1);
        }
    }
}