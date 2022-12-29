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
            spriteBatch.Draw(_texture, _location, Color.Violet);
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
            barriers.Add(new Barrier(barrierTex, new Rectangle(140, 615, 220, width)));
            barriers.Add(new Barrier(barrierTex, new Rectangle(430, 60, 220, width)));
            barriers.Add(new Barrier(barrierTex, new Rectangle(430, 615, 300, width)));
            barriers.Add(new Barrier(barrierTex, new Rectangle(720, 380, width, 255)));
            barriers.Add(new Barrier(barrierTex, new Rectangle(720, 60, width, 255)));
            barriers.Add(new Barrier(barrierTex, new Rectangle(140, 140, 580, width)));
            barriers.Add(new Barrier(barrierTex, new Rectangle(230, 535, 400, width)));
            barriers.Add(new Barrier(barrierTex, new Rectangle(145, 460, 480, width)));
            barriers.Add(new Barrier(barrierTex, new Rectangle(145, 240, width, 220)));
            barriers.Add(new Barrier(barrierTex, new Rectangle(210, 240, width, 160)));
            barriers.Add(new Barrier(barrierTex, new Rectangle(210, 400, 300, width)));
            barriers.Add(new Barrier(barrierTex, new Rectangle(145, 240, 200, width)));
            barriers.Add(new Barrier(barrierTex, new Rectangle(420, 240, 200, width)));
            barriers.Add(new Barrier(barrierTex, new Rectangle(610, 240, width, 220)));
            barriers.Add(new Barrier(barrierTex, new Rectangle(145, 535, width, 80)));
            barriers.Add(new Barrier(barrierTex, new Rectangle(350, 616, width, 80)));
            //barriers.Add(new Barrier(barrierTex, new Rectangle(709, 530, 30, width)));
        }
    }
}
