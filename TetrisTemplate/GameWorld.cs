using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Diagnostics;
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
                    currentBlock = new Block(currentBlockPosition, random.Next(blockVariations + 1));
                    futureBlock = new Block(futureBlockPosition, random.Next(blockVariations + 1));
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
        if (gameInfo.getLevel != 1)
        {
            timeBetweenDrops = defaultTimeBetweenDrops / (int)(gameInfo.getLevel * 0.65);
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
                    futureBlock = new Block(futureBlockPosition, random.Next(blockVariations + 1));
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
                    futureBlock = new Block(futureBlockPosition, random.Next(blockVariations + 1));
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
                break;
            default:
                break;
        }
        
    }
}
