using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
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
        private Rectangle _location;
        private Vector2 _speed;
        private Color _colorMask;
        public static Random rand = new Random();

        public Ghost(Texture2D leftTexture, Texture2D rightTexture, Rectangle rectangle, Color colorMask)
        {
            _leftTexture = leftTexture;
            _rightTexture = rightTexture;
            _currentTex = leftTexture;
            _location = rectangle;
            _speed = new Vector2(-2, 0);
            _colorMask = colorMask;
        }
        public Ghost(Texture2D leftTexture, Texture2D rightTexture, Rectangle rectangle)
        {
            _leftTexture = leftTexture;
            _rightTexture = rightTexture;
            _currentTex = leftTexture;
            _location = rectangle;
            _speed = new Vector2(-2, 0);
            _colorMask = Color.White;
        }
        public static void GenerateGhosts(List<Ghost> list, Texture2D ghostLeft, Texture2D ghostRight)
        {
            for (int i = 0; i < 2; i++)
            {
                list.Add(new Ghost(ghostLeft, ghostRight, new Rectangle(732, 632, 45, 45)));
                list.Add(new Ghost(ghostLeft, ghostRight, new Rectangle(638, 542, 45, 45), Color.Green));
                list.Add(new Ghost(ghostLeft, ghostRight, new Rectangle(85, 547, 45, 45), Color.Purple));
                list.Add(new Ghost(ghostLeft, ghostRight, new Rectangle(160, 480, 45, 45), Color.Red));
                list.Add(new Ghost(ghostLeft, ghostRight, new Rectangle(222, 261, 45, 45), Color.Turquoise));
            }
            for (int i = 0; i < 0; i++)
            {
                list.Add(new Ghost(ghostLeft, ghostRight, new Rectangle(7, 650, 45, 45), Color.Orange));
            }
        }
        public static void GenerateGhosts(List<Ghost> list, Texture2D ghostLeft, Texture2D ghostRight, int numGhosts)
        {
            for (int i = 0; i < numGhosts; i++)
            {
                list.Add(new Ghost(ghostLeft, ghostRight, new Rectangle(732, 632, 45, 45)));
                list.Add(new Ghost(ghostLeft, ghostRight, new Rectangle(638, 542, 45, 45), Color.Green));
                list.Add(new Ghost(ghostLeft, ghostRight, new Rectangle(85, 547, 45, 45), Color.Purple));
                list.Add(new Ghost(ghostLeft, ghostRight, new Rectangle(160, 480, 45, 45), Color.Red));
                list.Add(new Ghost(ghostLeft, ghostRight, new Rectangle(222, 261, 45, 45), Color.Turquoise));
            }
            for (int i = 0; i < numGhosts; i++)
            {
                list.Add(new Ghost(ghostLeft, ghostRight, new Rectangle(7, 650, 45, 45), Color.Orange));
            }
        }
        public static void GenerateOneGhost(List<Ghost> list, Texture2D ghostLeft, Texture2D ghostRight)
        {
            list.Add(new Ghost(ghostLeft, ghostRight, new Rectangle(732, 632, 45, 45)));
        }
        public void Reset()
        {
            _currentTex = _leftTexture;
            _speed = new Vector2 (-2,0);
            _location = new Rectangle(732, 632, 45, 45);
        }
        public void Move()
        {
            _location.X += (int)_speed.X;
            _location.Y += (int)_speed.Y;
        }
        public Rectangle Location()
        {
            return _location;
        }
        public void Crash(Rectangle rect)
        {
            if (_location.Intersects(rect)){
                _location.X -= (int)_speed.X;
                _location.Y -= (int)_speed.Y;
                int pick = rand.Next(0, 4);
                if (pick == 0){
                    _speed = new Vector2(2, 0);
                    _currentTex = _rightTexture;
                }
                else if (pick == 1){
                    _speed = new Vector2(0, 2);
                }
                else if (pick == 2){
                    _speed = new Vector2(0, -2);
                }
                else if (pick == 3){
                    _speed = new Vector2(-2, 0);
                    _currentTex = _leftTexture;
                }
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_currentTex, _location, _colorMask);
        }
        public void Draw(SpriteBatch spriteBatch, bool test)
        {
            if(test)
                spriteBatch.Draw(_currentTex, _location, Color.Black);
            else
                spriteBatch.Draw(_currentTex, _location, Color.Blue);
        }
    }
}
