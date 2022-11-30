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
        public bool EstBlanc;
        public string Name;
        public Image Pictogramme;

        public Piece(Image image, bool estblanc)
        {
            Pictogramme = image;
            EstBlanc = estblanc;
        }

        public Piece(Image image, bool estblanc, int ligne, int column)
        {
            Pictogramme = image;
            EstBlanc = estblanc;
        }

        public virtual List<string> Deplacement(int ligne,int colonne) // cette méthode est crée pour être override dans les classes dépandente de piece
        {
            return null;
        }

        public string ToString(int ligne,int colonne) // renvoye le nom de la piece avec les déplacement possibles sans les limitation des autres pieces
        {
            string Tostring = Name + ". Déplaçable en ";
            foreach (string item in Deplacement(ligne, colonne))
            {
                Tostring += item + " ";
            }
            return Tostring;
        }

        public List<string> Diagonale(int ligne, int colonne)
        {
            List<string> listDeplacement = new List<string>();
            string deplacement = "";
            int i = 1;

            while (colonne - i >= 0 && ligne - i >= 0) // calcule vers en bas à gauche
            {
                deplacement += $"{ligne - i}{colonne - i} ";
                i++;
            }
            i = 1;
            if (deplacement != "") { listDeplacement.Add(deplacement); deplacement = ""; }

            while (colonne + i <= 7 && ligne + i <= 7) // calcule vers la droite
            {
                deplacement += $"{ligne + i}{colonne + i} ";
                i++;
            }
            i = 1;
            if (deplacement != "") { listDeplacement.Add(deplacement); deplacement = ""; }

            while (colonne - i >= 0 && ligne + i <= 7) // calcule vers le haut
            {
                deplacement += $"{ligne + i}{colonne - i} ";
                i++;
            }
            i = 1;
            if (deplacement != "") { listDeplacement.Add(deplacement); deplacement = ""; }

            while (colonne + i <= 7 && ligne - i >= 0) // calcule vers le bas
            {
                deplacement += $"{ligne - i}{colonne + i} ";
                i++;
            }
            if (deplacement != "") { listDeplacement.Add(deplacement); }
            return listDeplacement;
        }

        public List<string> Croix(int ligne, int colonne)
        {
            List<string> listDeplacement = new List<string>();
            string deplacement = "";
            int i = 1;

            while (colonne - i > -1) // calcule vers la gauche
            {
                deplacement += $"{ligne}{colonne - i} ";
                i++;
            }
            i = 1;
            if (deplacement != "") {listDeplacement.Add(deplacement);deplacement = "";}

            while (colonne + i < 8) // calcule vers la droite
            {
                deplacement += $"{ligne}{colonne + i} ";
                i++;
            }
            i = 1;
            if (deplacement != "") { listDeplacement.Add(deplacement); deplacement = ""; }

            while (ligne + i < 8) // calcule vers le haut
            {
                deplacement += $"{ligne + i}{colonne} ";
                i++;
            }
            i = 1;
            if (deplacement != "") { listDeplacement.Add(deplacement); deplacement = ""; }

            while (ligne - i > -1) // calcule vers le bas
            {
                deplacement += $"{ligne - i}{colonne} ";
                i++;
            }
            if (deplacement != "") { listDeplacement.Add(deplacement); }
            return listDeplacement;
        }
    }

    public class Roi : Piece // classe dépandente de pièce pour le roi
    {
        public bool hasMoved;
        public Roi(Image image, bool estblanc) : base(image, estblanc) // constructeur de base
        {
            Name = "Roi";
            if (estblanc) Name += " blanc";
            else Name += " noir";
            EstBlanc = estblanc;
            Pictogramme = image;
            hasMoved = false;
        }

        public Roi(Image image, bool estblanc, int ligne, int column) : base(image, estblanc, ligne, column) // constructeur avec la postion du roi
        {
            Name = "Roi";
            if (estblanc) Name += " blanc";
            else Name += " noir";
            EstBlanc = estblanc;
            Pictogramme = image;
            hasMoved = false;
        }

        public override List<string> Deplacement(int ligne, int colonne) // calcule les déplacements possible pour la piece sans collision
        {
            List<string> listDeplacement = new List<string>();
            if (ligne + 1 < 8)
            {
                if (colonne + 1 < 8) { listDeplacement.Add($"{ligne + 1}{colonne +1} "); }
                if (colonne - 1 > -1) { listDeplacement.Add($"{ligne + 1}{colonne - 1} "); }
                listDeplacement.Add($"{ligne + 1}{colonne} ");
            }
            if (ligne - 1 > -1)
            {
                if (colonne + 1 < 8) { listDeplacement.Add($"{ligne - 1}{colonne + 1} "); }
                if (colonne - 1 > -1) { listDeplacement.Add($"{ligne - 1}{colonne - 1} "); }
                listDeplacement.Add($"{ligne - 1}{colonne} ");
            }
            if (colonne + 1 < 8) { listDeplacement.Add($"{ligne}{colonne + 1} "); }
            if (colonne - 1 > -1) { listDeplacement.Add($"{ligne}{colonne - 1} "); }
            return listDeplacement;
        }
    }

    public class Tour : Piece
    {
        public bool hasMoved;
        public Tour(Image image, bool estblanc) : base(image, estblanc) // constructeur de base
        {
            Name = "Tour";
            if (estblanc) Name += " blanc";
            else Name += " noir";
            EstBlanc = estblanc;
            Pictogramme = image;
            hasMoved = false;
        }
        public Tour(Image image, bool estblanc, int ligne, int column) : base(image, estblanc, ligne, column) // constructeur avec la postion de la tour
        {
            Name = "Tour";
            if (estblanc) Name += " blanc";
            else Name += " noir";
            EstBlanc = estblanc;
            Pictogramme = image;
            hasMoved = false;
        }

        public override List<string> Deplacement(int ligne, int colonne) // calcule les déplacement possible pour une tour
        {
            return Croix(ligne, colonne);
        }
    }

    public class Cheval : Piece
    {
        public Cheval(Image image, bool estblanc) : base(image, estblanc) // constructeur de base
        {
            Name = "Cheval";
            if (estblanc) Name += " blanc";
            else Name += " noir";
            EstBlanc = estblanc;
            Pictogramme = image;
        }
        public Cheval(Image image, bool estblanc, int ligne, int column) : base(image, estblanc, ligne, column) // constructeur avec la postion de la tour
        {
            Name = "Cheval";
            if (estblanc) Name += " blanc";
            else Name += " noir";
            EstBlanc = estblanc;
            Pictogramme = image;
        }
        public override List<string> Deplacement(int ligne, int colonne) // calcule les déplacement possible pour une tour
        {
            List<string> listDeplacement = new List<string>();
            if (ligne + 2 <= 7)
            {
                if (colonne + 1 <= 7)
                {
                    listDeplacement.Add($"{ligne + 2}{colonne + 1} ");
                }
                if (colonne - 1 >= 0)
                {
                    listDeplacement.Add($"{ligne + 2}{colonne + -1} ");
                }
            }

            if (ligne - 2 >= 0)
            {
                if (colonne + 1 <= 7)
                {
                    listDeplacement.Add($"{ligne - 2}{colonne + 1} ");
                }
                if (colonne - 1 >= 0)
                {
                    listDeplacement.Add($"{ligne - 2}{colonne + -1} ");
                }
            }

            if (colonne + 2 <= 7)
            {
                if (ligne + 1 <= 7)
                {
                    listDeplacement.Add($"{ligne + 1}{colonne + 2} ");
                }
                if (ligne - 1 >= 0)
                {
                    listDeplacement.Add($"{ligne - 1}{colonne + 2} ");
                }
            }

            if (colonne - 2 >= 0)
            {
                if (ligne + 1 <= 7)
                {
                    listDeplacement.Add($"{ligne + 1}{colonne - 2} ");
                }
                if (ligne - 1 >= 0)
                {
                    listDeplacement.Add($"{ligne - 1}{colonne - 2} ");
                }
            }

            return listDeplacement;
        }
    }
    public class Fou : Piece
    {
        public Fou(Image image, bool estblanc) : base(image, estblanc) // constructeur de base
        {
            Name = "Fou";
            if (estblanc) Name += " blanc";
            else Name += " noir";
            EstBlanc = estblanc;
            Pictogramme = image;
        }
        public Fou(Image image, bool estblanc, int ligne, int column) : base(image, estblanc, ligne, column) // constructeur avec la postion de la tour
        {
            Name = "Fou";
            if (estblanc) Name += " blanc";
            else Name += " noir";
            EstBlanc = estblanc;
            Pictogramme = image;
        }

        public override List<string> Deplacement(int ligne, int colonne) // calcule les déplacement possible pour une tour
        {
            return Diagonale(ligne, colonne);
        }
    }

    public class Dame : Piece
    {
        public Dame(Image image, bool estblanc) : base(image, estblanc) // constructeur de base
        {
            Name = "Dame";
            if (estblanc) Name += " blanc";
            else Name += " noir";
            EstBlanc = estblanc;
            Pictogramme = image;
        }
        public Dame(Image image, bool estblanc, int ligne, int column) : base(image, estblanc, ligne, column) // constructeur avec la postion de la tour
        {
            Name = "Dame";
            if (estblanc) Name += " blanc";
            else Name += " noir";
            EstBlanc = estblanc;
            Pictogramme = image;
        }

        public override List<string> Deplacement(int ligne, int colonne) // calcule les déplacement possible pour une tour
        {
            List<string> listDeplacement = Diagonale(ligne,colonne);
            foreach (string item in Croix(ligne, colonne))
            {
                listDeplacement.Add(item);
            }
            return listDeplacement;
        }
    }

    public class Pion:Piece
    {
        public bool doubleAvance;

        public Pion(Image image, bool estblanc) : base(image, estblanc) // constructeur de base
        {
            Name = "Pion";
            if (estblanc) Name += " blanc";
            else Name += " noir";
            EstBlanc = estblanc;
            Pictogramme = image;
            doubleAvance = false;
        }
        public Pion(Image image, bool estblanc, int ligne, int column) : base(image, estblanc, ligne, column) // constructeur avec la postion de la tour
        {
            Name = "Pion";
            if (estblanc) Name += " blanc";
            else Name += " noir";
            EstBlanc = estblanc;
            Pictogramme = image;

            doubleAvance = false;
        }

        public override List<string> Deplacement(int ligne, int colonne)
        {
            List<string> listDeplacement = new List<string>();

            if (EstBlanc) // pour les pions blancs
            {
                listDeplacement.Add($"{ligne + 1}{colonne} "); //avance

                if (colonne + 1 < 8){listDeplacement.Add($"{ligne + 1}{colonne + 1} ");} //attaque a droite
                else { listDeplacement.Add("99 "); } // retour négatif

                if (colonne - 1 > -1) { listDeplacement.Add($"{ligne + 1}{colonne - 1} "); } //attaque a gauche
                else { listDeplacement.Add("99 "); }// retour négatif

                if (ligne == 1) { listDeplacement.Add($"{ligne + 2}{colonne} "); } //double avancement
                else { listDeplacement.Add("99 "); }// retour négatif

            }
            else // pour les pions noirs
            {
                listDeplacement.Add($"{ligne - 1}{colonne} "); //avance

                if (colonne + 1 < 8){listDeplacement.Add($"{ligne - 1}{colonne + 1} "); } //attaque a droite
                else { listDeplacement.Add("99 "); }// retour négatif

                if (colonne - 1 > -1) { listDeplacement.Add($"{ligne - 1}{colonne - 1} "); } //attaque a gauche
                else { listDeplacement.Add("99 "); }// retour négatif

                if (ligne == 6) { listDeplacement.Add($"{ligne - 2}{colonne} "); } //double avancement
                else { listDeplacement.Add("99 "); }// retour négatif
            }

            //pour les cases attaquées par la prise en passant
            listDeplacement.Add($"{ligne}{colonne + 1} "); //attaque a droite
            if(colonne-1 > -1)
            {
                listDeplacement.Add($"{ligne}{colonne - 1} "); //attaque a gauche
            }else
            {
                listDeplacement.Add(Convert.ToString(ligne) + "9 "); //attaque a gauche
            }
            
            return listDeplacement;
        }
    }
}