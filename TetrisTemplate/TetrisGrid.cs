using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

/// <summary>
/// A class for representing the Tetris playing grid.
/// </summary>
class TetrisGrid
{
    public bool[,] collisionGrid = new bool[20, 10];
    public Color[,] colorGrid = new Color[20, 10];
    const int cellSize = 30;
    const int blockVariations = 8;
    

    /// The sprite of a single empty cell in the grid.
    Texture2D emptyCell;

    /// The position at which this TetrisGrid should be drawn.
    Vector2 position;

    /// The number of grid elements in the x-direction.
    public int Width { get { return 10; } }
   
    /// The number of grid elements in the y-direction.
    public int Height { get { return 20; } }

    Blocks blocks;
    /// <summary>
    /// Creates a new TetrisGrid.
    /// </summary>
    /// <param name="b"></param>
    public TetrisGrid()
    {
        emptyCell = TetrisGame.ContentManager.Load<Texture2D>("block");
        position = Vector2.Zero;
        Clear();
    }

    /// <summary>
    /// Updates the grid with new blocks
    /// </summary>
    public void LoadContent()
    {
        blocks = new Blocks();
    }
    public void HandleInput(InputHelper inputHelper)
    {
        blocks.HandleInput(inputHelper);
    }

    public void Update(GameTime gameTime, TetrisGrid grid)
    {
        blocks.addBlocks(blocks.Random.Next(0, blockVariations - 1));
        blocks.Update(gameTime, grid);
    }

    /// <summary>
    /// Draws the grid on the screen.
    /// </summary>
    /// <param name="gameTime">An object with information about the time that has passed in the game.</param>
    /// <param name="spriteBatch">The SpriteBatch used for drawing sprites and text.</param>
    public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        

        for (int i = 0; i < Height; i++)
        {
            for (int t = 0; t < Width; t++)
            {
                if (!collisionGrid[i, t])
                {
                    spriteBatch.Draw(emptyCell, new Vector2((float)t * cellSize, (float)i * cellSize), Color.White);
                }
                else if(collisionGrid[i, t])
                {
                    spriteBatch.Draw(emptyCell, new Vector2((float)t * cellSize, (float)i * cellSize), blocks.thisBlock.blockColor);
                }
            }
        }
        blocks.Draw(spriteBatch, emptyCell);
    }

    /// <summary>
    /// Clears the grid.
    /// </summary>
    public void Clear()
    {
    }
}

