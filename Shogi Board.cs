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
        private ShogiBench p1Bench;
        private ShogiBench p2Bench;
        private Texture2D _rectTexture;
        private bool _isPieceClicked;
        private int _pieceX; private int _pieceY;
        private int _dropPieceType;

        public Shogi_Board(List<Texture2D> textures, SpriteFont font)
        {
            this._rectTexture = textures[0];
            _isPieceClicked = false;
            //Benches
            p1Bench = new ShogiBench(textures, font, 15, 120, 1);
            p2Bench = new ShogiBench(textures, font, 725, 120, 2);
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
            _pieces[5, 8] = new ShogiPiece(textures[9], textures[10], textures[9], textures[10], 5, 1);//Gold
            _pieces[3, 8] = new ShogiPiece(textures[9], textures[10], textures[9], textures[10], 5, 1);
            _pieces[3, 0] = new ShogiPiece(textures[9], textures[10], textures[9], textures[10], 5, 2);
            _pieces[5, 0] = new ShogiPiece(textures[9], textures[10], textures[9], textures[10], 5, 2);//Gold
            _pieces[1, 7] = new ShogiPiece(textures[11], textures[12], textures[25], textures[26], 6, 1);//Bishop
            _pieces[7, 1] = new ShogiPiece(textures[11], textures[12], textures[25], textures[26], 6, 2);//Bishop
            _pieces[7, 7] = new ShogiPiece(textures[13], textures[14], textures[27], textures[28], 7, 1);//Rook
            _pieces[1, 1] = new ShogiPiece(textures[13], textures[14], textures[27], textures[28], 7, 2);//Rook
            _pieces[4, 8] = new ShogiPiece(textures[15], textures[16], textures[15], textures[16], 8, 1);//King
            _pieces[4, 0] = new ShogiPiece(textures[15], textures[16], textures[15], textures[16], 8, 2);//King

            p1Bench.ClonePiece(_pieces[0, 6].Clone(), _pieces[0, 8].Clone(), _pieces[1, 8].Clone(), _pieces[2, 8].Clone(), _pieces[3, 8].Clone(), _pieces[1, 7].Clone(), _pieces[7, 7].Clone());
            p2Bench.ClonePiece(_pieces[0, 2].Clone(), _pieces[0, 0].Clone(), _pieces[1, 0].Clone(), _pieces[2, 0].Clone(), _pieces[3, 0].Clone(), _pieces[7, 1].Clone(), _pieces[1, 1].Clone());

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_rectTexture, new Rectangle(123, 81, 543, 543), Color.Black);
            for (int i = 0; i < 9; ++i)
            {
                for (int j = 0; j < 9; ++j)
                {
                    if (_boardPositions[i,j] == 0)
                        spriteBatch.Draw(_rectTexture, new Rectangle((60 * i)+125, (60 * j)+83, 59, 59), Color.BlanchedAlmond);
                    else
                        spriteBatch.Draw(_rectTexture, new Rectangle((60 * i) + 125, (60 * j) + 83, 59, 59), Color.BurlyWood);
                }
            }
            for (int i = 0; i < 9; ++i)
            {
                for (int j = 0; j < 9; ++j)
                {
                    if (_pieces[i, j] != null)
                        _pieces[i, j].Draw(spriteBatch, new Rectangle((60 * i) + 126, (60 * j) + 84, 57, 57));
                }
            }
            p1Bench.Draw(spriteBatch);
            p2Bench.Draw(spriteBatch);
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
            if (_isPieceClicked){
                for (int i = 0; i < 9; ++i)
                    for (int j = 0; j < 9; ++j)
                        if (new Rectangle((60 * i) + 126, (60 * j) + 84, 57, 57).Contains(mouse.X, mouse.Y))
                            if (_boardPositions[i, j] == -1) {
                                if (_pieceX == -1){
                                    if (playerTurn == 1){
                                        _pieces[i, j] = p1Bench.PieceToDrop(_dropPieceType).Clone();
                                    }
                                    else{
                                        _pieces[i, j] = p2Bench.PieceToDrop(_dropPieceType).Clone();
                                    }
                                    moved = true;
                                }
                                else if (!moved){
                                    if (playerTurn == 1){
                                        if (j <= 2 || _pieceY <= 2)
                                            _pieces[_pieceX, _pieceY].Promote();
                                    }
                                    else if (playerTurn == 2){
                                        if (j >= 6 || _pieceY >= 6)
                                            _pieces[_pieceX, _pieceY].Promote();
                                    }
                                    if (_pieces[i, j] != null){
                                        if (playerTurn == 1){
                                            p1Bench.AddPiece(_pieces[i, j].PieceType());
                                        }
                                        else{
                                            p2Bench.AddPiece(_pieces[i, j].PieceType());
                                        }
                                    }
                                    _pieces[i, j] = _pieces[_pieceX, _pieceY];
                                    _pieces[_pieceX, _pieceY] = null;
                                    moved = true;
                                }
                            }
                _isPieceClicked = false;
                ResetSquareBoard();
            }
            else
            {
                for (int i = 0; i < 9; ++i)
                {
                    for (int j = 0; j < 9; ++j)
                    {
                        if (_pieces[i, j] != null){
                            if (new Rectangle((60 * i) + 125, (60 * j) + 83, 58, 58).Contains(mouse.X, mouse.Y)){
                                if (_pieces[i,j].Player() == playerTurn){
                                    ShogiPiece.PieceClicked(_boardPositions, _pieces, i, j,playerTurn);
                                    _isPieceClicked = true;                                         
                                    _pieceX = i; _pieceY = j;
                                }
                            }
                        }
                    }
                }
                if (!_isPieceClicked)
                {
                    if (playerTurn == 1)
                    {
                        int temp = p1Bench.IsBenchClicked(mouse);
                        if (temp != -1)
                        {
                            if (temp == 1 || temp == 2)
                            {
                                for (int i = 0; i < 9; ++i)
                                    for (int j = 1; j < 9; j++)
                                        if (_pieces[i, j] == null)
                                            _boardPositions[i, j] = -1;
                            }
                            else if (temp == 3)
                            {
                                for (int i = 0; i < 9; ++i)
                                    for (int j = 2; j < 9; j++)
                                        if (_pieces[i, j] == null)
                                            _boardPositions[i, j] = -1;
                            }
                            else
                            {
                                for (int i = 0; i < 9; ++i)
                                    for (int j = 0; j < 9; j++)
                                        if (_pieces[i, j] == null)
                                            _boardPositions[i, j] = -1;
                            }
                            _isPieceClicked = true; _pieceX = -1; _dropPieceType = temp;
                        }
                    }
                    else{
                        int temp = p2Bench.IsBenchClicked(mouse);
                        if (temp != -1)
                        {
                            if (temp == 1 || temp == 2)
                            {
                                for (int i = 0; i < 9; ++i)
                                    for (int j = 7; j >= 0; j--)
                                        if (_pieces[i, j] == null)
                                            _boardPositions[i, j] = -1;
                            }
                            else if (temp == 3)
                            {
                                for (int i = 0; i < 9; ++i)
                                    for (int j = 6; j >= 0; j--)
                                        if (_pieces[i, j] == null)
                                            _boardPositions[i, j] = -1;
                            }
                            else
                            {
                                for (int i = 0; i < 9; ++i)
                                    for (int j = 0; j < 9; j++)
                                        if (_pieces[i, j] == null)
                                            _boardPositions[i, j] = -1;
                            }
                            _isPieceClicked = true; _pieceX = -1; _dropPieceType = temp;
                        }
                    }
                }
            }
            return moved;
        }
        public bool DoublePawn(int playerTurn)
        {
            bool doublePawn = false;
            for (int i=0; i < 9; i++)
            {
                bool temp = false;
                for (int j = 0; j < 9; j++)
                {
                    if (_pieces[i,j] != null){
                        if (_pieces[i, j].Player() != playerTurn && _pieces[i, j].PieceType() == 1 && !_pieces[i, j].Promoted()){
                            if (temp)
                                doublePawn = true;
                            else
                                temp = true;
                        }
                    }
                }
            }
            return doublePawn;
        }
        
    }
}
