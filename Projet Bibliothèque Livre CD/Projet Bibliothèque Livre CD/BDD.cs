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
            List<Livre> livres = new List<Livre>();
            try
            {
                dbConnection.Open();

                string sql_selectAllLivres = "SELECT * FROM Livre";
                SQLiteCommand command = new SQLiteCommand(sql_selectAllLivres, dbConnection);
                SQLiteDataReader reader = command.ExecuteReader();

                while (reader.Read())
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

        public IEnumerable<Livre> getLivresEmpruntés()
        {
            List<Livre> livres = new List<Livre>();
            try
            {
                dbConnection.Open();

                string sql_selectAllLivres = "SELECT * FROM Livre WHERE NbEmprunts >= 1";
                SQLiteCommand command = new SQLiteCommand(sql_selectAllLivres, dbConnection);
                SQLiteDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    int id = reader.GetInt32(0);//.HasValue ? reader.GetInt32(0).Value : -1;
                    string titre = reader["Titre"] as string;
                    int nbr = (reader["NbEmprunts"] as int?).Value;
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

        public bool AjouterLivre(Livre livre)
        {
            bool result = false;
            try
            {
                dbConnection.Open();

                string sqlInsert_Livre = "INSERT INTO Livre(Titre, Nombre, Auteur, NumeroISBN, Genre) VALUES(@Titre, @Nombre, @Auteur, @ISBN, @Genre);";
                SQLiteCommand command = new SQLiteCommand(sqlInsert_Livre, dbConnection);
                command.Parameters.AddWithValue("@Titre", livre.Titre);
                command.Parameters.AddWithValue("@Nombre", livre.NombreEnStock);
                command.Parameters.AddWithValue("@Auteur", livre.AuteurDuLivre);
                command.Parameters.AddWithValue("@ISBN", livre.NumeroISBN);
                command.Parameters.AddWithValue("@Genre", livre.Genre.ToString());

                int nbrRowsInserted = command.ExecuteNonQuery();
                result = (nbrRowsInserted >= 1);
            }
            catch
            {
                result = false;
            }

            dbConnection.Close();

            return result;
        }

        public bool EmprunterUnLivre(string titre)
        {
            bool result = false;
            try
            {
                dbConnection.Open();

                string updateLivreEmprunter = "UPDATE Livre SET Nombre = Nombre - 1, NbEmprunts = NbEmprunts + 1  WHERE Titre = @Titre";
                SQLiteCommand command = new SQLiteCommand(updateLivreEmprunter, dbConnection);
                command.Parameters.AddWithValue("@Titre", titre);

                int nbrRowsInserted = command.ExecuteNonQuery();
                result = (nbrRowsInserted >= 1);
            }
            catch
            {
                result = false;
            }

            dbConnection.Close();
            return result;

        }

        public bool SupprimerUnLivre(string titre)
        {
            bool result = false;
            try
            {
                dbConnection.Open();

                string supprimerLivre = "DELETE FROM Livre WHERE Titre = @Titre";
                SQLiteCommand command = new SQLiteCommand(supprimerLivre, dbConnection);
                command.Parameters.AddWithValue("@Titre", titre);

                int nbrRowsInserted = command.ExecuteNonQuery();
                result = (nbrRowsInserted >= 1);
            }
            catch
            {
                result = false;
            }

            dbConnection.Close();
            return result;
        }

        public bool RamenerUnLivre(string livre)
        {
            bool result;
            try
            {              
                dbConnection.Open();
                string sql_UpdateNombreLivre = "UPDATE Livre SET Nombre = Nombre + 1, NbEmprunts = NbEmprunts - 1 WHERE Livre.Titre =  @Titre";
                command = new SQLiteCommand(sql_UpdateNombreLivre, dbConnection);
                command.Parameters.AddWithValue("@Titre", livre);
                command.ExecuteNonQuery();
                result = true;
            }
            catch (Exception)
            {
                result =  false;
            }
            dbConnection.Close();
            return result;
        }

        public Livre RechercherLivre(string titreLivre)
        {
            Livre livre = null;
            try
            {
                dbConnection.Open();

                string sql_SelectLivre = "SELECT * FROM Livre WHERE Livre.Titre = @Titre";
                command = new SQLiteCommand(sql_SelectLivre, dbConnection);
                command.Parameters.AddWithValue("@Titre", titreLivre);

                SQLiteDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    int id = reader.GetInt32(0);
                    string titre = reader["Titre"] as string;
                    int nbr = (reader["Nombre"] as int?).Value;
                    string auteur = reader["Auteur"] as string;
                    string ISBN = reader["NumeroISBN"] as string;
                    GenreDuLivre genre;
                    Enum.TryParse(reader["Genre"] as string, true, out genre);
                    livre = new Livre(id, titre, nbr, ISBN, auteur, genre);
                }
            }
            catch (Exception)
            {
                livre = null;
            }

            dbConnection.Close();

            return livre;
        }
        #endregion

        #region CD
        private List<Musique> getMusiquesFromCD(int identifiant)
        {
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

                command.ExecuteNonQuery();
            }
            catch (Exception)
            {
            }
        }
        
        public IEnumerable<CD> getCDs()
        {
            List<CD> CDs = new List<CD>();

            try
            {
                dbConnection.Open();

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

        public IEnumerable<CD> getCDsEmpruntés()
        {
            List<CD> CDs = new List<CD>();

            try
            {
                dbConnection.Open();

                string sql_selectAllCDs = "SELECT * FROM CD WHERE NbEmprunts >= 1;";
                SQLiteCommand command = new SQLiteCommand(sql_selectAllCDs, dbConnection);
                SQLiteDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    int id = reader.GetInt32(0);//.HasValue ? reader.GetInt32(0).Value : -1;
                    string titre = reader["Titre"] as string;
                    int nbr = (reader["NbEmprunts"] as int?).Value;
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

        public bool ramenerUnCd(string titre)
        {
            bool result = false;
            try
            {
                dbConnection.Open();

                string sql_UpdateNombreCd = "UPDATE CD SET Nombre = Nombre + 1, NbEmprunts = NbEmprunts - 1 WHERE Titre = @Titre AND NbEmprunts >= 1;";
                command = new SQLiteCommand(sql_UpdateNombreCd, dbConnection);
                command.Parameters.AddWithValue("@Titre", titre);

                int nbrRowsUpdated = command.ExecuteNonQuery();
                result = (nbrRowsUpdated >= 1);
            }
            catch (Exception)
            {
                result = false;
            }

            dbConnection.Close();
            return result;
        }

        public CD rechercherCD(string titreCD)
        {
            CD cd = null;

            try
            {
                dbConnection.Open();

                string sql_SelectCD = "SELECT * FROM CD WHERE CD.Titre = @Titre";
                command = new SQLiteCommand(sql_SelectCD, dbConnection);
                command.Parameters.AddWithValue("@Titre", titreCD);

                SQLiteDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    int id = reader.GetInt32(0);
                    string titre = reader["Titre"] as string;
                    int nbr = (reader["Nombre"] as int?).Value;
                    string artiste = reader["Artiste"] as string;
                    Style style;
                    Enum.TryParse(reader["Style"] as string, true, out style);

                    cd = new CD(id, titre, nbr, artiste, style, getMusiquesFromCD(id));
                }
                reader.Close();
            }
            catch (Exception)
            {
                cd = null;
            }

            dbConnection.Close();
            return cd;
        }
        
        public bool ajouterCD(CD CD)
        {
            bool result = false;
            try
            {
                dbConnection.Open();

                string sqlInsert_CD = "INSERT INTO CD(Titre, Nombre, Artiste, Style) VALUES(@Titre, @Nombre, @Artiste, @Style);";
                SQLiteCommand command = new SQLiteCommand(sqlInsert_CD, dbConnection);
                command.Parameters.AddWithValue("@Titre", CD.Titre);
                command.Parameters.AddWithValue("@Nombre", CD.NombreEnStock);
                command.Parameters.AddWithValue("@Artiste", CD.Artiste);
                command.Parameters.AddWithValue("@Style", CD.Style.ToString());

                int nbrRowsInserted = command.ExecuteNonQuery();

                result = (nbrRowsInserted >= 1);
            }
            catch (Exception)
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

        public bool EmprunterUnCD(string titre)
        {
            bool result = false;
            try
            {
                dbConnection.Open();

                string updateCDEmprunter = "UPDATE CD SET Nombre = Nombre - 1, NbEmprunts = NbEmprunts + 1  WHERE Titre = @Titre";
                SQLiteCommand command = new SQLiteCommand(updateCDEmprunter, dbConnection);
                command.Parameters.AddWithValue("@Titre", titre);

                int nbrRowsInserted = command.ExecuteNonQuery();
                result = (nbrRowsInserted >= 1);
            }
            catch
            {
                result = false;
            }

            dbConnection.Close();
            return result;
        }

        public bool SupprimerUnCD(string titre)
        {
            bool result = false;
            try
            {
                dbConnection.Open();

                string supprimerCD = "DELETE FROM CD WHERE Titre = @Titre";
                SQLiteCommand command = new SQLiteCommand(supprimerCD, dbConnection);
                command.Parameters.AddWithValue("@Titre", titre);

                int nbrRowsInserted = command.ExecuteNonQuery();
                result = (nbrRowsInserted >= 1);
            }
            catch
            {
                result = false;
            }

            dbConnection.Close();
            return result;
        }
        #endregion
    }
}