using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.ComponentModel;

class Blocks
{
    Random random = new Random();

    Vector2 blockPosition = new Vector2(0, 4);

    bool[,] blockArray = new bool[4, 4];
    bool canMoveRight = true;
    bool canMoveLeft = true;
    bool canMoveDown = true;
    bool previousCanMoveDown = true;
    bool blockPushed = true;

    double previousTimeDrop = 0;
    double previousTimePush = 0;

    int previousBlockPositionY = 0;

    const int blockArraySize = 4;
    const int timeToDrop = 1000;
    const int timeToPush = 2000;
    const int cellWidth = 30;

    Blocks currentBlock;
    public Blocks()
    {

    }

    public Random Random
    {
        get
        {
            return random;
        }
    }
    public virtual bool[,] layout
    {
        get
        {
            return new bool[4, 4];
        }
    }

    public virtual Color blockColor
    {
        get
        {
            return Color.White;
        }
    }

    public Blocks thisBlock
    {
        get
        {
            return currentBlock;
        }
    }
    public void addBlocks(int blockType)
    {
        if(blockPushed)
        {
            switch (blockType)
            {
                case (0):
                    currentBlock = new L();
                    blockPushed = false;
                    break;
                case (1):
                    currentBlock = new J();
                    blockPushed = false;
                    break;
                case (2):
                    currentBlock = new O();
                    blockPushed = false;
                    break;
                case (3):
                    currentBlock = new T();
                    blockPushed = false;
                    break;
                case (4):
                    currentBlock = new S();
                    blockPushed = false;
                    break;
                case (5):
                    currentBlock = new Z();
                    blockPushed = false;
                    break;
                case (6):
                    currentBlock = new I();
                    blockPushed = false;
                    break;
                case (7):
                    currentBlock = new U();
                    blockPushed = false;
                    break;
                default:
                    break;
            }
        }
    }



    public void HandleInput(InputHelper inputHelper)
    {
        if (inputHelper.KeyPressed(Microsoft.Xna.Framework.Input.Keys.Left) && canMoveLeft)
        {
            blockPosition.X -= 1;
        }

        if (inputHelper.KeyPressed(Microsoft.Xna.Framework.Input.Keys.Right) && canMoveRight)
        {
            blockPosition.X += 1;
        }

        if (inputHelper.KeyPressed(Microsoft.Xna.Framework.Input.Keys.Down))
        {
            blockPosition.Y += 1;
        }

        if (inputHelper.KeyPressed(Microsoft.Xna.Framework.Input.Keys.A))
        {
            RotateLeft();
        }

        if (inputHelper.KeyPressed(Microsoft.Xna.Framework.Input.Keys.D))
        {
            RotateRight();
        }
    }

    public void RotateRight()
    {
        bool[,] tempLayout = new bool[blockArraySize, blockArraySize];

        for (int i = 0; i < blockArraySize; i++)
        {
            for (int j = 0; j < blockArraySize; j++)
            {
                tempLayout[i, j] = currentBlock.layout[i, j];
            }
        }

        for (int x = 0; x < 4; x++)
        {
            for (int y = 0; y < 4; y++)
            {
                currentBlock.layout[x, y] = tempLayout[3 - y, x];
            }
        }
    }

    public void RotateLeft()
    {
        bool[,] tempLayout = new bool[blockArraySize, blockArraySize];

        for (int i = 0; i < blockArraySize; i++)
        {
            for (int j = 0; j < blockArraySize; j++)
            {
                tempLayout[i, j] = currentBlock.layout[i, j];
            }
        }

        for (int x = 0; x < 4; x++)
        {
            for (int y = 0; y < 4; y++)
            {
                currentBlock.layout[x, y] = tempLayout[y, 3 - x];
            }
        }
    }
    public void DropBlock(GameTime gameTime, TetrisGrid grid)
    {
        if(gameTime.TotalGameTime.TotalMilliseconds > previousTimeDrop + timeToDrop && canMoveDown)
        {
            previousTimeDrop = gameTime.TotalGameTime.TotalMilliseconds;
            blockPosition.Y += 1;
        }
    }

    // WORK IN PROGRESS
    public void PushBlock(TetrisGrid grid, GameTime gameTime)
    {
        GetCurrentPushTime(gameTime);
        if (!canMoveDown && gameTime.TotalGameTime.TotalMilliseconds > previousTimePush + timeToPush)
        {
            for (int y = 0; y < blockArraySize; y++)
            {
                for (int x = 0; x < blockArraySize; x++)
                {
                    if (currentBlock.layout[y, x])
                    {
                        grid.collisionGrid[(int)blockPosition.Y + y, (int)blockPosition.X + x] = true;
                        grid.colorGrid[(int)blockPosition.Y + y, (int)blockPosition.X + x] = currentBlock.blockColor;
                    }
                }
            }
            blockPushed = true;
            previousCanMoveDown = true;
            previousTimePush = gameTime.TotalGameTime.TotalMilliseconds;
            ResetPosition();
        }

    }

    public void ResetPosition()
    {
        blockPosition = new Vector2(4,0);
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
        for (int x = 0; x < blockArraySize; x++)
        {
            int blockRight = (int)blockPosition.X + x + 1;
            int blockLeft = (int)blockPosition.X + x - 1;
            for (int y = 0; y < blockArraySize; y++)
            {

                if (currentBlock.layout[y, x] && blockPosition.X + x > 0 && blockPosition.X + x < 9)
                {
                    canMoveLeft = true;
                    canMoveRight = true;
                }
                else if ((currentBlock.layout[y, x] && blockPosition.X + x <= 0) || (currentBlock.layout[y, x] && blockLeft >= 0 && grid.collisionGrid[y + (int)blockPosition.Y, blockLeft]))
                {
                    canMoveRight = true;
                    canMoveLeft = false;
                    goto loopEnd;
                }
                else if ((currentBlock.layout[y, x] && blockPosition.X + x >= 9) || (currentBlock.layout[y, x] && blockRight <= 9 && grid.collisionGrid[y + (int)blockPosition.Y, blockRight]))
                {
                    canMoveLeft = true;
                    canMoveRight = false;
                    goto loopEnd;
                }
            }
        }
        loopEnd:;
    }

    public void Update(GameTime gameTime, TetrisGrid grid)
    {
        CanMoveRightLeft(grid);
        CanMoveDown(grid, gameTime);
        DropBlock(gameTime, grid);
        PushBlock(grid,gameTime);

    }
    public void Draw(SpriteBatch spriteBatch, Texture2D texture)
    {
        for(int y = 0; y < blockArraySize; y++)
        {
            for(int x = 0; x < blockArraySize; x++)
            {
                if (currentBlock.layout[y,x])
                {
                    spriteBatch.Draw(texture, new Vector2((float) (blockPosition.X + x) * cellWidth, (float) (blockPosition.Y + y) * cellWidth), currentBlock.blockColor);
                }
            }
        }
        
    }
}


class L : Blocks
{
    bool[,] layoutL = new bool[,] {
            {false, true, false, false},
            {false, true, false, false},
            {false, true, true, false},
            {false, false, false, false}
        };



    public L()
    {
    }

    public override Color blockColor
    {
        get
        {
            return Color.Orange;
        }
    }

    public override bool[,] layout
    {
        get
        {
            return layoutL;
        }
    }
}

class J : Blocks
{
    bool[,] layoutJ = new bool[,]
{
            {false, false, true, false},
            {false, false, true, false},
            {false, true, true, false},
            {false, false, false, false}
};

    public J()
    {

    }

    public override Color blockColor
    {
        get
        {
            return Color.DarkBlue;
        }
    }
    public override bool[,] layout
    {
        get
        {
            return layoutJ;
        }
    }
}


class O : Blocks
{
    bool[,] layoutO = new bool[,]
    {
            {false, false, false, false},
            {false, true, true, false},
            {false, true, true, false},
            {false, false, false, false}
    };

    public O()
    {

    }
    public override Color blockColor
    {
        get
        {
            return Color.Yellow;
        }
    }



    public override bool[,] layout
    {
        get
        {
            return layoutO;
        }
    }
}

class I : Blocks
{
    bool[,] layoutI = new bool[,]
    {
            {false, true, false, false},
            {false, true, false, false},
            {false, true, false, false},
            {false, true, false, false}
    };


    public I()
    {

    }
    public override Color blockColor
    {
        get
        {
            return Color.LightBlue;
        }
    }

    public override bool[,] layout
    {
        get
        {
            return layoutI;
        }
    }
}

class S : Blocks
{

    bool[,] layoutS = new bool[,]
    {
            {false, true, false, false },
            {false, true, true, false},
            {false, false, true, false},
            {false, false, false, false}
    };

    public S()
    {

    }
    public override Color blockColor
    {
        get
        {
            return Color.LightGreen;
        }
    }

    public override bool[,] layout
    {
        get
        {
            return layoutS;
        }
    }
}

class Z : Blocks
{
    bool[,] layoutZ = new bool[,]
    {
            {true, true, false, false},
            {false, true, true, false},
            {false, false, false, false},
            {false, false, false, false}
    };

    public Z()
    {

    }
    public override Color blockColor
    {
        get
        {
            return Color.Red;
        }
    }

    public override bool[,] layout
    {
        get
        {
            return layoutZ;
        }
    }
}

class T : Blocks
{
    bool[,] layoutT = new bool[,]
    {
            {false, false, false, false},
            {false, true, true, true},
            {false, false, true, false},
            {false, false, false, false},
    };

    public T()
    {

    }
    public override Color blockColor
    {
        get
        {
            return Color.Purple;
        }
    }

    public override bool[,] layout
    {
        get
        {
            return layoutT;
        }
    }
}

class U : Blocks
{
    bool[,] layoutU = new bool[,]
    {
            {false, false, false, false},
            {true, false, true, false},
            {true, true, true, false},
            {false, false, false, false}
    };

    public U()
    {

    }

    public override Color blockColor
    {
        get
        {
            return Color.Pink;
        }
    }

    public override bool[,] layout
    {
        get
        {
            return layoutU;
        }
    }
}

