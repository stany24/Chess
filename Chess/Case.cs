using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Chess
{
    public class Case:Button
    {
        public Piece Piece { get; set; }
        public int Ligne { get; set; }
        public int Colonne { get; set; }

        public void Draw() // dessine le pièce si la case en possède une, efface la pièce si elle n'en possède plus
        {
            if (Piece != null){Image = Piece.Pictogramme;
            }else{Image = null;}
        }
    }
}
