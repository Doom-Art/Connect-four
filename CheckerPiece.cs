using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connect_four
{
    internal class CheckerPiece
    {
        private Texture2D _pieceTex;
        private Texture2D _kingTex;
        private bool _isKing;
        private int _player;
        private int _winner;

        public CheckerPiece(Texture2D pieceTex, Texture2D kingTex, int player)
        {
            _pieceTex = pieceTex;
            _kingTex = kingTex;
            _isKing = false;
            _player = player;
        }
        public void Draw(SpriteBatch sprite, Rectangle rect)
        {
            if (!_isKing)
            {
                if (_player == 1)
                    sprite.Draw(_pieceTex, rect, Color.Red);
                else
                    sprite.Draw(_pieceTex, rect, Color.LightGray);
            }
            else
            {
                if (_player == 1)
                {
                    sprite.Draw(_kingTex, rect, Color.Pink);
                    sprite.Draw(_kingTex, new Rectangle(rect.X + 4, rect.Y + 4, rect.Width - 8, rect.Height - 8), Color.Red);
                }
                else
                {
                    sprite.Draw(_kingTex, rect, Color.Gray);
                    sprite.Draw(_kingTex, new Rectangle(rect.X + 4, rect.Y + 4, rect.Width - 8, rect.Height - 8), Color.LightGray);
                }
            }
        }
        public int Player()
        {
            return _player;
        }
        public bool IsKing()
        {
            return _isKing;
        }
        public void King()
        {
            _isKing = true;
        }
        public int Winner()
        {
            return _winner;
        }
    }
}
