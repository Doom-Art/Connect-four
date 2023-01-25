using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.IO;

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
        KeyboardState prevKeyboardState;
        float seconds;
        float startTime;
        SpriteFont font;
        SpriteFont smallFont;
        SpriteFont mediumFont;
        SoundEffectInstance gameOverInstance;
        Random rand;
        Texture2D easterEgg;
        enum Screen
        {
            Menu,
            Connect4,
            Connect4Help,
            Pacman,
            PacmanInstructions,
            BuildingJumper,
            Shogi,
            Checkers,
            ShogiInstructions,
            CheckersInstructions,
            HunterWilsonEasterEgg,
            BuildingJumperInstructions
        }
        Screen screen;

        //Menu Variables:
        List<Texture2D> menuTex;
        List<Rectangle> menuRects;

        // Connect 4 variables:
        bool undoC4Move;
        Texture2D gameBoard;
        Texture2D gamePiece;
        List<Texture2D> buttonTextures;
        int playerTurn;
        int winner;
        Board board;
        bool gameWon;
        Button closeButton;
        Button helpButton;
        Button saveButton;
        Button loadButton;
        SoundEffectInstance player1WonInstance;
        SoundEffectInstance player2WonInstance;
        List<Color> pieceColors;

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
        int high_score;
        int highScore;

        //Shogi
        List<Texture2D> shogiPieceTextures;
        Shogi_Board shogiBoard;

        //Checkers
        CheckersBoard checkerBoard;

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
            highScore = 0;
            playerTurn = 1;
            _graphics.PreferredBackBufferWidth = 800;
            _graphics.PreferredBackBufferHeight = 700;
            _graphics.ApplyChanges();
            temp = true;
            undoC4Move = false;
            rand = new Random();

            //Lists
            berries = new List<PowerUpBerry>();
            barriers = new List<Barrier>();
            ghosts = new List<Ghost>();
            coins = new List<Coin>();
            buildings = new List<Building>();
            buildingTextures = new List<Texture2D>();
            shogiPieceTextures = new List<Texture2D>();
            buttonTextures = new List<Texture2D>();
            pieceColors = new List<Color>();
            menuTex = new List<Texture2D>();
            menuRects = new List<Rectangle>();

            menuRects.Add(new Rectangle(100, 200, 150, 150));
            menuRects.Add(new Rectangle(300, 200, 150, 150));
            menuRects.Add(new Rectangle(500, 200, 150, 150));
            menuRects.Add(new Rectangle(200, 450, 150, 150));
            menuRects.Add(new Rectangle(400, 450, 150, 150));

            pieceColors.Add(Color.Red);
            pieceColors.Add(Color.Green);
            pieceColors.Add(Color.Blue);
            pieceColors.Add(Color.Black);
            pieceColors.Add(Color.Purple);
            pieceColors.Add(Color.Tomato);
            pieceColors.Add(Color.Chartreuse);
            pieceColors.Add(Color.Gray);
            pieceColors.Add(Color.Navy);

            base.Initialize();
            closeButton = new Button(buttonTextures[0], new Rectangle(740, 20, 50, 50));
            helpButton = new Button(buttonTextures[1], new Rectangle(680, 20, 50, 50));
            saveButton = new Button(buttonTextures[2], new Rectangle(620, 20, 50, 50));
            loadButton = new Button(buttonTextures[3], new Rectangle(560, 20, 50, 50));
            board = new Board(gameBoard, gamePiece);
            pacman = new Pacman(pacUp, pacDown, pacLeft, pacRight);
            Barrier.PositionSet(barriers, barrierTex);
            Ghost.GenerateGhosts(ghosts, ghostLeft, ghostRight);
            rabbitJumper = new Jumper(rabbitTex, _graphics);
            shogiBoard = new Shogi_Board(shogiPieceTextures, smallFont);
            checkerBoard = new CheckersBoard(shogiPieceTextures[0], circleTex, circleTex);

        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            font = Content.Load<SpriteFont>("MilkyHoney");
            smallFont = Content.Load<SpriteFont>("Small Font");
            mediumFont = Content.Load<SpriteFont>("MedFont");
            gameOverInstance = Content.Load<SoundEffect>("game_over").CreateInstance();
            gameWonInstance = Content.Load<SoundEffect>("goodJobPac").CreateInstance();
            player1WonInstance= Content.Load<SoundEffect>("Player1W").CreateInstance();
            player2WonInstance = Content.Load<SoundEffect>("Player2W").CreateInstance();
            coinSound = Content.Load<SoundEffect>("ding");
            easterEgg = Content.Load<Texture2D>("Hunter");

            menuTex.Add(Content.Load<Texture2D>("Connect4Play"));
            menuTex.Add(Content.Load<Texture2D>("pacPlay"));
            menuTex.Add(Content.Load<Texture2D>("checkers"));
            menuTex.Add(Content.Load<Texture2D>("shogi"));
            menuTex.Add(Content.Load<Texture2D>("bunnyJump"));
            menuTex.Add(Content.Load<Texture2D>("arcadeBG"));


            buttonTextures.Add(Content.Load<Texture2D>("close_box_red"));
            buttonTextures.Add(Content.Load<Texture2D>("questionIcon"));
            buttonTextures.Add(Content.Load<Texture2D>("save"));
            buttonTextures.Add(Content.Load<Texture2D>("download"));
            gamePiece = Content.Load<Texture2D>("circle");
            gameBoard = Content.Load<Texture2D>("Connect4Board");

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
            buildingTextures.Add(Content.Load<Texture2D>("buildingC"));
            buildingTextures.Add(Content.Load<Texture2D>("buildingD"));
            rabbitTex = Content.Load<Texture2D>("bunny");

            //Shogi Pieces
            shogiPieceTextures.Add(Content.Load<Texture2D>("rectangle"));
            shogiPieceTextures.Add(Content.Load<Texture2D>("shogiPieces/pawn1"));
            shogiPieceTextures.Add(Content.Load<Texture2D>("shogiPieces/pawn2"));
            shogiPieceTextures.Add(Content.Load<Texture2D>("shogiPieces/Lance1"));
            shogiPieceTextures.Add(Content.Load<Texture2D>("shogiPieces/Lance2"));
            shogiPieceTextures.Add(Content.Load<Texture2D>("shogiPieces/Knight1"));
            shogiPieceTextures.Add(Content.Load<Texture2D>("shogiPieces/Knight2"));
            shogiPieceTextures.Add(Content.Load<Texture2D>("shogiPieces/Silver1"));
            shogiPieceTextures.Add(Content.Load<Texture2D>("shogiPieces/Silver2"));
            shogiPieceTextures.Add(Content.Load<Texture2D>("shogiPieces/Gold1"));
            shogiPieceTextures.Add(Content.Load<Texture2D>("shogiPieces/Gold2"));
            shogiPieceTextures.Add(Content.Load<Texture2D>("shogiPieces/Bishop1"));
            shogiPieceTextures.Add(Content.Load<Texture2D>("shogiPieces/Bishop2"));
            shogiPieceTextures.Add(Content.Load<Texture2D>("shogiPieces/Rook1"));
            shogiPieceTextures.Add(Content.Load<Texture2D>("shogiPieces/Rook2"));
            shogiPieceTextures.Add(Content.Load<Texture2D>("shogiPieces/King1"));
            shogiPieceTextures.Add(Content.Load<Texture2D>("shogiPieces/King2"));
            shogiPieceTextures.Add(Content.Load<Texture2D>("shogiPieces/PPawn1"));
            shogiPieceTextures.Add(Content.Load<Texture2D>("shogiPieces/PPawn2"));
            shogiPieceTextures.Add(Content.Load<Texture2D>("shogiPieces/PLance1"));
            shogiPieceTextures.Add(Content.Load<Texture2D>("shogiPieces/PLance2"));
            shogiPieceTextures.Add(Content.Load<Texture2D>("shogiPieces/PKnight1"));
            shogiPieceTextures.Add(Content.Load<Texture2D>("shogiPieces/PKnight2"));
            shogiPieceTextures.Add(Content.Load<Texture2D>("shogiPieces/PSilver1"));
            shogiPieceTextures.Add(Content.Load<Texture2D>("shogiPieces/PSilver2"));
            shogiPieceTextures.Add(Content.Load<Texture2D>("shogiPieces/PBishop1"));
            shogiPieceTextures.Add(Content.Load<Texture2D>("shogiPieces/PBishop2"));
            shogiPieceTextures.Add(Content.Load<Texture2D>("shogiPieces/PRook1"));
            shogiPieceTextures.Add(Content.Load<Texture2D>("shogiPieces/PRook2"));



        }

        protected override void Update(GameTime gameTime)
        {
            seconds = (float)gameTime.TotalGameTime.TotalSeconds - startTime;
            prevKeyboardState = keyboardState;
            keyboardState = Keyboard.GetState();
            prevMouseState = mouseState;
            mouseState = Mouse.GetState();
            //this.Window.Title = $"Mouse X: {mouseState.X} Mouse Y: {mouseState.Y}";
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            if (screen == Screen.Menu){
                if(mouseState.LeftButton == ButtonState.Pressed && prevMouseState.LeftButton == ButtonState.Released){
                    if(rand.Next(10) == 0)
                    {
                        temp = true;
                        screen = Screen.HunterWilsonEasterEgg;
                    }
                    else if (menuRects[0].Contains(mouseState.X, mouseState.Y)){
                        screen = Screen.Connect4;
                        winner = 0;
                        playerTurn = 1;
                        gameWon = false;
                        IsMouseVisible = false;
                        board.Reset();
                        this.Window.Title = "Connect 4";
                        board.ColorsSet(pieceColors, rand);
                    }
                    else if (menuRects[1].Contains(mouseState.X, mouseState.Y)){
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
                    else if (menuRects[2].Contains(mouseState.X, mouseState.Y))
                    {
                        this.Window.Title = "Checkers";
                        gameWon = false;
                        winner = 0;
                        playerTurn = 1;
                        screen = Screen.CheckersInstructions;
                        checkerBoard.ResetGame(circleTex, circleTex);
                    }
                    else if (menuRects[3].Contains(mouseState.X, mouseState.Y))
                    {
                        this.Window.Title = "Shogi";
                        gameWon = false;
                        winner = 0;
                        playerTurn = 1;
                        screen = Screen.ShogiInstructions;
                        shogiBoard.ResetGame(shogiPieceTextures);
                    }
                    else if (menuRects[4].Contains(mouseState.X, mouseState.Y))
                    {
                        this.Window.Title = "BuildingJumper";
                        gameWon = false;
                        winner = 0;
                        screen = Screen.BuildingJumperInstructions;
                        buildings.Clear();
                        buildings.Add(new Building(buildingTextures[0], _graphics));
                        score = 0;
                        rabbitJumper.Reset(_graphics);
                        if (File.Exists("highScore.txt"))
                        {
                            foreach (string line in File.ReadLines("highScore.txt"))
                            high_score = Convert.ToInt32(line);
                        }
                        else
                            high_score = 0;
                    }
                }
            }
            else if(screen == Screen.Shogi){
                if (!gameWon && mouseState.LeftButton == ButtonState.Pressed && prevMouseState.LeftButton == ButtonState.Released){
                    if (shogiBoard.MouseClicked(mouseState, playerTurn)){
                        if (shogiBoard.GameWon()){
                            gameWon = true;
                            winner = playerTurn;
                        }
                        if (playerTurn == 1)
                            playerTurn = 2;
                        else
                            playerTurn = 1;
                    }
                    if (shogiBoard.DoublePawn(playerTurn)){
                        gameWon = true;
                        winner = playerTurn;
                    }
                }
                else if (keyboardState.IsKeyDown(Keys.C))
                {
                    screen = Screen.Menu;
                    this.Window.Title = "Mini Arcade Menu";
                }
                else if(keyboardState.IsKeyDown(Keys.R))
                {
                    shogiBoard.ResetGame(shogiPieceTextures);
                    gameWon = false;
                    playerTurn = 1;
                    winner = 0;
                }
            }
            else if(screen == Screen.Checkers){
                if (!gameWon && mouseState.LeftButton == ButtonState.Pressed && prevMouseState.LeftButton == ButtonState.Released)
                {
                    this.Window.Title = $"p: {playerTurn} gw{gameWon} mclick{mouseState.LeftButton == ButtonState.Pressed}";
                    if (checkerBoard.MouseClicked(mouseState, playerTurn))
                    {
                        if (checkerBoard.GameWon(playerTurn))
                        {
                            gameWon = true;
                            winner = playerTurn;
                        }
                        if (playerTurn == 1)
                            playerTurn = 2;
                        else
                            playerTurn = 1;
                        if (!checkerBoard.CanMove(playerTurn))
                        {
                            if (playerTurn == 1)
                                playerTurn = 2;
                            else
                                playerTurn = 1;
                        }
                    }

                }
                else if (keyboardState.IsKeyDown(Keys.C))
                {
                    screen = Screen.Menu;
                    this.Window.Title = "Mini Arcade Menu";
                }
                else if(gameWon && keyboardState.IsKeyDown(Keys.R))
                {
                    checkerBoard.ResetGame(circleTex, circleTex);
                    gameWon = false;
                    playerTurn = 1;
                }
            }
            else if (screen == Screen.BuildingJumper){
                this.Window.Title = $"Rabbit Jumper, Buildings Jumped: {score}";
                if (keyboardState.IsKeyDown(Keys.C))
                    screen = Screen.Menu;
                else if (!gameWon){
                    rabbitJumper.Update(keyboardState, _graphics);
                    for (int i = 0; i < buildings.Count; i++)
                    {
                        buildings[i].Update();
                        if (buildings[i].Location().Intersects(rabbitJumper.Location())){
                            gameWon = true;
                            gameOverInstance.Play();
                            if (score > high_score)
                            {
                                highScore = score;
                                high_score = score;
                                StreamWriter writer = new StreamWriter("highScore.txt");
                                writer.WriteLine(score);
                                writer.Close();
                            }
                            else if( score > highScore)
                            {
                                highScore = score;
                            }
                        }
                        else if (buildings[i].Location().Right < 0){
                            buildings.Add(new Building(buildingTextures[rand.Next(0, 5)], _graphics, rand.Next(2,4), rand.Next(170,211)));
                            buildings.RemoveAt(i);
                            i--;
                            score++;
                        }
                    }
                }
                else if (keyboardState.IsKeyDown(Keys.R)){
                    gameWon = false;
                    score = 0;
                    rabbitJumper.Reset(_graphics);
                    buildings.Clear();
                    buildings.Add(new Building(buildingTextures[0], _graphics));
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
                else if (keyboardState.IsKeyDown(Keys.A)){
                    pacman.SetSize(10);
                    pacman.Reset();
                }
                else if (keyboardState.IsKeyDown(Keys.N)){
                    pacman.SetSize(45);
                    pacman.Reset();
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
                    else if (!gameWon && saveButton.Clicked(mouseState)){
                        StreamWriter writer = new StreamWriter("saved.txt");
                        board.SaveGame(writer,playerTurn);
                        writer.Close();
                    }
                    else if (!gameWon && loadButton.Clicked(mouseState)){
                        if (File.Exists("saved.txt")){
                            playerTurn = board.LoadGame("saved.txt", pieceColors);
                        }
                    }
                    else if (!gameWon && seconds > .8){
                        if (board.PlayerTurn(mouseState, playerTurn)){
                            if (playerTurn == 1)
                                 playerTurn = 2;
                            else if (playerTurn == 2)
                                playerTurn = 1;
                            //board.PlayerTurnAI(2);
                            undoC4Move = false;
                            startTime = (float)gameTime.TotalGameTime.TotalSeconds;
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
                else if (keyboardState.IsKeyDown(Keys.Z) && prevKeyboardState.IsKeyDown(Keys.Z) && !undoC4Move && !gameWon){
                    board.Undo();
                    if (playerTurn == 1)
                        playerTurn = 2;
                    else if (playerTurn == 2)
                        playerTurn = 1;
                    undoC4Move = true;
                }
                else if (keyboardState.IsKeyDown(Keys.C)){
                    screen = Screen.Menu;
                    this.Window.Title = "Mini Arcade Menu";
                    IsMouseVisible = true;
                }
                else if (keyboardState.IsKeyDown(Keys.R) && gameWon){
                    winner = 0;
                    playerTurn = 1;
                    gameWon = false;
                    board.Reset();
                    this.Window.Title = "Connect 4";
                    board.ColorsSet(pieceColors, rand);
                }
            }
            else if(screen == Screen.Connect4Help){
                if(mouseState.LeftButton == ButtonState.Pressed && prevMouseState.LeftButton == ButtonState.Released){
                    if (closeButton.Clicked(mouseState))
                    {
                        screen = Screen.Connect4;
                        this.Window.Title = "Connect 4";
                    }
                }
            }
            else if(screen == Screen.HunterWilsonEasterEgg)
            {
                if (temp)
                {
                    gameOverInstance.Play();
                    temp = false;
                }
                this.Window.Title = "Welcome To The Hunter Easter Egg, Press C to Close";
                if (keyboardState.IsKeyDown(Keys.C)){
                    screen = Screen.Menu;
                    this.Window.Title = "Mini Arcade Menu";
                    temp = true;
                }
            }
            else if (screen == Screen.CheckersInstructions)
            {
                if ((mouseState.LeftButton == ButtonState.Pressed && prevMouseState.LeftButton == ButtonState.Released) || keyboardState.IsKeyDown(Keys.Enter))
                {
                    screen = Screen.Checkers;
                }
                else if (keyboardState.IsKeyDown(Keys.C))
                {
                    screen = Screen.Menu;
                    this.Window.Title = "Mini Arcade Menu";
                }
            }
            else if (screen == Screen.ShogiInstructions)
            {
                if ((mouseState.LeftButton == ButtonState.Pressed && prevMouseState.LeftButton == ButtonState.Released) || keyboardState.IsKeyDown(Keys.Enter))
                {
                    screen = Screen.Shogi;
                }
                else if (keyboardState.IsKeyDown(Keys.C))
                {
                    screen = Screen.Menu;
                    this.Window.Title = "Mini Arcade Menu";
                }
            }
            else if (screen == Screen.BuildingJumperInstructions)
            {
                if ((mouseState.LeftButton == ButtonState.Pressed && prevMouseState.LeftButton == ButtonState.Released) || keyboardState.IsKeyDown(Keys.Enter))
                {
                    screen = Screen.BuildingJumper;
                }
                else if (keyboardState.IsKeyDown(Keys.C))
                {
                    screen = Screen.Menu;
                    this.Window.Title = "Mini Arcade Menu";
                }
            }
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            _spriteBatch.Begin();
            if (screen == Screen.Menu){
                GraphicsDevice.Clear(Color.Chartreuse);
                _spriteBatch.Draw(menuTex[5], new Rectangle(0, 0, 800, 700), Color.White);
                _spriteBatch.DrawString(font, "Welcome to the mini ", new Vector2(30, 20), Color.White);
                _spriteBatch.DrawString(font, "Arcade", new Vector2(280, 90), Color.White);
                for (int i = 0; i< menuRects.Count; i++)
                {
                    _spriteBatch.Draw(menuTex[i], menuRects[i], Color.White);
                }
            }
            else if(screen == Screen.Shogi)
            {
                GraphicsDevice.Clear(Color.DarkGray);
                shogiBoard.Draw(_spriteBatch);
                if (gameWon)
                {
                    if (winner == 1)
                    {
                        _spriteBatch.DrawString(mediumFont, "Player 1 Won", new Vector2(192, 7), Color.Gold);
                        _spriteBatch.DrawString(mediumFont, "Player 1 Won", new Vector2(195, 10), Color.Red);
                    }
                    else if (winner == 2)
                    {
                        _spriteBatch.DrawString(mediumFont, "Player 2 Won", new Vector2(192, 7), Color.Gold);
                        _spriteBatch.DrawString(mediumFont, "Player 2 Won", new Vector2(195, 10), Color.Black);
                    }
                }
                else
                {
                    if (playerTurn == 1)
                        _spriteBatch.DrawString(mediumFont, "Player 1's Turn", new Vector2(170, 10), Color.Red);
                    else if (playerTurn == 2)
                        _spriteBatch.DrawString(mediumFont, "Player 2's Turn", new Vector2(170, 10), Color.Black);
                }
            }
            else if(screen == Screen.Checkers)
            {
                GraphicsDevice.Clear(Color.DarkGray);
                checkerBoard.Draw(_spriteBatch);
                if (gameWon){
                    if (winner == 1){
                        _spriteBatch.DrawString(mediumFont, "Player 1 Won", new Vector2(222, 7), Color.Gold);
                        _spriteBatch.DrawString(mediumFont, "Player 1 Won", new Vector2(225, 10), Color.Red);
                    }
                    else if (winner == 2){
                        _spriteBatch.DrawString(mediumFont, "Player 2 Won", new Vector2(222, 7), Color.Gold);
                        _spriteBatch.DrawString(mediumFont, "Player 2 Won", new Vector2(225, 10), Color.Black);
                    }
                }
                else{
                    if (playerTurn == 1)
                        _spriteBatch.DrawString(mediumFont, "Player 1's Turn", new Vector2(200, 10), Color.Red);
                    else if (playerTurn == 2)
                        _spriteBatch.DrawString(mediumFont, "Player 2's Turn", new Vector2(200, 10), Color.Black);
                }
            }
            else if(screen == Screen.BuildingJumper)
            {
                GraphicsDevice.Clear(Color.Turquoise);
                _spriteBatch.DrawString(mediumFont, $"Buildings Jumped: {score}", new Vector2(0, 0), Color.Black);
                _spriteBatch.DrawString(mediumFont, $"High Score: {highScore}", new Vector2(0, 60), Color.Black);
                _spriteBatch.DrawString(mediumFont, $"All Time High Score: {high_score}", new Vector2(0, 120), Color.Black);
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
                _spriteBatch.DrawString(font, "Pacman Instructions", new Vector2(20, 20), Color.Black);
                _spriteBatch.DrawString(smallFont, "Use the Arrow keys to move Pacman around\nCollect all the coins to win\nYou lose if you touch a ghost\nLeft Click or press Enter to start the game\nAfter the game ends press R to restart or C to close\nGrab a power berry to get ghost eating powers for 5 seconds\nChoose the difficulty:\n1 for Hell\n2 for Normal (default) \n3 for Hacker Mode\n4 for Random Speeds\n'L' for exploration mode\n\nPress A for ant size\nPress N for normal size (default)", new Vector2(10, 120), Color.Black);
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
                        _spriteBatch.Draw(gamePiece, new Rectangle(mouseState.X - 40, mouseState.Y - 40, 75, 75), Color.White * 0.5f);
                        _spriteBatch.Draw(gamePiece, new Rectangle(mouseState.X - 37, mouseState.Y - 37, 75, 75), board.P1Color()* 0.7f);
                    }
                    else if (playerTurn == 2){
                        _spriteBatch.Draw(gamePiece, new Rectangle(mouseState.X - 40, mouseState.Y - 40, 75, 75), Color.White * 0.5f);
                        _spriteBatch.Draw(gamePiece, new Rectangle(mouseState.X - 37, mouseState.Y - 37, 75, 75), board.P2Color()*0.7f);
                    }
                }
                if (!gameWon){
                    saveButton.Draw(_spriteBatch);
                    loadButton.Draw(_spriteBatch);
                    if (playerTurn == 1)
                        _spriteBatch.DrawString(font, "Player 1's Turn", new Vector2(0, 10), board.P1Color());
                    else if (playerTurn == 2)
                        _spriteBatch.DrawString(font, "Player 2's Turn", new Vector2(0, 10), board.P2Color());
                }
                else{
                    if (winner == 1){
                        _spriteBatch.DrawString(font, "Player 1 Won", new Vector2(102, 7), Color.Gold);
                        _spriteBatch.DrawString(font, "Player 1 Won", new Vector2(105, 10), board.P1Color());
                    }
                    else if (winner == 2){
                        _spriteBatch.DrawString(font, "Player 2 Won", new Vector2(102, 7), Color.Gold);
                        _spriteBatch.DrawString(font, "Player 2 Won", new Vector2(105, 10), board.P2Color());
                    }
                    else if (winner == -1)
                        _spriteBatch.DrawString(font, "No one wins", new Vector2(130, 10), Color.Turquoise);
                }
            }
            //below code is for connect 4 help screen
            else if(screen == Screen.Connect4Help){
                GraphicsDevice.Clear(Color.White);
                closeButton.Draw(_spriteBatch);
                _spriteBatch.DrawString(font, "Rules", new Vector2(300, 20), Color.Black);
                _spriteBatch.DrawString(smallFont, "Left Click a column to drop a piece", new Vector2(10, 120), Color.Black);
                _spriteBatch.DrawString(smallFont, "Players must connect 4 of the same colored discs in a row to win.\nOnly one piece is played at a time.\nPlayers can be on the offensive or defensive.\r\nThe game ends when there is a 4-in-a-row or a stalemate.\r\nPress C or the close button to return to the menu\nPress R to restart the game when it ends", new Vector2(10, 150), Color.Black);
            }
            else if(screen == Screen.HunterWilsonEasterEgg)
            {
                GraphicsDevice.Clear(Color.Turquoise);
                _spriteBatch.Draw(easterEgg, new Rectangle(100,50,600,600), Color.White);
            }
            else if (screen == Screen.CheckersInstructions)
            {
                GraphicsDevice.Clear(Color.White);
                _spriteBatch.DrawString(font, "Checkers Instructions", new Vector2(0, 20), Color.Black);
                _spriteBatch.DrawString(smallFont, "Left Click or press Enter to start\nPieces can move diagonally\nClick a Piece to Move it\nPress C to Close\nPress R to restart after a game finishes\nCan ONLY single Jump", new Vector2(10, 130), Color.Black);
            }
            else if (screen == Screen.ShogiInstructions)
            {
                GraphicsDevice.Clear(Color.White);
                _spriteBatch.DrawString(font, "Shogi Instructions", new Vector2(20, 20), Color.Black);
                _spriteBatch.DrawString(smallFont, "Left Click or press Enter to start\nClick a Piece to Move it\nPress C to Close\nPress R to restart after a game finishes", new Vector2(10, 130), Color.Black);
            }
            else if (screen == Screen.BuildingJumperInstructions)
            {
                GraphicsDevice.Clear(Color.White);
                _spriteBatch.DrawString(font, "Jumper Instructions", new Vector2(20, 20), Color.Black);
                _spriteBatch.DrawString(smallFont, "Left Click or press Enter to start\nMove Left and Right with the arrow keys\nPress space or up to jump\nIf you hit a building you Lose\nPress C to Close", new Vector2(10, 130), Color.Black);
            }

            _spriteBatch.End();
            base.Draw(gameTime);
        }
        public string LetterTyped(KeyboardState keyboard, KeyboardState prevKeyboard)
        {
            string c = "";
            if (keyboard.IsKeyDown(Keys.A) && prevKeyboard.IsKeyUp(Keys.A))
                c += 'A';
            else if (keyboard.IsKeyDown(Keys.B) && prevKeyboard.IsKeyUp(Keys.B))
                c += 'B';
            else if (keyboard.IsKeyDown(Keys.C) && prevKeyboard.IsKeyUp(Keys.C))
                c += 'C';
            else if (keyboard.IsKeyDown(Keys.D) && prevKeyboard.IsKeyUp(Keys.D))
                c += 'D';
            else if (keyboard.IsKeyDown(Keys.E) && prevKeyboard.IsKeyUp(Keys.E))
                c += 'E';
            else if (keyboard.IsKeyDown(Keys.F) && prevKeyboard.IsKeyUp(Keys.F))
                c += 'F';
            else if (keyboard.IsKeyDown(Keys.G) && prevKeyboard.IsKeyUp(Keys.G))
                c += 'G';
            else if (keyboard.IsKeyDown(Keys.H) && prevKeyboard.IsKeyUp(Keys.H))
                c += 'H';
            else if (keyboard.IsKeyDown(Keys.I) && prevKeyboard.IsKeyUp(Keys.I))
                c += 'I';
            else if (keyboard.IsKeyDown(Keys.J) && prevKeyboard.IsKeyUp(Keys.J))
                c += 'J';
            else if (keyboard.IsKeyDown(Keys.K) && prevKeyboard.IsKeyUp(Keys.K))
                c += 'K';
            else if (keyboard.IsKeyDown(Keys.L) && prevKeyboard.IsKeyUp(Keys.L))
                c += 'L';
            else if (keyboard.IsKeyDown(Keys.M) && prevKeyboard.IsKeyUp(Keys.M))
                c += 'M';
            else if (keyboard.IsKeyDown(Keys.N) && prevKeyboard.IsKeyUp(Keys.N))
                c += 'N';
            else if (keyboard.IsKeyDown(Keys.O) && prevKeyboard.IsKeyUp(Keys.O))
                c += 'O';
            else if (keyboard.IsKeyDown(Keys.P) && prevKeyboard.IsKeyUp(Keys.P))
                c += 'P';
            else if (keyboard.IsKeyDown(Keys.Q) && prevKeyboard.IsKeyUp(Keys.Q))
                c += 'Q';
            else if (keyboard.IsKeyDown(Keys.R) && prevKeyboard.IsKeyUp(Keys.R))
                c += 'R';
            else if (keyboard.IsKeyDown(Keys.S) && prevKeyboard.IsKeyUp(Keys.S))
                c += 'S';
            else if (keyboard.IsKeyDown(Keys.T) && prevKeyboard.IsKeyUp(Keys.T))
                c += 'T';
            else if (keyboard.IsKeyDown(Keys.U) && prevKeyboard.IsKeyUp(Keys.U))
                c += 'U';
            else if (keyboard.IsKeyDown(Keys.V) && prevKeyboard.IsKeyUp(Keys.V))
                c += 'V';
            else if (keyboard.IsKeyDown(Keys.W) && prevKeyboard.IsKeyUp(Keys.W))
                c += 'W';
            else if (keyboard.IsKeyDown(Keys.X) && prevKeyboard.IsKeyUp(Keys.X))
                c += 'X';
            else if (keyboard.IsKeyDown(Keys.Y) && prevKeyboard.IsKeyUp(Keys.Y))
                c += 'Y';
            else if (keyboard.IsKeyDown(Keys.Z) && prevKeyboard.IsKeyUp(Keys.Z))
                c += 'Z';
            else if (keyboard.IsKeyDown(Keys.Delete) && prevKeyboard.IsKeyUp(Keys.Delete))
                c.Remove(c.Length - 1);
            return c;
        }
    }
}