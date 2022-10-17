using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.ComponentModel;

class Blocks
{
    const int cellWidth = 30;
    protected bool[,] blockArray = new bool[4, 4];
    static Random random = new Random();
    Vector2 blockPosition = new Vector2(0, 0);
    bool canMoveRight = true;
    bool canMoveLeft = true;
    bool canMoveDown = true;
    const int blockArraySize = 4;
    double previousTime = 0;
    bool blockPushed = true;
    const int timeToDrop = 1000;

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
    }

    public void DropBlock(GameTime gameTime, TetrisGrid grid)
    {
        CanMoveDown(grid);
        if(gameTime.TotalGameTime.TotalMilliseconds > previousTime + timeToDrop && canMoveDown)
        {
            previousTime = gameTime.TotalGameTime.TotalMilliseconds;
            blockPosition.Y += 1;
        } 
    }

    // WORK IN PROGRESS
    public void PushBlock(TetrisGrid grid)
    {
        CanMoveDown(grid);
        if (!canMoveDown)
        {
            for (int y = 0; y < blockArraySize; y++)
            {
                for (int x = 0; x < blockArraySize; x++)
                {
                    if (currentBlock.layout[y, x])
                    {
                        grid.collisionGrid[(int)blockPosition.Y + y, (int)blockPosition.X + x] = true;
                    }
                }
            }
            blockPushed = true;
            ResetPosition();
        }
    }

    public void ResetPosition()
    {
        blockPosition = new Vector2(0, 4);
    }

    //WORK IN PROGRESS
    public void CanMoveDown(TetrisGrid grid)
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

    public void CanMoveRightLeft()
    {
        for (int x = 0; x < blockArraySize; x++)
        {
            for (int y = 0; y < blockArraySize; y++)
            {
                if (currentBlock.layout[y, x] && blockPosition.X + x > 0 && blockPosition.X + x < 9)
                {
                    canMoveLeft = true;
                    canMoveRight = true;
                }
                else if (currentBlock.layout[y, x])
                {
                    if(blockPosition.X + x == 0)
                    {
                        canMoveRight = true;
                        canMoveLeft = false;
                        goto loopEnd;
                    }
                    else if(blockPosition.X + x == 9)
                    {
                        canMoveLeft = true;
                        canMoveRight = false;
                        goto loopEnd;
                    }
                }
            }
        }
        loopEnd:;
    }

    public void Update(GameTime gameTime, TetrisGrid grid)
    {
        CanMoveRightLeft();
        DropBlock(gameTime, grid);
        PushBlock(grid);

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
            {false, true, true, false},
            {false, true, true, false},
            {false, false, false, false},
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
            {false, false, true, true},
            {false, true, true, false},
            {false, false, false, false},
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

