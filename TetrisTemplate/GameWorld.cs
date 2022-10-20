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


    double previousGameTime = 0;

    /// <summary>
    /// The main font of the game.
    /// </summary>
    SpriteFont font;

    /// <summary>
    /// The current game state.
    /// </summary>
    GameStates gameState;

    /// <summary>
    /// The main grid of the game.
    /// </summary>
    TetrisGrid grid;

    //block
    Blocks block;

    //Gameinfo
    GameInfo gameInfo;

    Menu menu;

    public GameWorld()
    {
        random = new Random();
        gameState = GameStates.Menu;

        font = TetrisGame.ContentManager.Load<SpriteFont>("SpelFont");

        grid = new TetrisGrid();
        block = new Blocks();
        block = block.CreateBlock(random.Next(blockVariations + 1));
        gameInfo = new GameInfo();
        menu = new Menu();
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
                if (inputHelper.KeyPressed(Keys.D) && grid.CanRotateRight(block))
                {
                    block.RotateRight();
                }

                if (inputHelper.KeyPressed(Keys.A) && grid.CanRotateLeft(block))
                {
                    block.RotateLeft();
                }

                if (inputHelper.KeyPressed(Keys.Left) && grid.CanMoveLeft(block))
                {
                    block.MoveLeft();
                }

                if (inputHelper.KeyPressed(Keys.Right) && grid.CanMoveRight(block))
                {

                    block.MoveRight();
                }

                if (inputHelper.KeyPressed(Keys.Space))
                {
                    while (grid.CanMoveDown(block))
                    {
                        block.MoveDown();
                    }

                    grid.AddToGrid(block);
                    block = block.CreateBlock(random.Next(blockVariations + 1));
                }

                //temporary
                if (inputHelper.KeyPressed(Keys.Down) && grid.CanMoveDown(block))
                {
                    block.MoveDown();
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
                if (checkIfTimeElapsed(gameTime, timeBetweenDrops, grid.CanMoveDown(block)))
                {
                    block.MoveDown();
                }

                if (checkIfTimeElapsed(gameTime, timeUntilAddedToGrid, !grid.CanMoveDown(block)))
                {
                    grid.AddToGrid(block);
                    block = block.CreateBlock(random.Next(blockVariations + 1));
                }

                grid.CheckRow(ref gameInfo.scoreRows);
                gameInfo.UpdateScore();
                gameInfo.UpdateLevel(grid.getLevelRows);
                if (grid.GameOverCollision(block))
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
                grid.Draw(gameTime, spriteBatch, block);
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


    // WORK IN PROGRESS
    public void Reset()
    {
        grid.Clear();
    }

}
