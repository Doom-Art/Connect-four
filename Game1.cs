using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace Connect_four
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        Texture2D gameBoard;
        Texture2D gamePiece;
        int playerTurn;
        MouseState mouseState;
        MouseState prevMouseState;
        Board board;
        bool gameWon;
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            playerTurn = 1;
            gameWon = false;
            _graphics.PreferredBackBufferWidth = 800;
            _graphics.PreferredBackBufferHeight = 600;
            _graphics.ApplyChanges();
            base.Initialize();
            board = new Board(gameBoard, gamePiece);
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            gameBoard = Content.Load<Texture2D>("Connect4Board");
            gamePiece = Content.Load<Texture2D>("circle");
        }

        protected override void Update(GameTime gameTime)
        {
            prevMouseState = mouseState;
            mouseState = Mouse.GetState();
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            if (!gameWon && mouseState.LeftButton == ButtonState.Pressed && prevMouseState.LeftButton == ButtonState.Released){
                if(board.PlayerTurn(mouseState, playerTurn)){
                    if (playerTurn == 1)
                        playerTurn = 2;
                    else if (playerTurn == 2)
                        playerTurn = 1;
                }
                int winner = board.CheckForFour();
                if (winner != 0){
                    this.Window.Title = $"Player {winner} wins";
                    gameWon = true;
                }
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            _spriteBatch.Begin();

            board.Draw(_spriteBatch);

            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}