using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
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
        public static void BerrySet(List<PowerUpBerry>berries, Texture2D tex, List<Coin>coins)
        {
            berries.Clear();
            berries.Add(new PowerUpBerry(tex, new Rectangle(755, 5, 40, 40), coins));
            berries.Add(new PowerUpBerry(tex, new Rectangle(225, 360, 40, 40), coins));
        }
        public bool GetPowerUp(Rectangle rect)
        {
            return _location.Intersects(rect);
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, _location, Color.Blue);
        }
    }
}
