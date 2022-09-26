using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Chess
{
    public class Piece
    {
        public int Column { get; set; }
        public int Ligne { get; set; }
        public bool EstBlanc;
        public string Name;
        public Image Pictogramme;

        public Piece(Image image, bool estblanc)
        {
            Pictogramme = image;
            EstBlanc = estblanc;
        }

        public Piece(Image image, bool estblanc,int ligne,int column)
        {
            Pictogramme = image;
            EstBlanc = estblanc;
            Ligne=ligne;
            Column=column;
        }

        public virtual List<string> Deplacement() // cette méthode est crée pour être override dans les classes dépandente de piece
        {
            return null;
        }

        public override string ToString() // renvoye le nom de la piece avec les déplacement possibles sans les limitation des autres pieces
        {
            string Tostring = Name + ". Déplaçable en ";
            foreach (string item in Deplacement())
            {
                Tostring += item + " ";
            }
            return Tostring;
        }
    }

    public class Roi:Piece // classe dépandente de pièce pour le roi
    {
        public Roi(Image image, bool estblanc) :base(image, estblanc) // constructeur de base
        {
            Name = "Roi";
            if (estblanc) Name += " blanc";
            else Name += " noir";
            EstBlanc= estblanc;
            Pictogramme = image;
        }

        public Roi(Image image, bool estblanc,int ligne,int column) :base(image, estblanc,ligne,column) // constructeur avec la postion du roi
        {
            Name = "Roi";
            if (estblanc) Name += " blanc";
            else Name += " noir";
            EstBlanc= estblanc;
            Pictogramme = image;
            Ligne = ligne;
            Column = column;
        }

        public override List<string> Deplacement() // calcule les déplacements possible pour la piece sans collision
        {
            List<string> listDeplacement = new List<string>();
            if (Ligne+1<8)
            {
                if (Column +1<8){listDeplacement.Add(Convert.ToString(Ligne + 1) + Convert.ToString(Column + 1) + " ");}
                if (Column -1>-1){listDeplacement.Add(Convert.ToString(Ligne + 1) + Convert.ToString(Column - 1) + " ");}
                listDeplacement.Add(Convert.ToString(Ligne + 1) + Convert.ToString(Column));
            }
            if (Ligne - 1 > -1)
            {
                if (Column + 1 < 8){listDeplacement.Add(Convert.ToString(Ligne - 1) + Convert.ToString(Column + 1) + " ");}
                if (Column - 1 > -1){listDeplacement.Add(Convert.ToString(Ligne - 1) + Convert.ToString(Column - 1) + " ");}
                listDeplacement.Add(Convert.ToString(Ligne - 1) + Convert.ToString(Column));
            }
            if (Column + 1 < 8){listDeplacement.Add(Convert.ToString(Ligne) + Convert.ToString(Column + 1) + " ");}
            if (Column - 1 > -1){listDeplacement.Add(Convert.ToString(Ligne) + Convert.ToString(Column - 1) + " ");}
            return listDeplacement;
        }
    }

    public class Tour:Piece
    {
        public Tour(Image image, bool estblanc) : base(image, estblanc) // constructeur de base
        {
            Name = "Tour";
            if (estblanc) Name += " blanc";
            else Name += " noir";
            EstBlanc = estblanc;
            Pictogramme = image;
        }
        public Tour(Image image, bool estblanc, int ligne, int column) : base(image, estblanc, ligne, column) // constructeur avec la postion de la tour
        {
            Name = "Tour";
            if (estblanc) Name += " blanc";
            else Name += " noir";
            EstBlanc = estblanc;
            Pictogramme = image;
            Ligne = ligne;
            Column = column;
        }

        public override List<string> Deplacement() // calcule les déplacement possible pour une tour
        {
            List<string> listDeplacement = new List<string>();
            string deplacement="";
            int i = 1;

            while (Column - i > -1) // calcule vers la gauche
            {
                deplacement += Convert.ToString(Ligne) + Convert.ToString(Column - i) + " ";
                i++;
            }
            i = 1;
            if (deplacement != "") { listDeplacement.Add(deplacement); deplacement = ""; }

            while (Column + i < 8) // calcule vers la droite
            {
                deplacement += Convert.ToString(Ligne) + Convert.ToString(Column + i) + " ";
                i++;
            }
            i = 1;
            if (deplacement != ""){ listDeplacement.Add(deplacement); deplacement = ""; }

            while (Ligne + i < 8) // calcule vers le haut
            {
                deplacement += Convert.ToString(Ligne + i) + Convert.ToString(Column) + " ";
                i++;
            }
            i = 1;
            if (deplacement != "") { listDeplacement.Add(deplacement); deplacement = ""; }

            while (Ligne - i > -1) // calcule vers le bas
            {
                deplacement += Convert.ToString(Ligne - i) + Convert.ToString(Column) + " ";
                i++;
            }
            if (deplacement != "") { listDeplacement.Add(deplacement); }
            return listDeplacement;
        }
    }
}