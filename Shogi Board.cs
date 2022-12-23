using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connect_four
{
    internal class Shogi_Board
    {
        private Texture2D _pawn;
        private Texture2D _lance;
        private Texture2D _knight;
        private Texture2D _silverGeneral;
        private Texture2D _goldGeneral;
        private Texture2D _king;
        private Texture2D _bishop;
        private Texture2D _rook;
        private int[,] _boardPositions;
        public Shogi_Board(Texture2D pawn, Texture2D lance, Texture2D knight, Texture2D silverGeneral, Texture2D goldGeneral, Texture2D king, Texture2D bishop, Texture2D rook)
        {
            _pawn = pawn;
            _lance = lance;
            _knight = knight;
            _silverGeneral = silverGeneral;
            _goldGeneral = goldGeneral;
            _king = king;
            _bishop = bishop;
            _rook = rook;
            _boardPositions = new int[9,9];
        }
        
    }
}
