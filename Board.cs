using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using Color = Microsoft.Xna.Framework.Color;
using Rectangle = Microsoft.Xna.Framework.Rectangle;

namespace Connect_four
{
    internal class Board
    {
        private Texture2D _board;
        private Texture2D _piece;
        private int[,] _boardPositions;
        public static Random rand = new Random();
        private int prevX; int prevY;
        private Rectangle _pieceDropRect;
        private Color p1Color; private int p1ColorNum;
        private Color p2Color; private int p2ColorNum;

        public Board(Texture2D board, Texture2D piece)
        {
            _board = board;
            _piece = piece;
            _boardPositions = new int[7, 6];
            prevX= 0;
            prevY = 0;
            p1Color = Color.ForestGreen;
            p2Color = Color.DarkOrange;
        }
        public void PieceFall(int j)
        {
            if (_pieceDropRect.Y < (100 + (100 * j)))
                _pieceDropRect.Y += 10;
        }
        public void ColorsSet(List<Color> colors, Random rand)
        {
            p1ColorNum = rand.Next(0, colors.Count);
            p2ColorNum = rand.Next(0, colors.Count);
            while (p2ColorNum == p1ColorNum)
            {
                p2ColorNum = rand.Next(0, colors.Count);
            }
            this.p1Color = colors[p1ColorNum];
            this.p2Color = colors[p2ColorNum];
            
        }
        public void ColorsSet(Color color1, Color color2)
        {
            this.p1Color = color1;
            this.p2Color = color2;
        }
        public void P1Color(Color color)
        {
            this.p1Color = color;
        }
        public void P2Color(Color color)
        {
            this.p2Color = color;
        }
        public Color P1Color()
        {
            return this.p1Color;
        }
        public Color P2Color()
        {
            return this.p2Color;
        }
        public bool DidPieceFall()
        {
            return _pieceDropRect.Y == (100 + (100 * prevY));
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < 7; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    if (i == prevX && j == prevY){
                        if (_boardPositions[i,j] == 2){
                            spriteBatch.Draw(_piece, _pieceDropRect, p2Color);
                        }
                        else if (_boardPositions[i, j] == 1){
                            spriteBatch.Draw(_piece, _pieceDropRect, p1Color);
                        }
                        else if (_boardPositions[i,j] == 3){
                            spriteBatch.Draw(_piece, _pieceDropRect, Color.Yellow);
                        }
                        else{
                            spriteBatch.Draw(_piece, _pieceDropRect, Color.White);
                        }
                        PieceFall(j);
                    }
                    else if (_boardPositions[i,j] == 0)
                        spriteBatch.Draw(_piece, new Rectangle((6+ (i*114)),(100 + (j*100)), 100, 100), Color.White);
                    else if (_boardPositions[i, j] == 1)
                        spriteBatch.Draw(_piece, new Rectangle((6 + (i * 114)), (100 + (j * 100)), 100, 100), p1Color);
                    else if (_boardPositions[i, j] == 2)
                        spriteBatch.Draw(_piece, new Rectangle((6 + (i * 114)), (100 + (j * 100)), 100, 100), p2Color);
                    else if (_boardPositions[i, j] == 3)
                        spriteBatch.Draw(_piece, new Rectangle((6 + (i * 114)), (100 + (j * 100)), 100, 100), Color.Yellow);
                }
            }
            spriteBatch.Draw(_board, new Rectangle(0, 100, 800, 600), Color.White);
        }
        public bool PlayerTurnAI(int player)
        {
            bool moved = false;
            int row = rand.Next(0, 7);
            for (int i = 5; i >= 0; i--)
            {
                if (_boardPositions[row, i] == 0 && !moved)
                {
                    moved = true;
                    _boardPositions[row, i] = player;
                }
            }
            return moved;
        }
        public void Undo()
        {
            _boardPositions[prevX,prevY] = 0;
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
                        prevX = 0; prevY = i;
                        _pieceDropRect = new Rectangle((6 + (0 * 114)), (80), 100, 100);
                    }
                }
            }
            else if (mouseState.X <= 230){
                for (int i = 5; i >= 0; i--)
                {
                    if (_boardPositions[1, i] == 0&& !moved){
                        moved = true;
                        _boardPositions[1, i] = player;
                        prevX = 1; prevY = i;
                        _pieceDropRect = new Rectangle((6 + (1 * 114)), (80), 100, 100);
                    }
                }
            }
            else if (mouseState.X <= 341){
                for (int i = 5; i >= 0; i--)
                {
                    if (_boardPositions[2, i] == 0 && !moved){
                        moved = true;
                        _boardPositions[2, i] = player;
                        prevX = 2; prevY = i;
                        _pieceDropRect = new Rectangle((6 + (2 * 114)), (80), 100, 100);
                    }
                }
            }
            else if (mouseState.X <= 456){
                for (int i = 5; i >= 0; i--)
                {
                    if (_boardPositions[3, i] == 0 && !moved){
                        moved = true;
                        _boardPositions[3, i] = player;
                        prevX = 3; prevY = i;
                        _pieceDropRect = new Rectangle((6 + (3 * 114)), (80), 100, 100);
                    }
                }
            }
            else if(mouseState.X <= 569){
                for (int i = 5; i >= 0; i--)
                {
                    if (_boardPositions[4, i] == 0 && !moved){
                        moved = true;
                        _boardPositions[4, i] = player;
                        prevX = 4; prevY = i;
                        _pieceDropRect = new Rectangle((6 + (4 * 114)), (80), 100, 100);
                    }
                }
            }
            else if (mouseState.X <= 681){
                for (int i = 5; i >= 0; i--)
                {
                    if (_boardPositions[5, i] == 0 && !moved){
                        moved = true;
                        _boardPositions[5, i] = player;
                        prevX = 5; prevY = i;
                        _pieceDropRect = new Rectangle((6 + (5 * 114)), (80), 100, 100);
                    }
                }
            }
            else{
                for (int i = 5; i >= 0; i--)
                {
                    if (_boardPositions[6, i] == 0 && !moved){
                        moved = true;
                        _boardPositions[6, i] = player;
                        prevX = 6; prevY = i;
                        _pieceDropRect = new Rectangle((6 + (6 * 114)), (80), 100, 100);
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
                        if (_boardPositions[i,j] == p && _boardPositions[i, j+1] == p && _boardPositions[i, j + 2] == p && _boardPositions[i, j + 3] == p){
                            _boardPositions[i, j] = 3; _boardPositions[i, j+1] = 3; _boardPositions[i, j+2] = 3; _boardPositions[i, j+3] = 3;
                            winner = p;
                        }
                //Checking for horizontal 4 in a row
                for (int i = 0; i < 6; i++)
                    for (int j = 0; j < 4; j++)
                        if (_boardPositions[j, i] == p && _boardPositions[j + 1, i] == p && _boardPositions[j + 2, i] == p && _boardPositions[j + 3,i] == p){
                            _boardPositions[j, i] = 3; _boardPositions[j+1, i] = 3; _boardPositions[j+2, i] = 3; _boardPositions[j+3, i] = 3;
                            winner = p;
                        }
                //Checking for diagonal 4 in a row
                for (int i = 0; i < 7; i++)
                    for (int j = 0; j < 6; j++)
                        if (i+3 < 7 && j+3 < 6)
                            if (_boardPositions[i, j] == p && _boardPositions[i+1, j+1] == p && _boardPositions[i+2, j+2] == p && _boardPositions[i+3, j+3] == p){
                                _boardPositions[i, j] =3 ; _boardPositions[i + 1, j + 1] = 3;  _boardPositions[i + 2, j + 2] =3;  _boardPositions[i + 3, j + 3] = 3;
                                winner = p;
                            }
                for (int i = 0; i < 7; i++)
                    for (int j = 5; j >= 0; j--)
                        if (i + 3 < 7 && j - 3 >=0)
                            if (_boardPositions[i, j] == p && _boardPositions[i + 1, j - 1] == p && _boardPositions[i + 2, j - 2] == p && _boardPositions[i + 3, j - 3] == p){
                                _boardPositions[i, j] = 3; _boardPositions[i + 1, j - 1] = 3; _boardPositions[i + 2, j - 2] =3; _boardPositions[i + 3, j - 3] = 3;
                                winner = p;
                            }
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
            _pieceDropRect = new Rectangle(0, 0, 0, 0);
        }
        public void SaveGame(StreamWriter writer, int playerTurn)
        {
            string temp;
            writer.WriteLine(playerTurn);
            writer.WriteLine((p1ColorNum));
            writer.WriteLine((p2ColorNum));
            for (int j = 0; j<6; j++)
            {
                temp = "";
                for (int i = 0; i < 7; i++)
                {
                    temp += _boardPositions[i, j];
                }
                writer.WriteLine(temp);
            }
        }
        public int LoadGame(string txtFileName, List<Color> colors, int player)
        {
            int playerTurn = player;
            int j = 0;
            int lineNum = 0;
            prevX = -1;
            if (File.Exists(txtFileName))
            {
                foreach (string line in File.ReadLines(@txtFileName))
                {
                    if (lineNum == 0)
                    {
                        Int32.TryParse(line, out playerTurn);
                        lineNum++;
                    }
                    else if (lineNum == 1)
                    {
                        p1Color = colors[Convert.ToInt32(line)];
                        lineNum++;
                    }
                    else if (lineNum == 2)
                    {
                        p2Color = colors[Convert.ToInt32(line)];
                        lineNum++;
                    }
                    else
                    {
                        for (int i = 0; i < 7; i++)
                        {
                            string temp = "";
                            temp += line[i];
                            Int32.TryParse(temp, out int d);
                            _boardPositions[i, j] = d;
                        }
                        j++;
                    }
                }
            }
            return playerTurn;
        }
    }
}
