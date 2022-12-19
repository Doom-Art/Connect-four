using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connect_four
{
    internal class Barrier
    {
        private Texture2D _texture;
        private Rectangle _location;
        public Barrier(Texture2D texture, Rectangle location)
        {
            _texture = texture;
            _location = location;
        }
        public Rectangle location()
        {
            return _location;
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, _location, Color.DarkBlue);
        }
    }
}
