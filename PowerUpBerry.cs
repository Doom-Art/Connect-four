using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Color = Microsoft.Xna.Framework.Color;
using Rectangle = Microsoft.Xna.Framework.Rectangle;

namespace Connect_four
{
    internal class PowerUpBerry
    {
        private Texture2D _texture;
        private Rectangle _location;
        public PowerUpBerry(Texture2D texture, Rectangle location, List<Coin>coins)
        {
            _texture = texture;
            _location = location;
            for (int i = 0; i < coins.Count; i++)
            {
                if (_location.Intersects(coins[i].Location()))
                {
                    coins.RemoveAt(i);
                    i--;
                }
            }
        }
        public bool GetPowerUp(Rectangle rect)
        {
            return _location.Intersects(rect);
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, _location, Color.Yellow);
        }
    }
}
