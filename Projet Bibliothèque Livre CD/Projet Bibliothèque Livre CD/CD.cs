using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_Bibliothèque_Livre_CD
{
    public enum Style
    {
        Classique = 0,
        Rock,
        Metal,
        // etc...
    }

    public class CD : ContenuMultimedia
    {
        public string Artiste { get; set; }
        public Style Style { get; set; }
        public List<Musique> Musiques { get; set; }

        public CD(int id, string titre, int nombreEnStock, string artiste, Style style, List<Musique> musiques)
            : base(id, titre, nombreEnStock)
        {
            Artiste = artiste;
            Style = style;
            Musiques = musiques;
        }

        /// <summary>
        /// Constructeur de copie
        /// </summary>
        /// <param name="cd"></param>
        public CD(CD cd)
            : base(cd.IdentifiantUnique, cd.Titre, cd.NombreEnStock)
        {
            Artiste = cd.Artiste;
            Style = cd.Style;
            Musiques = cd.Musiques;
        }

        public override string ToString()
        {
            return "Titre : " + Titre + ", Nombre : " + NombreEnStock + ", Artiste : " + Artiste +
                   ", Style : " + Style.ToString();
        }

        public static string[] getStylesDisponibles()
        {
            return Enum.GetNames(typeof(Style));
        }
        
        public static List<Musique> remplirListeDeMusique()
        {
            bool result;
            string valeur;
            int intParse;
            List<Musique> maListe = new List<Musique>();

            do
            {
                Console.WriteLine("Combien y'a t'il de musique dans votre album ( 1 à 12 )");
                valeur = Console.ReadLine().ToString();
                result = Int32.TryParse(valeur, out intParse);
            }
            while(result == false || intParse >= 12);

            for(int i = 0; i < intParse; i++)
            {
                int y = i + 1;
                Console.WriteLine("Quel est le titre de la musique " + y);
                Musique maMusique = new Musique(Console.ReadLine().ToString(), i+1);
                maListe.Add(maMusique);
            }
            return maListe;
        }
    }
}
