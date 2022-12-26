using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
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
        SoundEffectInstance gameOverInstance;
        Random rand;
        enum Screen
        {
            Menu,
            Connect4,
            Connect4Help,
            Pacman,
            PacmanInstructions,
            BuildingJumper
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
        SoundEffectInstance player1WonInstance;
        SoundEffectInstance player2WonInstance;

        //Pacman variables:
        List<Barrier> barriers;
        List<Coin> coins;
        List<Ghost> ghosts;
        List<PowerUpBerry> berries;
        bool powerUp;
        bool temp;
        Pacman pacman;
        Texture2D pacUp;
        Texture2D pacDown;
        Texture2D pacLeft;
        Texture2D pacRight;
        Texture2D barrierTex;
        Texture2D coinTex;
        Texture2D circleTex;
        Texture2D ghostLeft;
        Texture2D ghostRight;
        SoundEffectInstance gameWonInstance;
        SoundEffect coinSound;

        //Building Jumper Lua Game
        List<Building> buildings;
        List<Texture2D> buildingTextures;
        Jumper rabbitJumper;
        Texture2D rabbitTex;
        int score;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            //Menu and General vars
            this.Window.Title = "Mini Arcade Menu";
            screen = Screen.Menu;
            _graphics.PreferredBackBufferWidth = 800;
            _graphics.PreferredBackBufferHeight = 700;
            _graphics.ApplyChanges();
            temp = true;
            c4Rect = new Rectangle(450, 290, 200, 200);
            pacPlayRect = new Rectangle(150, 290, 200, 200);
            rand = new Random();

            //Lists
            berries = new List<PowerUpBerry>();
            barriers = new List<Barrier>();
            ghosts = new List<Ghost>();
            coins = new List<Coin>();
            buildings = new List<Building>();
            buildingTextures = new List<Texture2D>();

            base.Initialize();
            closeButton = new Button(closeButtonTex, new Rectangle(720, 20, 50, 50));
            helpButton = new Button(questionIcon, new Rectangle(660, 20, 50, 50));
            board = new Board(gameBoard, gamePiece);
            pacman = new Pacman(pacUp, pacDown, pacLeft, pacRight, new Rectangle(5, 5, 50,50));
            Barrier.PositionSet(barriers, barrierTex);
            Ghost.GenerateGhosts(ghosts, ghostLeft, ghostRight);

            rabbitJumper = new Jumper(rabbitTex, _graphics);
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            font = Content.Load<SpriteFont>("MilkyHoney");
            smallFont = Content.Load<SpriteFont>("Small Font");
            gameOverInstance = Content.Load<SoundEffect>("game_over").CreateInstance();
            gameWonInstance = Content.Load<SoundEffect>("goodJobPac").CreateInstance();
            player1WonInstance= Content.Load<SoundEffect>("Player1W").CreateInstance();
            player2WonInstance = Content.Load<SoundEffect>("Player2W").CreateInstance();
            coinSound = Content.Load<SoundEffect>("ding");

            questionIcon = Content.Load<Texture2D>("questionIcon");
            gameBoard = Content.Load<Texture2D>("Connect4Board");
            pacPlay = Content.Load<Texture2D>("pacPlay");
            connect4Play = Content.Load<Texture2D>("Connect4Play");
            gamePiece = Content.Load<Texture2D>("circle");
            closeButtonTex = Content.Load<Texture2D>("close_box_red");

            pacDown = Content.Load<Texture2D>("HelmetDown");
            pacUp = Content.Load<Texture2D>("HelmetUp");
            pacLeft = Content.Load<Texture2D>("HelmetLeft");
            pacRight = Content.Load<Texture2D>("HelmetRight");
            barrierTex = Content.Load<Texture2D>("rock_barrier");
            coinTex = Content.Load<Texture2D>("coin");
            ghostLeft = Content.Load<Texture2D>("GhostLeft");
            ghostRight = Content.Load<Texture2D>("GhostRight");
            circleTex = Content.Load<Texture2D>("circle");

            buildingTextures.Add(Content.Load<Texture2D>("buildingA"));
            buildingTextures.Add(Content.Load<Texture2D>("buildingB"));
            buildingTextures.Add(Content.Load<Texture2D>("house"));
            rabbitTex = Content.Load<Texture2D>("bunny");
        }

        protected override void Update(GameTime gameTime)
        {
            seconds = (float)gameTime.TotalGameTime.TotalSeconds - startTime;
            keyboardState = Keyboard.GetState();
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
                        this.Window.Title = "Connect 4";
                    }
                    else if (pacPlayRect.Contains(mouseState.X, mouseState.Y)){
                        this.Window.Title = "Pacman";
                        gameWon = false;
                        powerUp = false;
                        pacman.Reset();
                        ghosts.Clear();
                        Ghost.GenerateGhosts(ghosts, ghostLeft, ghostRight);
                        screen = Screen.PacmanInstructions;
                        winner = 0;
                        Coin.SetCoins(coins, coinTex, barriers);
                        PowerUpBerry.BerrySet(berries, circleTex, coins);
                    }
                }
                else if (keyboardState.IsKeyDown(Keys.L)){
                    screen = Screen.BuildingJumper;
                    buildings.Clear();
                    buildings.Add(new Building(buildingTextures[0], _graphics));
                    score = 0;
                }
            }
            else if (screen == Screen.BuildingJumper){
                this.Window.Title = $"Rabbit Jumper, Buildings Jumped: {score}";
                if (!gameWon){
                    rabbitJumper.Update(keyboardState, _graphics);
                    for (int i = 0; i < buildings.Count; i++)
                    {
                        buildings[i].Update();
                        if (buildings[i].Location().Intersects(rabbitJumper.Location())){
                            //gameWon = true;
                            score--;
                        }
                        else if (buildings[i].Location().Right < 0){
                            buildings.Add(new Building(buildingTextures[rand.Next(0, 3)], _graphics));
                            buildings.RemoveAt(i);
                            i--;
                            score++;
                        }
                    }
                }
                else{
                    if (keyboardState.IsKeyDown(Keys.R)){
                        gameWon = false; 
                        score= 0;
                        rabbitJumper.Reset(_graphics);
                        buildings.Clear();
                        buildings.Add(new Building(buildingTextures[0], _graphics));
                    }
                }
                
            }
            else if (screen == Screen.PacmanInstructions){
                if ((mouseState.LeftButton == ButtonState.Pressed && prevMouseState.LeftButton == ButtonState.Released) || keyboardState.IsKeyDown(Keys.Enter)){
                    screen = Screen.Pacman;
                }
                else if (keyboardState.IsKeyDown(Keys.L)){
                    gameWon = false;
                    pacman.Reset();
                    ghosts.Clear();
                    powerUp = false;
                    ghosts.Clear();
                    screen = Screen.Pacman;
                    Coin.SetCoins(coins, coinTex, barriers);
                    PowerUpBerry.BerrySet(berries, circleTex, coins);
                }
                if (keyboardState.IsKeyDown(Keys.D1))
                    pacman.SpeedSet(1);
                else if (keyboardState.IsKeyDown(Keys.D2))
                    pacman.SpeedSet(2);
                else if (keyboardState.IsKeyDown(Keys.D3))
                    pacman.SpeedSet(3);
                else if (keyboardState.IsKeyDown(Keys.D4))
                    pacman.SpeedSet(4);
                else if (keyboardState.IsKeyDown(Keys.K)){
                    pacman.SpeedSet(9);
                }
            }
            else if (screen == Screen.Pacman){
                if (!gameWon){
                    pacman.Update(keyboardState);
                    pacman.Move();
                    if (!powerUp)
                        foreach (Ghost ghost in ghosts)
                            ghost.Move();
                    else{
                        if (temp){
                            foreach (Ghost ghost in ghosts)
                                ghost.Move();
                            temp = false;
                        }
                        else
                            temp = true;
                    }
                    foreach (Barrier b in barriers)
                    {
                        pacman.IntersectsBarrier(b.location());
                        foreach (Ghost ghost in ghosts)
                            ghost.Crash(b.location());
                    }
                    for (int i = 0; i < coins.Count; i++)
                    {
                        if (pacman.Intersect(coins[i].Location())){
                            coins.RemoveAt(i);
                            i--;
                            coinSound.Play(volume: 0.1f, pitch: 0.0f, pan: 0.0f);
                        }
                    }
                    for (int i = 0; i < berries.Count; i++)
                    {
                        if (berries[i].GetPowerUp(pacman.Location())){
                            powerUp = true;
                            berries.RemoveAt(i);
                            i--;
                            startTime = (float)gameTime.TotalGameTime.TotalSeconds;
                            seconds = (float)gameTime.TotalGameTime.TotalSeconds - startTime;
                        }
                    }
                    if (powerUp){
                        this.Window.Title = $"Pacman || {(5.1 - seconds).ToString("00:0")} Seconds Left";
                        if (seconds > 5){
                            powerUp = false;
                            this.Window.Title = "Pacman";

                        }
                    }
                    if (coins.Count == 0){
                        gameWon = true; 
                        winner = 1;
                        gameWonInstance.Play();
                    }
                    for(int i = 0; i<ghosts.Count; i++)
                    {
                        if (pacman.Intersect(ghosts[i].Location())){
                            if (!powerUp){
                                gameWon = true;
                                winner = -1;
                                gameOverInstance.Play();
                            }
                            else{
                                ghosts.RemoveAt(i);
                                i--;
                            }
                        }
                    }
                }
                else{
                    if ((mouseState.LeftButton == ButtonState.Pressed && prevMouseState.LeftButton == ButtonState.Released)){
                        if (closeButton.Clicked(mouseState))
                            screen = Screen.Menu;
                        this.Window.Title = "Mini Arcade Menu";
                    }
                    else if (keyboardState.IsKeyDown(Keys.R)){
                        gameWon = false;
                        pacman.Reset();
                        ghosts.Clear();
                        powerUp = false;
                        Ghost.GenerateGhosts(ghosts, ghostLeft, ghostRight);
                        screen = Screen.Pacman;
                        Coin.SetCoins(coins, coinTex, barriers);
                        PowerUpBerry.BerrySet(berries, circleTex, coins);
                    }//One ghost mode is triggered by I
                    else if (keyboardState.IsKeyDown(Keys.I)){
                        gameWon = false;
                        pacman.Reset();
                        ghosts.Clear();
                        powerUp = false;
                        Ghost.GenerateOneGhost(ghosts, ghostLeft, ghostRight);
                        screen = Screen.Pacman;
                        Coin.SetCoins(coins, coinTex, barriers);
                        PowerUpBerry.BerrySet(berries, circleTex, coins);
                    }
                    else if (keyboardState.IsKeyDown(Keys.L)){
                        gameWon = false;
                        pacman.Reset();
                        ghosts.Clear();
                        powerUp = false;
                        screen = Screen.Pacman;
                        Coin.SetCoins(coins, coinTex, barriers);
                        PowerUpBerry.BerrySet(berries, circleTex, coins);
                    }
                }
                if (keyboardState.IsKeyDown(Keys.C))
                {
                    screen = Screen.Menu;
                    this.Window.Title = "Mini Arcade Menu";
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
                            if (winner == 1)
                                player1WonInstance.Play();
                            else
                                player2WonInstance.Play();
                        }
                        if (board.CheckStalemate()){
                            gameWon = true;
                            winner = -1;
                            gameOverInstance.Play();
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
            else if(screen == Screen.BuildingJumper)
            {
                GraphicsDevice.Clear(Color.Turquoise);
                foreach (Building b in buildings)
                    b.Draw(_spriteBatch);
                rabbitJumper.Draw(_spriteBatch);
                if (gameWon)
                {
                    _spriteBatch.DrawString(font, "You Lose", new Vector2(200, 300), Color.Black);
                }
            }
            else if (screen == Screen.PacmanInstructions){
                GraphicsDevice.Clear(Color.White);
                _spriteBatch.DrawString(font, "Instructions", new Vector2(230, 20), Color.Black);
                _spriteBatch.DrawString(smallFont, "Use the Arrow keys to move Pacman around\nCollect all the coins to win\nYou lose if you touch a ghost\nLeft Click or press Enter to start the game\nAfter the game ends press R to restart or C to close\nGrab a power berry to get ghost eating powers for 5 seconds (Can only eat one at a time)\nChoose the difficulty:\n1 for Hell\n2 for Normal (default) \n3 for Hacker Mode\n4 for Random Speeds\n'L' for exploration mode", new Vector2(10, 120), Color.Black);
            }
            else if(screen == Screen.Pacman){
                GraphicsDevice.Clear(Color.Turquoise);
                pacman.Draw(_spriteBatch);
                foreach (Barrier b in barriers)
                    b.Draw(_spriteBatch);
                foreach(Coin c in coins)
                    c.Draw(_spriteBatch);
                foreach (PowerUpBerry berry in berries)
                    berry.Draw(_spriteBatch);
                foreach (Ghost ghost in ghosts)
                {
                    if (!powerUp)
                        ghost.Draw(_spriteBatch);
                    else{
                        if (seconds < 4)
                            ghost.Draw(_spriteBatch, false);
                        else if (seconds <= 4.5)
                            ghost.Draw(_spriteBatch, true);
                        else
                            ghost.Draw(_spriteBatch, false);
                    }

                }
                if (gameWon){
                    if (winner == 1)
                        _spriteBatch.DrawString(font, "You Won", new Vector2(200, 300), Color.Black);
                    else
                        _spriteBatch.DrawString(font, "You Lose", new Vector2(200, 300), Color.White);
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