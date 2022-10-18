using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.ComponentModel;

class Blocks
{
    Random random = new Random();

    Vector2 blockPosition = new Vector2(0, 4);

    const int blockArraySize = 4;
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

    public Blocks block
    {
        get
        {
            return currentBlock;
        }
    }
    public void addBlocks(int blockType, bool blockPushed)
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

