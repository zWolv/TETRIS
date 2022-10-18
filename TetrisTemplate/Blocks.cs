using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.ComponentModel;

class Blocks
{
    const int cellWidth = 30;
    protected bool[,] blockArray = new bool[4,4];
    static Random random = new Random();
    Color color;
    Vector2 blockPosition = new Vector2(0,0);
    bool canMoveRight = true;
    bool canMoveLeft = true;
    bool canMoveDown = true;
    const int blockArraySize = 4;
    double previousTime = 0;

    Blocks currentBlock;
    public Blocks()
    {

    }
     
    public virtual bool[,] layout
    {
        get
        {
            return new bool[4,4];
        }
    }

    public void addBlocks(string blockType)
    {

        switch (blockType)
        {
            case ("L"):
                currentBlock = new L();
                break;
            case ("J"):
                currentBlock = new J();
                break;
            case ("O"):
                currentBlock = new O();
                break;
            case ("T"):
                currentBlock = new T();
                break;
            case ("S"):
                currentBlock = new S();
                break;
            case ("Z"):
                currentBlock = new Z();
                break;
            case ("I"):
                currentBlock = new I();
                break;
            case ("U"):
                currentBlock = new U();
                break;
            default:
                break;
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

        if (inputHelper.KeyPressed(Microsoft.Xna.Framework.Input.Keys.A))
        {
            bool[,] tempLayout = currentBlock.layout;
            int x = 0;
            int y = 0;
            int i = x;
            int j = y;

            for (x = 0; x < 4; x++)
            {
                if (i < 3)
                {
                    i++;
                }

                for (y = 0; y < 4; y++)
                {
                    if (j < 3)
                    {
                        j++;
                    };

                    currentBlock.layout[x, y] = tempLayout[j, i];
                }
            }
        }
    }

    public void DropBlock(GameTime gameTime)
    {
        if(gameTime.TotalGameTime.TotalMilliseconds > previousTime + 1000 && canMoveDown)
        {
            previousTime = gameTime.TotalGameTime.TotalMilliseconds;
            blockPosition.Y += 1;
        } 
    }

    // WORK IN PROGRESS
    public void PushBlock(TetrisGrid grid)
    {
        if (blockPosition.Y + blockArraySize - 1 == 19)
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
        }
    }

    //WORK IN PROGRESS
    public void CanMoveDown(TetrisGrid grid)
    {
        if (blockPosition.Y + blockArraySize - 1 < 19)
        {
            for (int y = 0; y < grid.Height; y++)
            {
                for (int x = 0; x < grid.Width; x++)
                {
                }
            }
        }
        else if(blockPosition.Y + blockArraySize - 1 == 19)
        {
            canMoveDown = false;
        }
        else
        {
            canMoveDown = true;
        }
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
        CanMoveDown(grid);
        DropBlock(gameTime);
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
                    spriteBatch.Draw(texture, new Vector2((float) (blockPosition.X + x) * cellWidth, (float) (blockPosition.Y + y) * cellWidth), Color.Red);
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
    //color = new Color.Orange

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
    //color = new Color.DarkBlue;
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
    // color = new Color.Yellow;



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
    // color = new Color.LightBlue;

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
    //color = new Color.LightGreen;

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
    // color = new Color.Red;

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
    // color = new Color.Purple;

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

    public override bool[,] layout
    {
        get
        {
            return layoutU;
        }
    }
}

