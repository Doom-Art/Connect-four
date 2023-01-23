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
    internal class CheckersBoard
    {
        private int[,] _boardPositions;
        private Texture2D _rectangleTex;
        private CheckerPiece[,] _piecePositions;
        private bool _isPieceClicked;
        private int _selectedX, _selectedY;
        private int xTake, yTake;
        private List<int> _piecesToTake;
        public CheckersBoard(Texture2D rect, Texture2D checkersPiece, Texture2D checkersKing)
        {
            _rectangleTex= rect;
            _boardPositions = new int[8, 8];
            _piecePositions = new CheckerPiece[8, 8];
            _piecesToTake = new List<int>();
            _isPieceClicked = false;
            ResetGame(checkersPiece, checkersKing);
            xTake = -1;
        }
        public void ResetBoard()
        {
            for (int i = 0; i < 8; i += 1)
            {
                for (int j = 0; j < 8; j += 1)
                {
                    _boardPositions[i, j] = 0;
                }
            }
            for (int i = 0; i < 8; i += 2)
            {
                for (int j = 1; j < 8; j += 2)
                {
                    _boardPositions[i, j] = 1;
                }
            }
            for (int i = 1; i < 8; i += 2)
            {
                for (int j = 0; j < 8; j += 2)
                {
                    _boardPositions[i, j] = 1;
                }
            }
        }
        public void ResetGame(Texture2D checkersPiece, Texture2D checkersKing)
        {
            ResetBoard();
            for (int i = 0; i < 8; i += 2)
            {
                for (int j = 0; j < 3; j += 2)
                {
                    _piecePositions[i, j] = new CheckerPiece(checkersPiece, checkersKing, 1);
                }
            }
            for (int i = 1; i < 8; i += 2)
            {
                _piecePositions[i, 1] = new CheckerPiece(checkersPiece, checkersKing, 1);
            }

            for (int i = 1; i < 8; i += 2)
            {
                for (int j = 5; j < 8; j += 2)
                {
                    _piecePositions[i, j] = new CheckerPiece(checkersPiece, checkersKing, 2);
                }
            }
            for (int i = 0; i < 8; i += 2)
            {
                _piecePositions[i, 6] = new CheckerPiece(checkersPiece, checkersKing, 2);
            }

        }
        public void Draw(SpriteBatch sprite)
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (_boardPositions[i, j] == 0)
                    {
                        sprite.Draw(_rectangleTex, new Rectangle((-65 * i) + 615, (-65 * j) + 552, 65, 65), Color.Black);
                    }
                    else if (_boardPositions[i, j] == 1)
                    {
                        sprite.Draw(_rectangleTex, new Rectangle((-65 * i) + 615, (-65 * j) + 552, 65, 65), Color.SandyBrown);
                    }
                    else
                    {
                        sprite.Draw(_rectangleTex, new Rectangle((-65 * i) + 615, (-65 * j) + 552, 65, 65), Color.White);
                    }
                }
            }
            for (int i = 0; i < 8; i++)
                for (int j = 0; j < 8; j++)
                {
                    if (_piecePositions[i,j] != null)
                    {
                        _piecePositions[i,j].Draw(sprite, new Rectangle ((-65 * i) + 618, (-65 * j) + 555, 59, 59));
                    }
                }

        }
        public void Jump(int i, int j)
        {
            for (int k = 0; k < _piecesToTake.Count; k += 2)
            {
                if (i + 1 == _piecesToTake[k] && j + 1 == _piecesToTake[k + 1])
                {
                    _piecePositions[_piecesToTake[k], _piecesToTake[k + 1]] = null;
                }
                else if (i + 1 == _piecesToTake[k] && j - 1 == _piecesToTake[k + 1])
                {
                    _piecePositions[_piecesToTake[k], _piecesToTake[k + 1]] = null;
                }
                else if (i - 1 == _piecesToTake[k] && j + 1 == _piecesToTake[k + 1])
                {
                    _piecePositions[_piecesToTake[k], _piecesToTake[k + 1]] = null;
                }
                else if (i - 1 == _piecesToTake[k] && j - 1 == _piecesToTake[k + 1])
                {
                    _piecePositions[_piecesToTake[k], _piecesToTake[k + 1]] = null;
                }
            }
            xTake = -1;
            _piecesToTake.Clear();
        }
        public bool CanMove(int playerTurn)
        {
            bool move = false;
            ///////////////////////////////////WIP
            return move;
        }
        public bool MouseClicked(MouseState mouse, int playerTurn)
        {
            bool moved = false;
            if (_isPieceClicked)
            {
                for (int i = 0; i<8; i++)
                    for (int j = 0; j<8; j++)
                        if (new Rectangle((-65 * i) + 615, (-65 * j) + 552, 65, 65).Contains(mouse.X, mouse.Y)){
                            if (_boardPositions[i, j] == -1){
                                _piecePositions[i, j] = _piecePositions[_selectedX, _selectedY];
                                _piecePositions[_selectedX, _selectedY] = null;
                                if (playerTurn == 1 && j == 7)
                                    _piecePositions[i, j].King();
                                else if(playerTurn == 2 && j == 0)
                                    _piecePositions[i, j].King();
                                if (xTake != -1){
                                    Jump(i, j);
                                }
                                moved = true;
                                _isPieceClicked = false;
                                ResetBoard();
                            }
                            else
                            {
                                xTake = -1;
                                _isPieceClicked = false;
                                ResetBoard();
                            }
                        }
            }
            else
            {
                for (int i = 0; i < 8; i++)
                    for (int j = 0; j < 8; j++)
                    {
                        if((new Rectangle((-65 * i) + 618, (-65 * j) + 555, 59, 59).Contains(mouse.X, mouse.Y)))
                        {
                            if (_piecePositions[i, j] != null){
                                if (_piecePositions[i, j].Player() == playerTurn){
                                    if (_piecePositions[i, j].IsKing()){
                                        if (i - 1 >= 0 && j + 1 < 8)
                                        {
                                            if (_piecePositions[i - 1, j + 1] == null)
                                            {
                                                _boardPositions[i - 1, j + 1] = -1;
                                            }
                                            else if (i - 1 > 0 && j + 2 < 8)
                                            {
                                                if (_piecePositions[i - 2, j + 2] == null && _piecePositions[i - 1, j + 1].Player() != playerTurn)
                                                {
                                                    _piecesToTake.Add(i - 1); _piecesToTake.Add(j + 1);
                                                    _boardPositions[i - 2, j + 2] = -1; xTake = 0;
                                                }
                                            }
                                        }
                                        if (i + 1 < 8 && j + 1 < 8)
                                        {
                                            if (_piecePositions[i + 1, j + 1] == null)
                                            {
                                                _boardPositions[i + 1, j + 1] = -1;
                                            }
                                            else if (i + 2 < 8 && j + 2 < 8)
                                            {
                                                if (_piecePositions[i + 2, j + 2] == null && _piecePositions[i + 1, j + 1].Player() != playerTurn)
                                                {
                                                    _boardPositions[i + 2, j + 2] = -1; xTake = 0;
                                                    _piecesToTake.Add(i + 1); _piecesToTake.Add(j + 1);
                                                }
                                            }
                                        }
                                        if (i - 1 >= 0 && j > 0)
                                        {
                                            if (_piecePositions[i - 1, j - 1] == null)
                                            {
                                                _boardPositions[i - 1, j - 1] = -1;
                                            }
                                            else if (i - 1 > 0 && j - 1 > 0)
                                            {
                                                if (_piecePositions[i - 2, j - 2] == null && _piecePositions[i - 1, j - 1].Player() != playerTurn)
                                                {
                                                    _boardPositions[i - 2, j - 2] = -1; xTake = 0;
                                                    _piecesToTake.Add(i - 1); _piecesToTake.Add(j - 1);
                                                }
                                            }
                                        }
                                        if (i + 1 < 8 && j > 0)
                                        {
                                            if (_piecePositions[i + 1, j - 1] == null)
                                                _boardPositions[i + 1, j - 1] = -1;
                                            else if (i + 2 < 8 && j - 1 > 0)
                                            {
                                                if (_piecePositions[i + 2, j - 2] == null && _piecePositions[i + 1, j - 1].Player() != playerTurn)
                                                {
                                                    _boardPositions[i + 2, j - 2] = -1; xTake = 0;
                                                    _piecesToTake.Add(i + 1); _piecesToTake.Add(j - 1);
                                                }
                                            }
                                        }
                                        _isPieceClicked = true;
                                        _selectedX = i;
                                        _selectedY= j;
                                    }
                                    else if (playerTurn == 1)
                                    {
                                        if (i - 1 >= 0 && j + 1 < 8){
                                            if (_piecePositions[i - 1, j + 1] == null){
                                                _boardPositions[i - 1, j + 1] = -1;
                                            }
                                            else if (i - 1 > 0 && j + 2 < 8){
                                                if (_piecePositions[i - 2, j + 2] == null && _piecePositions[i - 1, j + 1].Player() != playerTurn){
                                                    _piecesToTake.Add(i - 1); _piecesToTake.Add(j + 1);
                                                    _boardPositions[i - 2, j + 2] = -1; xTake = 0;
                                                }
                                            }
                                        }
                                        if (i + 1 < 8 && j + 1 < 8){
                                            if (_piecePositions[i + 1, j + 1] == null){
                                                _boardPositions[i + 1, j + 1] = -1;
                                            }
                                            else if (i + 2 < 8 && j + 2 < 8){
                                                if (_piecePositions[i + 2, j + 2] == null && _piecePositions[i + 1, j + 1].Player() != playerTurn){
                                                    _boardPositions[i + 2, j + 2] = -1; xTake = 0;
                                                    _piecesToTake.Add(i + 1); _piecesToTake.Add(j + 1);
                                                }
                                            }
                                        }
                                        _isPieceClicked = true;
                                        _selectedX = i;
                                        _selectedY = j;
                                    }
                                    else
                                    {
                                        if (i - 1 >= 0 && j > 0)
                                        {
                                            if (_piecePositions[i - 1, j - 1] == null)
                                            {
                                                _boardPositions[i - 1, j - 1] = -1;
                                            }
                                            else if (i - 1 > 0 && j - 1 > 0)
                                            {
                                                if (_piecePositions[i - 2, j - 2] == null && _piecePositions[i - 1, j - 1].Player() != playerTurn)
                                                {
                                                    _boardPositions[i - 2, j - 2] = -1; xTake = 0;
                                                    _piecesToTake.Add(i - 1); _piecesToTake.Add(j - 1);
                                                }
                                            }
                                        }
                                        if (i + 1 < 8 && j > 0)
                                        {
                                            if (_piecePositions[i + 1, j - 1] == null)
                                                _boardPositions[i + 1, j - 1] = -1;
                                            else if (i + 2 < 8 && j - 1 > 0)
                                            {
                                                if (_piecePositions[i + 2, j - 2] == null && _piecePositions[i + 1, j - 1].Player() != playerTurn)
                                                {
                                                    _boardPositions[i + 2, j - 2] = -1; xTake = 0;
                                                    _piecesToTake.Add(i + 1); _piecesToTake.Add(j - 1);
                                                }
                                            }
                                        }
                                        _isPieceClicked = true;
                                        _selectedX = i;
                                        _selectedY = j;
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
