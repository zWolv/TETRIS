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

    bool canMoveLeft;
    bool canMoveRight;
    bool canMoveDown;
    bool previousCanMoveDown;
    bool blockPushed = true;
    bool pushBlockManual;


    const int yLength = 0;
    const int xLength = 1;
    const int cellSize = 30;
    const int blockVariations = 8;
    const int timeUntilPush = 2000;

    double previousPushTime;

    int blockY;
    int blockX;
    int previousBlockY;

    /// The sprite of a single empty cell in the grid.
    Texture2D emptyCell;

    /// The position at which this TetrisGrid should be drawn.
    Vector2 position;

    /// The number of grid elements in the x-direction.
    public int Width { get { return 10; } }
   
    /// The number of grid elements in the y-direction.
    public int Height { get { return 20; } }

    Blocks blockMaster;
    Blocks movingBlock;
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

    

    public void PushBlock(GameTime gameTime)
    {
        if (!canMoveDown && previousCanMoveDown != canMoveDown || blockY != previousBlockY)
        {
            previousPushTime = gameTime.TotalGameTime.TotalMilliseconds;
            previousCanMoveDown = canMoveDown;
            previousBlockY = blockY;
        }

        if ((!canMoveDown && gameTime.TotalGameTime.TotalMilliseconds > previousPushTime + timeUntilPush) || pushBlockManual)
        {
            pushBlockManual = false;
            for (int y = 0; y < movingBlock.layout.GetLength(yLength); y++)
            {
                for (int x = 0; x < movingBlock.layout.GetLength(xLength); x++)
                {
                    if (movingBlock.layout[y, x])
                    {
                        collisionGrid[blockY + y, blockX + x] = true;
                        colorGrid[blockY + y, blockX + x] = movingBlock.blockColor;
                    }
                }
            }
            blockPushed = true;
            previousCanMoveDown = true;
            previousPushTime = gameTime.TotalGameTime.TotalMilliseconds;
            movingBlock.ResetPosition();
        }

    }

    //WORK IN PROGRESS
    public void CanMoveDown()
    {
        canMoveDown = true;
        for (int y = 0; y < movingBlock.layout.GetLength(yLength); y++)
        {
            int nextBlock = y + blockY + 1;
            for (int x = 0; x < movingBlock.layout.GetLength(xLength); x++)
            {
                if (!(movingBlock.layout[y,x]))
                {
                    continue;
                }

                if(blockY + y >= Height - 1)
                {
                    canMoveDown = false;
                    goto loopEnd;
                }

                if (nextBlock < Height && collisionGrid[nextBlock, x + blockX])
                {
                    canMoveDown = false;
                    goto loopEnd;
                }
            }
        }
    loopEnd:;
    }

    public void HandleInput(InputHelper inputHelper)
    {
        if(movingBlock != null)
        {
            movingBlock.HandleInput(inputHelper);
        }
        
    }

    public void CanRotateRight()
    {
        movingBlock.RotateRight();
        for(int x = 0; x < movingBlock.layout.GetLength(xLength); x++)
        {
            for (int y = 0; y < movingBlock.layout.GetLength(yLength); y++)
            {
                if(!movingBlock.layout[y,x])
                {
                    continue;
                }


            }
        }
    }

    public void GetBlockPosition()
    {
        blockX = (int)movingBlock.getBlockPosition.X;
        blockY = (int)movingBlock.getBlockPosition.Y;
    }

    public void CanMoveRightLeft()
    {
        canMoveLeft = true;
        canMoveRight = true;
        for (int x = 0; x < movingBlock.layout.GetLength(xLength); x++)
        {
            int blockRight = blockX + x + 1;
            int blockLeft = blockX + x - 1;
            for (int y = 0; y < movingBlock.layout.GetLength(yLength); y++)
            {

                if (!movingBlock.layout[y, x])
                {
                    continue;
                }

                if (!(blockX + x > 0))
                {
                    canMoveLeft = false;
                }

                if (!(blockX + x < Width - 1))
                {
                    canMoveRight = false;
                }

                if (canMoveLeft && collisionGrid[y + blockY, blockLeft])
                {
                    canMoveLeft = false;
                }

                if (canMoveRight && collisionGrid[y + blockY, blockRight])
                {
                    canMoveRight = false;
                }
            }
        }
    }

    /// <summary>
    /// Updates the grid with new blocks
    /// </summary>
    public void LoadContent()
    {
        blockMaster = new Blocks();
    }

    public void AddMovingBlock()
    {
        if(blockPushed)
        {
            movingBlock = blockMaster.AddBlocks(blockMaster.Random.Next(0, blockVariations - 1));
            blockPushed = false;
        }
    }


    public void Update(GameTime gameTime, TetrisGrid grid)
    {
        AddMovingBlock();
        GetBlockPosition();
        CanMoveDown();
        CanMoveRightLeft();
        movingBlock.GiveMovementPossibilities(canMoveDown, canMoveLeft, canMoveRight);
        movingBlock.DropBlock(gameTime);
        PushBlock(gameTime);

        
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
                    spriteBatch.Draw(emptyCell, new Vector2((float)t * cellSize, (float)i * cellSize), colorGrid[i,t]);
                }
            }
        }
        movingBlock.Draw(spriteBatch, emptyCell);
    }

    /// <summary>
    /// Clears the grid.
    /// </summary>
    public void Clear()
    {
    }
}

