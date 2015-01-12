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

        #region Livres
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
        #endregion

        #region CD
        private List<Musique> getMusiquesFromCD(int identifiant)
        {
            // dbConnection.Open();

            List<Musique> musiques = new List<Musique>();

            try
            {
                string sql_selectMusiques = "SELECT * FROM Musique where Musique.FK_CD = " + identifiant;
                SQLiteCommand command = new SQLiteCommand(sql_selectMusiques, dbConnection);
                SQLiteDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    int id = reader.GetInt32(0);//.HasValue ? reader.GetInt32(0).Value : -1;
                    string titre = reader["Titre"] as string;
                    int numero = reader.GetInt32(2);

                    Musique musique = new Musique(titre, numero);
                    musiques.Add(musique);
                }

                reader.Close();
            }
            catch (Exception)
            {
                musiques = null;
            }

            //dbConnection.Close();

            return musiques;
        }

        private void ajouterMusique(Musique musique, int idCD)
        {
            try
            {
                string sql_insertMusique = "INSERT INTO Musique(FK_CD, Numero, Titre) VALUES(@fkcd, @Numero, @Titre);";
                SQLiteCommand command = new SQLiteCommand(sql_insertMusique, dbConnection);
                command.Parameters.AddWithValue("@fkcd", idCD);
                command.Parameters.AddWithValue("@Numero", musique.Numero);
                command.Parameters.AddWithValue("@Titre", musique.Titre);

                SQLiteDataReader reader = command.ExecuteReader();
            }
            catch (Exception)
            {

            }
        }
        
        public IEnumerable<CD> getCDs()
        {
            dbConnection.Open();
            List<CD> CDs = new List<CD>();

            try
            {
                string sql_selectAllCDs = "SELECT * FROM CD";
                SQLiteCommand command = new SQLiteCommand(sql_selectAllCDs, dbConnection);
                SQLiteDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    int id = reader.GetInt32(0);//.HasValue ? reader.GetInt32(0).Value : -1;
                    string titre = reader["Titre"] as string;
                    int nbr = (reader["Nombre"] as int?).Value;
                    string artiste = reader["Artiste"] as string;
                    Style style;
                    Enum.TryParse(reader["Style"] as string, true, out style);

                    CD CD = new CD(id, titre, nbr, artiste, style, getMusiquesFromCD(id));
                    CDs.Add(CD);
                }

                reader.Close();
            }
            catch (Exception)
            {
                CDs = null;
            }

            dbConnection.Close();

            return CDs;
        }

        
        public bool ajouterCD(CD CD)
        {
            dbConnection.Open();

            string sqlInsert_CD = "INSERT INTO CD(Titre, Nombre, Artiste, Style) VALUES(@Titre, @Nombre, @Artiste, @Style);";
            SQLiteCommand command = new SQLiteCommand(sqlInsert_CD, dbConnection);
            command.Parameters.Add(new SQLiteParameter("@Titre", CD.Titre));
            command.Parameters.Add(new SQLiteParameter("@Nombre", CD.NombreEnStock));
            command.Parameters.Add(new SQLiteParameter("@Artiste", CD.Artiste));
            command.Parameters.Add(new SQLiteParameter("@Style", CD.Style.ToString()));

            bool result = false;
            try
            {
                int nbrRowsInserted = command.ExecuteNonQuery();

                result = (nbrRowsInserted >= 1);
            }
            catch (Exception e)
            {
                result = false;
            }

            foreach (var musique in CD.Musiques)
            {
                ajouterMusique(musique, CD.IdentifiantUnique);
            }

            dbConnection.Close();

            return result;
        }
        /*
        public bool setNombreEnStock_CD(string titre, int nombre)
        {
            dbConnection.Open();

            string sqlInsert_CD = "UPDATE CD SET Nombre = @Nombre";// WHERE Titre = @Titre";
            SQLiteCommand command = new SQLiteCommand(sqlInsert_CD, dbConnection);
            command.Parameters.Add(new SQLiteParameter("@Titre", titre));
            command.Parameters.Add(new SQLiteParameter("@Nombre", nombre));

            int result = command.ExecuteNonQuery();

            dbConnection.Close();

            return (result >= 1);
        }
        */
        #endregion
    }
}