﻿using Chess.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Linq;

namespace Chess
{
    public partial class Form1 : Form
    {
        readonly private ImageList imageList = new ImageList(); // list des images pour les pièces
        readonly private Case[][] damier = new Case[8][]; //array de deux dimension pour stocker les cases
        private Case selectedCase;
        bool colorToPlay = true;
        //variables pour la barre d'évaluation
        readonly Button buttonEvalWhite = new Button();
        readonly Button buttonEvalBlack = new Button();
        const int nbPixel = 20;
        const int minHeight = 20;
        const int maxHeight = 380;

        public Form1()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Permet d'inisialiser le plateau ainsi que la barre d'évaluation.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
            string[] letters = new string[] { "A", "B", "C", "D", "E", "F", "G", "H" };

            // création de la barre d'évaluation
            buttonEvalWhite.Location = new Point(10, 50);
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
                Case[] ligneCase = new Case[8];
                for (int i = 0; i < 8; i++)
                {
                    Case cases = new Case(j, i);
                    cases.Click += new EventHandler(this.BtnCases_Click);
                    cases.Size = new Size(50, 50);
                    cases.Location = new Point(200 + i * 49, 400 - j * 49);
                    cases.Name = "btn" + letters[i] + (j + 1);
                    cases.FlatStyle = FlatStyle.Flat;
                    if ((i + j) % 2 == 0)
                    {
                        cases.BackColor = Color.Gray;
                    }
                    else { cases.BackColor = Color.White; }
                    this.Controls.Add(cases);
                    ligneCase[i] = cases;
                }
                damier[j] = ligneCase;
            }

            for (int col = 0; col < 8; col++) // affichage des lettres des colonnes
            {
                Label label = new Label
                {
                    Text = letters[col],
                    Location = new Point(220 + col * 49, 470),
                    Size = new Size(15, 15)
                };
                this.Controls.Add(label);
            }

            for (int row = 8; row > 0; row--) // affichage des numéro des lignes
            {
                Label label = new Label
                {
                    Text = Convert.ToString(row),
                    Size = new Size(15, 15),
                    Location = new Point(170, 460 - row * 49)
                };
                this.Controls.Add(label);
            }
        }
        /// <summary>
        /// Place toutes les pièces.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnCommencer_Click(object sender, EventArgs e)
        {
            colorToPlay = true;
            for (int i = 0; i < damier.Length; i++)
            {
                for (int j = 0; j < (damier[i]).Length; j++)
                {
                    damier[i][j].Piece = null;
                }
            }
            // déclaration des pions blancs
            for (int i = 0; i < 8; i++)
            {
                damier[1][i].Piece = new Pion(imageList.Images[0], true);
            }

            // déclaration des pions noirs
            for (int i = 0; i < 8; i++)
            {
                damier[6][i].Piece = new Pion(imageList.Images[6], false);
            }
            // déclaration des tours
            damier[0][0].Piece = new Tour(imageList.Images[1], true);
            damier[0][7].Piece = new Tour(imageList.Images[1], true);
            damier[7][0].Piece = new Tour(imageList.Images[7], false);
            damier[7][7].Piece = new Tour(imageList.Images[7], false);
            // déclaration des cavaliers
            damier[0][1].Piece = new Cheval(imageList.Images[2], true);
            damier[0][6].Piece = new Cheval(imageList.Images[2], true);
            damier[7][1].Piece = new Cheval(imageList.Images[8], false);
            damier[7][6].Piece = new Cheval(imageList.Images[8], false);
            // déclaration des fous
            damier[0][2].Piece = new Fou(imageList.Images[3], true);
            damier[0][5].Piece = new Fou(imageList.Images[3], true);
            damier[7][2].Piece = new Fou(imageList.Images[9], false);
            damier[7][5].Piece = new Fou(imageList.Images[9], false);
            // déclaration des rois
            damier[0][4].Piece = new Roi(imageList.Images[5], true);
            damier[7][4].Piece = new Roi(imageList.Images[11], false);
            // déclaration des reines
            damier[0][3].Piece = new Dame(imageList.Images[4], true);
            damier[7][3].Piece = new Dame(imageList.Images[10], false);

            //tests
            Actualiser();
        }

        /// <summary>
        /// Pour afficher toutes les pièces sur toutes les cases.
        /// </summary>
        private void Actualiser()
        {
            for (int i = 0; i < damier.Length; i++)
            {
                for (int j = 0; j < (damier[i]).Length; j++)
                {
                    damier[i][j].Draw();
                }
            }
        }

        /// <summary>
        /// Retourne un tableau des cases menacées par la couleur donnée en paramètre.
        /// </summary>
        /// <param name="EstNoir">La couleur dont on veux connaitre les menaces.</param>
        /// <returns></returns>
        private List<string> Menace(bool EstNoir)
        {
            List<string> listMenace = new List<string>(); // list des cases menacées

            for (int i = 0; i < damier.Length; i++) // on parcours toutes les cases du damiers
            {
                for (int j = 0; j < (damier[i]).Length; j++)// on parcours toutes les cases du damiers
                {
                    if (damier[i][j].Piece != null && damier[i][j].Piece.EstBlanc != EstNoir) // si il y a une Piece de la couleur voulue
                    {
                        List<string> listDeplacement = damier[i][j].Piece.Deplacement(damier[i][j].Ligne, damier[i][j].Colonne);//déplacement possibles de la Piece sans réstriction

                        if (damier[i][j].Piece is Pion) // si la Piece est un pion 
                        {
                            int attack1 = Convert.ToInt32(listDeplacement[1]);
                            int attack2 = Convert.ToInt32(listDeplacement[2]);

                            if (attack1 != 99)
                            {
                                int ligne = Convert.ToInt32(listDeplacement[1].Substring(0, 1));
                                int colonne = Convert.ToInt32(listDeplacement[1].Substring(1, 1));
                                Case caseAttack1 = damier[ligne][colonne];
                                if (caseAttack1.Piece != null && caseAttack1.Piece.EstBlanc != damier[i][j].Piece.EstBlanc)
                                {
                                    listMenace.Add(Convert.ToString(ligne) + Convert.ToString(colonne) + " ");
                                }
                            }
                            if (attack2 != 99)
                            {
                                int ligne = Convert.ToInt32(listDeplacement[2].Substring(0, 1));
                                int colonne = Convert.ToInt32(listDeplacement[2].Substring(1, 1));
                                Case caseAttack2 = damier[ligne][colonne];
                                if (caseAttack2.Piece != null && caseAttack2.Piece.EstBlanc != damier[i][j].Piece.EstBlanc)
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
                                    if (casePossible.Piece == null) { listMenace.Add(Convert.ToString(ligne) + Convert.ToString(colonne) + " "); } // ajout de la case attaquée si elle est vide
                                    else
                                    {
                                        if (casePossible.Piece.EstBlanc == EstNoir) { listMenace.Add(Convert.ToString(ligne) + Convert.ToString(colonne) + " "); } //si la couleur de la pièce est différente on l'ajoute a la list des cases attaquées.
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
        /// <summary>
        /// Permet de savoir si une case est attaqué par la couleur donnée.
        /// </summary>
        /// <param name="EstNoir">La couleur donnée.</param>
        /// <param name="ligne">La ligne de la case.</param>
        /// <param name="colonne">La colonne de la case.</param>
        /// <returns></returns>
        public bool IsMenacedFrom(bool EstNoir, int ligne, int colonne)
        {
            bool result = false;
            List<string> listMenace = Menace(!EstNoir);
            foreach (string item in listMenace)
            {
                if (item == Convert.ToString(ligne) + Convert.ToString(colonne) + " ")
                {
                    result = true;
                }
            }
            return result;
        }
        /// <summary>
        /// Remet la couleur de défault sur toutes les cases.
        /// </summary>
        private void ResetColors()
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
        /// <summary>
        /// Utilisé pour vérifié si la couleur donnée à un coup à jouer ou non.
        /// </summary>
        /// <param name="estBlanc"> La couleur à vérifier.</param>
        /// <returns></returns>
        private bool CanMove(bool estBlanc)
        {
            for (int i = 0; i < damier.Length; i++)
            {
                for (int j = 0; j < (damier[i]).Length; j++)
                {
                    if (damier[i][j].Piece == null) { continue; }
                    if (damier[i][j].Piece.EstBlanc == estBlanc) { continue; }
                    foreach (string deplacement in damier[i][j].Piece.Deplacement(damier[i][j].Ligne, damier[i][j].Colonne))
                    {
                        if (damier[i][j].Piece is Pion pion)
                        {
                            // à compléter
                        }
                        else
                        {
                            for (int k = 0; k < deplacement.Length / 3; k++)
                            {
                                int ligne = Convert.ToInt32(deplacement.Substring(0 + 3 * k, 1));
                                int colonne = Convert.ToInt32(deplacement.Substring(1 + 3 * k, 1));
                                Case casePossible = damier[ligne][colonne];
                                if (casePossible.Piece == null || casePossible.Piece.EstBlanc == estBlanc) // pas de capture de nos pièces
                                {
                                    if (DeplacementSansEchec(!estBlanc, damier[i][j], damier[ligne][colonne]))// vérification que le mouvement ne met pas notre roi en échec
                                    {
                                        return true;
                                    }
                                }
                                if (casePossible.Piece != null) { k = deplacement.Length; } // arret après rencontre d'une pièce
                            }
                        }
                    }

                }
            }
            return false;
        }
        /// <summary>
        /// Remet à zero la valeurDoubleAvance à tout les pions de la couleur donnée.
        /// </summary>
        /// <param name="estBlanc">La couleur des pions que vous voulez reset.</param>
        private void ResetDoubleAvance(bool estBlanc)
        {
            for (int i = 0; i < damier.Length; i++)
            {
                for (int j = 0; j < (damier[i]).Length; j++)
                {
                    if (!(damier[i][j].Piece is Pion pion)) { continue; }
                    if (pion.EstBlanc != estBlanc) { continue; }
                    pion.DoubleAvance = false;
                }
            }
        }
        /// <summary>
        /// Gére le rock entre le roi et les toures.
        /// </summary>
        /// <param name="button">La case de la tour cliquée.</param>
        public void Rock(Case button)
        {
            if (button.Piece.EstBlanc == colorToPlay)
            {
                bool color;
                if (button.Colonne == 0) // rock a gauche
                {
                    color = button.Piece.EstBlanc;
                    damier[button.Ligne][3].Piece = button.Piece; // déplacement de la tour

                    damier[button.Ligne][2].Piece = selectedCase.Piece; //déplacement du roi

                    Roi roi = (Roi)damier[button.Ligne][4].Piece;
                    roi.HasMoved = true;
                    selectedCase.Piece = null;
                    button.Piece = null;
                }
                else // rock a droite
                {
                    color = button.Piece.EstBlanc;
                    damier[button.Ligne][5].Piece = button.Piece; // déplacement de la tour

                    damier[button.Ligne][6].Piece = selectedCase.Piece; //déplacement du roi

                    Roi roi = (Roi)damier[button.Ligne][4].Piece;
                    roi.HasMoved = true;
                    selectedCase.Piece = null;
                    button.Piece = null;
                }
                colorToPlay = !colorToPlay;
                ResetDoubleAvance(!color);
                UpdateEvaluation();
            }
            else { lblInfo.Text = "No your turn to play"; }
            ResetColors();
            Actualiser();
        }
        /// <summary>
        /// Gére la prise en passant pour les pions.
        /// </summary>
        /// <param name="button">La case attaquée par la prise en passant.</param>
        public void PrisePassant(Case button)
        {
            if (button.Piece.EstBlanc == colorToPlay)
            {
                if (selectedCase.Piece.EstBlanc)
                {
                    //déplacement
                    Case newCase = damier[button.Ligne + 1][button.Colonne];
                    newCase.Piece = selectedCase.Piece;
                    //enlever les pions
                    button.Piece = null;
                    selectedCase.Piece = null;
                }
                else
                {
                    Case newCase = damier[button.Ligne - 1][button.Colonne];
                    newCase.Piece = selectedCase.Piece;
                    //enlever les pions
                    button.Piece = null;
                    selectedCase.Piece = null;
                }
                colorToPlay = !colorToPlay;
                ResetDoubleAvance(true);
                ResetDoubleAvance(false);
                UpdateEvaluation();
            }
            else { lblInfo.Text = "Not your turn to play"; }
            ResetColors();
            Actualiser();
        }
        /// <summary>
        /// Est appelé pour afficher les déplacement possible de la pièce sur laquel on à cliqué.
        /// </summary>
        /// <param name="button">le boutton sur lequel est la pièce.</param>
        private void Selection(Case button)
        {
            lblEchec.Text = "";
            lblInfo.Text = "";
            lblPiece.Text = button.Piece.Name;
            //actualisation de la dernière case cliquée
            selectedCase = button;
            button.BackColor = Color.LightBlue;
            List<string> listcase = button.Piece.Deplacement(button.Ligne, button.Colonne);// tout les déplacement possibles pour la pièce cliquée

            if (button.Piece is Pion) // régles spéciales si la pièce est un pion
            {
                SelectionPion(button, listcase);
                return;
            }

            for (int i = 0; i < listcase.Count; i++)// on parcours tout les déplacements
            {
                for (int j = 0; j * 3 < listcase[i].Length; j++)// on parcours tout les déplacements
                {
                    int ligne = Convert.ToInt32(listcase[i].Substring(0 + 3 * j, 1));// extraction de la ligne actuel
                    int colonne = Convert.ToInt32(listcase[i].Substring(1 + 3 * j, 1));// extraction de la colonne actuel
                    Case casePossible = damier[ligne][colonne];
                    if (casePossible.Piece == null || casePossible.Piece.EstBlanc != button.Piece.EstBlanc) // pas de capture de nos pièces
                    {
                        if (DeplacementSansEchec(button.Piece.EstBlanc, button, casePossible))// vérification que le mouvement ne met pas notre roi en échec
                        {
                            //changement de couleurs pour les cases vides
                            if ((ligne + colonne) % 2 == 0) { casePossible.BackColor = Color.Yellow; }
                            else { casePossible.BackColor = Color.Gold; }
                            // si la case contient une pièce ennemie on la met en rouge
                            if (casePossible.Piece != null && casePossible.Piece.EstBlanc != button.Piece.EstBlanc) { casePossible.BackColor = Color.Red; }
                        }
                    }
                    if (casePossible.Piece != null) { j = listcase[i].Length; } // arret après rencontre d'une pièce
                }
            }

            if (button.Piece is Roi) // gestion du rock
            {
                SelectionRoi(button);
            }
        }
        /// <summary>
        /// Gestion de l'affichage des déplacements pour les pions.
        /// </summary>
        /// <param name="button">Le boutton sur lequel le pion ce trouve.</param>
        /// <param name="listcase">Liste des déplacement du pion.</param>
        public void SelectionPion(Case button, List<string> listcase)
        {
            int deplacement = Convert.ToInt32(listcase[0]);//case de déplacement
            int attack1 = Convert.ToInt32(listcase[1]);//case de la première attaque
            int attack2 = Convert.ToInt32(listcase[2]);//case de la deuxième attaque
            int doubleDeplacement = Convert.ToInt32(listcase[3]);//case pour la double poussée

            if (deplacement != 99) //vérification de la posibilité de mouvement
            {
                int ligne = Convert.ToInt32(listcase[0].Substring(0, 1));
                int colonne = Convert.ToInt32(listcase[0].Substring(1, 1));
                Case caseDeplacement = damier[ligne][colonne];

                if (caseDeplacement.Piece == null)
                {
                    if (DeplacementSansEchec(button.Piece.EstBlanc, button, caseDeplacement))// vérification que le mouvement ne met pas notre roi en échec
                    {
                        if ((ligne + colonne) % 2 == 0) { caseDeplacement.BackColor = Color.Yellow; }
                        else { caseDeplacement.BackColor = Color.Gold; }
                    }

                    if (doubleDeplacement != 99) // vérification de la posibilité de la double poussée
                    {
                        int ligneDouble = Convert.ToInt32(listcase[3].Substring(0, 1));
                        int colonneDouble = Convert.ToInt32(listcase[3].Substring(1, 1));
                        Case caseDouble = damier[ligneDouble][colonneDouble];
                        if (DeplacementSansEchec(button.Piece.EstBlanc, button, caseDeplacement))// vérification que le mouvement ne met pas notre roi en échec
                        {
                            if (caseDouble.Piece == null)
                            {
                                if ((ligneDouble + colonneDouble) % 2 == 0) { caseDouble.BackColor = Color.Yellow; }
                                else { caseDouble.BackColor = Color.Gold; }
                            }
                        }
                    }
                }
            }
            if (attack1 != 99) //vérification de la première case d'attaque
            {
                int ligne = Convert.ToInt32(listcase[1].Substring(0, 1));
                int colonne = Convert.ToInt32(listcase[1].Substring(1, 1));
                Case caseAttack1 = damier[ligne][colonne];
                if (DeplacementSansEchec(button.Piece.EstBlanc, button, caseAttack1))// vérification que le mouvement ne met pas notre roi en échec
                {
                    if (caseAttack1.Piece != null && caseAttack1.Piece.EstBlanc != button.Piece.EstBlanc)
                    {
                        if ((ligne + colonne) % 2 == 0) { caseAttack1.BackColor = Color.Red; }
                        else { caseAttack1.BackColor = Color.Red; }
                    }
                }
            }
            if (attack2 != 99)//vérification de la deuxième case d'attaque
            {
                int ligne = Convert.ToInt32(listcase[2].Substring(0, 1));
                int colonne = Convert.ToInt32(listcase[2].Substring(1, 1));
                Case caseAttack2 = damier[ligne][colonne];
                if (DeplacementSansEchec(button.Piece.EstBlanc, button, caseAttack2))// vérification que le mouvement ne met pas notre roi en échec
                {
                    if (caseAttack2.Piece != null && caseAttack2.Piece.EstBlanc != button.Piece.EstBlanc)
                    {
                        if ((ligne + colonne) % 2 == 0) { caseAttack2.BackColor = Color.Red; }
                        else { caseAttack2.BackColor = Color.Red; }
                    }
                }
            }

            if (button.Piece.EstBlanc && button.Ligne == 4 || !button.Piece.EstBlanc && button.Ligne == 3) // case possible pour la prise en passant
            {
                // cases menacées
                int ligne = button.Ligne;
                int colonneRight = Convert.ToInt32(listcase[4].Substring(1, 1));
                int colonneLeft = Convert.ToInt32(listcase[5].Substring(1, 1));

                // gestion de la case menacée à droite
                if (colonneRight < 8)
                {
                    Case casePassantRight = damier[ligne][colonneRight];
                    if (casePassantRight.Piece is Pion pion)
                    {
                        if (pion.DoubleAvance)
                        {
                            casePassantRight.BackColor = Color.Orange;
                        }
                    }
                }
                // gestion de la case menacée à gauche
                if (colonneLeft != 9)
                {
                    Case casePassantLeft = damier[ligne][colonneLeft];
                    if (casePassantLeft.Piece is Pion pion)
                    {
                        if (pion.DoubleAvance)
                        {
                            casePassantLeft.BackColor = Color.Orange;
                        }
                    }
                }
            }
        }
        /// <summary>
        /// Gestion de l'affichage des déplacements pour les rois.
        /// </summary>
        /// <param name="button">Le boutton sur lequel est le roi.</param>
        public void SelectionRoi(Case button)
        {
            Roi roi = (Roi)button.Piece;

            if (!roi.HasMoved) //vérification que le roi n'a pas encore bougé
            {
                if (roi.EstBlanc) //pour le roi blanc
                {
                    if (damier[0][0].Piece is Tour tour1 && damier[0][0].Piece.EstBlanc == roi.EstBlanc && damier[0][1].Piece == null && damier[0][2].Piece == null && damier[0][3].Piece == null) // tour à la bonne place - tour de la bonne couleur - pas de Piece entre
                    {
                        if (!IsMenacedFrom(false, 0, 1) && !IsMenacedFrom(false, 0, 2) && !IsMenacedFrom(false, 0, 3) && !IsMenacedFrom(false, 0, 4)) // vérification que les cases ne sont pas menacées
                        {
                            if (!tour1.HasMoved) //vérification que la tour n'a pas bougé
                            {
                                damier[0][0].BackColor = Color.Green;
                            }
                        }
                    }
                    if (damier[0][7].Piece is Tour tour2 && damier[0][7].Piece.EstBlanc == roi.EstBlanc && damier[0][5].Piece == null && damier[0][6].Piece == null) // tour à la bonne place - tour de la bonne couleur - pas de Piece entre
                    {
                        if (!IsMenacedFrom(false, 0, 4) && !IsMenacedFrom(false, 0, 5) && !IsMenacedFrom(false, 0, 6)) // vérification que les cases ne sont pas menacées
                        {
                            if (!tour2.HasMoved) //vérification que la tour n'a pas bougé
                            {
                                damier[0][7].BackColor = Color.Green;
                            }
                        }
                    }
                }
                else // pour le roi noir
                {
                    if (damier[7][0].Piece is Tour tour1 && damier[7][0].Piece.EstBlanc == roi.EstBlanc && damier[7][1].Piece == null && damier[7][2].Piece == null && damier[7][3].Piece == null) // tour à la bonne place - tour de la bonne couleur - pas de Piece entre
                    {
                        if (!IsMenacedFrom(true, 7, 1) && !IsMenacedFrom(true, 7, 2) && !IsMenacedFrom(true, 7, 3) && !IsMenacedFrom(true, 7, 4)) // vérification que les cases ne sont pas menacées
                        {
                            if (!tour1.HasMoved) //vérification que la tour n'a pas bougé
                            {
                                damier[7][0].BackColor = Color.Green;
                            }
                        }
                    }
                    if (damier[7][7].Piece is Tour tour2 && damier[7][7].Piece.EstBlanc == roi.EstBlanc && damier[7][5].Piece == null && damier[7][6].Piece == null) // tour à la bonne place - tour de la bonne couleur - pas de Piece entre
                    {
                        if (!IsMenacedFrom(true, 7, 4) && !IsMenacedFrom(true, 7, 5) && !IsMenacedFrom(true, 7, 6)) // vérification que les cases ne sont pas menacées
                        {
                            if (!tour2.HasMoved) //vérification que la tour n'a pas bougé
                            {
                                damier[7][7].BackColor = Color.Green;
                            }
                        }
                    }
                }
            }
        }
        /// <summary>
        /// Après que l'on a cliqué sur une case pour voir les déplacements.
        /// </summary>
        /// <param name="button">La case sur laquel on a cliqué.</param>
        public void Deplacement(Case button)
        {
            if (selectedCase.Piece.EstBlanc == colorToPlay)
            {
                //cacher les bouton d'abandon après un coup
                btnNoirAbandon.Visible = false;
                btnBlancAbandon.Visible = false;
                //affichage en text du déplacement de la pièce
                if (button.Piece != null) { lblInfo.Text = "Déplacement de " + selectedCase.Piece.Name + " en " + button.Name.Substring(3, 1) + button.Name.Substring(4, 1) + " dégustant: " + button.Piece.Name; }
                else { lblInfo.Text = "Déplacement de " + selectedCase.Piece.Name + " en " + button.Name.Substring(3, 1) + button.Name.Substring(4, 1); }
                bool color = selectedCase.Piece.EstBlanc;
                button.Piece = selectedCase.Piece;
                // changement des valeurs stockées dans la pièce

                if (selectedCase.Piece is Pion)
                {
                    Promotion(button);
                }
                // supprimer la pièce de sa case prècèdente
                selectedCase.Piece = null;

                if (button.Piece is Roi roi) { roi.HasMoved = true; }
                if (button.Piece is Tour tour) { tour.HasMoved = true; }

                // affichage si le roi est en échec ou non
                if (button.Piece.EstBlanc)
                {
                    lblEchec.Text = "Le roi noir n'est pas attaqué.";
                    foreach (var _ in DeplacementPossible(Menace(false), true).Where(casepossible => casepossible.Piece is Roi).Select(casepossible => new { }))
                    {
                        lblEchec.Text = "Echec au Roi noir";
                        btnNoirAbandon.Visible = true;
                    }
                }
                else
                {
                    lblEchec.Text = "Le roi blanc n'est pas attaqué.";
                    foreach (var _ in DeplacementPossible(Menace(true), false).Where(casepossible => casepossible.Piece is Roi).Select(casepossible => new { }))
                    {
                        lblEchec.Text = "Echec au Roi blanc";
                        btnBlancAbandon.Visible = true;
                    }
                }
                colorToPlay = !colorToPlay;
                //modification de l'affichage d'après les modifications faites plus haut.
                ResetDoubleAvance(!color);
                UpdateEvaluation();
            }
            else { lblInfo.Text = "No your turn to play"; }
            ResetColors();
            Actualiser();
        }
        /// <summary>
        /// Promotion automatique des pions en reine.
        /// </summary>
        /// <param name="button">Boutton sur lequel le pion se transforme en reine.</param>
        private void Promotion(Case button)
        {
            int departLigne = selectedCase.Ligne;
            if (button.Ligne == 7) // pour les pions blanc en dame sur la dernière ligne
            {
                button.Piece = new Dame(imageList.Images[4], true);
            }
            if (button.Ligne == 0) // pour les pions noir en dame sur la dernière ligne
            {
                button.Piece = new Dame(imageList.Images[4], false);
            }

            if (button.Ligne == 4 && departLigne == 6) // si le pion noir à fait une double poussée
            {
                Pion pion = (Pion)button.Piece;
                pion.DoubleAvance = true;
            }
            if (button.Ligne == 3 && departLigne == 1) // si le pion blanc à fait une double poussée
            {
                Pion pion = (Pion)button.Piece;
                pion.DoubleAvance = true;
            }
        }
        /// <summary>
        /// L'orsque que l'on clique sur une case.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnCases_Click(object sender, EventArgs e)
        {
            //pour le mat
            if (CanMove(true) == false)
            {
                lblInfo.Text = "les noirs sont mat";
                return;
            }
            if (CanMove(false) == false)
            {
                lblInfo.Text = "les blancs sont mat";
                return;
            }

            Case button = sender as Case;
            lblCase.Text = button.Name.Substring(3, 2);
            // déplacement du rock
            if (button.BackColor == Color.Green || button.BackColor == Color.DarkGreen)
            {
                Rock(button);
                return;
            }
            // gestion de la prise en passant
            if (button.BackColor == Color.Orange)
            {
                PrisePassant(button);
                return;
            }
            //capture et déplacement
            if (button.BackColor == Color.Yellow || button.BackColor == Color.Gold || button.BackColor == Color.Red) // si c'est une case de déplacement faire le déplacement
            {
                Deplacement(button);
                return;
            }

            ResetColors();
            if (button.Piece != null)
            {
                Selection(button);
            }
            else { lblInfo.Text = "Case vide"; }
        }
        /// <summary>
        /// En lui donnant une nouvelle position elle determine si le coup joué est légal ou non.
        /// </summary>
        /// <param name="estblanc">Si la pièce et blanche.</param>
        /// <param name="depart">Case de départ de la pièce.</param>
        /// <param name="arrivee">Case d'arrivée de la pièce.</param>
        /// <returns></returns>
        private bool DeplacementSansEchec(bool estblanc, Case depart, Case arrivee)
        {
            //stockage des valeurs initiales
            bool answer = true;
            Piece PieceDépart = depart.Piece;
            Piece PieceArrive;
            if (arrivee.Piece != null) { PieceArrive = arrivee.Piece; }
            else { PieceArrive = null; }
            //modification de la position
            arrivee.Piece = depart.Piece;
            depart.Piece = null;
            foreach (var _ in DeplacementPossible(Menace(estblanc), !estblanc).Where(casemenacee => casemenacee.Piece is Roi).Select(casemenacee => new { }))
            {
                answer = false;
            }
            //remise à zero de la position
            depart.Piece = PieceDépart;
            arrivee.Piece = PieceArrive;

            return answer;
        }
        /// <summary>
        /// Liste toutes les cases où l'on peux se déplacer.
        /// </summary>
        /// <param name="listcase">Liste de toutes les déplacements.</param>
        /// <param name="EstBlanc"></param>
        /// <returns></returns>
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
                    if (casePossible.Piece != null && casePossible.Piece.EstBlanc != EstBlanc) // pas de capture de nos pièces
                    {
                        listPossible.Add(casePossible);
                    }
                    if (casePossible.Piece != null) { j = listcase[i].Length; } // arret après rencontre d'une pièce
                }
            }
            return listPossible;
        }
        /// <summary>
        /// Boutton pour afficher les cases attaquée par les noirs.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnMenaceNoir_Click(object sender, EventArgs e)
        {
            ColorInRed(Menace(true));
        }
        /// <summary>
        /// Boutton pour afficher les cases attaquée par les blancs.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnMenaceBlanc_Click(object sender, EventArgs e)
        {
            ColorInRed(Menace(false));
        }
        /// <summary>
        /// Affiche en rouge la list de cases fournies.
        /// </summary>
        /// <param name="list"></param>
        private void ColorInRed(List<string> list)
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
        /// <summary>
        /// Fonction pour mettre à jour la barre dévaluation grace à la valeur des pièces sur le plateau.
        /// </summary>
        private void UpdateEvaluation()
        {
            int pointBlanc = 1;
            int pointNoir = 1;
            for (int i = 0; i < damier.Length; i++) // on parcours le plateau
            {
                for (int j = 0; j < (damier[i]).Length; j++)// on parcours le plateau
                {
                    if (damier[i][j].Piece != null)
                    {
                        if (damier[i][j].Piece.EstBlanc)
                        {
                            switch (damier[i][j].Piece)// addition de la valeur des pièces blaches
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
                            switch (damier[i][j].Piece)// addition de la valeur des pièces noires
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
            int dif = pointBlanc - pointNoir;
            int height = 200 + dif * nbPixel;
            if (height > maxHeight) { height = maxHeight; }
            if (height < minHeight) { height = minHeight; }
            //modification visuel de la barre
            buttonEvalWhite.Size = new Size(15, height);
        }
    }
}