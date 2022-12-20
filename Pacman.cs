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
    internal class Pacman
    {
        private Texture2D _pacUp;
        private Texture2D _pacDown;
        private Texture2D _pacLeft;
        private Texture2D _pacRight;
        private Texture2D _pacCurrentTex;
        private Rectangle _location;
        private Vector2 _speed;
        private Vector2 _left;
        private Vector2 _right;
        private Vector2 _up;
        private Vector2 _down;
        public Pacman(Texture2D pacUp, Texture2D pacDown, Texture2D pacLeft, Texture2D pacRight, Rectangle location)
        {
            _pacUp = pacUp;
            _pacDown = pacDown;
            _pacLeft = pacLeft;
            _pacRight = pacRight;
            _location = location;
            _speed = new Vector2(0,0);
            _pacCurrentTex = _pacRight;
            _left = new Vector2(-2, 0);
            _right = new Vector2(2, 0);
            _up = new Vector2(0, -2);
            _down = new Vector2(0, 2);
        }
        public void Move()
        {
            _location.X += (int)_speed.X;
            _location.Y += (int)_speed.Y;
        }
        public void Update(KeyboardState keyboardState)
        {
            if (keyboardState.IsKeyDown(Keys.Left)){
                _speed = _left;
                _pacCurrentTex = _pacLeft;
            }
            else if (keyboardState.IsKeyDown(Keys.Right)){
                _speed = _right;
                _pacCurrentTex = _pacRight;
            }
            else if (keyboardState.IsKeyDown(Keys.Up)){
                _speed = _up;
                _pacCurrentTex = _pacUp;
            }
            else if (keyboardState.IsKeyDown(Keys.Down)){
                _speed = _down;
                _pacCurrentTex = _pacDown;
            }
        }
        public void Intersects(Rectangle barrier)
        {
            if (_speed == _left){
                if (_location.Intersects(barrier)){
                    _location.X = barrier.Right;
                }
            }
            else if (_speed == _right){
                if (_location.Intersects(barrier)){
                    _location.X = barrier.Left - _location.Width;
                }
            }
            else if (_speed == _up){
                if (_location.Intersects(barrier)){
                    _location.Y = barrier.Bottom;
                }
            }
            else if (_speed == _down){
                if (_location.Intersects(barrier)){
                    _location.Y = barrier.Top - _location.Height;
                }
            }
        }
        public bool IntersectCoin(Rectangle rect)
        {
            return _location.Intersects(rect);
        }
        public void Draw(SpriteBatch spriteBatch)
        { 
            spriteBatch.Draw(_pacCurrentTex, _location, Color.White);
        }
    }
}
