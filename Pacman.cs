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
        private Texture2D _pacCloseMouth;
        private Rectangle _location;
        private int _speed;
        private bool _mouthOpen;
        public Pacman(Texture2D pacUp, Texture2D pacDown, Texture2D pacLeft, Texture2D pacRight, Rectangle location, Texture2D pacCloseMouth)
        {
            _pacUp = pacUp;
            _pacDown = pacDown;
            _pacLeft = pacLeft;
            _pacRight = pacRight;
            _location = location;
            _speed = 2;
            _pacCurrentTex = _pacRight;
            _mouthOpen = false;
            _pacCloseMouth = pacCloseMouth;
        }
        public void Mouth()
        {
            if (_mouthOpen)
                _mouthOpen = false;
            else
                _mouthOpen = true;
        }
        public void Move(KeyboardState keyboardState)
        {
            if (keyboardState.IsKeyDown(Keys.Left)){
                _location.X -= _speed;
                _pacCurrentTex = _pacLeft;
            }
            else if (keyboardState.IsKeyDown(Keys.Right)){
                _location.X += _speed;
                _pacCurrentTex = _pacRight;
            }
            else if (keyboardState.IsKeyDown(Keys.Up)){
                _location.Y -= _speed;
                _pacCurrentTex = _pacUp;
            }
            else if (keyboardState.IsKeyDown(Keys.Down)){
                _location.Y += _speed;
                _pacCurrentTex = _pacDown;
            }
        }
        public void Intersects(Rectangle barrier, KeyboardState keyboardState)
        {
            if (keyboardState.IsKeyDown(Keys.Left)){
                if (_location.Intersects(barrier))
                    _location.X = barrier.Right;
            }
            if (keyboardState.IsKeyDown(Keys.Right)){
                if (_location.Intersects(barrier)){
                    _location.X = barrier.Left - _location.Width;
                }
            }
            if (keyboardState.IsKeyDown(Keys.Up)){
                if (_location.Intersects(barrier)){
                    _location.Y = barrier.Bottom;
                }
            }
            if (keyboardState.IsKeyDown(Keys.Down)){
                if (_location.Intersects(barrier)){
                    _location.Y = barrier.Top - _location.Height;
                }
            }
        }
        /*public void Intersects(Rectangle barrier)
        {
            
            if (_location.Intersects(barrier))
                _location.X = barrier.Right;
            if (_location.Intersects(barrier)){
                _location.X = barrier.Left - _location.Width;
            }
            if (_location.Intersects(barrier)){
                _location.Y = barrier.Bottom;
            }
            if (_location.Intersects(barrier)){
                _location.Y = barrier.Top - _location.Height;
            }
        }*/
        /// <summary>
        /// Moves pacman based on Up, Down, Left, Right arrows and checks for collisions with barriers
        /// </summary>
        /// <param name="keyboardState">The keyboard's current state</param>
        /// <param name="barrier">any barriers that should not be crossable</param>
        public void Move(KeyboardState keyboardState, Rectangle barrier)
        {
            if (keyboardState.IsKeyDown(Keys.Left)){
                _location.X -= _speed;
                _pacCurrentTex = _pacLeft;
                if (_location.Intersects(barrier))
                    _location.X = barrier.Right;
            }
            if (keyboardState.IsKeyDown(Keys.Right)){
                _location.X += _speed;
                _pacCurrentTex = _pacRight;
                if (_location.Intersects(barrier)){
                    _location.X = barrier.Left - _location.Width;
                }
            }
            if (keyboardState.IsKeyDown(Keys.Up)){
                _location.Y -= _speed;
                _pacCurrentTex = _pacUp;
                if (_location.Intersects(barrier)){
                    _location.Y = barrier.Bottom;
                }
            }
            if (keyboardState.IsKeyDown(Keys.Down)){
                _location.Y += _speed;
                _pacCurrentTex = _pacDown;
                if (_location.Intersects(barrier)){
                    _location.Y = barrier.Top - _location.Height;
                }
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            if(_mouthOpen)
                spriteBatch.Draw(_pacCurrentTex, _location, Color.White);
            else
                spriteBatch.Draw(_pacCloseMouth, _location, Color.White);

        }
    }
}
