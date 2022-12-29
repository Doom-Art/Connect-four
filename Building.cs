using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connect_four
{
    internal class Building
    {
        private Texture2D _texture;
        private Rectangle _location;
        private Vector2 _speed;
        public Building(Texture2D texture, Rectangle location, Vector2 speed)
        {
            _texture = texture;
            _location = location;
            _speed = speed;
        }
        public Building(Texture2D tex, GraphicsDeviceManager graphics)
        {
            _texture = tex;
            _location = new Rectangle(graphics.PreferredBackBufferWidth + 20, graphics.PreferredBackBufferHeight - 200, 100, 200);
            _speed = new Vector2(-2, 0);
        }
        public Building(Texture2D tex, GraphicsDeviceManager graphics, int speed)
        {
            _texture = tex;
            _location = new Rectangle(graphics.PreferredBackBufferWidth + 20, graphics.PreferredBackBufferHeight - 200, 100, 200);
            _speed = new Vector2(-speed, 0);
        }
        public Building(Texture2D tex, GraphicsDeviceManager graphics, int speed, int height)
        {
            _texture = tex;
            _location = new Rectangle(graphics.PreferredBackBufferWidth + 20, graphics.PreferredBackBufferHeight - height, 100, height);
            _speed = new Vector2(-speed, 0);
        }
        public void Update()
        {
            _location.X += (int)_speed.X;
            _location.Y += (int)_speed.Y;
        }
        public Rectangle Location()
        {
            return _location;
        }
        public void SpeedSet(Vector2 speed)
        {
            _speed = speed;
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, _location, Color.White);
        }
    }
}
