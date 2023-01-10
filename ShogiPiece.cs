using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public static void PieceClicked(int[,] board, ShogiPiece[,] pieces, int X, int Y)
        {
            if (pieces[X,Y].PieceType() == 1){
                if (pieces[X,Y].Player() == 1){
                    if (!pieces[X, Y].Promoted()){
                        board[X, Y - 1] = -1;
                    }
                }
                else{
                    if (!pieces[X, Y].Promoted()){
                        board[X, Y + 1] = -1;
                    }
                }
            }
            else if(pieces[X,Y].PieceType() == 2){
                if (pieces[X, Y].Player() == 1){
                    if (!pieces[X, Y].Promoted()){
                        bool temp = true;
                        board[X, Y - 1] = -1;
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
                }
                else{
                    if (!pieces[X, Y].Promoted()){
                        bool temp = true;
                        board[X, Y + 1] = -1;
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
                }
            }
        }
    }
}
