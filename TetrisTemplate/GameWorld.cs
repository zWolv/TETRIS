// onderdeel van de TetrisTemplate
// edits van Thomas van Egmond en Steijn Hoks
//           8471533              5002311

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;


/// <summary>
/// A class for representing the game world.
/// This contains the grid, the falling block, and everything else that the player can see/do.
/// </summary>
class GameWorld
{
    

    // de gamestates binnen de game
    enum GameStates
    {
        Menu,
        Playing,
        GameOver
    }

    // aantal constante waardes voor instelllingen
    const int blockVariations = 7;
    const float speedScale = 0.65f;
    const int defaultTimeBetweenDrops = 1000;
    const int timeUntilAddedToGrid = 2000;

    int timeBetweenDrops;
    
    //random-number generator
    public static Random Random { get { return random; } }
    static Random random;

    Vector2 futureBlockPosition = new Vector2(13, 5);
    Vector2 currentBlockPosition = new Vector2(4, 0);
    double previousGameTime = 0;

    private SpriteFont endText;

    // de huidige gamestate
    GameStates gameState;
    // de vorige gamestate
    GameStates previousGameState;

    // het grid van de game
    TetrisGrid grid;

    // huidig blok
    Block currentBlock;
    // volgend blok
    Block futureBlock;

    // gameinfo -- score/level
    GameInfo gameInfo;

    Menu menu;

    public GameWorld()
    {
        random = new Random();
        gameState = GameStates.Menu;
        previousGameState = GameStates.GameOver;
        // speel de achtergrondmuziek
        PlayMusic("backgroundMusic");
        // initialiseer spelfont
        endText = TetrisGame.ContentManager.Load<SpriteFont>("SpelFont");
    }

    // genereer een random block op basis van "blockType" op positie "blockPosition
    public Block GenerateRandomBlock(int blockType, Vector2 blockPosition)
    {
        switch (blockType)
        {
            case (0):
                return new L(blockPosition);
            case (1):
                return new J(blockPosition);
            case (2):
                return new O(blockPosition);
            case (3):
               return new I(blockPosition);
            case (4):
                return new S(blockPosition);
            case (5):
                return new Z(blockPosition);
            case (6):
                return new T(blockPosition);
            case (7):
                return new U(blockPosition);
            default:
                return null;
        }
    }

    // initialiseer de objecten die nodig zijn voor de huidige gamestate als de gamestate veranderd is
    public void Initialize()
    {
        if(gameState != previousGameState)
        {
            previousGameState = gameState;
            switch (gameState)
            {
                case GameStates.Playing:
                    gameInfo = new GameInfo();
                    grid = new TetrisGrid();
                    currentBlock = GenerateRandomBlock(random.Next(blockVariations + 1), currentBlockPosition);
                    futureBlock = GenerateRandomBlock(random.Next(blockVariations + 1), futureBlockPosition);
                    break;
                case GameStates.Menu:                   
                    menu = new Menu();
                    break;
                default:
                    break;
            }
        }
    }

    // speel achtergrondmuziek
    public void PlayMusic(string assetName, bool repeat = true)
    {
        MediaPlayer.IsRepeating = repeat;
        MediaPlayer.Play(TetrisGame.ContentManager.Load<Song>(assetName));
    }
    
    // speel soundeffect
    public void PlaySound(string assetName)
    {
        SoundEffect snd = TetrisGame.ContentManager.Load<SoundEffect>(assetName);
        snd.Play();
    }

    // bereken hoeveel sneller het blok moet vallen afhankelijk van het level
    public void levelSpeedup()
    {
        if ((int)gameInfo.getLevel * speedScale >= 1)
        {
            timeBetweenDrops = defaultTimeBetweenDrops / (int)(gameInfo.getLevel * speedScale);
        }
        else
        {
            timeBetweenDrops = defaultTimeBetweenDrops;
        }
    }

    // handel alle inputs af
    public void HandleInput(GameTime gameTime, InputHelper inputHelper)
    {

        switch(gameState)
        {
            case GameStates.Playing:
                // handel alle inputs tijdens het spelen af
                
                // roteer huidig blok naar rechts
                if (inputHelper.KeyPressed(Keys.D) && grid.CanRotateRight(currentBlock))
                {
                    currentBlock.RotateRight();
                }

                // roteer huidig blok naar links
                if (inputHelper.KeyPressed(Keys.A) && grid.CanRotateLeft(currentBlock))
                {
                    currentBlock.RotateLeft();
                }

                // beweeg huidig blok naar links
                if (inputHelper.KeyPressed(Keys.Left) && !grid.CheckCollision(currentBlock, new Vector2(currentBlock.getBlockPosition.X - 1, currentBlock.getBlockPosition.Y)))
                {
                    currentBlock.MoveLeft();
                }

                // beweeg huidig blok naar rechts
                if (inputHelper.KeyPressed(Keys.Right) && !grid.CheckCollision(currentBlock, new Vector2(currentBlock.getBlockPosition.X + 1, currentBlock.getBlockPosition.Y)))
                {

                    currentBlock.MoveRight();
                }

                // plaats blok zo ver als het naar onder kan in de huidige rotatie en x-positie
                if (inputHelper.KeyPressed(Keys.Space))
                {
                    while (!grid.CheckCollision(currentBlock, new Vector2(currentBlock.getBlockPosition.X, currentBlock.getBlockPosition.Y + 1)))
                    {
                        currentBlock.MoveDown();
                    }

                    grid.AddToGrid(currentBlock);
                    PlaySound("addToGridSound");
                    currentBlock = futureBlock;
                    currentBlock.MoveToStartPosition();
                    futureBlock = GenerateRandomBlock(random.Next(blockVariations + 1), futureBlockPosition);
                }

                break;
            case GameStates.Menu:
                // begin met spelen
                if (inputHelper.KeyPressed(Keys.Enter))
                {
                    gameState = GameStates.Playing;
                }

                break;
            case GameStates.GameOver:
                // begin opnieuw met spelen
                if (inputHelper.KeyPressed(Keys.Enter))
                {
                    gameState = GameStates.Playing;
                }
                break;
            default:
                break;
        }
    }


    //Check of er een bepaalde tijd "timeElapsed" voorbij is
    public bool checkIfTimeElapsed(GameTime gameTime,double timeElapsed, bool additionalCondition = true)
    {
        if (gameTime.TotalGameTime.TotalMilliseconds > previousGameTime + timeBetweenDrops && additionalCondition)
        {
            previousGameTime = gameTime.TotalGameTime.TotalMilliseconds;
            return true;
        }
        else
        {
            return false;
        }
    }

    public void Update(GameTime gameTime)
    {
        //Initializeer de objecten etc.
        Initialize();
        switch(gameState)
        {
            case GameStates.Playing:
                //afhandeling van het spelen -- zie omschrijving functies binnen classes Block en TetrisGrid en GameInfo
                levelSpeedup();
                //als er x tijd voorbij is, plaats huidig blok naar beneden
                if (checkIfTimeElapsed(gameTime, timeBetweenDrops, !grid.CheckCollision(currentBlock, new Vector2(currentBlock.getBlockPosition.X, currentBlock.getBlockPosition.Y + 1))))
                {
                    currentBlock.MoveDown();
                }

                //als blok niet meer naar beneden kan, zet het vast in het grid
                if (checkIfTimeElapsed(gameTime, timeUntilAddedToGrid, grid.CheckCollision(currentBlock, new Vector2(currentBlock.getBlockPosition.X, currentBlock.getBlockPosition.Y + 1))))
                {
                    grid.AddToGrid(currentBlock);
                    currentBlock = futureBlock;
                    currentBlock.MoveToStartPosition();
                    futureBlock = GenerateRandomBlock(random.Next(blockVariations + 1), futureBlockPosition);
                }

                grid.CheckRow(ref gameInfo.scoreRows);
                gameInfo.UpdateScore();
                gameInfo.UpdateLevel(grid.getLevelRows);
                if (grid.GameOverCollision(currentBlock))
                {
                   gameState = GameStates.GameOver;
                }
                break;
            case GameStates.GameOver:
                break;
            case GameStates.Menu:
                break;
            default:
                break;
        }
    }

    //  teken wat er getekend moet worden volgens de gamestate
    public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        switch(gameState)
        {
            case GameStates.Playing:
                spriteBatch.Begin();
                grid.Draw(gameTime, spriteBatch, currentBlock);
                futureBlock.Draw(spriteBatch, grid.getTexture);
                currentBlock.Draw(spriteBatch, grid.getTexture);
                gameInfo.Draw(spriteBatch);
                spriteBatch.End();
                break;
            case GameStates.GameOver:
                spriteBatch.Begin();
                // teken de gameovertekst naast het scherm, maar blijf het scherm en score tekenen
                spriteBatch.DrawString(endText, "Game over!", new Vector2(400f, 100f), Color.Black);
                spriteBatch.DrawString(endText, "Press Enter to play again", new Vector2(400f, 130f), Color.Black);
                grid.Draw(gameTime, spriteBatch, currentBlock);
                gameInfo.Draw(spriteBatch);
                spriteBatch.End();
                break;
            case GameStates.Menu:
                spriteBatch.Begin();
                menu.Draw(spriteBatch);
                spriteBatch.End();
                break;
            default:
                break;
        }
        
    }
}
