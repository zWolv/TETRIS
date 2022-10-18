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
    bool blockPushed;
    bool pushBlockManual;


    const int yLength = 0;
    const int xLength = 1;
    const int cellSize = 30;
    const int blockVariations = 8;
    const int timeBetweenDrop = 1000;
    const int timeUntilPush = 2000;

    double previousDropTime = 0;
    double previousPushTime;

    /// The sprite of a single empty cell in the grid.
    Texture2D emptyCell;

    /// The position at which this TetrisGrid should be drawn.
    Vector2 position;

    /// The number of grid elements in the x-direction.
    public int Width { get { return 10; } }
   
    /// The number of grid elements in the y-direction.
    public int Height { get { return 20; } }

    Blocks block;
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

    public void DropBlock(GameTime gameTime)
    {
        if (gameTime.TotalGameTime.TotalMilliseconds > previousDropTime + timeBetweenDrop && canMoveDown)
        {
            previousDropTime = gameTime.TotalGameTime.TotalMilliseconds;
            block.blockPosition.Y += 1;
        }
    }

    public void PushBlock(TetrisGrid grid, GameTime gameTime)
    {
        GetCurrentPushTime(gameTime);
        if ((!canMoveDown && gameTime.TotalGameTime.TotalMilliseconds > previousPushTime + timeUntilPush) || pushBlockManual)
        {
            pushBlockManual = false;
            for (int y = 0; y < block.block.layout.GetLength(yLength); y++)
            {
                for (int x = 0; x < block.block.layout.GetLength(xLength); x++)
                {
                    if (block.block.layout[y, x])
                    {
                        grid.collisionGrid[(int)blockPosition.Y + y, (int)blockPosition.X + x] = true;
                        grid.colorGrid[(int)blockPosition.Y + y, (int)blockPosition.X + x] = currentBlock.blockColor;
                    }
                }
            }
            blockPushed = true;
            previousCanMoveDown = true;
            previousPushTime = gameTime.TotalGameTime.TotalMilliseconds;
            ResetPosition();
        }

    }

    public void ResetPosition()
    {
        blockPosition = new Vector2(4, 0);
    }

    //WORK IN PROGRESS
    public void CanMoveDown(TetrisGrid grid, GameTime gameTime)
    {

        for (int blockY = 0; blockY < blockArraySize; blockY++)
        {
            for (int blockX = 0; blockX < blockArraySize; blockX++)
            {
                int nextBlock = (int)blockPosition.Y + blockY + 1;
                if ((currentBlock.layout[blockY, blockX] && nextBlock <= 19 && grid.collisionGrid[nextBlock, blockX + (int)blockPosition.X]) || (currentBlock.layout[blockY, blockX] && blockPosition.Y + blockY >= grid.Height - 1))
                {
                    canMoveDown = false;
                    goto loopEnd;
                }
                else
                {
                    canMoveDown = true;
                }
            }
        }
    loopEnd:;
    }

    public void GetCurrentPushTime(GameTime gameTime)
    {
        if (!canMoveDown && previousCanMoveDown != canMoveDown || blockPosition.Y != previousBlockPositionY)
        {
            previousTimePush = gameTime.TotalGameTime.TotalMilliseconds;
            previousCanMoveDown = canMoveDown;
            previousBlockPositionY = (int)blockPosition.Y;
        }
    }

    public void CanMoveRightLeft(TetrisGrid grid)
    {
        canMoveLeft = true;
        canMoveRight = true;
        for (int x = 0; x < blockArraySize; x++)
        {
            int blockRight = (int)blockPosition.X + x + 1;
            int blockLeft = (int)blockPosition.X + x - 1;
            for (int y = 0; y < blockArraySize; y++)
            {

                if (!currentBlock.layout[y, x])
                {
                    continue;
                }

                if (!(blockPosition.X + x > 0))
                {
                    canMoveLeft = false;
                }

                if (!(blockPosition.X + x < 9))
                {
                    canMoveRight = false;
                }

                if (canMoveLeft && grid.collisionGrid[y + (int)blockPosition.Y, blockLeft])
                {
                    canMoveLeft = false;
                }

                if (canMoveRight && grid.collisionGrid[y + (int)blockPosition.Y, blockRight])
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
        block = new Blocks();
    }
    public void HandleInput(InputHelper inputHelper)
    {
        block.HandleInput(inputHelper);

        if (inputHelper.KeyPressed(Microsoft.Xna.Framework.Input.Keys.Left) && canMoveLeft)
        {
            blockPosition.X -= 1;
        }

        if (inputHelper.KeyPressed(Microsoft.Xna.Framework.Input.Keys.Right) && canMoveRight)
        {
            blockPosition.X += 1;
        }


        // 2e condition moet nog
        if (inputHelper.KeyPressed(Microsoft.Xna.Framework.Input.Keys.Space))
        {
        }

        if (inputHelper.KeyPressed(Microsoft.Xna.Framework.Input.Keys.Down))
        {
            blockPosition.Y += 1;
        }
    }

    public void Update(GameTime gameTime, TetrisGrid grid)
    {
        block.addBlocks(block.Random.Next(0, blockVariations - 1));
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
        block.Draw(spriteBatch, emptyCell);
    }

    /// <summary>
    /// Clears the grid.
    /// </summary>
    public void Clear()
    {
    }
}

