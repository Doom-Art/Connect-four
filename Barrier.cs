using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
        public bool DoesIntersect(Rectangle rectangle)
        {
            return _location.Intersects(rectangle);
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, _location, Color.DarkBlue);
        }
        public static void PositionSet(List<Barrier> barriers, Texture2D barrierTex)
        {
            barriers.Add(new Barrier(barrierTex, new Rectangle(0, 0, 5, 700)));
            barriers.Add(new Barrier(barrierTex, new Rectangle(795, 0, 5, 700)));
            barriers.Add(new Barrier(barrierTex, new Rectangle(0, 0, 800, 5)));
            barriers.Add(new Barrier(barrierTex, new Rectangle(0, 695, 800, 5)));
            barriers.Add(new Barrier(barrierTex, new Rectangle(60, 60, 300, 20)));
            barriers.Add(new Barrier(barrierTex, new Rectangle(60, 60, 20, 255)));
            barriers.Add(new Barrier(barrierTex, new Rectangle(60, 380, 20, 255)));
            barriers.Add(new Barrier(barrierTex, new Rectangle(60, 615, 300, 20)));
            barriers.Add(new Barrier(barrierTex, new Rectangle(430, 60, 220, 20)));
            barriers.Add(new Barrier(barrierTex, new Rectangle(430, 615, 300, 20)));
            barriers.Add(new Barrier(barrierTex, new Rectangle(720, 380, 20, 255)));
            barriers.Add(new Barrier(barrierTex, new Rectangle(720, 60, 20, 255)));
            barriers.Add(new Barrier(barrierTex, new Rectangle(140, 140, 600, 20)));
        }
    }
}
