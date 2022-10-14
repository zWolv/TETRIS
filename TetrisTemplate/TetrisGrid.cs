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

    bool l = true;
    /// The sprite of a single empty cell in the grid.
    Texture2D emptyCell;

    /// The position at which this TetrisGrid should be drawn.
    Vector2 position;

    /// The number of grid elements in the x-direction.
    public int Width { get { return 10; } }
   
    /// The number of grid elements in the y-direction.
    public int Height { get { return 20; } }

    BlockVariations blocks;
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

    bool[,] movementGrid = new bool[20, 10];
    Color[,] colorGrid = new Color[20, 10];


    public void Initialize()
    {
        blocks = new BlockVariations();

        blocks.addBlocks("S");
    }
    /// <summary>
    /// Updates the grid with new blocks
    /// </summary>
    public void drawBlocks()
    {
        if(l)
        {
            int x;
            int y;

            for (y = 0; y < 4; y++)
            {
                for (x = 0; x < 4; x++)
                {
                    movementGrid[y, x] = blocks.blockList[0].layout(x, y);
                }
            }
            l = false;
        }
       
    }



    public void HandleInput(InputHelper inputHelper)
    {
        bool canMove = true;
        if (inputHelper.KeyPressed(Microsoft.Xna.Framework.Input.Keys.Left))
        {
            for (int i = 0; i < 20; i++) {


                if (movementGrid[i, 0])
                {
                    canMove = false;
                }
                else if(canMove)
                {
                    for (int t = 0; t < 10; t++)
                    {
                        if (movementGrid[i, t])
                        {
                            movementGrid[i, t] = false;
                            movementGrid[i, t - 1] = true;
                        }
                    }
                }
                
            }
        }
        
        if(inputHelper.KeyPressed(Microsoft.Xna.Framework.Input.Keys.Right))
        {
            for (int i = 0; i < 20; i++)
            {
                if (movementGrid[i, 9]) 
                {
                    canMove = false;
                }

                if (canMove) 
                {
                    for (int t = 9; t >= 0; t--)
                    {
                        if (movementGrid[i, t])
                        {
                            movementGrid[i, t] = false;
                            movementGrid[i, t + 1] = true;
                        }
                    }
                }
               
            }
        }

    }

    public void Update(GameTime gameTime)
    {
        drawBlocks();
    }

    /// <summary>
    /// Draws the grid on the screen.
    /// </summary>
    /// <param name="gameTime">An object with information about the time that has passed in the game.</param>
    /// <param name="spriteBatch">The SpriteBatch used for drawing sprites and text.</param>
    public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        
        for (int i = 0; i < 20; i ++)
        {
            for(int t = 0; t < 10; t ++)
            {
                if(movementGrid[i, t] == true)
                {
                    spriteBatch.Draw(emptyCell, new Vector2((float)t * 30, (float)i * 30), Color.Red);
                }
                else
                {
                    spriteBatch.Draw(emptyCell, new Vector2((float)t * 30, (float)i * 30), Color.White);
                }
            }    
        }
    }

    /// <summary>
    /// Clears the grid.
    /// </summary>
    public void Clear()
    {
    }
}

