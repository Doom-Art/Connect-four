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
        KeyboardState keyboardState;
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
        List<Barrier> barriers;
        List<Coin> coins;
        Ghost ghost;
        Pacman pacman;
        Texture2D pacUp;
        Texture2D pacDown;
        Texture2D pacLeft;
        Texture2D pacRight;
        Texture2D barrierTex;
        Texture2D coinTex;
        Texture2D ghostLeft;
        Texture2D ghostRight;
        

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            this.Window.Title = "Mini Arcade Menu";
            screen = Screen.Menu;
            pacPlayRect = new Rectangle(150, 290, 200, 200);
            c4Rect = new Rectangle(450, 290, 200, 200);
            barriers = new List<Barrier>();
            coins = new List<Coin>();
            _graphics.PreferredBackBufferWidth = 800;
            _graphics.PreferredBackBufferHeight = 700;
            _graphics.ApplyChanges();
            base.Initialize();
            closeButton = new Button(closeButtonTex, new Rectangle(720, 20, 50, 50));
            helpButton = new Button(questionIcon, new Rectangle(660, 20, 50, 50));
            board = new Board(gameBoard, gamePiece);
            pacman = new Pacman(pacUp, pacDown, pacLeft, pacRight, new Rectangle(5, 5, 50,50));
            Barrier.PositionSet(barriers, barrierTex);
            ghost = new Ghost(ghostLeft, ghostRight, new Rectangle(200, 200, 50, 50));
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

            pacDown = Content.Load<Texture2D>("HelmetDown");
            pacUp = Content.Load<Texture2D>("HelmetUp");
            pacLeft = Content.Load<Texture2D>("HelmetLeft");
            pacRight = Content.Load<Texture2D>("HelmetRight");
            barrierTex = Content.Load<Texture2D>("rock_barrier");
            coinTex = Content.Load<Texture2D>("coin");
            ghostLeft = Content.Load<Texture2D>("GhostLeft");
            ghostRight = Content.Load<Texture2D>("GhostRight");
        }

        protected override void Update(GameTime gameTime)
        {
            seconds = (float)gameTime.TotalGameTime.TotalSeconds - startTime;
            keyboardState = Keyboard.GetState();
            prevMouseState = mouseState;
            mouseState = Mouse.GetState();
            //this.Window.Title = $"Mouse X: {mouseState.X} Mouse Y: {mouseState.Y}";
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
                        this.Window.Title = "Connect 4";
                    }
                    if (pacPlayRect.Contains(mouseState.X, mouseState.Y)){
                        this.Window.Title = "Pacman";
                        gameWon = false;
                        pacman.Reset();
                        screen = Screen.Pacman;
                        for (int i = 5; i < 700; i += 47)
                        {
                            for (int j = 10; j < 800; j += 50)
                            {
                                Rectangle temp = new Rectangle(j, i, 30, 30);
                                bool temp2 = true;
                                foreach (Barrier b in barriers)
                                    if (b.DoesIntersect(temp))
                                        temp2 = false;
                                if (temp2){
                                    coins.Add(new Coin(coinTex, temp));
                                }
                            }
                        }
                    }
                }
            }
            else if (screen == Screen.Pacman){
                if (!gameWon){
                    pacman.Update(keyboardState);
                    pacman.Move();
                    foreach (Barrier b in barriers)
                    {
                        pacman.Intersects(b.location());
                    }
                    for (int i = 0; i < coins.Count; i++)
                    {
                        if (pacman.IntersectCoin(coins[i].Location())){
                            coins.RemoveAt(i);
                            i--;
                        }
                    }
                    if (coins.Count == 0){
                        gameWon = true;
                        
                    }
                }
                else{
                    if (mouseState.LeftButton == ButtonState.Pressed && prevMouseState.LeftButton == ButtonState.Released){
                        if (closeButton.Clicked(mouseState))
                            screen = Screen.Menu;
                        this.Window.Title = "Mini Arcade Menu";
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
                        this.Window.Title = "Mini Arcade Menu";
                    }
                    else if (helpButton.Clicked(mouseState)){
                        screen = Screen.Connect4Help;
                        this.Window.Title = "Connect 4 Rules/Instructions";
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
                    this.Window.Title = "Connect 4";
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
            else if(screen == Screen.Pacman){
                GraphicsDevice.Clear(Color.SteelBlue);
                //_spriteBatch.Draw(roadBackground, new Rectangle(0, 0, 800, 700), Color.White);
                pacman.Draw(_spriteBatch);
                foreach (Barrier b in barriers){
                    b.Draw(_spriteBatch);
                }
                foreach(Coin c in coins){
                    c.Draw(_spriteBatch);
                }
                if (gameWon){
                    _spriteBatch.DrawString(font, "You Won", new Vector2(200, 300), Color.Black);
                    closeButton.Draw(_spriteBatch);
                }
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