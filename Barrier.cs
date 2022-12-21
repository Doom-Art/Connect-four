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
            spriteBatch.Draw(_texture, _location, Color.DarkTurquoise);
        }
        public static void PositionSet(List<Barrier> barriers, Texture2D barrierTex)
        {
            int width = 10;
            barriers.Add(new Barrier(barrierTex, new Rectangle(0, 0, 5, 700)));
            barriers.Add(new Barrier(barrierTex, new Rectangle(795, 0, 5, 700)));
            barriers.Add(new Barrier(barrierTex, new Rectangle(0, 0, 800, 5)));
            barriers.Add(new Barrier(barrierTex, new Rectangle(0, 695, 800, 5)));
            barriers.Add(new Barrier(barrierTex, new Rectangle(60, 60, 300, width)));
            barriers.Add(new Barrier(barrierTex, new Rectangle(60, 60, width, 255)));
            barriers.Add(new Barrier(barrierTex, new Rectangle(60, 380, width, 255)));
            barriers.Add(new Barrier(barrierTex, new Rectangle(60, 615, 300, width)));
            barriers.Add(new Barrier(barrierTex, new Rectangle(430, 60, 220, width)));
            barriers.Add(new Barrier(barrierTex, new Rectangle(430, 615, 300, width)));
            barriers.Add(new Barrier(barrierTex, new Rectangle(720, 380, width, 255)));
            barriers.Add(new Barrier(barrierTex, new Rectangle(720, 60, width, 255)));
            barriers.Add(new Barrier(barrierTex, new Rectangle(140, 140, 600, width)));
            barriers.Add(new Barrier(barrierTex, new Rectangle(145, 535, 480, width)));
            barriers.Add(new Barrier(barrierTex, new Rectangle(145, 460, 480, width)));
            barriers.Add(new Barrier(barrierTex, new Rectangle(145, 260, width, 200)));
            barriers.Add(new Barrier(barrierTex, new Rectangle(210, 240, width, 160)));
            barriers.Add(new Barrier(barrierTex, new Rectangle(210, 400, 300, width)));
            barriers.Add(new Barrier(barrierTex, new Rectangle(210, 240, 300, width)));
        }
    }
}
