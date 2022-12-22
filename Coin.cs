using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Rectangle = Microsoft.Xna.Framework.Rectangle;
using Color = Microsoft.Xna.Framework.Color;

namespace Connect_four
{
    internal class Coin
    {
        private Texture2D _texture;
        private Rectangle _location;
        public Coin(Texture2D texture, Rectangle location)
        {
            _texture = texture;
            _location = location;
        }
        public Rectangle Location()
        {
            return _location;
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, _location, Color.Black);
        }
        public static void SetCoins(List<Coin> coins, Texture2D coinTex, List<Barrier>barriers)
        {
            for (int i = 5; i < 700; i += 47)
            {
                for (int j = 10; j < 800; j += 50)
                {
                    Rectangle temp = new Rectangle(j, i, 30, 30);
                    bool temp2 = true;
                    foreach (Barrier b in barriers)
                        if (b.DoesIntersect(temp))
                            temp2 = false;
                    if (temp2)
                    {
                        coins.Add(new Coin(coinTex, temp));
                    }
                }
            }
        }
    }
}
