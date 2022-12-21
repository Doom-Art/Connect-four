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
        public static Random rand = new Random();
        public Ghost(Texture2D leftTexture, Texture2D rightTexture, Rectangle rectangle)
        {
            _leftTexture = leftTexture;
            _rightTexture = rightTexture;
            _currentTex = leftTexture;
            _location = rectangle;
            _speed = new Vector2(-2,0);
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
            spriteBatch.Draw(_currentTex, _location, Color.Green);
        }
    }
}
