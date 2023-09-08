using System;
using System.Collections.Generic;
using System.Drawing;

namespace Chess
{
    public class Piece
    {
        internal bool EstBlanc { get; set; }
        internal string Name { get; set; }
        internal Image Pictogramme { get; set; }

        public Piece(Image image, bool estblanc)
        {
            Pictogramme = image;
            EstBlanc = estblanc;
        }

        public virtual List<string> Deplacement(int ligne, int colonne) // cette méthode est crée pour être override dans les classes dépandente de piece
        {
            return  new List<string>();
        }

        public string ToString(int ligne, int colonne) // renvoye le nom de la piece avec les déplacement possibles sans les limitation des autres pieces
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
            string deplacementHautDroite = "", deplacementHautGauche = "", deplacementBasDroite = "", deplacementBasGauche = "";
            for (int i = 1; colonne - i >= 0 && ligne - i >= 0; i++) { deplacementBasGauche += $"{ligne - i}{colonne - i} "; }
            listDeplacement.Add(deplacementBasGauche);
            for (int i = 1; colonne + i <= 7 && ligne + i <= 7; i++) { deplacementHautDroite += $"{ligne + i}{colonne + i} "; }
            listDeplacement.Add(deplacementHautDroite);
            for (int i = 1; colonne - i >= 0 && ligne + i <= 7; i++) { deplacementHautGauche += $"{ligne + i}{colonne - i} "; }
            listDeplacement.Add(deplacementHautGauche);
            for (int i = 1; colonne + i <= 7 && ligne - i >= 0; i++) { deplacementBasDroite += $"{ligne - i}{colonne + i} "; }
            listDeplacement.Add(deplacementBasDroite);
            return listDeplacement;
        }

        public List<string> Croix(int ligne, int colonne)
        {
            List<string> listDeplacement = new List<string>();
            string deplacementHaut = "", deplacementBas = "", deplacementDroite = "", deplacementGauche = "";
            for (int i = 1; colonne - i > -1; i++) { deplacementGauche += $"{ligne}{colonne - i} "; }
            listDeplacement.Add(deplacementGauche);
            for (int i = 1; colonne + i < 8; i++) { deplacementDroite += $"{ligne}{colonne + i} "; }
            listDeplacement.Add(deplacementDroite);
            for (int i = 1; ligne - i > -1; i++) { deplacementBas += $"{ligne - i}{colonne} "; }
            listDeplacement.Add(deplacementBas);
            for (int i = 1; ligne + i < 8; i++) { deplacementHaut += $"{ligne + i}{colonne} "; }
            listDeplacement.Add(deplacementHaut);
            return listDeplacement;
        }
    }

    public class Roi : Piece // classe dépandente de pièce pour le roi
    {
        public bool HasMoved { get; set; }
        public Roi(Image image, bool estblanc) : base(image, estblanc) // constructeur de base
        {
            Name = "Roi ";
            if (estblanc) Name += "blanc";
            else Name += "noir";
            EstBlanc = estblanc;
            Pictogramme = image;
            HasMoved = false;
        }

        public override List<string> Deplacement(int ligne, int colonne) // calcule les déplacements possible pour la piece sans collision
        {
            List<string> listDeplacement = new List<string>();
            if (ligne + 1 < 8)
            {
                if (colonne + 1 < 8) { listDeplacement.Add($"{ligne + 1}{colonne + 1} "); }
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
        internal bool HasMoved { get; set; }
        public Tour(Image image, bool estblanc) : base(image, estblanc) // constructeur de base
        {
            Name = "Tour ";
            if (estblanc) Name += "blanc";
            else Name += "noir";
            EstBlanc = estblanc;
            Pictogramme = image;
            HasMoved = false;
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
            Name = "Cheval ";
            if (estblanc) Name += "blanc";
            else Name += "noir";
            EstBlanc = estblanc;
            Pictogramme = image;
        }

        public override List<string> Deplacement(int ligne, int colonne) // calcule les déplacement possible pour une tour
        {
            List<string> listDeplacement = new List<string>();
            if (ligne + 2 <= 7)
            {
                if (colonne + 1 <= 7) { listDeplacement.Add($"{ligne + 2}{colonne + 1} "); }
                if (colonne - 1 >= 0) { listDeplacement.Add($"{ligne + 2}{colonne - 1} "); }
            }

            if (ligne - 2 >= 0)
            {
                if (colonne + 1 <= 7) { listDeplacement.Add($"{ligne - 2}{colonne + 1} "); }
                if (colonne - 1 >= 0) { listDeplacement.Add($"{ligne - 2}{colonne - 1} "); }
            }

            if (colonne + 2 <= 7)
            {
                if (ligne + 1 <= 7) { listDeplacement.Add($"{ligne + 1}{colonne + 2} "); }
                if (ligne - 1 >= 0) { listDeplacement.Add($"{ligne - 1}{colonne + 2} "); }
            }

            if (colonne - 2 >= 0)
            {
                if (ligne + 1 <= 7) { listDeplacement.Add($"{ligne + 1}{colonne - 2} "); }
                if (ligne - 1 >= 0) { listDeplacement.Add($"{ligne - 1}{colonne - 2} "); }
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
            else Name += "noir";
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
            Name = "Dame ";
            if (estblanc) Name += "blanc";
            else Name += "noir";
            EstBlanc = estblanc;
            Pictogramme = image;
        }

        public override List<string> Deplacement(int ligne, int colonne) // calcule les déplacement possible pour une tour
        {
            List<string> listDeplacement = Diagonale(ligne, colonne);
            foreach (string item in Croix(ligne, colonne))
            {
                listDeplacement.Add(item);
            }
            return listDeplacement;
        }
    }

    public class Pion : Piece
    {
        public bool DoubleAvance { get; set; }

        public Pion(Image image, bool estblanc) : base(image, estblanc) // constructeur de base
        {
            Name = "Pion ";
            if (estblanc) Name += "blanc";
            else Name += "noir";
            EstBlanc = estblanc;
            Pictogramme = image;
        }

        public override List<string> Deplacement(int ligne, int colonne)
        {
            List<string> listDeplacement = new List<string>();

            if (EstBlanc) // pour les pions blancs
            {
                listDeplacement.Add($"{ligne + 1}{colonne} "); //avance

                if (colonne + 1 < 8) { listDeplacement.Add($"{ligne + 1}{colonne + 1} "); } //attaque a droite
                else { listDeplacement.Add("99 "); }

                if (colonne - 1 > -1) { listDeplacement.Add($"{ligne + 1}{colonne - 1} "); } //attaque a gauche
                else { listDeplacement.Add("99 "); }

                if (ligne == 1) { listDeplacement.Add($"{ligne + 2}{colonne} "); } //double avancement
                else { listDeplacement.Add("99 "); }

            }
            else // pour les pions noirs
            {
                listDeplacement.Add($"{ligne - 1}{colonne} "); //avance

                if (colonne + 1 < 8) { listDeplacement.Add($"{ligne - 1}{colonne + 1} "); } //attaque a droite
                else { listDeplacement.Add("99 "); }

                if (colonne - 1 > -1) { listDeplacement.Add($"{ligne - 1}{colonne - 1} "); } //attaque a gauche
                else { listDeplacement.Add("99 "); }

                if (ligne == 6) { listDeplacement.Add($"{ligne - 2}{colonne} "); } //double avancement
                else { listDeplacement.Add("99 "); }
            }

            listDeplacement.Add($"{ligne}{colonne + 1} ");
            if (colonne - 1 > -1)
            {
                listDeplacement.Add($"{ligne}{colonne - 1} "); //attaque a gauche
            }
            else
            {
                listDeplacement.Add(Convert.ToString(ligne) + "9 "); //attaque a gauche
            }

            return listDeplacement;
        }
    }
}