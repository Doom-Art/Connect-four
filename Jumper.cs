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
    internal class Jumper
    {
        private Texture2D _texture;
        private Rectangle _location;
        private bool _isJump;
        private bool _jumpUp;
        public Jumper(Texture2D texture, Rectangle location)
        {
            _texture = texture;
            _location = location;
            _isJump = false;
        }
        public Jumper(Texture2D texture, GraphicsDeviceManager graphics)
        {
            _texture = texture;
            _location = new Rectangle(50, graphics.PreferredBackBufferHeight-50, 50,50);
            _isJump = false;
            _jumpUp = false;
        }
        public void Reset(GraphicsDeviceManager graphics)
        {
            _location = new Rectangle(50, graphics.PreferredBackBufferHeight - 50, 50, 50);
            _isJump = false;
            _jumpUp = false;
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, _location, Color.White);
        }
        public Rectangle Location()
        {
            return _location;
        }
        public void Update(KeyboardState keys, GraphicsDeviceManager graphics)
        {
            if (keys.IsKeyDown(Keys.Space) && !_isJump){
                Jump();
            }
            if (_isJump){
                if (_location.Y > 250 && !_jumpUp){
                    _location.Y -= 5;
                }
                else if (_location.Y < 650){
                    _jumpUp = true;
                    _location.Y += 4;
                }
                else{
                    _isJump = false;
                    _jumpUp = false;
                }
            }
            if (keys.IsKeyDown(Keys.Left) && _location.Left > 0){
                _location.X += -2;
            }
            if (keys.IsKeyDown(Keys.Right) && _location.Right < graphics.PreferredBackBufferWidth){
                _location.X += 2;
            }
        }
        public bool IsJump()
        {
            return _isJump;
        }
        public void Jump()
        {
            _isJump = true;
        }
    }
}
