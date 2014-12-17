using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_Bibliothèque_Livre_CD
{
    enum Style
    {
        Classique = 0,
        Rock,
        Metal,
        // etc...
    }

    class CD : ContenuMultimedia
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

        public static string[] getStylesDisponibles()
        {
            return Enum.GetNames(typeof(Style));
        }
    }
}
