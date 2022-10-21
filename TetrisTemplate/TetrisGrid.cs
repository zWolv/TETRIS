using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

/// <summary>
/// A class for representing the Tetris playing grid.
/// </summary>
class TetrisGrid
{
    public bool[,] collisionGrid = new bool[20, 10];
    public Color[,] colorGrid = new Color[20, 10];

    const int yLength = 0;
    const int xLength = 1;
    const int cellSize = 30;

    int rowCounterForLevel = 0;

    /// The sprite of a single empty cell in the grid.
    Texture2D emptyCell;

    /// The position at which this TetrisGrid should be drawn.
    Vector2 position;

    /// The number of grid elements in the x-direction.
    public int Width { get { return 10; } }

    /// The number of grid elements in the y-direction.
    public int Height { get { return 20; } }
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
    public void AddToGrid(Block block)
    {
            for (int y = 0; y < block.layout.GetLength(yLength); y++)
            {
                for (int x = 0; x < block.layout.GetLength(xLength); x++)
                {
                    if (block.layout[y, x])
                    {
                        collisionGrid[(int)block.getBlockPosition.Y + y, (int)block.getBlockPosition.X + x] = true;
                        colorGrid[(int)block.getBlockPosition.Y + y, (int)block.getBlockPosition.X + x] = block.blockColor;
                    }
                }
            }
            block.MoveToStartPosition();
    }

    public bool CheckCollision(Block block, Vector2 newPosition)
    {
        for (int y = 0; y < block.layout.GetLength(yLength); y++)
        {
            for (int x = 0; x < block.layout.GetLength(xLength); x++)
            {
                int gridBlockPixelPositionX = (int)newPosition.X + x;
                int gridBlockPixelPositionY = (int)newPosition.Y + y;
                if(block.layout[y, x])
                {
                    if (gridBlockPixelPositionX < Width 
                        && gridBlockPixelPositionX >= 0 
                        && gridBlockPixelPositionY >= 0 
                        && gridBlockPixelPositionY < Height)
                    {                   
                        if(collisionGrid[gridBlockPixelPositionY, gridBlockPixelPositionX])
                        {
                            return true;
                        }    
                    }
                    else
                    {
                        return true;
                    }
                }
            }
        }
        return false;
    }
    public void CheckRow(ref int scoreRows)
    {
        int counter = 0;
        int rowCounterForScore = 0;
        for (int y = 0; y < Height; y++)
        {
            counter = 0;
            for (int x = 0; x < Width; x++)
            {
                if (!collisionGrid[y, x])
                {
                    break;
                }
                else
                {
                    counter++;
                }
            }
            if (counter == Width)
            {
                LowerGrid(y);
                rowCounterForScore++;
                rowCounterForLevel++;
            }
        }
        scoreRows = rowCounterForScore;
    }

    public Texture2D getTexture
    {
        get
        {
            return emptyCell;
        }
    }
    public int getLevelRows
    {
        get
        {
            return rowCounterForLevel;
        }
    }
    public void LowerGrid(int rowRemoved)
    {
        for(int y = rowRemoved; y > 0 ; y--)
        {
            for(int x = 0; x < Width; x++)
            {
                    collisionGrid[y, x] = collisionGrid[y - 1, x];
                    colorGrid[y, x] = colorGrid[y - 1, x];
            }
        }
    }

    public bool CanRotateRight(Block block)
    {
        bool canRotateRight = true;
        block.RotateRight();
        if (!RotateCollisionCheck(canRotateRight, block))
        {
            canRotateRight = false;
        }
        block.RotateLeft();
        return canRotateRight;
    }

    public bool GameOverCollision(Block block)
    {
        for(int x = 0; x < Width; x++)
        {
            if (collisionGrid[0,x] && CheckCollision(block, new Vector2(block.getBlockPosition.X, block.getBlockPosition.Y + 1)))
            {
                return true;
            }
        }
        return false;
    }


    public bool CanRotateLeft(Block block)
    {
        bool canRotateLeft = true;
        block.RotateLeft();
        if (!RotateCollisionCheck(canRotateLeft, block))
        {
            canRotateLeft = false;
        }
         block.RotateRight();
        return canRotateLeft;
    }


    public bool RotateCollisionCheck(bool canRotate, Block block)
    {
        for (int x = 0; x < block.layout.GetLength(xLength); x++)
        {
            for (int y = 0; y < block.layout.GetLength(yLength); y++)
            {
                if (!block.layout[y, x])
                {
                    continue;
                }

                if (block.getBlockPosition.X + x < 0 
                    || block.getBlockPosition.Y + y > Height - 1 
                    || block.getBlockPosition.X + x > Width - 1 
                    || block.getBlockPosition.Y + y < 0 
                    || collisionGrid[(int)block.getBlockPosition.Y + y, (int)block.getBlockPosition.X + x])
                {
                    canRotate = false;
                }
            }
        }
        return canRotate;
    }

    /// <summary>
    /// Draws the grid on the screen.
    /// </summary>
    /// <param name="gameTime">An object with information about the time that has passed in the game.</param>
    /// <param name="spriteBatch">The SpriteBatch used for drawing sprites and text.</param>
    public void Draw(GameTime gameTime, SpriteBatch spriteBatch, Block block)
    {

        for (int i = 0; i < Height; i++)
        {
            for (int t = 0; t < Width; t++)
            {
                if (!collisionGrid[i, t])
                {
                    spriteBatch.Draw(emptyCell, new Vector2((float)t * cellSize, (float)i * cellSize), Color.White);
                }
                else if (collisionGrid[i, t])
                {
                    spriteBatch.Draw(emptyCell, new Vector2((float)t * cellSize, (float)i * cellSize), colorGrid[i, t]);
                }
            }
        }
        
    }

    /// <summary>
    /// Clears the grid.
    /// </summary>
    public void Clear()
    {
        collisionGrid = new bool[Height, Width];
        colorGrid = new Color[Height, Width];
    }
}

