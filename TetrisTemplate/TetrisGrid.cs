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
    protected bool[,] movementGrid = new bool[20, 10];

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

    
    Color[,] colorGrid = new Color[20, 10];

    /// <summary>
    /// Updates the grid with new blocks
    /// </summary>




    public void Initialize()
    {
        blocks = new Blocks();
        blocks.addBlocks("I");
    }

    public void HandleInput(InputHelper inputHelper)
    {
        blocks.HandleInput(inputHelper);
    }

    public void Update(GameTime gameTime)
    {
        //drawBlocks();
        blocks.Update();
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

                if (movementGrid[i, t])
                {
                    spriteBatch.Draw(emptyCell, new Vector2((float)t * 30, (float)i * 30), Color.Red);
                }
                else
                {
                    spriteBatch.Draw(emptyCell, new Vector2((float)t * 30, (float)i * 30), Color.White);
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

