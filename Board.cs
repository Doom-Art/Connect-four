using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connect_four
{
    internal class Board
    {
        private Texture2D _board;
        private Texture2D _piece;
        private int[,] _boardPositions;

        public Board(Texture2D board, Texture2D piece)
        {
            _board = board;
            _piece = piece;
            _boardPositions = new int[6, 7];
            for (int i = 0; i < 6; i++)
                for (int j = 0; j < 7; j++)
                    _boardPositions[i, j] = 0;
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_board, new Rectangle(0, 0, 800, 600), Color.White);
            foreach (int coordinate in _boardPositions)
            {

            }
        }
    }
}
