using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace Connect_four
{
    internal class Pawn
    {
        private Texture2D _pawnTexUp;
        private Texture2D _pawnTexDown;
        private Vector2 _size;
        private int _player;

        public Pawn(Texture2D pawnTexUp, Texture2D pawnTexDown, int player)
        {
            _pawnTexUp = pawnTexUp;
            _pawnTexDown = pawnTexDown;
            _size = new Vector2(60, 60);
            _player = player;
        }

    }
}
