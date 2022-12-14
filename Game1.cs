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
        Texture2D mouseGamePiece;
        int playerTurn;
        MouseState mouseState;
        MouseState prevMouseState;
        Board board;
        bool gameWon;
        SpriteFont font;
        int winner;
        enum Screen
        {
            Menu,
            Connect4
        }
        Screen screen;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            winner = 0;
            screen = Screen.Connect4;
            playerTurn = 1;
            gameWon = false;
            _graphics.PreferredBackBufferWidth = 800;
            _graphics.PreferredBackBufferHeight = 700;
            _graphics.ApplyChanges();
            base.Initialize();
            board = new Board(gameBoard, gamePiece);
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            gameBoard = Content.Load<Texture2D>("Connect4Board");
            gamePiece = Content.Load<Texture2D>("circle");
            font = Content.Load<SpriteFont>("MilkyHoney");
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
                winner = board.CheckForFour();
                if (winner != 0){
                    this.Window.Title = $"Player {winner} wins";
                    gameWon = true;
                }
                if (board.CheckStalemate()){
                    gameWon = true;
                    winner = -1;
                }
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);
            _spriteBatch.Begin();

            if (screen == Screen.Connect4){
                if (!gameWon){
                    if (playerTurn == 1)
                        _spriteBatch.DrawString(font, "Player 1's Turn", new Vector2(110, 10), Color.Red);
                    else if (playerTurn == 2)
                        _spriteBatch.DrawString(font, "Player 2's Turn", new Vector2(110, 10), Color.Black);
                }
                else{
                    if (winner == 1)
                        _spriteBatch.DrawString(font, "Player 1 Won", new Vector2(115, 10), Color.Red);
                    else if (winner == 2)
                        _spriteBatch.DrawString(font, "Player 2 Won", new Vector2(115, 10), Color.Black);
                    else if (winner == -1)
                        _spriteBatch.DrawString(font, "No one wins", new Vector2(115, 10), Color.Turquoise);
                }
                board.Draw(_spriteBatch);
            }

            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}