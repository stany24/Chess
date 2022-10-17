using Chess.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Chess;

namespace Chess
{
    public partial class Form1 : Form
    {
        public ImageList imageList = new ImageList(); // list des images pour les pièces
        public Case[][] damier = new Case[8][]; //array de deux dimension pour stocker les cases
        public Case selectedCase;
        Button buttonEvalWhite = new Button();
        Button buttonEvalBlack = new Button();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //ajout de toutes les images
            imageList.Images.Add(Resources._0_PionBlanc);
            imageList.Images.Add(Resources._1_TourBlanc);
            imageList.Images.Add(Resources._2_ChevalBlanc);
            imageList.Images.Add(Resources._3_FouBlanc);
            imageList.Images.Add(Resources._4_ReineBlanc);
            imageList.Images.Add(Resources._5_RoiBlanc);
            imageList.Images.Add(Resources._6_PionNoir);
            imageList.Images.Add(Resources._7_TourNoir);
            imageList.Images.Add(Resources._8_ChevalNoir);
            imageList.Images.Add(Resources._9_FouNoir);
            imageList.Images.Add(Resources._10_ReineNoir);
            imageList.Images.Add(Resources._11_RoiNoir);
            string[] letters = new string[] { "a", "b", "c", "d", "e", "f", "g", "h" };

            // création de la barre d'évaluation
            buttonEvalWhite.Location= new Point(10, 50);
            buttonEvalWhite.Size = new Size(15, 200);
            buttonEvalWhite.BackColor = Color.White;
            buttonEvalWhite.FlatStyle = FlatStyle.Flat;
            buttonEvalWhite.Name = "btnEvaluationWhite";
            this.Controls.Add(buttonEvalWhite);

            buttonEvalBlack.Location = new Point(10, 50);
            buttonEvalBlack.Size = new Size(15, 400);
            buttonEvalBlack.BackColor = Color.Black;
            buttonEvalBlack.FlatStyle = FlatStyle.Flat;
            buttonEvalBlack.Name = "btnEvaluationBlack";
            this.Controls.Add(buttonEvalBlack);


            for (int j = 0; j < 8; j++) // création des cases
            {
                Case[] cases1 = new Case[8];
                for (int i = 0; i < 8; i++)
                {
                    Case cases = new Case();
                    cases.Click += new EventHandler(this.btnCases_Click);
                    cases.Size = new Size(50, 50);
                    cases.Location = new Point(200 + i * 49, 400 - j * 49);
                    cases.Name = "btn" + letters[i].ToUpper() + (j+1);
                    cases.FlatStyle = FlatStyle.Flat;
                    if ((i+j)%2 == 0){cases.BackColor = Color.Gray;
                    }else { cases.BackColor = Color.White; }
                    this.Controls.Add(cases);
                    cases1[i] = cases;
                }
                damier[j] = cases1;
            }

            for (int col = 0; col < 8; col++) // affichage des lettres des colonnes
            {
                Label label = new Label();
                label.Text = letters[col];
                label.Location = new Point(220 + col * 49, 470);
                label.Size = new Size(15, 15);
                this.Controls.Add(label);
            }

            for (int row = 8; row > 0; row--) // affichage des numéro des lignes
            {
                Label label = new Label();
                label.Text = Convert.ToString(row);
                label.Size = new Size(15, 15);
                label.Location = new Point(170, 460 - row * 49);
                this.Controls.Add(label);
            }
        }

        private void btnCommencer_Click(object sender, EventArgs e) // Instanciation initiale des pièces
        {
            // déclaration des pions blancs
            for (int i = 0; i < 8; i++)
            {
                damier[1][i].piece = new Pion(imageList.Images[0], true, 1, i);
            }

            // déclaration des pions noirs
            for (int i = 0; i < 8; i++)
            {
                damier[6][i].piece = new Pion(imageList.Images[6], false, 6, i);
            }
            // déclaration des tours
            damier[0][0].piece = new Tour(imageList.Images[1], true,0,0);
            damier[0][7].piece = new Tour(imageList.Images[1], true,0,7);
            damier[7][0].piece = new Tour(imageList.Images[7], false,7,0);
            damier[7][7].piece = new Tour(imageList.Images[7], false,7,7);
            // déclaration des cavaliers
            damier[0][1].piece = new Cheval(imageList.Images[2], true, 0, 1);
            damier[0][6].piece = new Cheval(imageList.Images[2], true, 0, 6);
            damier[7][1].piece = new Cheval(imageList.Images[8], false, 7, 1);
            damier[7][6].piece = new Cheval(imageList.Images[8], false, 7, 6);
            // déclaration des fous
            damier[0][2].piece = new Fou(imageList.Images[3], true, 0, 2);
            damier[0][5].piece = new Fou(imageList.Images[3], true, 0, 5);
            damier[7][2].piece = new Fou(imageList.Images[9], false, 7, 2);
            damier[7][5].piece = new Fou(imageList.Images[9], false, 7, 5);
            // déclaration des rois
            damier[0][4].piece = new Roi(imageList.Images[5], true,0,4);
            damier[7][4].piece = new Roi(imageList.Images[11], false,7,4);
            // déclaration des reines
            damier[0][3].piece = new Dame(imageList.Images[4], true, 0, 3);
            damier[7][3].piece = new Dame(imageList.Images[10], false, 7, 3);
            Actualiser();
        }

        private int LetterToNumber(string letter) // converti une lettre du plateau en nombre pour le double array
        {
            switch(letter)
            {
                case "A": return 0;
                case "B": return 1;
                case "C": return 2;
                case "D": return 3;
                case "E": return 4;
                case "F": return 5;
                case "G": return 6;
                case "H": return 7;
                default: return 0;
            }
        }
        private string NumberToLetter(int number) // converti une lettre du plateau en nombre pour le double array
        {
            switch (number)
            {
                case 1: return "A";
                case 2: return "B";
                case 3: return "C";
                case 4: return "D";
                case 5: return "E";
                case 6: return "F";
                case 7: return "G";
                case 8: return "H";
                default: return "A";
            }
        }

        private void Actualiser() // actualise l'affichage de toute les cases
        {
            for (int i = 0; i < damier.Length; i++)
            {
                for (int j = 0; j < (damier[i]).Length; j++)
                {
                    damier[i][j].Draw();
                }
            }
        }

        private List<string> Menace(bool EstNoir) //Calcule les toutes menaces des pièces blanches ou noirs 
        {
            List<string> listMenace = new List<string>(); // list des cases menacées

            for (int i = 0; i < damier.Length; i++) // on parcours toutes les cases du damiers
            {
                for (int j = 0; j < (damier[i]).Length; j++)// on parcours toutes les cases du damiers
                {
                    if(damier[i][j].piece!=null && damier[i][j].piece.EstBlanc != EstNoir) // si il y a une piece de la couleur voulue
                    {
                        List<string> listDeplacement = damier[i][j].piece.Deplacement();//déplacement possibles de la piece sans réstriction

                        if (damier[i][j].piece is Pion) // si la piece est un pion 
                        {
                            int attack1 = Convert.ToInt32(listDeplacement[1]);
                            int attack2 = Convert.ToInt32(listDeplacement[2]);

                            if (attack1 != 99)
                            {
                                int ligne = Convert.ToInt32(listDeplacement[1].Substring(0, 1));
                                int colonne = Convert.ToInt32(listDeplacement[1].Substring(1, 1));
                                Case caseAttack1 = damier[ligne][colonne];
                                if (caseAttack1.piece != null && caseAttack1.piece.EstBlanc != damier[i][j].piece.EstBlanc)
                                {
                                    listMenace.Add(Convert.ToString(ligne) + Convert.ToString(colonne) + " ");
                                }
                            }
                            if (attack2 != 99)
                            {
                                int ligne = Convert.ToInt32(listDeplacement[2].Substring(0, 1));
                                int colonne = Convert.ToInt32(listDeplacement[2].Substring(1, 1));
                                Case caseAttack2 = damier[ligne][colonne];
                                if (caseAttack2.piece != null && caseAttack2.piece.EstBlanc != damier[i][j].piece.EstBlanc)
                                {
                                    listMenace.Add(Convert.ToString(ligne) + Convert.ToString(colonne) + " ");
                                }
                            }
                        }
                        else
                        {
                            for (int k = 0; k < listDeplacement.Count; k++) //on parcours tout les déplacements
                            {
                                for (int l = 0; l * 3 < listDeplacement[k].Length; l++)//on parcours tout les déplacements
                                {
                                    int ligne = Convert.ToInt32(listDeplacement[k].Substring(0 + 3 * l, 1)); //ligne de la case possible pour ce déplacer
                                    int colonne = Convert.ToInt32(listDeplacement[k].Substring(1 + 3 * l, 1));//colonne de la case possible pour ce déplacer
                                    Case casePossible = damier[ligne][colonne];
                                    if (casePossible.piece == null) { listMenace.Add(Convert.ToString(ligne) + Convert.ToString(colonne) + " "); } // ajout de la case attaquée si elle est vide
                                    else
                                    {
                                        if (casePossible.piece.EstBlanc == EstNoir) { listMenace.Add(Convert.ToString(ligne) + Convert.ToString(colonne) + " "); } //si la couleur de la pièce est différente on l'ajoute a la list des cases attaquées.
                                        l = listDeplacement[k].Length;// arret après rencontre d'une pièce de la même couleur
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return listMenace;
        }

        public bool IsMenacedFrom(bool EstNoir, int ligne,int colonne)
        {
            bool result = false;
            List<string> listMenace = Menace(!EstNoir);
            foreach (string item in listMenace)
            {
                if(item == Convert.ToString(ligne)+Convert.ToString(colonne)+" ")
                {
                    result = true;
                }
            }
            return result;
        }

        private void ResetColors()// reset toutes les couleurs des cases
        {
            for (int i = 0; i < damier.Length; i++)
            {
                for (int j = 0; j < (damier[i]).Length; j++)
                {
                    if ((i + j) % 2 == 0) { damier[i][j].BackColor = Color.Gray; }
                    else { damier[i][j].BackColor = Color.White; }
                }
            }
        }

        private void btnCases_Click(object sender, EventArgs e) // lorsque une case est cliquée
        {
            Case button = sender as Case;
            lblCase.Text = button.Name.Substring(3,2);

            if (button.BackColor == Color.Green || button.BackColor == Color.DarkGreen ) // déplacement du rock
            {
                if(button.piece.Column == 0) // rock a gauche
                {
                    damier[button.piece.Ligne][3].piece = button.piece; // déplacement de la tour
                    damier[button.piece.Ligne][3].piece.Column = 3;

                    damier[button.piece.Ligne][2].piece = selectedCase.piece; //déplacement du roi
                    damier[button.piece.Ligne][2].piece.Column = 2;

                    Roi roi = (Roi)damier[button.piece.Ligne][4].piece;
                    roi.hasMoved = true;
                    selectedCase.piece = null;
                    button.piece = null;
                }
                else // rock a droite
                {
                    damier[button.piece.Ligne][5].piece = button.piece; // déplacement de la tour
                    damier[button.piece.Ligne][5].piece.Column = 5;

                    damier[button.piece.Ligne][6].piece = selectedCase.piece; //déplacement du roi
                    damier[button.piece.Ligne][6].piece.Column = 6;

                    Roi roi = (Roi)damier[button.piece.Ligne][4].piece;
                    roi.hasMoved = true;
                    selectedCase.piece = null;
                    button.piece = null;
                }

                UpdateEvaluation();
                ResetColors();
                Actualiser();
                return;
            }

            if (button.BackColor == Color.Yellow || button.BackColor == Color.Gold || button.BackColor == Color.Red ) // si c'est une case de déplacement faire le déplacement
            {
                //cacher les bouton d'abandon après un coup
                btnNoirAbandon.Visible = false;
                btnBlancAbandon.Visible = false;
                //affichage en text du déplacement de la pièce
                if(button.piece != null){lblInfo.Text = "Déplacement de " + selectedCase.piece.Name + " en " + button.Name.Substring(3, 1) + button.Name.Substring(4, 1) +" dégustant: "+ button.piece.Name;}
                else{lblInfo.Text = "Déplacement de " + selectedCase.piece.Name + " en " + button.Name.Substring(3, 1) + button.Name.Substring(4, 1);}
                
                button.piece = selectedCase.piece;
                // changement des valeurs stockées dans la pièce
                button.piece.Ligne = Convert.ToInt32(button.Name.Substring(4, 1))-1;
                button.piece.Column = Convert.ToInt32(LetterToNumber(button.Name.Substring(3, 1)));

                if(selectedCase.piece is Pion) // pour les pions en dame sur la dernière ligne
                {
                    if(button.piece.Ligne == 7 && button.piece.EstBlanc )
                    {
                        button.piece = new Dame(imageList.Images[4], true, 7, button.piece.Column);
                    }
                    if(button.piece.Ligne == 0 && button.piece.EstBlanc == false )
                    {
                        button.piece = new Dame(imageList.Images[4], false, 0, button.piece.Column);
                    }
                }
                // supprimer la pièce de sa case prècèdente
                selectedCase.piece = null;

                if(button.piece is Roi)
                {
                    Roi roi = (Roi)button.piece;
                    roi.hasMoved = true;
                }
                if (button.piece is Tour)
                {
                    Tour tour = (Tour)button.piece;
                    tour.hasMoved = true;
                }

                
                
                // verification des échec pour les rois
                if (button.piece.EstBlanc)
                { 
                    lblEchec.Text = "Le roi noir n'est pas attaqué.";
                    foreach(Case casepossible in DeplacementPossible(Menace(false), true))
{
                        if (casepossible.piece is Roi)
                        {
                            lblEchec.Text = "Echec au Roi noir";
                            btnNoirAbandon.Visible = true;
                        }
                    }
                }
                else
                {
                    lblEchec.Text = "Le roi blanc n'est pas attaqué.";
                    foreach (Case casepossible in DeplacementPossible(Menace(false), false))
                    {
                        if (casepossible.piece is Roi){
                            lblEchec.Text = "Echec au Roi blanc";
                            btnBlancAbandon.Visible = true;
                        }
                    }
                }
                //modification de l'affichage d'après les modifications faites plus haut.
                UpdateEvaluation();
                ResetColors();
                Actualiser();
                return;
            }

            ResetColors();
            if (button.piece != null)
            {
                lblEchec.Text = "";
                lblInfo.Text = "";
                lblPiece.Text = button.piece.Name;
                //actualisation de la dernière case cliquée
                selectedCase = button;
                button.BackColor = Color.LightBlue;
                List<string> listcase = button.piece.Deplacement();// tout les déplacement possibles pour la pièce cliquée

                if (button.piece is Pion) // régles spéciales si la pièce est un pion
                {
                    int deplacement = Convert.ToInt32(listcase[0]);//case de déplacement
                    int attack1 = Convert.ToInt32(listcase[1]);//case de la première attaque
                    int attack2 = Convert.ToInt32(listcase[2]);//case de la deuxième attaque
                    int doubleDeplacement = Convert.ToInt32(listcase[3]);//case pour la double poussée

                    if(deplacement!=99) //vérification de la posibilité de mouvement
                    {
                        int ligne = Convert.ToInt32(listcase[0].Substring(0, 1));
                        int colonne = Convert.ToInt32(listcase[0].Substring(1, 1));
                        Case caseDeplacement = damier[ligne][colonne];
                        if(caseDeplacement.piece ==null)
                        {
                            if ((ligne + colonne) % 2 == 0) { caseDeplacement.BackColor = Color.Yellow; }
                            else { caseDeplacement.BackColor = Color.Gold; }

                            if (doubleDeplacement != 99) // vérification de la posibilité de la double poussée
                            {
                                int ligneDouble = Convert.ToInt32(listcase[3].Substring(0, 1));
                                int colonneDouble = Convert.ToInt32(listcase[3].Substring(1, 1));
                                Case caseDouble = damier[ligneDouble][colonneDouble];
                                if (caseDouble.piece == null)
                                {
                                    if ((ligneDouble + colonneDouble) % 2 == 0) { caseDouble.BackColor = Color.Yellow; }
                                    else { caseDouble.BackColor = Color.Gold; }
                                }
                            }
                        }
                    }
                    if(attack1!=99) //vérification de la première case d'attaque
                    {
                        int ligne = Convert.ToInt32(listcase[1].Substring(0, 1));
                        int colonne = Convert.ToInt32(listcase[1].Substring(1, 1));
                        Case caseAttack1 = damier[ligne][colonne];
                        if (caseAttack1.piece != null && caseAttack1.piece.EstBlanc != button.piece.EstBlanc)
                        {
                            if ((ligne + colonne) % 2 == 0) { caseAttack1.BackColor = Color.Yellow; }
                            else { caseAttack1.BackColor = Color.Gold; }
                        }
                    }
                    if (attack2 != 99)//vérification de la deuxième case d'attaque
                    {
                        int ligne = Convert.ToInt32(listcase[2].Substring(0, 1));
                        int colonne = Convert.ToInt32(listcase[2].Substring(1, 1));
                        Case caseAttack2 = damier[ligne][colonne];
                        if (caseAttack2.piece != null && caseAttack2.piece.EstBlanc != button.piece.EstBlanc)
                        {
                            if ((ligne + colonne) % 2 == 0) { caseAttack2.BackColor = Color.Yellow; }
                            else { caseAttack2.BackColor = Color.Gold; }
                        }
                    }
                    return;
                }

                for (int i = 0; i < listcase.Count; i++)// on parcours tout les déplacements
                {
                    for (int j = 0; j*3 < listcase[i].Length; j++)// on parcours tout les déplacements
                    {
                        int ligne = Convert.ToInt32(listcase[i].Substring(0+3*j, 1));// extraction de la ligne actuel
                        int colonne = Convert.ToInt32(listcase[i].Substring(1+3*j, 1));// extraction de la colonne actuel
                        Case casePossible = damier[ligne][colonne];
                        if (casePossible.piece == null || casePossible.piece.EstBlanc != button.piece.EstBlanc) // pas de capture de nos pièces
                        {
                            if(DeplacementSansEchec(button.piece.EstBlanc,button,casePossible))// vérification que le mouvement ne met pas notre roi en échec
                            {
                                //changement de couleurs pour les cases vides
                                if ((ligne + colonne) % 2 == 0) { casePossible.BackColor = Color.Yellow; }
                                else { casePossible.BackColor = Color.Gold; }
                                // si la case contient une pièce ennemie on la met en rouge
                                if (casePossible.piece != null && casePossible.piece.EstBlanc != button.piece.EstBlanc) { casePossible.BackColor = Color.Red; } 
                            }
                        }
                        if (casePossible.piece != null) {j= listcase[i].Length;} // arret après rencontre d'une pièce
                    }
                }

                if(button.piece is Roi) // gestion du rock
                {
                    Roi roi = (Roi)button.piece;

                    if(!roi.hasMoved) //vérification que le roi n'a pas encore bougé
                    {
                        if (roi.EstBlanc) //pour le roi blanc
                        {
                            if (damier[0][0].piece is Tour && damier[0][0].piece.EstBlanc == roi.EstBlanc && damier[0][1].piece == null && damier[0][2].piece == null && damier[0][3].piece == null) // tour à la bonne place - tour de la bonne couleur - pas de piece entre
                            {
                                if (!IsMenacedFrom(false, 0, 1) && !IsMenacedFrom(false, 0, 2) && !IsMenacedFrom(false, 0, 3) && !IsMenacedFrom(false, 0, 4)) // vérification que les cases ne sont pas menacées
                                {
                                    Tour tour = (Tour)damier[0][0].piece; 
                                    if (!tour.hasMoved) //vérification que la tour n'a pas bougé
                                    {
                                        damier[0][0].BackColor = Color.Green;
                                    }
                                }
                            }
                            if (damier[0][7].piece is Tour && damier[0][7].piece.EstBlanc == roi.EstBlanc && damier[0][5].piece == null && damier[0][6].piece == null) // tour à la bonne place - tour de la bonne couleur - pas de piece entre
                            {
                                if (!IsMenacedFrom(false, 0, 4) && !IsMenacedFrom(false, 0, 5) && !IsMenacedFrom(false, 0, 6)) // vérification que les cases ne sont pas menacées
                                {
                                    Tour tour = (Tour)damier[0][7].piece;
                                    if (!tour.hasMoved) //vérification que la tour n'a pas bougé
                                    {
                                        damier[0][7].BackColor = Color.Green;
                                    }
                                }
                            }
                        }
                        else // pour le roi noir
                        {
                            if (damier[7][0].piece is Tour && damier[7][0].piece.EstBlanc == roi.EstBlanc && damier[7][1].piece == null && damier[7][2].piece == null && damier[7][3].piece == null) // tour à la bonne place - tour de la bonne couleur - pas de piece entre
                            {
                                if (!IsMenacedFrom(true, 7, 1) && !IsMenacedFrom(true, 7, 2) && !IsMenacedFrom(true, 7, 3) && !IsMenacedFrom(true, 7, 4)) // vérification que les cases ne sont pas menacées
                                {
                                    Tour tour = (Tour)damier[7][0].piece; 
                                    if (!tour.hasMoved) //vérification que la tour n'a pas bougé
                                    {
                                        damier[7][0].BackColor = Color.Green;
                                    }
                                }
                            }
                            if (damier[7][7].piece is Tour && damier[7][7].piece.EstBlanc == roi.EstBlanc && damier[7][5].piece == null && damier[7][6].piece == null) // tour à la bonne place - tour de la bonne couleur - pas de piece entre
                            {
                                if (!IsMenacedFrom(true, 7, 4) && !IsMenacedFrom(true, 7, 5) && !IsMenacedFrom(true, 7, 6)) // vérification que les cases ne sont pas menacées
                                {
                                    Tour tour = (Tour)damier[7][7].piece;
                                    if (!tour.hasMoved) //vérification que la tour n'a pas bougé
                                    {
                                        damier[7][7].BackColor = Color.Green;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            else { lblInfo.Text = "Case vide"; }
        }

        private bool DeplacementSansEchec(bool estblanc,Case depart,Case arrivee) // en lui donnant une nouvelle position elle determine si le coup joué est légal ou non.
        {
            //stockage des valeurs initiales
            bool answer = true;
            Piece PieceDépart = depart.piece;
            Piece PieceArrive;
            if (arrivee.piece != null){PieceArrive = arrivee.piece;}
            else{PieceArrive = null;}
            //modification de la position
            arrivee.piece = depart.piece;
            depart.piece = null;

            foreach (Case casemenacee in DeplacementPossible(Menace(estblanc),!estblanc))//vérification des toutes les menaces dans la nouvelle position
            {
                if (casemenacee.piece is Roi) // si le roi est menacé le coup précédent était ilégal
                    answer = false;
            }
            //remise à zero de la position
            depart.piece = PieceDépart;
            arrivee.piece = PieceArrive;

            return answer;
        }

        private List<Case> DeplacementPossible(List<String> listcase, bool EstBlanc)
        {
            List<Case> listPossible = new List<Case>();
            for (int i = 0; i < listcase.Count; i++)// on parcours tout les déplacements
            {
                for (int j = 0; j * 3 < listcase[i].Length; j++)// on parcours tout les déplacements
                {
                    int ligne = Convert.ToInt32(listcase[i].Substring(0 + 3 * j, 1));// extraction de la ligne actuel
                    int colonne = Convert.ToInt32(listcase[i].Substring(1 + 3 * j, 1));// extraction de la colonne actuel
                    Case casePossible = damier[ligne][colonne];
                    if (casePossible.piece != null && casePossible.piece.EstBlanc != EstBlanc) // pas de capture de nos pièces
                    {
                        listPossible.Add(casePossible);
                    }
                    if (casePossible.piece != null) { j = listcase[i].Length; } // arret après rencontre d'une pièce
                }
            }
            return listPossible;
        }

        private void btnMenaceNoir_Click(object sender, EventArgs e)
        {
            ColorInRed(Menace(true));
        }

        private void btnMenaceBlanc_Click(object sender, EventArgs e)
        {
            ColorInRed(Menace(false));
        }

        private void ColorInRed(List<string> list) // affiche en rouge la list de cases fournies
        {
            ResetColors();
            for (int i = 0; i < list.Count; i++)
            {
                for (int j = 0; j * 3 < list[i].Length; j++)
                {
                    int ligne = Convert.ToInt32(list[i].Substring(0 + 3 * j, 1));
                    int colonne = Convert.ToInt32(list[i].Substring(1 + 3 * j, 1));
                    damier[ligne][colonne].BackColor = Color.Red;
                }
            }
        }

        private void UpdateEvaluation()//fonction pour mettre à jour la barre dévaluation grace à la valeur des pièces sur le plateau
        {
            int pointBlanc = 1;
            int pointNoir = 1;
            for (int i = 0; i < damier.Length; i++) // on parcours le plateau
            {
                for (int j = 0; j < (damier[i]).Length; j++)// on parcours le plateau
                {
                    if (damier[i][j].piece != null)
                    {
                        if (damier[i][j].piece.EstBlanc)
                        {
                            switch (damier[i][j].piece)// addition de la valeur des pièces blaches
                            {
                                case Dame dame:
                                    pointBlanc += 9;
                                    break;
                                case Tour tour:
                                    pointBlanc += 5;
                                    break;
                                case Fou fou:
                                    pointBlanc += 3;
                                    break;
                                case Cheval cheval:
                                    pointBlanc += 3;
                                    break;
                                case Pion pion:
                                    pointBlanc += 1;
                                    break;
                            }
                        }
                        else
                        {
                            switch (damier[i][j].piece)// addition de la valeur des pièces noires
                            {
                                case Dame dame:
                                    pointNoir += 9;
                                    break;
                                case Tour tour:
                                    pointNoir += 5;
                                    break;
                                case Fou fou:
                                    pointNoir += 3;
                                    break;
                                case Cheval cheval:
                                    pointNoir += 3;
                                    break;
                                case Pion pion:
                                    pointNoir += 1;
                                    break;
                            }
                        }
                    }
                }
            }
            //modification visuel de la barre
            int height = Convert.ToInt32(200 + pointBlanc - pointNoir * 5);
            buttonEvalWhite.Size = new Size(15,height);
        }

        private void btnDebug_Click(object sender, EventArgs e)
        {
        }
    }
}