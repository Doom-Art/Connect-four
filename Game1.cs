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
        float seconds;
        float startTime;
        SpriteFont font;
        SpriteFont smallFont;
        enum Screen
        {
            Menu,
            Connect4,
            Connect4Help,
            Pacman
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
        Texture2D closeButtonTex;
        Texture2D questionIcon;
        int playerTurn;
        int winner;
        Board board;
        bool gameWon;
        Button closeButton;
        Button helpButton;

        //Pacman variables:
        Texture2D pacUp;
        Texture2D pacDown;
        Texture2D pacLeft;
        Texture2D pacRight;
        Texture2D barrier;
        Texture2D coin;
        

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
            closeButton = new Button(closeButtonTex, new Rectangle(720, 20, 50, 50));
            helpButton = new Button(questionIcon, new Rectangle(660, 20, 50, 50));
            board = new Board(gameBoard, gamePiece);
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            questionIcon = Content.Load<Texture2D>("questionIcon");
            gameBoard = Content.Load<Texture2D>("Connect4Board");
            pacPlay = Content.Load<Texture2D>("pacPlay");
            connect4Play = Content.Load<Texture2D>("Connect4Play");
            gamePiece = Content.Load<Texture2D>("circle");
            font = Content.Load<SpriteFont>("MilkyHoney");
            smallFont = Content.Load<SpriteFont>("Small Font");
            closeButtonTex = Content.Load<Texture2D>("close_box_red");

            pacDown = Content.Load<Texture2D>("pac_down");
            pacUp = Content.Load<Texture2D>("pac_up");
            pacLeft = Content.Load<Texture2D>("pac_left");
            pacRight = Content.Load<Texture2D>("pac_right");
            barrier = Content.Load<Texture2D>("rock_barrier");
            coin = Content.Load<Texture2D>("coin");
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
                    if (pacPlayRect.Contains(mouseState.X, mouseState.Y)){
                        screen = Screen.Pacman;
                    }
                }
            }
            else if(screen == Screen.Connect4){
                if (mouseState.Y < 100)
                    IsMouseVisible = true;
                else
                    IsMouseVisible = false;
                if(mouseState.LeftButton == ButtonState.Pressed && prevMouseState.LeftButton == ButtonState.Released){
                    if (closeButton.Clicked(mouseState)) {
                        screen = Screen.Menu;
                    }
                    else if (helpButton.Clicked(mouseState)){
                        screen = Screen.Connect4Help;
                    }
                    else if (!gameWon){
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
                        }
                    }
                }
            }
            else if(screen == Screen.Connect4Help){
                if(mouseState.LeftButton == ButtonState.Pressed && prevMouseState.LeftButton == ButtonState.Released){
                    if (closeButton.Clicked(mouseState))
                        screen = Screen.Connect4;
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
                closeButton.Draw(_spriteBatch);
                helpButton.Draw(_spriteBatch);
                board.Draw(_spriteBatch);
                //Below code is for drawing the game piece when the mouse is on the board
                if (mouseState.Y > 100){
                    if (playerTurn == 1){
                        _spriteBatch.Draw(gamePiece, new Rectangle(mouseState.X - 40, mouseState.Y - 40, 75, 75), Color.Blue);
                        _spriteBatch.Draw(gamePiece, new Rectangle(mouseState.X - 37, mouseState.Y - 37, 75, 75), Color.Red);
                    }
                    else if (playerTurn == 2){
                        _spriteBatch.Draw(gamePiece, new Rectangle(mouseState.X - 40, mouseState.Y - 40, 75, 75), Color.White);
                        _spriteBatch.Draw(gamePiece, new Rectangle(mouseState.X - 37, mouseState.Y - 37, 75, 75), Color.Black);
                    }
                }
                if (!gameWon){
                    if (playerTurn == 1)
                        _spriteBatch.DrawString(font, "Player 1's Turn", new Vector2(100, 10), Color.Red);
                    else if (playerTurn == 2)
                        _spriteBatch.DrawString(font, "Player 2's Turn", new Vector2(100, 10), Color.Black);
                }
                else{
                    if (winner == 1){
                        _spriteBatch.DrawString(font, "Player 1 Won", new Vector2(102, 7), Color.Gold);
                        _spriteBatch.DrawString(font, "Player 1 Won", new Vector2(105, 10), Color.Red);
                    }
                    else if (winner == 2){
                        _spriteBatch.DrawString(font, "Player 2 Won", new Vector2(102, 7), Color.Gold);
                        _spriteBatch.DrawString(font, "Player 2 Won", new Vector2(105, 10), Color.Black);
                    }
                    else if (winner == -1)
                        _spriteBatch.DrawString(font, "No one wins", new Vector2(120, 10), Color.Turquoise);
                }
            }
            //below code is for connect 4 help screen
            else if(screen == Screen.Connect4Help){
                GraphicsDevice.Clear(Color.White);
                closeButton.Draw(_spriteBatch);
                _spriteBatch.DrawString(font, "Rules", new Vector2(300, 20), Color.Black);
                _spriteBatch.DrawString(smallFont, "Left Click a column to drop a piece", new Vector2(10, 120), Color.Black);
                _spriteBatch.DrawString(smallFont, "Players must connect 4 of the same colored discs in a row to win.\nOnly one piece is played at a time.\nPlayers can be on the offensive or defensive.\r\nThe game ends when there is a 4-in-a-row or a stalemate.\r\nPress the close button to return to the game", new Vector2(10, 150), Color.Black);
            }
            
            _spriteBatch.End();
            base.Draw(gameTime);
        }
        
    }
}