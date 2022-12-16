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
    internal class Button
    {
        private Texture2D _icon;
        private Rectangle _location;
        public Button(Texture2D icon, Rectangle location)
        {
            _icon = icon;
            _location = location;
        }

        public Rectangle Location()
        {
            return _location;
        }
        public bool Clicked(MouseState mouse)
        {
            return _location.Contains(mouse.Position);
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_icon, _location, Color.White);
        }
    }
}
