using Microsoft.VisualBasic.FileIO;
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
    bool previousCanMoveDown;
    bool blockPushed = true;
    bool pushBlockManual;


    const int yLength = 0;
    const int xLength = 1;
    const int cellSize = 30;
    const int blockVariations = 8;
    const int timeUntilPush = 2000;
    const int timeBetweenDrop = 1000;

    double previousPushTime;
    double previousDropTime;

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
        if (!CanMoveDown() && previousCanMoveDown != CanMoveDown() || blockY != previousBlockY)
        {
            previousPushTime = gameTime.TotalGameTime.TotalMilliseconds;
            previousCanMoveDown = CanMoveDown();
            previousBlockY = blockY;
        }

        if ((!CanMoveDown() && gameTime.TotalGameTime.TotalMilliseconds > previousPushTime + timeUntilPush) || pushBlockManual)
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
    public bool CanMoveDown()
    {
        bool canMoveDown = true;
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
        return canMoveDown;
    }

    public void HandleInput(InputHelper inputHelper)
    {
        if(inputHelper.KeyPressed(Microsoft.Xna.Framework.Input.Keys.D) && CanRotateRight()) 
        {
                movingBlock.RotateRight();
        }

        if(inputHelper.KeyPressed(Microsoft.Xna.Framework.Input.Keys.A) && CanRotateLeft())
        {
            movingBlock.RotateLeft();
        }

        if(inputHelper.KeyPressed(Microsoft.Xna.Framework.Input.Keys.Left) && CanMoveLeft())
        {
            movingBlock.MoveLeft();
        }

        if(inputHelper.KeyPressed(Microsoft.Xna.Framework.Input.Keys.Right) && CanMoveRight())
        {
            movingBlock.MoveRight();
        }

        //temporary
        if (inputHelper.KeyPressed(Microsoft.Xna.Framework.Input.Keys.Down) && CanMoveDown())
        {
            movingBlock.MoveDown();
        }
    }

    public bool CanRotateRight()
    {
        bool canRotateRight = true;
        movingBlock.RotateRight();
        if (!RotateCollisionCheck(canRotateRight))
        {
            canRotateRight = false;
        }
        movingBlock.RotateLeft();
        return canRotateRight;
    }


    public bool CanRotateLeft()
    {
        bool canRotateLeft = true;
        movingBlock.RotateLeft();
        if(!RotateCollisionCheck(canRotateLeft))
        {
            canRotateLeft = false;
        }
        movingBlock.RotateRight();
        return canRotateLeft;
    }


    public bool RotateCollisionCheck(bool canRotate)
    {
        for (int x = 0; x < movingBlock.layout.GetLength(xLength); x++)
        {
            for (int y = 0; y < movingBlock.layout.GetLength(yLength); y++)
            {
                if (!movingBlock.layout[y, x])
                {
                    continue;
                }

                if (blockX + x < 0 || blockY + y > Height - 1 || blockX + x > Width - 1 || blockY + y < 0 || collisionGrid[blockY + y, blockX + x])
                {
                    canRotate = false;
                }
            }
        }
        return canRotate;
    }

    public void GetBlockPosition()
    {
        blockX = (int)movingBlock.getBlockPosition.X;
        blockY = (int)movingBlock.getBlockPosition.Y;
    }

    public bool CanMoveRight()
    {
        bool canMoveRight = true;
        for (int x = 0; x < movingBlock.layout.GetLength(xLength); x++)
        {
            int blockRight = blockX + x + 1;
            for (int y = 0; y < movingBlock.layout.GetLength(yLength); y++)
            {

                if (!movingBlock.layout[y, x])
                {
                    continue;
                }

                if (!(blockX + x < Width - 1))
                {
                    canMoveRight = false;
                    goto loopEnd;
                }

                if (canMoveRight && collisionGrid[y + blockY, blockRight])
                {
                    canMoveRight = false;
                    goto loopEnd;
                }
            }
        }
    loopEnd:;
        return canMoveRight;
    }

    public bool CanMoveLeft()
    {
        bool canMoveLeft = true;
        for (int x = 0; x < movingBlock.layout.GetLength(xLength); x++)
        {
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
                    goto loopEnd;
                }

                if (canMoveLeft && collisionGrid[y + blockY, blockLeft])
                {
                    canMoveLeft = false;
                    goto loopEnd;
                }
            }
        }
        loopEnd:;
        return canMoveLeft;
    }

    public void DropBlock(GameTime gameTime)
    {
        if (gameTime.TotalGameTime.TotalMilliseconds > previousDropTime + timeBetweenDrop && CanMoveDown())
        {
            previousDropTime = gameTime.TotalGameTime.TotalMilliseconds;
            movingBlock.MoveDown();
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

