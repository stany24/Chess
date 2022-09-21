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
        public Piece piece { get; set; }

        public void Draw() // dessine le pièce si la case en possède une, efface la pièce si elle n'en possède plus
        {
            if (piece != null){Image = piece.Pictogramme;
            }else{Image = null;}
        }
    }
}
