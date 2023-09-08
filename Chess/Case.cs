using System.Windows.Forms;

namespace Chess
{
    public class Case : Button
    {
        public Piece Piece { get; set; }
        readonly public int Ligne;
        readonly public int Colonne;

        public Case(int ligne, int colonne)
        {
            Ligne = ligne;
            Colonne = colonne;
        }
        public void Draw() // dessine le pièce si la case en possède une, efface la pièce si elle n'en possède plus
        {
            if (Piece != null)
            {
                Image = Piece.Pictogramme;
            }
            else { Image = null; }
        }
    }
}
