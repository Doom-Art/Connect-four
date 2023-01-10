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
    internal class Shogi_Board
    {
        /*private Texture2D _pawn; //num 1,2,  promoted 17,18
        private Texture2D _lance; // num 3,4 promoted 19,20 
        private Texture2D _knight;// num 5,6 promoted 21,22
        private Texture2D _silverGeneral; // num 7,8 promoted 23,24
        private Texture2D _goldGeneral;// num 9, 10
        private Texture2D _bishop;// num 11,12 promoted 25,26
        private Texture2D _rook;//num 13,14 promoted 27,28
        private Texture2D _king;// num 15,16*/

        private int[,] _boardPositions;
        private ShogiPiece[,] _pieces;
        private List<ShogiPiece> _player1Bench;
        private List<ShogiPiece> _player2Bench;
        private Texture2D rectTexture;
        private bool isPieceClicked;
        private int pieceX; private int pieceY;

        public Shogi_Board(List<Texture2D> textures)
        {
            this.rectTexture = textures[0];
            isPieceClicked = false;
            //Benches
            _player1Bench = new List<ShogiPiece>();
            _player2Bench = new List<ShogiPiece>();
            //Empty Spaces
            _boardPositions = new int[9, 9];
            for (int i = 0; i < 9; i++)
                for (int j = 0; j < 9; j++)
                    _boardPositions[i, j] = 0;
            //Seting up board
            _pieces = new ShogiPiece[9, 9];
            for (int i = 0; i < 9; i++)//Pawn
                _pieces[i, 2] = new ShogiPiece(textures[1], textures[2], textures[17], textures[18], 1, 2);

            for (int i = 0; i < 9; i++)
                _pieces[i, 6] = new ShogiPiece(textures[1], textures[2], textures[17], textures[18], 1, 1);//Pawn

            _pieces[0, 0] = new ShogiPiece(textures[3], textures[4], textures[19], textures[20], 2, 2);//Lance
            _pieces[8, 0] = new ShogiPiece(textures[3], textures[4], textures[19], textures[20], 2, 2);
            _pieces[0, 8] = new ShogiPiece(textures[3], textures[4], textures[19], textures[20], 2, 1);
            _pieces[8, 8] = new ShogiPiece(textures[3], textures[4], textures[19], textures[20], 2, 1);//Lance
            _pieces[1, 0] = new ShogiPiece(textures[5], textures[6], textures[21], textures[22], 3, 2);//Knight
            _pieces[7, 0] = new ShogiPiece(textures[5], textures[6], textures[21], textures[22], 3, 2);
            _pieces[1, 8] = new ShogiPiece(textures[5], textures[6], textures[21], textures[22], 3, 1);
            _pieces[7, 8] = new ShogiPiece(textures[5], textures[6], textures[21], textures[22], 3, 1);//Knight
            _pieces[2, 0] = new ShogiPiece(textures[7], textures[8], textures[23], textures[24], 4, 2);//Silver
            _pieces[6, 0] = new ShogiPiece(textures[7], textures[8], textures[23], textures[24], 4, 2);
            _pieces[2, 8] = new ShogiPiece(textures[7], textures[8], textures[23], textures[24], 4, 1);
            _pieces[6, 8] = new ShogiPiece(textures[7], textures[8], textures[23], textures[24], 4, 1);//Silver
            _pieces[5, 8] = new ShogiPiece(textures[9], textures[10], 5, 1);//Gold
            _pieces[3, 8] = new ShogiPiece(textures[9], textures[10], 5, 1);
            _pieces[3, 0] = new ShogiPiece(textures[9], textures[10], 5, 2);
            _pieces[5, 0] = new ShogiPiece(textures[9], textures[10], 5, 2);//Gold
            _pieces[1, 7] = new ShogiPiece(textures[11], textures[12], textures[25], textures[26], 6, 1);//Bishop
            _pieces[7, 1] = new ShogiPiece(textures[11], textures[12], textures[25], textures[26], 6, 2);//Bishop
            _pieces[7, 7] = new ShogiPiece(textures[13], textures[14], textures[27], textures[28], 7, 1);//Rook
            _pieces[1, 1] = new ShogiPiece(textures[13], textures[14], textures[27], textures[28], 7, 2);//Rook
            _pieces[4, 8] = new ShogiPiece(textures[15], textures[16], 8, 1);//King
            _pieces[4, 0] = new ShogiPiece(textures[15], textures[16], 8, 2);//King

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(rectTexture, new Rectangle(123, 81, 543, 543), Color.Black);
            for (int i = 0; i < 9; ++i)
            {
                for (int j = 0; j < 9; ++j)
                {
                    if (_boardPositions[i,j] == 0)
                        spriteBatch.Draw(rectTexture, new Rectangle((60 * i)+125, (60 * j)+83, 59, 59), Color.BlanchedAlmond);
                    else
                        spriteBatch.Draw(rectTexture, new Rectangle((60 * i) + 125, (60 * j) + 83, 59, 59), Color.BurlyWood);
                }
            }
            for (int i = 0; i < 9; ++i)
            {
                for (int j = 0; j < 9; ++j)
                {
                    if (_pieces[i, j] != null)
                        _pieces[i, j].Draw(spriteBatch, new Rectangle((60 * i) + 125, (60 * j) + 83, 59, 59));
                }
            }
        }
        public void ResetSquareBoard()
        {
            for (int i = 0; i < 9; i++)
                for (int j = 0; j < 9; j++)
                    _boardPositions[i, j] = 0;
        }
        
        public bool MouseClicked(MouseState mouse, int playerTurn)
        {
            bool moved = false;
            if (isPieceClicked){
                for (int i = 0; i < 9; ++i)
                {
                    for (int j = 0; j < 9; ++j)
                    {
                        if (new Rectangle((60 * i) + 125, (60 * j) + 83, 59, 59).Contains(mouse.X, mouse.Y)){
                            if (_boardPositions[i,j] == -1){
                                if (_pieces[i,j] != null){
                                    if (_pieces[i, j].Player() == 1)
                                        _player2Bench.Add(_pieces[i, j]);
                                    else
                                        _player2Bench.Add(_pieces[i, j]);
                                }
                                _pieces[i, j] = _pieces[pieceX, pieceY];
                                _pieces[pieceX, pieceY] = null;
                                moved = true;
                            }
                        }
                    }
                }
                isPieceClicked = false;
                ResetSquareBoard();
            }
            else
            {
                for (int i = 0; i < 9; ++i)
                {
                    for (int j = 0; j < 9; ++j)
                    {
                        if (_pieces[i, j] != null){
                            if (new Rectangle((60 * i) + 125, (60 * j) + 83, 59, 59).Contains(mouse.X, mouse.Y)){
                                if (_pieces[i,j].Player() == playerTurn){
                                    ShogiPiece.PieceClicked(_boardPositions, _pieces, i, j);
                                    isPieceClicked = true;                                         
                                    pieceX = i; pieceY = j;
                                }
                            }
                        }
                    }
                }
            }
            return moved;
        }
        
    }
}
