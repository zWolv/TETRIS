using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

/// <summary>
/// A class for representing the game world.
/// This contains the grid, the falling block, and everything else that the player can see/do.
/// </summary>
class GameWorld
{
    /// <summary>
    /// An enum for the different game states that the game can have.
    /// </summary>
    enum GameState
    {
        Playing,
        GameOver
    }

    /// <summary>
    /// The random-number generator of the game.
    /// </summary>
    public static Random Random { get { return random; } }
    static Random random;

    /// <summary>
    /// The main font of the game.
    /// </summary>
    SpriteFont font;

    /// <summary>
    /// The current game state.
    /// </summary>
    GameState gameState;

    /// <summary>
    /// The main grid of the game.
    /// </summary>
    TetrisGrid grid;

    //block
    Blocks block;

    public GameWorld()
    {
        random = new Random();
        gameState = GameState.Playing;

        font = TetrisGame.ContentManager.Load<SpriteFont>("SpelFont");

        grid = new TetrisGrid();
        block = new Blocks();
        block = block.CreateBlock(random.Next(8));
    }

    public void HandleInput(GameTime gameTime, InputHelper inputHelper)
    {
        if (inputHelper.KeyPressed(Microsoft.Xna.Framework.Input.Keys.D) && grid.CanRotateRight(block))
        {
            block.RotateRight();
        }

        if (inputHelper.KeyPressed(Microsoft.Xna.Framework.Input.Keys.A) && grid.CanRotateLeft(block))
        {
            block.RotateLeft();
        }

        if (inputHelper.KeyPressed(Microsoft.Xna.Framework.Input.Keys.Left) && grid.CanMoveLeft(block))
        {
            block.MoveLeft();
        }

        if (inputHelper.KeyPressed(Microsoft.Xna.Framework.Input.Keys.Right) && grid.CanMoveRight(block))
        {

            block.MoveRight();
        }

        if (inputHelper.KeyPressed(Microsoft.Xna.Framework.Input.Keys.Space))
        {
            while (grid.CanMoveDown(block))
            { 
                block.MoveDown();
            }

            grid.addToGrid(block);
        }

        //temporary
        if (inputHelper.KeyPressed(Microsoft.Xna.Framework.Input.Keys.Down) && grid.CanMoveDown(block))
        {
            block.MoveDown();
        }
    }

    public void Update(GameTime gameTime)
    {

    }

    public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        spriteBatch.Begin();
        grid.Draw(gameTime, spriteBatch, block);
        spriteBatch.End();
    }

    public void Reset()
    {
    }

}
