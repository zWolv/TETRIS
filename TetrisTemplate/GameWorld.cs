using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Security.Cryptography;
using TetrisTemplate;

/// <summary>
/// A class for representing the game world.
/// This contains the grid, the falling block, and everything else that the player can see/do.
/// </summary>
class GameWorld
{
    const int blockVariations = 7;
    /// <summary>
    /// An enum for the different game states that the game can have.
    /// </summary>
    enum GameStates
    {
        Menu,
        Playing,
        GameOver
    }

    const float speedScale = 0.65f;
    const int defaultTimeBetweenDrops = 1000;
    int timeBetweenDrops;
    const int timeUntilAddedToGrid = 2000;
    /// <summary>
    /// The random-number generator of the game.
    /// </summary>
    public static Random Random { get { return random; } }
    static Random random;

    Vector2 futureBlockPosition = new Vector2(13, 5);
    Vector2 currentBlockPosition = new Vector2(4, 0);
    double previousGameTime = 0;

    /// <summary>
    /// The current game state.
    /// </summary>
    GameStates gameState;
    GameStates previousGameState;

    /// <summary>
    /// The main grid of the game.
    /// </summary>
    TetrisGrid grid;

    //blocks
    Block currentBlock;
    Block futureBlock;

    //Gameinfo
    GameInfo gameInfo;

    Menu menu;

    public GameWorld()
    {
        random = new Random();
        gameState = GameStates.Playing;
    }

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
                case GameStates.GameOver:
                    // maak gameoverscherm
                    break;
                default:
                    break;
            }
        }
    }

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

    public void HandleInput(GameTime gameTime, InputHelper inputHelper)
    {

        switch(gameState)
        {
            case GameStates.Playing:
                if (inputHelper.KeyPressed(Keys.D) && grid.CanRotateRight(currentBlock))
                {
                    currentBlock.RotateRight();
                }

                if (inputHelper.KeyPressed(Keys.A) && grid.CanRotateLeft(currentBlock))
                {
                    currentBlock.RotateLeft();
                }

                if (inputHelper.KeyPressed(Keys.Left) && !grid.CheckCollision(currentBlock, new Vector2(currentBlock.getBlockPosition.X - 1, currentBlock.getBlockPosition.Y)))
                {
                    currentBlock.MoveLeft();
                }

                if (inputHelper.KeyPressed(Keys.Right) && !grid.CheckCollision(currentBlock, new Vector2(currentBlock.getBlockPosition.X + 1, currentBlock.getBlockPosition.Y)))
                {

                    currentBlock.MoveRight();
                }

                if (inputHelper.KeyPressed(Keys.Space))
                {
                    while (!grid.CheckCollision(currentBlock, new Vector2(currentBlock.getBlockPosition.X, currentBlock.getBlockPosition.Y + 1)))
                    {
                        currentBlock.MoveDown();
                    }

                    grid.AddToGrid(currentBlock);
                    currentBlock = futureBlock;
                    currentBlock.MoveToStartPosition();
                    futureBlock = GenerateRandomBlock(random.Next(blockVariations + 1), futureBlockPosition);
                }

                //temporary
                if (inputHelper.KeyPressed(Keys.Down) && !grid.CheckCollision(currentBlock, new Vector2(currentBlock.getBlockPosition.X, currentBlock.getBlockPosition.Y + 1)))
                {
                    currentBlock.MoveDown();
                }

                break;
            case GameStates.Menu:
                // menu knoppen etc
                if (inputHelper.KeyPressed(Keys.Enter))
                {
                    gameState = GameStates.Playing;
                }

                break;
            case GameStates.GameOver:
                // terug naar menu knop
                break;
            default:
                break;
        }
    }

    //naamgeving parameters ??
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

        switch(gameState)
        {
            case GameStates.Playing:
                levelSpeedup();
                if (checkIfTimeElapsed(gameTime, timeBetweenDrops, !grid.CheckCollision(currentBlock, new Vector2(currentBlock.getBlockPosition.X, currentBlock.getBlockPosition.Y + 1))))
                {
                    currentBlock.MoveDown();
                }

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
                break;
            case GameStates.Menu:
                menu.Draw(spriteBatch);
                break;
            default:
                break;
        }
        
    }
}
