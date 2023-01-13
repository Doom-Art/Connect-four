using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Connect_four
{
    internal class ShogiPiece
    {
        private Texture2D _texture1;
        private Texture2D _texture2;
        private Texture2D _promotedTex1;
        private Texture2D _promotedTex2;
        private int _pieceType;
        private int _player;
        private bool _promoted;
        /// <summary>
        /// create a new shogi piece
        /// </summary>
        /// <param name="texture1">Texture for non promoted piece for player 1's side</param>
        /// <param name="texture2">Texture for non promoted piece for player 2's side</param>
        /// <param name="promoted1">Texture for promoted piece for player 1's side</param>
        /// <param name="promoted2">Texture for promoted piece for player 2's side</param>
        /// <param name="pieceType">pawn = 1, lance = 2, knight = 3, silver general = 4, gold general = 5, bishop = 6, rook = 7, king = 8</param>
        /// <param name="player">which player the piece currently belongs to</param>
        public ShogiPiece(Texture2D texture1, Texture2D texture2, Texture2D promoted1, Texture2D promoted2, int pieceType, int player)
        {
            _texture1 = texture1;
            _texture2 = texture2;
            _promotedTex1 = promoted1;
            _promotedTex2 = promoted2;
            _pieceType = pieceType;
            _player = player;
            _promoted = false;
        }
        /// <summary>
        /// create a new shogi piece
        /// </summary>
        /// <param name="texture1">Texture for non promoted piece for player 1's side</param>
        /// <param name="texture2">Texture for non promoted piece for player 2's side</param>
        /// <param name="pieceType">pawn = 1, lance = 2, knight = 3, silver general = 4, gold general = 5, bishop = 6, rook = 7, king = 8</param>
        /// <param name="player">which player the piece currently belongs to</param>
        public ShogiPiece(Texture2D texture1, Texture2D texture2, int pieceType, int player)
        {
            _texture1 = texture1;
            _texture2 = texture2;
            _promotedTex1 = null;
            _promotedTex2 = null;
            _pieceType = pieceType;
            _player = player;
            _promoted = false;
        }
        public void Draw(SpriteBatch spriteBatch, Rectangle rect)
        {
            if (!_promoted){
                if (_player == 1)
                    spriteBatch.Draw(_texture1, rect, Color.White);
                else
                    spriteBatch.Draw(_texture2, rect, Color.White);
            }
            else{
                if (_player == 1)
                    spriteBatch.Draw(_promotedTex1, rect, Color.White);
                else
                    spriteBatch.Draw(_promotedTex2, rect, Color.White);
            }
        }
        public void PlayerChange()
        {
            _promoted = false;
            if (_player == 1)
                _player = 2;
            else 
                _player = 1;
        }
        public int PieceType()
        {
            return _pieceType;
        }
        public int Player()
        {
            return _player;
        }
        public bool Promoted()
        {
            return _promoted;
        }

        public static void PieceClicked(int[,] board, ShogiPiece[,] pieces, int X, int Y, int playerTurn)
        {
            if (pieces[X,Y].PieceType() == 1){
                if (pieces[X,Y].Player() == 1){
                    if (!pieces[X, Y].Promoted()){
                        if (pieces[X,Y-1] != null){
                            if (pieces[X, Y - 1].Player() != 1){
                                board[X, Y - 1] = -1;
                            }
                        }
                        else
                            board[X, Y - 1] = -1;
                    }
                    else{
                        if (Y - 1 >= 0)
                        {
                            if (pieces[X, Y - 1] != null)
                            {
                                if (pieces[X, Y - 1].Player() != playerTurn)
                                {
                                    board[X, Y - 1] = -1;
                                }
                            }
                            else
                                board[X, Y - 1] = -1;
                        }//Up One
                        if (Y + 1 < 9)
                        {
                            if (pieces[X, Y + 1] != null)
                            {
                                if (pieces[X, Y + 1].Player() != playerTurn)
                                {
                                    board[X, Y + 1] = -1;
                                }
                            }
                            else
                                board[X, Y + 1] = -1;
                        }//Down One
                        if (X - 1 >= 0 && Y - 1 >= 0)
                        {
                            if (pieces[X - 1, Y - 1] != null)
                            {
                                if (pieces[X - 1, Y - 1].Player() != playerTurn)
                                {
                                    board[X - 1, Y - 1] = -1;
                                }
                            }
                            else
                                board[X - 1, Y - 1] = -1;
                        }//Up Left Diagonal
                        if (X + 1 < 9 && Y - 1 >= 0)
                        {
                            if (pieces[X + 1, Y - 1] != null)
                            {
                                if (pieces[X + 1, Y - 1].Player() != playerTurn)
                                {
                                    board[X + 1, Y - 1] = -1;
                                }
                            }
                            else
                                board[X + 1, Y - 1] = -1;
                        }// Up Right Diagonal
                        if (X - 1 >= 0)
                        {
                            if (pieces[X - 1, Y] != null)
                            {
                                if (pieces[X - 1, Y].Player() != playerTurn)
                                {
                                    board[X - 1, Y] = -1;
                                }
                            }
                            else
                                board[X - 1, Y] = -1;
                        }//Left One
                        if (X + 1 < 9)
                        {
                            if (pieces[X + 1, Y] != null)
                            {
                                if (pieces[X + 1, Y].Player() != playerTurn)
                                {
                                    board[X + 1, Y] = -1;
                                }
                            }
                            else
                                board[X + 1, Y] = -1;
                        }//Right One
                    }//Promoted to Gold Player 1
                }
                else{
                    if (!pieces[X, Y].Promoted())
                    {
                        if (pieces[X, Y + 1] != null)
                        {
                            if (pieces[X, Y + 1].Player() != 2)
                            {
                                board[X, Y + 1] = -1;
                            }
                        }
                        else
                            board[X, Y + 1] = -1;
                    }
                    else
                    {
                        if (Y - 1 >= 0)
                        {
                            if (pieces[X, Y - 1] != null)
                            {
                                if (pieces[X, Y - 1].Player() != playerTurn)
                                {
                                    board[X, Y - 1] = -1;
                                }
                            }
                            else
                                board[X, Y - 1] = -1;
                        }//Up One
                        if (Y + 1 < 9)
                        {
                            if (pieces[X, Y + 1] != null)
                            {
                                if (pieces[X, Y + 1].Player() != playerTurn)
                                {
                                    board[X, Y + 1] = -1;
                                }
                            }
                            else
                                board[X, Y + 1] = -1;
                        }//Down One    
                        if (X - 1 >= 0)
                        {
                            if (pieces[X - 1, Y] != null)
                            {
                                if (pieces[X - 1, Y].Player() != playerTurn)
                                {
                                    board[X - 1, Y] = -1;
                                }
                            }
                            else
                                board[X - 1, Y] = -1;
                        }//Left One
                        if (X + 1 < 9)
                        {
                            if (pieces[X + 1, Y] != null)
                            {
                                if (pieces[X + 1, Y].Player() != playerTurn)
                                {
                                    board[X + 1, Y] = -1;
                                }
                            }
                            else
                                board[X + 1, Y] = -1;
                        }//Right One
                        if (X - 1 >= 0 && Y + 1 < 9)
                        {
                            if (pieces[X - 1, Y + 1] != null)
                            {
                                if (pieces[X - 1, Y + 1].Player() != playerTurn)
                                {
                                    board[X - 1, Y + 1] = -1;
                                }
                            }
                            else
                                board[X - 1, Y + 1] = -1;
                        }//Down Left Diagonal
                        if (X + 1 < 9 && Y + 1 < 9)
                        {
                            if (pieces[X + 1, Y + 1] != null)
                            {
                                if (pieces[X + 1, Y + 1].Player() != playerTurn)
                                {
                                    board[X + 1, Y + 1] = -1;
                                }
                            }
                            else
                                board[X + 1, Y + 1] = -1;
                        }//Down Right Diagonal
                    }//Promoted to Gold Player 2
                }
            }//Pawn Movement
            else if(pieces[X,Y].PieceType() == 2){
                if (pieces[X, Y].Player() == 1){
                    if (!pieces[X, Y].Promoted()){
                        bool temp = true;
                        for (int i = Y-1; i >= 0; i--){
                            if (pieces[X,i] == null && temp){
                                board[X, i] = -1;
                            }
                            else if (temp){
                                if (pieces[X, i].Player() == 2)
                                {
                                    board[X, i] = -1;
                                    temp = false;
                                }
                                else
                                    temp = false;
                            }
                        }
                    }
                    else
                    {
                        if (Y - 1 >= 0)
                        {
                            if (pieces[X, Y - 1] != null)
                            {
                                if (pieces[X, Y - 1].Player() != playerTurn)
                                {
                                    board[X, Y - 1] = -1;
                                }
                            }
                            else
                                board[X, Y - 1] = -1;
                        }//Up One
                        if (Y + 1 < 9)
                        {
                            if (pieces[X, Y + 1] != null)
                            {
                                if (pieces[X, Y + 1].Player() != playerTurn)
                                {
                                    board[X, Y + 1] = -1;
                                }
                            }
                            else
                                board[X, Y + 1] = -1;
                        }//Down One
                        if (X - 1 >= 0 && Y - 1 >= 0)
                        {
                            if (pieces[X - 1, Y - 1] != null)
                            {
                                if (pieces[X - 1, Y - 1].Player() != playerTurn)
                                {
                                    board[X - 1, Y - 1] = -1;
                                }
                            }
                            else
                                board[X - 1, Y - 1] = -1;
                        }//Up Left Diagonal
                        if (X + 1 < 9 && Y - 1 >= 0)
                        {
                            if (pieces[X + 1, Y - 1] != null)
                            {
                                if (pieces[X + 1, Y - 1].Player() != playerTurn)
                                {
                                    board[X + 1, Y - 1] = -1;
                                }
                            }
                            else
                                board[X + 1, Y - 1] = -1;
                        }// Up Right Diagonal
                        if (X - 1 >= 0)
                        {
                            if (pieces[X - 1, Y] != null)
                            {
                                if (pieces[X - 1, Y].Player() != playerTurn)
                                {
                                    board[X - 1, Y] = -1;
                                }
                            }
                            else
                                board[X - 1, Y] = -1;
                        }//Left One
                        if (X + 1 < 9)
                        {
                            if (pieces[X + 1, Y] != null)
                            {
                                if (pieces[X + 1, Y].Player() != playerTurn)
                                {
                                    board[X + 1, Y] = -1;
                                }
                            }
                            else
                                board[X + 1, Y] = -1;
                        }//Right One
                    }//Promoted to Gold Player 1

                }
                else
                {
                    if (!pieces[X, Y].Promoted()){
                        bool temp = true;
                        for (int i = Y + 1; i < 9; i++)
                        {
                            if (pieces[X, i] == null && temp)
                            {
                                board[X, i] = -1;
                            }
                            else if (temp)
                            {
                                if (pieces[X, i].Player() == 1)
                                {
                                    board[X, i] = -1;
                                    temp = false;
                                }
                                else
                                    temp = false;
                            }
                        }
                    }
                    else
                    {
                        if (Y - 1 >= 0)
                        {
                            if (pieces[X, Y - 1] != null)
                            {
                                if (pieces[X, Y - 1].Player() != playerTurn)
                                {
                                    board[X, Y - 1] = -1;
                                }
                            }
                            else
                                board[X, Y - 1] = -1;
                        }//Up One
                        if (Y + 1 < 9)
                        {
                            if (pieces[X, Y + 1] != null)
                            {
                                if (pieces[X, Y + 1].Player() != playerTurn)
                                {
                                    board[X, Y + 1] = -1;
                                }
                            }
                            else
                                board[X, Y + 1] = -1;
                        }//Down One    
                        if (X - 1 >= 0)
                        {
                            if (pieces[X - 1, Y] != null)
                            {
                                if (pieces[X - 1, Y].Player() != playerTurn)
                                {
                                    board[X - 1, Y] = -1;
                                }
                            }
                            else
                                board[X - 1, Y] = -1;
                        }//Left One
                        if (X + 1 < 9)
                        {
                            if (pieces[X + 1, Y] != null)
                            {
                                if (pieces[X + 1, Y].Player() != playerTurn)
                                {
                                    board[X + 1, Y] = -1;
                                }
                            }
                            else
                                board[X + 1, Y] = -1;
                        }//Right One
                        if (X - 1 >= 0 && Y + 1 < 9)
                        {
                            if (pieces[X - 1, Y + 1] != null)
                            {
                                if (pieces[X - 1, Y + 1].Player() != playerTurn)
                                {
                                    board[X - 1, Y + 1] = -1;
                                }
                            }
                            else
                                board[X - 1, Y + 1] = -1;
                        }//Down Left Diagonal
                        if (X + 1 < 9 && Y + 1 < 9)
                        {
                            if (pieces[X + 1, Y + 1] != null)
                            {
                                if (pieces[X + 1, Y + 1].Player() != playerTurn)
                                {
                                    board[X + 1, Y + 1] = -1;
                                }
                            }
                            else
                                board[X + 1, Y + 1] = -1;
                        }//Down Right Diagonal
                    }//Promoted to Gold Player 2
                }
            } //Lance Movement
            else if (pieces[X,Y].PieceType() == 3){
                if (pieces[X, Y].Player() == 1)
                {
                    if (!pieces[X, Y].Promoted()){
                        if (X-1 >=0 && Y-2 >= 0){
                            if (pieces[X - 1, Y - 2] != null){
                                if (pieces[X - 1, Y - 2].Player() != 1){
                                    board[X - 1, Y - 2] = -1;
                                }
                            }
                            else
                                board[X - 1, Y - 2] = -1;
                        }
                        if(X+1 < 9 && Y - 2 >= 0){
                            if (pieces[X + 1, Y - 2] != null){
                                if (pieces[X + 1, Y - 2].Player() != 1){
                                    board[X + 1, Y - 2] = -1;
                                }
                            }
                            else
                                board[X + 1, Y - 2] = -1;
                        }
                    }
                    else
                    {
                        if (Y - 1 >= 0)
                        {
                            if (pieces[X, Y - 1] != null)
                            {
                                if (pieces[X, Y - 1].Player() != playerTurn)
                                {
                                    board[X, Y - 1] = -1;
                                }
                            }
                            else
                                board[X, Y - 1] = -1;
                        }//Up One
                        if (Y + 1 < 9)
                        {
                            if (pieces[X, Y + 1] != null)
                            {
                                if (pieces[X, Y + 1].Player() != playerTurn)
                                {
                                    board[X, Y + 1] = -1;
                                }
                            }
                            else
                                board[X, Y + 1] = -1;
                        }//Down One
                        if (X - 1 >= 0 && Y - 1 >= 0)
                        {
                            if (pieces[X - 1, Y - 1] != null)
                            {
                                if (pieces[X - 1, Y - 1].Player() != playerTurn)
                                {
                                    board[X - 1, Y - 1] = -1;
                                }
                            }
                            else
                                board[X - 1, Y - 1] = -1;
                        }//Up Left Diagonal
                        if (X + 1 < 9 && Y - 1 >= 0)
                        {
                            if (pieces[X + 1, Y - 1] != null)
                            {
                                if (pieces[X + 1, Y - 1].Player() != playerTurn)
                                {
                                    board[X + 1, Y - 1] = -1;
                                }
                            }
                            else
                                board[X + 1, Y - 1] = -1;
                        }// Up Right Diagonal
                        if (X - 1 >= 0)
                        {
                            if (pieces[X - 1, Y] != null)
                            {
                                if (pieces[X - 1, Y].Player() != playerTurn)
                                {
                                    board[X - 1, Y] = -1;
                                }
                            }
                            else
                                board[X - 1, Y] = -1;
                        }//Left One
                        if (X + 1 < 9)
                        {
                            if (pieces[X + 1, Y] != null)
                            {
                                if (pieces[X + 1, Y].Player() != playerTurn)
                                {
                                    board[X + 1, Y] = -1;
                                }
                            }
                            else
                                board[X + 1, Y] = -1;
                        }//Right One
                    }//Promoted to Gold Player 1

                }
                else
                {
                    if (!pieces[X, Y].Promoted())
                    {
                        if (X - 1 >= 0 && Y + 2 <9)
                        {
                            if (pieces[X - 1, Y + 2] != null)
                            {
                                if (pieces[X - 1, Y + 2].Player() != 2)
                                {
                                    board[X - 1, Y + 2] = -1;
                                }
                            }
                            else
                                board[X - 1, Y + 2] = -1;
                        }
                        if (X + 1 <9 && Y + 2 < 9)
                        {
                            if (pieces[X + 1, Y + 2] != null)
                            {
                                if (pieces[X + 1, Y + 2].Player() != 2)
                                {
                                    board[X + 1, Y + 2] = -1;
                                }
                            }
                            else
                                board[X + 1, Y + 2] = -1;
                        }
                    }
                    else
                    {
                        if (Y - 1 >= 0)
                        {
                            if (pieces[X, Y - 1] != null)
                            {
                                if (pieces[X, Y - 1].Player() != playerTurn)
                                {
                                    board[X, Y - 1] = -1;
                                }
                            }
                            else
                                board[X, Y - 1] = -1;
                        }//Up One
                        if (Y + 1 < 9)
                        {
                            if (pieces[X, Y + 1] != null)
                            {
                                if (pieces[X, Y + 1].Player() != playerTurn)
                                {
                                    board[X, Y + 1] = -1;
                                }
                            }
                            else
                                board[X, Y + 1] = -1;
                        }//Down One    
                        if (X - 1 >= 0)
                        {
                            if (pieces[X - 1, Y] != null)
                            {
                                if (pieces[X - 1, Y].Player() != playerTurn)
                                {
                                    board[X - 1, Y] = -1;
                                }
                            }
                            else
                                board[X - 1, Y] = -1;
                        }//Left One
                        if (X + 1 < 9)
                        {
                            if (pieces[X + 1, Y] != null)
                            {
                                if (pieces[X + 1, Y].Player() != playerTurn)
                                {
                                    board[X + 1, Y] = -1;
                                }
                            }
                            else
                                board[X + 1, Y] = -1;
                        }//Right One
                        if (X - 1 >= 0 && Y + 1 < 9)
                        {
                            if (pieces[X - 1, Y + 1] != null)
                            {
                                if (pieces[X - 1, Y + 1].Player() != playerTurn)
                                {
                                    board[X - 1, Y + 1] = -1;
                                }
                            }
                            else
                                board[X - 1, Y + 1] = -1;
                        }//Down Left Diagonal
                        if (X + 1 < 9 && Y + 1 < 9)
                        {
                            if (pieces[X + 1, Y + 1] != null)
                            {
                                if (pieces[X + 1, Y + 1].Player() != playerTurn)
                                {
                                    board[X + 1, Y + 1] = -1;
                                }
                            }
                            else
                                board[X + 1, Y + 1] = -1;
                        }//Down Right Diagonal
                    }//Promoted to Gold Player 2
                }
            }//Knight Movement
            else if (pieces[X, Y].PieceType() == 4)
            {
                if (playerTurn == 1){
                    if (!pieces[X, Y].Promoted()){
                        if (X - 1 >= 0 && Y - 1 >= 0)
                        {
                            if (pieces[X - 1, Y - 1] != null)
                            {
                                if (pieces[X - 1, Y - 1].Player() != playerTurn)
                                {
                                    board[X - 1, Y - 1] = -1;
                                }
                            }
                            else
                                board[X - 1, Y - 1] = -1;
                        }//Up Left Diagonal
                        if (X + 1 < 9 && Y - 1 >= 0)
                        {
                            if (pieces[X + 1, Y - 1] != null)
                            {
                                if (pieces[X + 1, Y - 1].Player() != playerTurn)
                                {
                                    board[X + 1, Y - 1] = -1;
                                }
                            }
                            else
                                board[X + 1, Y - 1] = -1;
                        }// Up Right Diagonal
                        if (X - 1 >= 0 && Y + 1 < 9)
                        {
                            if (pieces[X - 1, Y + 1] != null)
                            {
                                if (pieces[X - 1, Y + 1].Player() != playerTurn)
                                {
                                    board[X - 1, Y + 1] = -1;
                                }
                            }
                            else
                                board[X - 1, Y + 1] = -1;
                        }//Down Left Diagonal
                        if (X + 1 < 9 && Y + 1 < 9)
                        {
                            if (pieces[X + 1, Y + 1] != null)
                            {
                                if (pieces[X + 1, Y + 1].Player() != playerTurn)
                                {
                                    board[X + 1, Y + 1] = -1;
                                }
                            }
                            else
                                board[X + 1, Y + 1] = -1;
                        }//Down Right Diagonal
                        if (Y - 1 >= 0)
                        {
                            if (pieces[X, Y - 1] != null)
                            {
                                if (pieces[X, Y - 1].Player() != playerTurn)
                                {
                                    board[X, Y - 1] = -1;
                                }
                            }
                            else
                                board[X, Y - 1] = -1;
                        }//Up One
                    }
                    else
                    {
                        if (Y - 1 >= 0)
                        {
                            if (pieces[X, Y - 1] != null)
                            {
                                if (pieces[X, Y - 1].Player() != playerTurn)
                                {
                                    board[X, Y - 1] = -1;
                                }
                            }
                            else
                                board[X, Y - 1] = -1;
                        }//Up One
                        if (Y + 1 < 9)
                        {
                            if (pieces[X, Y + 1] != null)
                            {
                                if (pieces[X, Y + 1].Player() != playerTurn)
                                {
                                    board[X, Y + 1] = -1;
                                }
                            }
                            else
                                board[X, Y + 1] = -1;
                        }//Down One
                        if (X - 1 >= 0 && Y - 1 >= 0)
                        {
                            if (pieces[X - 1, Y - 1] != null)
                            {
                                if (pieces[X - 1, Y - 1].Player() != playerTurn)
                                {
                                    board[X - 1, Y - 1] = -1;
                                }
                            }
                            else
                                board[X - 1, Y - 1] = -1;
                        }//Up Left Diagonal
                        if (X + 1 < 9 && Y - 1 >= 0)
                        {
                            if (pieces[X + 1, Y - 1] != null)
                            {
                                if (pieces[X + 1, Y - 1].Player() != playerTurn)
                                {
                                    board[X + 1, Y - 1] = -1;
                                }
                            }
                            else
                                board[X + 1, Y - 1] = -1;
                        }// Up Right Diagonal
                        if (X - 1 >= 0)
                        {
                            if (pieces[X - 1, Y] != null)
                            {
                                if (pieces[X - 1, Y].Player() != playerTurn)
                                {
                                    board[X - 1, Y] = -1;
                                }
                            }
                            else
                                board[X - 1, Y] = -1;
                        }//Left One
                        if (X + 1 < 9)
                        {
                            if (pieces[X + 1, Y] != null)
                            {
                                if (pieces[X + 1, Y].Player() != playerTurn)
                                {
                                    board[X + 1, Y] = -1;
                                }
                            }
                            else
                                board[X + 1, Y] = -1;
                        }//Right One
                    }//Promoted to Gold Player 1
                }
                else{
                    if (!pieces[X, Y].Promoted())
                    {
                        if (X - 1 >= 0 && Y - 1 >= 0)
                        {
                            if (pieces[X - 1, Y - 1] != null)
                            {
                                if (pieces[X - 1, Y - 1].Player() != playerTurn)
                                {
                                    board[X - 1, Y - 1] = -1;
                                }
                            }
                            else
                                board[X - 1, Y - 1] = -1;
                        }//Up Left Diagonal
                        if (X + 1 < 9 && Y - 1 >= 0)
                        {
                            if (pieces[X + 1, Y - 1] != null)
                            {
                                if (pieces[X + 1, Y - 1].Player() != playerTurn)
                                {
                                    board[X + 1, Y - 1] = -1;
                                }
                            }
                            else
                                board[X + 1, Y - 1] = -1;
                        }// Up Right Diagonal
                        if (X - 1 >= 0 && Y + 1 < 9)
                        {
                            if (pieces[X - 1, Y + 1] != null)
                            {
                                if (pieces[X - 1, Y + 1].Player() != playerTurn)
                                {
                                    board[X - 1, Y + 1] = -1;
                                }
                            }
                            else
                                board[X - 1, Y + 1] = -1;
                        }//Down Left Diagonal
                        if (X + 1 < 9 && Y + 1 < 9)
                        {
                            if (pieces[X + 1, Y + 1] != null)
                            {
                                if (pieces[X + 1, Y + 1].Player() != playerTurn)
                                {
                                    board[X + 1, Y + 1] = -1;
                                }
                            }
                            else
                                board[X + 1, Y + 1] = -1;
                        }//Down Right Diagonal
                        if (Y + 1 < 9)
                        {
                            if (pieces[X, Y + 1] != null)
                            {
                                if (pieces[X, Y + 1].Player() != playerTurn)
                                {
                                    board[X, Y + 1] = -1;
                                }
                            }
                            else
                                board[X, Y + 1] = -1;
                        }//Down One    
                    }
                    else
                    {
                        if (Y - 1 >= 0)
                        {
                            if (pieces[X, Y - 1] != null)
                            {
                                if (pieces[X, Y - 1].Player() != playerTurn)
                                {
                                    board[X, Y - 1] = -1;
                                }
                            }
                            else
                                board[X, Y - 1] = -1;
                        }//Up One
                        if (Y + 1 < 9)
                        {
                            if (pieces[X, Y + 1] != null)
                            {
                                if (pieces[X, Y + 1].Player() != playerTurn)
                                {
                                    board[X, Y + 1] = -1;
                                }
                            }
                            else
                                board[X, Y + 1] = -1;
                        }//Down One    
                        if (X - 1 >= 0)
                        {
                            if (pieces[X - 1, Y] != null)
                            {
                                if (pieces[X - 1, Y].Player() != playerTurn)
                                {
                                    board[X - 1, Y] = -1;
                                }
                            }
                            else
                                board[X - 1, Y] = -1;
                        }//Left One
                        if (X + 1 < 9)
                        {
                            if (pieces[X + 1, Y] != null)
                            {
                                if (pieces[X + 1, Y].Player() != playerTurn)
                                {
                                    board[X + 1, Y] = -1;
                                }
                            }
                            else
                                board[X + 1, Y] = -1;
                        }//Right One
                        if (X - 1 >= 0 && Y + 1 < 9)
                        {
                            if (pieces[X - 1, Y + 1] != null)
                            {
                                if (pieces[X - 1, Y + 1].Player() != playerTurn)
                                {
                                    board[X - 1, Y + 1] = -1;
                                }
                            }
                            else
                                board[X - 1, Y + 1] = -1;
                        }//Down Left Diagonal
                        if (X + 1 < 9 && Y + 1 < 9)
                        {
                            if (pieces[X + 1, Y + 1] != null)
                            {
                                if (pieces[X + 1, Y + 1].Player() != playerTurn)
                                {
                                    board[X + 1, Y + 1] = -1;
                                }
                            }
                            else
                                board[X + 1, Y + 1] = -1;
                        }//Down Right Diagonal
                    }//Promoted to Gold Player 2
                }
                
            }//Silver Movement
            else if (pieces[X, Y].PieceType() == 5)
            {
                if (playerTurn == 1){
                    if (Y - 1 >= 0)
                    {
                        if (pieces[X, Y - 1] != null)
                        {
                            if (pieces[X, Y - 1].Player() != playerTurn)
                            {
                                board[X, Y - 1] = -1;
                            }
                        }
                        else
                            board[X, Y - 1] = -1;
                    }//Up One
                    if (Y + 1 < 9)
                    {
                        if (pieces[X, Y + 1] != null)
                        {
                            if (pieces[X, Y + 1].Player() != playerTurn)
                            {
                                board[X, Y + 1] = -1;
                            }
                        }
                        else
                            board[X, Y + 1] = -1;
                    }//Down One    
                    if (X - 1 >= 0 && Y - 1 >= 0)
                    {
                        if (pieces[X - 1, Y - 1] != null)
                        {
                            if (pieces[X - 1, Y - 1].Player() != playerTurn)
                            {
                                board[X - 1, Y - 1] = -1;
                            }
                        }
                        else
                            board[X - 1, Y - 1] = -1;
                    }//Up Left Diagonal
                    if (X + 1 < 9 && Y - 1 >= 0)
                    {
                        if (pieces[X + 1, Y - 1] != null)
                        {
                            if (pieces[X + 1, Y - 1].Player() != playerTurn)
                            {
                                board[X + 1, Y - 1] = -1;
                            }
                        }
                        else
                            board[X + 1, Y - 1] = -1;
                    }// Up Right Diagonal
                    if (X - 1 >= 0)
                    {
                        if (pieces[X - 1, Y] != null)
                        {
                            if (pieces[X - 1, Y].Player() != playerTurn)
                            {
                                board[X - 1, Y] = -1;
                            }
                        }
                        else
                            board[X - 1, Y] = -1;
                    }//Left One
                    if (X + 1 < 9)
                    {
                        if (pieces[X + 1, Y] != null)
                        {
                            if (pieces[X + 1, Y].Player() != playerTurn)
                            {
                                board[X + 1, Y] = -1;
                            }
                        }
                        else
                            board[X + 1, Y] = -1;
                    }//Right One
                }
                else{
                    if (Y - 1 >= 0)
                    {
                        if (pieces[X, Y - 1] != null)
                        {
                            if (pieces[X, Y - 1].Player() != playerTurn)
                            {
                                board[X, Y - 1] = -1;
                            }
                        }
                        else
                            board[X, Y - 1] = -1;
                    }//Up One
                    if (Y + 1 < 9)
                    {
                        if (pieces[X, Y + 1] != null)
                        {
                            if (pieces[X, Y + 1].Player() != playerTurn)
                            {
                                board[X, Y + 1] = -1;
                            }
                        }
                        else
                            board[X, Y + 1] = -1;
                    }//Down One    
                    if (X - 1 >= 0)
                    {
                        if (pieces[X - 1, Y] != null)
                        {
                            if (pieces[X - 1, Y].Player() != playerTurn)
                            {
                                board[X - 1, Y] = -1;
                            }
                        }
                        else
                            board[X - 1, Y] = -1;
                    }//Left One
                    if (X + 1 < 9)
                    {
                        if (pieces[X + 1, Y] != null)
                        {
                            if (pieces[X + 1, Y].Player() != playerTurn)
                            {
                                board[X + 1, Y] = -1;
                            }
                        }
                        else
                            board[X + 1, Y] = -1;
                    }//Right One
                    if (X - 1 >= 0 && Y + 1 < 9)
                    {
                        if (pieces[X - 1, Y + 1] != null)
                        {
                            if (pieces[X - 1, Y + 1].Player() != playerTurn)
                            {
                                board[X - 1, Y + 1] = -1;
                            }
                        }
                        else
                            board[X - 1, Y + 1] = -1;
                    }//Down Left Diagonal
                    if (X + 1 < 9 && Y + 1 < 9)
                    {
                        if (pieces[X + 1, Y + 1] != null)
                        {
                            if (pieces[X + 1, Y + 1].Player() != playerTurn)
                            {
                                board[X + 1, Y + 1] = -1;
                            }
                        }
                        else
                            board[X + 1, Y + 1] = -1;
                    }//Down Right Diagonal
                }
            }//Gold Movement
            else if (pieces[X, Y].PieceType() == 6)
            {
                bool temp = true;
                int j = Y - 1;
                for (int i = X - 1; i >= 0 && j>=0; i--)
                {
                    if (pieces[i, j] == null && temp)
                    {
                        board[i, j] = -1;
                    }
                    else if (temp)
                    {
                        if (pieces[i, j].Player() != playerTurn)
                        {
                            board[i, j] = -1;
                            temp = false;
                        }
                        else
                            temp = false;
                    }
                    j--;
                }//Up Left Diagonal
                temp = true;
                j = Y - 1;
                for (int i = X + 1; i <9 && j >= 0; i++)
                {
                    if (pieces[i, j] == null && temp)
                    {
                        board[i, j] = -1;
                    }
                    else if (temp)
                    {
                        if (pieces[i, j].Player() != playerTurn)
                        {
                            board[i, j] = -1;
                            temp = false;
                        }
                        else
                            temp = false;
                    }
                    j--;
                }//Up Right Diagonal
                temp = true;
                j = Y + 1;
                for (int i = X + 1;  i < 9 && j<9; i++)
                {
                    if (pieces[i, j] == null && temp)
                    {
                        board[i, j] = -1;
                    }
                    else if (temp)
                    {
                        if (pieces[i, j].Player() != playerTurn)
                        {
                            board[i, j] = -1;
                            temp = false;
                        }
                        else
                            temp = false;
                    }
                    j++;
                }//Down Right Diagonal
                temp = true;
                j = Y + 1;
                for (int i = X - 1; i >= 0 && j<9; i--)
                {
                    if(i >= 0 && j < 9)
                    {
                        if (pieces[i, j] == null && temp)
                        {
                            board[i, j] = -1;
                        }
                        else if (temp)
                        {
                            if (pieces[i, j].Player() != playerTurn)
                            {
                                board[i, j] = -1;
                                temp = false;
                            }
                            else
                                temp = false;
                        }
                        j++;
                    }
                    
                }//Down Left Diagonal
                if (pieces[X, Y].Promoted()){
                    if (Y - 1 >= 0)
                    {
                        if (pieces[X, Y - 1] != null)
                        {
                            if (pieces[X, Y - 1].Player() != playerTurn)
                            {
                                board[X, Y - 1] = -1;
                            }
                        }
                        else
                            board[X, Y - 1] = -1;
                    }//Up One
                    if (Y + 1 < 9)
                    {
                        if (pieces[X, Y + 1] != null)
                        {
                            if (pieces[X, Y + 1].Player() != playerTurn)
                            {
                                board[X, Y + 1] = -1;
                            }
                        }
                        else
                            board[X, Y + 1] = -1;
                    }//Down One    
                    if (X - 1 >= 0)
                    {
                        if (pieces[X - 1, Y] != null)
                        {
                            if (pieces[X - 1, Y].Player() != playerTurn)
                            {
                                board[X - 1, Y] = -1;
                            }
                        }
                        else
                            board[X - 1, Y] = -1;
                    }//Left One
                    if (X + 1 < 9)
                    {
                        if (pieces[X + 1, Y] != null)
                        {
                            if (pieces[X + 1, Y].Player() != playerTurn)
                            {
                                board[X + 1, Y] = -1;
                            }
                        }
                        else
                            board[X + 1, Y] = -1;
                    }//Right One
                }
            }//Bishop Movement
            else if (pieces[X, Y].PieceType() == 7){
                bool temp = true;
                for (int i = Y + 1; i < 9; i++)
                {
                    if (pieces[X, i] == null && temp)
                    {
                        board[X, i] = -1;
                    }
                    else if (temp)
                    {
                        if (pieces[X, i].Player() != playerTurn)
                        {
                            board[X, i] = -1;
                            temp = false;
                        }
                        else
                            temp = false;
                    }
                }//Down
                temp = true;
                for (int i = Y - 1; i >= 0; i--)
                {
                    if (pieces[X, i] == null && temp)
                    {
                        board[X, i] = -1;
                    }
                    else if (temp)
                    {
                        if (pieces[X, i].Player() != playerTurn)
                        {
                            board[X, i] = -1;
                            temp = false;
                        }
                        else
                            temp = false;
                    }
                }//Up
                temp = true;
                for (int j = X + 1; j < 9; j++)
                {
                    if (pieces[j, Y] == null && temp){
                        board[j, Y] = -1;
                    }
                    else if (temp)
                    {
                        if (pieces[j, Y].Player() != playerTurn){
                            board[j, Y] = -1;
                            temp = false;
                        }
                        else
                            temp = false;
                    }
                }//Right
                temp = true;
                for (int j = X - 1; j >= 0; j--)
                {
                    if (pieces[j, Y] == null && temp){
                        board[j, Y] = -1;
                    }
                    else if (temp){
                        if (pieces[j, Y].Player() != playerTurn){
                            board[j, Y] = -1;
                            temp = false;
                        }
                        else
                            temp = false;
                    }
                }//Left
                if (pieces[X, Y].Promoted()){
                    if (X - 1 >= 0 && Y - 1 >= 0)
                    {
                        if (pieces[X - 1, Y - 1] != null)
                        {
                            if (pieces[X - 1, Y - 1].Player() != playerTurn)
                            {
                                board[X - 1, Y - 1] = -1;
                            }
                        }
                        else
                            board[X - 1, Y - 1] = -1;
                    }//Up Left Diagonal
                    if (X + 1 < 9 && Y - 1 >= 0)
                    {
                        if (pieces[X + 1, Y - 1] != null)
                        {
                            if (pieces[X + 1, Y - 1].Player() != playerTurn)
                            {
                                board[X + 1, Y - 1] = -1;
                            }
                        }
                        else
                            board[X + 1, Y - 1] = -1;
                    }// Up Right Diagonal
                    if (X - 1 >= 0 && Y + 1 < 9)
                    {
                        if (pieces[X - 1, Y + 1] != null)
                        {
                            if (pieces[X - 1, Y + 1].Player() != playerTurn)
                            {
                                board[X - 1, Y + 1] = -1;
                            }
                        }
                        else
                            board[X - 1, Y + 1] = -1;
                    }//Down Left Diagonal
                    if (X + 1 < 9 && Y + 1 < 9)
                    {
                        if (pieces[X + 1, Y + 1] != null)
                        {
                            if (pieces[X + 1, Y + 1].Player() != playerTurn)
                            {
                                board[X + 1, Y + 1] = -1;
                            }
                        }
                        else
                            board[X + 1, Y + 1] = -1;
                    }//Down Right Diagonal
                }
            }//Rook Movement
            else if (pieces[X, Y].PieceType() == 8){
                if (Y-1 >= 0)
                {
                    if (pieces[X, Y - 1] != null)
                    {
                        if (pieces[X, Y - 1].Player() != playerTurn)
                        {
                            board[X, Y - 1] = -1;
                        }
                    }
                    else
                        board[X, Y - 1] = -1;
                }//Up One
                if (Y + 1 < 9)
                {
                    if (pieces[X, Y + 1] != null)
                    {
                        if (pieces[X, Y + 1].Player() != playerTurn)
                        {
                            board[X, Y + 1] = -1;
                        }
                    }
                    else
                        board[X, Y + 1] = -1;
                }//Down One    
                if (X-1 >= 0 && Y-1 >= 0)
                {
                    if (pieces[X - 1, Y - 1] != null)
                    {
                        if (pieces[X - 1, Y - 1].Player() != playerTurn)
                        {
                            board[X - 1, Y - 1] = -1;
                        }
                    }
                    else
                        board[X - 1, Y - 1] = -1;
                }//Up Left Diagonal
                if (X +1 <9 && Y - 1 >= 0)
                {
                    if (pieces[X+1, Y - 1] != null)
                    {
                        if (pieces[X+1, Y - 1].Player() != playerTurn)
                        {
                            board[X+1, Y - 1] = -1;
                        }
                    }
                    else
                        board[X+1, Y - 1] = -1;
                }// Up Right Diagonal
                if (X - 1 >= 0){
                    if (pieces[X - 1, Y] != null)
                    {
                        if (pieces[X - 1, Y].Player() != playerTurn)
                        {
                            board[X - 1, Y] = -1;
                        }
                    }
                    else
                        board[X - 1,Y] = -1;
                }//Left One
                if (X + 1 <9)
                {
                    if (pieces[X + 1, Y] != null)
                    {
                        if (pieces[X + 1, Y].Player() != playerTurn)
                        {
                            board[X + 1, Y] = -1;
                        }
                    }
                    else
                        board[X + 1, Y] = -1;
                }//Right One
                if (X - 1 >= 0 && Y + 1 < 9)
                {
                    if (pieces[X - 1, Y + 1] != null)
                    {
                        if (pieces[X - 1, Y + 1].Player() != playerTurn)
                        {
                            board[X - 1, Y + 1] = -1;
                        }
                    }
                    else
                        board[X - 1, Y + 1] = -1;
                }//Down Left Diagonal
                if (X + 1 < 9 && Y + 1 < 9)
                {
                    if (pieces[X + 1, Y + 1] != null)
                    {
                        if (pieces[X + 1, Y + 1].Player() != playerTurn)
                        {
                            board[X + 1, Y + 1] = -1;
                        }
                    }
                    else
                        board[X + 1, Y + 1] = -1;
                }//Down Right Diagonal

            }//King Movement
        }
        public void Promote()
        {
            _promoted = true;
        }
    }
}
