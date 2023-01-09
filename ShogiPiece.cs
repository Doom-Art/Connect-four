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
        private Texture2D _promoted1;
        private Texture2D _promoted2;
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
            _promoted1 = promoted1;
            _promoted2 = promoted2;
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
            _promoted1 = null;
            _promoted2 = null;
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
                    spriteBatch.Draw(_promoted1, rect, Color.White);
                else
                    spriteBatch.Draw(_promoted2, rect, Color.White);
            }
        }
    }
}
