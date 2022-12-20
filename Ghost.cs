using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connect_four
{
    internal class Ghost
    {
        private Texture2D _currentTex;
        private Texture2D _leftTexture;
        private Texture2D _rightTexture;
        private Rectangle _rectangle;
        private Vector2 _speed;
        public Ghost(Texture2D leftTexture, Texture2D rightTexture, Rectangle rectangle)
        {
            _leftTexture = leftTexture;
            _rightTexture = rightTexture;
            _currentTex = leftTexture;
            _rectangle = rectangle;
            _speed = new Vector2(0,0);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_currentTex, _rectangle, Color.White);
        }
    }
}
