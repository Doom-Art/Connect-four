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

        //General Variables:
        MouseState mouseState;
        MouseState prevMouseState;
        Texture2D questionIcon;
        float seconds;
        float startTime;
        SpriteFont font;
        enum Screen
        {
            Menu,
            Connect4
        }
        Screen screen;

        //Menu Variables:
        Texture2D connect4Play;
        Texture2D pacPlay;
        Rectangle pacPlayRect;
        Rectangle c4Rect;

        // Connect 4 variables:
        Texture2D gameBoard;
        Texture2D gamePiece;
        int playerTurn;
        int winner;
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
            screen = Screen.Menu;
            pacPlayRect = new Rectangle(150, 290, 200, 200);
            c4Rect = new Rectangle(450, 290, 200, 200);
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
            pacPlay = Content.Load<Texture2D>("pacPlay");
            connect4Play = Content.Load<Texture2D>("Connect4Play");
            gamePiece = Content.Load<Texture2D>("circle");
            font = Content.Load<SpriteFont>("MilkyHoney");
        }

        protected override void Update(GameTime gameTime)
        {
            seconds = (float)gameTime.TotalGameTime.TotalSeconds - startTime;
            prevMouseState = mouseState;
            mouseState = Mouse.GetState();
            this.Window.Title = $"Mouse X: {mouseState.X} Mouse Y: {mouseState.Y}";
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            if (screen == Screen.Menu){
                if(mouseState.LeftButton == ButtonState.Pressed && prevMouseState.LeftButton == ButtonState.Released){
                    if (c4Rect.Contains(mouseState.X, mouseState.Y)){
                        screen = Screen.Connect4;
                        winner = 0;
                        playerTurn = 1;
                        gameWon = false;
                        IsMouseVisible = false;
                        board.Reset();
                    }
                }
            }
            else if(screen == Screen.Connect4){
                if (!gameWon && mouseState.LeftButton == ButtonState.Pressed && prevMouseState.LeftButton == ButtonState.Released){
                    if (board.PlayerTurn(mouseState, playerTurn)){
                        if (playerTurn == 1)
                            playerTurn = 2;
                        else if (playerTurn == 2)
                            playerTurn = 1;
                    }
                    winner = board.CheckForFour();
                    if (winner != 0){
                        this.Window.Title = $"Player {winner} wins";
                        gameWon = true;
                        startTime = (float)gameTime.TotalGameTime.TotalSeconds;
                    }
                    if (board.CheckStalemate()){
                        gameWon = true;
                        winner = -1;
                        startTime = (float)gameTime.TotalGameTime.TotalSeconds;
                    }
                }
                else if(gameWon && seconds >= 6){
                    screen = Screen.Menu;
                    IsMouseVisible=true;
                }
            }
            
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            _spriteBatch.Begin();
            if (screen == Screen.Menu){
                GraphicsDevice.Clear(Color.Turquoise);
                _spriteBatch.DrawString(font, "Welcome to the mini ", new Vector2(30, 20), Color.Black);
                _spriteBatch.DrawString(font, "Arcade", new Vector2(280, 90), Color.Black);
                _spriteBatch.Draw(pacPlay, pacPlayRect, Color.White);
                _spriteBatch.Draw(connect4Play, c4Rect, Color.White);
            }
            else if (screen == Screen.Connect4){
                GraphicsDevice.Clear(Color.White);
                board.Draw(_spriteBatch);
                if (playerTurn == 1){
                    _spriteBatch.Draw(gamePiece, new Rectangle(mouseState.X - 40, mouseState.Y - 40, 75, 75), Color.Blue);
                    _spriteBatch.Draw(gamePiece, new Rectangle(mouseState.X - 37, mouseState.Y - 37, 75, 75), Color.Red);
                }
                else if (playerTurn == 2){
                    _spriteBatch.Draw(gamePiece, new Rectangle(mouseState.X - 40, mouseState.Y - 40, 75, 75), Color.White);
                    _spriteBatch.Draw(gamePiece, new Rectangle(mouseState.X - 37, mouseState.Y - 37, 75, 75), Color.Black);
                }
                if (!gameWon){
                    if (playerTurn == 1)
                        _spriteBatch.DrawString(font, "Player 1's Turn", new Vector2(110, 10), Color.Red);
                    else if (playerTurn == 2)
                        _spriteBatch.DrawString(font, "Player 2's Turn", new Vector2(110, 10), Color.Black);
                }
                else{
                    if (winner == 1){
                        _spriteBatch.DrawString(font, "Player 1 Won", new Vector2(112, 7), Color.Gold);
                        _spriteBatch.DrawString(font, "Player 1 Won", new Vector2(115, 10), Color.Red);
                    }
                    else if (winner == 2){
                        _spriteBatch.DrawString(font, "Player 2 Won", new Vector2(112, 7), Color.Gold);
                        _spriteBatch.DrawString(font, "Player 2 Won", new Vector2(115, 10), Color.Black);
                    }
                    else if (winner == -1)
                        _spriteBatch.DrawString(font, "No one wins", new Vector2(120, 10), Color.Turquoise);
                }
            }

            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}