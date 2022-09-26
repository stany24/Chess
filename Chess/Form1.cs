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
        public int selectedLigne; // ligne de la case séléctionnée
        public int selectedColumn; // colonne de la case séléctionnée

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
                    if ((i+j)%2 == 0){cases.BackColor = Color.White;
                    }else { cases.BackColor = Color.Gray;}
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

        private void btnActualiser_Click(object sender, EventArgs e) 
        {
            Actualiser();
        }

        private void btnCommencer_Click(object sender, EventArgs e) // Instanciation initiale des pièces
        {
            damier[0][0].piece = new Tour(imageList.Images[1], true,0,0);
            damier[0][7].piece = new Tour(imageList.Images[1], true,0,7);
            damier[7][0].piece = new Tour(imageList.Images[7], false,7,0);
            damier[7][7].piece = new Tour(imageList.Images[7], false,7,7);
            damier[0][3].piece = new Roi(imageList.Images[5], true,0,3);
            damier[7][3].piece = new Roi(imageList.Images[11], false,7,3);
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
            List<string> listMenace = new List<string>();
            for (int i = 0; i < damier.Length; i++)
            {
                for (int j = 0; j < (damier[i]).Length; j++)
                {
                    if(damier[i][j].piece!=null)
                    {
                        if( damier[i][j].piece.EstBlanc != EstNoir)
                        {
                            foreach (string list in damier[i][j].piece.Deplacement())
                            {
                                listMenace.Add(list);
                            }
                        }
                    }
                }
            }
            return listMenace;
        }

        private void ResetColors()// reset toutes les couleurs des cases
        {
            for (int i = 0; i < damier.Length; i++)
            {
                for (int j = 0; j < (damier[i]).Length; j++)
                {
                    if ((i + j) % 2 == 0) { damier[i][j].BackColor = Color.White; }
                    else { damier[i][j].BackColor = Color.Gray; }
                }
            }
        }

        private void ResetColorIn(List<string> listToReset) // reset la couleur des cases de la liste
        {
  
            for (int i = 0; i < listToReset.Count; i++)
            {
                for (int j = 0; j * 3 < listToReset[i].Length; j++)
                {
                    int ligne = Convert.ToInt32(listToReset[i].Substring(0 + 3 * j, 1));
                    int colonne = Convert.ToInt32(listToReset[i].Substring(1 + 3 * j, 1));
                    if ((ligne + colonne) % 2 == 0) { damier[ligne][colonne].BackColor = Color.White; }
                    else { damier[ligne][colonne].BackColor = Color.Gray; }
                }
            }
        }

        private void btnCases_Click(object sender, EventArgs e) // lorsque une case est cliquée
        {
            Case button = sender as Case;

            if (button.BackColor == Color.Yellow || button.BackColor == Color.Gold || button.BackColor == Color.Red ) // si c'est une case de déplacement faire le déplacement
            {
                btnNoirAbandon.Visible = false;
                btnBlancAbandon.Visible = false;
                Piece selectedPiece = damier[selectedLigne][selectedColumn].piece;
                lblInfo.Text = "Déplacement de " + selectedPiece.Name + " en " + button.Name.Substring(3,1) + button.Name.Substring(4,1);
                button.piece = selectedPiece;
                button.piece.Ligne = Convert.ToInt32(button.Name.Substring(4, 1))-1; 
                button.piece.Column = Convert.ToInt32(LetterToNumber(button.Name.Substring(3, 1)));
                damier[selectedLigne][selectedColumn].piece = null;
                if (button.piece.EstBlanc){ // verification des échec pour les rois
                    lblEchec.Text = "Test de la situation du roi noir";
                    List<string> listCaseMenacee = Menace(false);
                    foreach(Case casepossible in DeplacementPossible(listCaseMenacee, true))
                    {
                        if (casepossible.piece is Roi){
                            lblEchec.Text = "Echec au Roi noir";
                            btnNoirAbandon.Visible = true;
                        }
                    }
                }
                else{
                    lblEchec.Text = "Test de la situation du roi blanc";
                    List<string> listCaseMenacee = Menace(true);
                    foreach (Case casepossible in DeplacementPossible(listCaseMenacee, false))
                    {
                        if (casepossible.piece is Roi){
                            lblEchec.Text = "Echec au Roi blanc";
                            btnBlancAbandon.Visible = true;
                        }
                    }
                }
                ResetColors();
                Actualiser();
                return;
            }

            ResetColors();

            if (button.piece != null) // affichage des déplacement possibles
            {
                lblEchec.Text = "";
                selectedLigne = button.piece.Ligne;
                selectedColumn = button.piece.Column;
                button.BackColor = Color.LightBlue;
                lblInfo.Text = "";
                foreach (string item in button.piece.Deplacement())
                {
                    lblInfo.Text += NumberToLetter(Convert.ToInt32(item.Substring(0, 1)))+ item.Substring(1, 1)+" ";
                }
                List<string> listcase = button.piece.Deplacement();
                for (int i = 0; i < listcase.Count; i++)
                {
                    for (int j = 0; j*3 < listcase[i].Length; j++)
                    {
                        int ligne = Convert.ToInt32(listcase[i].Substring(0+3*j, 1));
                        int colonne = Convert.ToInt32(listcase[i].Substring(1+3*j, 1));
                        Case casePossible = damier[ligne][colonne];
                        if (casePossible.piece == null || casePossible.piece.EstBlanc != button.piece.EstBlanc) // pas de capture de nos pièces
                        {
                            if(DeplacementSansEchec(button.piece.EstBlanc,button,casePossible))
                            {
                                if ((ligne + colonne) % 2 == 0) { casePossible.BackColor = Color.Yellow; }
                                else { casePossible.BackColor = Color.Gold; }
                                if (casePossible.piece != null && casePossible.piece.EstBlanc != button.piece.EstBlanc) { casePossible.BackColor = Color.Red; } // si pièce ennemie mettre en rouge
                                if (button.piece is Roi) // si c'est un roi enlever les cases menacées
                                {
                                    if (button.piece.EstBlanc != true) { ResetColorIn(Menace(false)); }
                                    else { ResetColorIn(Menace(true)); }
                                }
                            }
                        }
                        if (casePossible.piece != null) {j= listcase[i].Length;} // arret après rencontre d'une pièce
                    }
                }
            }
            else { lblInfo.Text = "Case vide"; }
        }

        private bool DeplacementSansEchec(bool estblanc,Case depart,Case arrivee)
        {
            bool answer = true;
            Piece PieceDépart = depart.piece;
            Piece PieceArrive;
            if (arrivee.piece != null){PieceArrive = arrivee.piece;}
            else{PieceArrive = null;}
            

            arrivee.piece = depart.piece;
            depart.piece = null;

            foreach (Case casemenacee in DeplacementPossible(Menace(estblanc),!estblanc))
            {
                if (casemenacee.piece is Roi)
                    answer = false;
            }
            depart.piece = PieceDépart;
            arrivee.piece = PieceArrive;

            return answer;
        }

        private List<Case> DeplacementPossible(List<String> listcase, bool EstBlanc)
        {
            List<Case> listPossible = new List<Case>();
            for (int i = 0; i < listcase.Count; i++)
            {
                for (int j = 0; j * 3 < listcase[i].Length; j++)
                {
                    int ligne = Convert.ToInt32(listcase[i].Substring(0 + 3 * j, 1));
                    int colonne = Convert.ToInt32(listcase[i].Substring(1 + 3 * j, 1));
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

        private List<Case> StringToCase(List<String> liststring)
        {
            List<Case> listcase = new List<Case>();
            for (int i = 0; i < liststring.Count; i++)
            {
                for (int j = 0; j * 3 < liststring[i].Length; j++)
                {
                    int ligne = Convert.ToInt32(liststring[i].Substring(0 + 3 * j, 1));
                    int colonne = Convert.ToInt32(liststring[i].Substring(1 + 3 * j, 1));
                    listcase.Add(damier[ligne][colonne]);
                }
            }
            return listcase;
        }

        private void btnMenaceNoir_Click(object sender, EventArgs e)
        {
            ColorInRed(Menace(true));
        }

        private void btnMenaceBlanc_Click(object sender, EventArgs e)
        {
            ColorInRed(Menace(false));
        }

        private void ColorInRed(List<string> list) // affiche en rouge les cases fournies
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
    }
}