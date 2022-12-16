using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
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
            _boardPositions = new int[7, 6];
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < 7; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    if (_boardPositions[i,j] == 0)
                        spriteBatch.Draw(_piece, new Rectangle((6+ (i*114)),(100 + (j*100)), 100, 100), Color.White);
                    else if (_boardPositions[i, j] == 1)
                        spriteBatch.Draw(_piece, new Rectangle((6 + (i * 114)), (100 + (j * 100)), 100, 100), Color.Red);
                    else if (_boardPositions[i, j] == 2)
                        spriteBatch.Draw(_piece, new Rectangle((6 + (i * 114)), (100 + (j * 100)), 100, 100), Color.Black);
                    else if (_boardPositions[i, j] == 3)
                        spriteBatch.Draw(_piece, new Rectangle((6 + (i * 114)), (100 + (j * 100)), 100, 100), Color.Yellow);
                }
            }
            spriteBatch.Draw(_board, new Rectangle(0, 100, 800, 600), Color.White);
        }
        public bool PlayerTurn(MouseState mouseState, int player)
        {
            bool moved = false;
            if (mouseState.X <= 115){
                for (int i = 5; i >= 0; i--)
                {
                    if (_boardPositions[0,i] == 0 && !moved){
                        moved = true;
                        _boardPositions[0,i] = player;
                    }
                }
            }
            else if (mouseState.X <= 230){
                for (int i = 5; i >= 0; i--)
                {
                    if (_boardPositions[1, i] == 0&& !moved){
                        moved = true;
                        _boardPositions[1, i] = player;
                    }
                }
            }
            else if (mouseState.X <= 341){
                for (int i = 5; i >= 0; i--)
                {
                    if (_boardPositions[2, i] == 0 && !moved){
                        moved = true;
                        _boardPositions[2, i] = player;
                    }
                }
            }
            else if (mouseState.X <= 456){
                for (int i = 5; i >= 0; i--)
                {
                    if (_boardPositions[3, i] == 0 && !moved){
                        moved = true;
                        _boardPositions[3, i] = player;
                    }
                }
            }
            else if(mouseState.X <= 569){
                for (int i = 5; i >= 0; i--)
                {
                    if (_boardPositions[4, i] == 0 && !moved){
                        moved = true;
                        _boardPositions[4, i] = player;
                    }
                }
            }
            else if (mouseState.X <= 681){
                for (int i = 5; i >= 0; i--)
                {
                    if (_boardPositions[5, i] == 0 && !moved){
                        moved = true;
                        _boardPositions[5, i] = player;
                    }
                }
            }
            else{
                for (int i = 5; i >= 0; i--)
                {
                    if (_boardPositions[6, i] == 0 && !moved){
                        moved = true;
                        _boardPositions[6, i] = player;
                    }
                }
            }
            return moved;
        }
        /// <summary>
        /// Checks if game is locked in a stalemate
        /// </summary>
        /// <returns> True if stalemate, False if no stalemate</returns>
        public bool CheckStalemate()
        {
            bool stalemate = true;
            for (int i =0; i<7; i++)
                for(int j = 0; j<6; j++)
                    if (_boardPositions[i,j] ==0)
                        stalemate = false;
            return stalemate;
        }
        public int CheckForFour()
        {
            int winner = 0;
            
            //Repeating following loop 2 times to test for both player 1 or 2 got 4 in a row
            for (int p = 1; p < 3; p++)
            {
                //Checking for vertical 4 in a row
                for (int i = 0; i < 7; i++)
                    for (int j = 0; j < 3; j++)
                        if (_boardPositions[i,j] == p && _boardPositions[i, j+1] == p && _boardPositions[i, j + 2] == p && _boardPositions[i, j + 3] == p)
                            winner = p;
                //Checking for horizontal 4 in a row
                for (int i = 0; i < 6; i++)
                    for (int j = 0; j < 4; j++)
                        if (_boardPositions[j, i] == p && _boardPositions[j + 1, i] == p && _boardPositions[j + 2, i] == p && _boardPositions[j + 3,i] == p)
                            winner = p;
                //Checking for diagonal 4 in a row
                for (int i = 0; i < 7; i++)
                    for (int j = 0; j < 6; j++)
                        if (i+3 < 7 && j+3 < 6)
                            if (_boardPositions[i, j] == p && _boardPositions[i+1, j+1] == p && _boardPositions[i+2, j+2] == p && _boardPositions[i+3, j+3] == p)
                                winner = p;
                for (int i = 0; i < 7; i++)
                    for (int j = 5; j >= 0; j--)
                        if (i + 3 < 7 && j - 3 >=0)
                            if (_boardPositions[i, j] == p && _boardPositions[i + 1, j - 1] == p && _boardPositions[i + 2, j - 2] == p && _boardPositions[i + 3, j - 3] == p)
                                winner = p;
            }

            return winner;
        }
        /// <summary>
        /// Resets the board to an empty state
        /// </summary>
        public void Reset()
        {
            for (int i = 0; i < 7; i++)
                for (int j = 0; j < 6; j++)
                    _boardPositions[i, j] = 0;
        }
    }
}
