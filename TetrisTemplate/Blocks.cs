using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.ComponentModel;

class Blocks
{
    Random random = new Random();

    Vector2 blockPosition = new Vector2(4, 0);

    const int blockArraySize = 4;
    const int cellWidth = 30;

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
        protected set
        {
            
        }
    }

    public virtual Color blockColor
    {
        get
        {
            return Color.White;
        }
    }

    public Vector2 getBlockPosition
    {
        get
        {
            return blockPosition;
        }
    }

    public void MoveDown()
    {
        blockPosition.Y += 1;
    }

    public void MoveLeft()
    {
        blockPosition.X -= 1;
    }

    public void MoveRight()
    {
        blockPosition.X += 1;
    }

    public void ResetPosition()
    {
        blockPosition = new Vector2(4, 0);
    }

    public Blocks AddBlocks(int blockType)
    {
        switch (blockType)
        {
            case (0):
                return new L();
            case (1):
                return new J();
            case (2):
                return new O();
            case (3):
                return new T();
            case (4):
                return new S();
            case (5):
                return new Z();
            case (6):
                return new I();
            case (7):
                return new U();
            default:
                return null;
        }
    }

    

    public void RotateRight()
    {
        bool[,] tempLayout = new bool[blockArraySize, blockArraySize];


        for (int x = 0; x < 4; x++)
        {
            for (int y = 0; y < 4; y++)
            {
                tempLayout[x, y] = this.layout[3 - y, x];
            }
        }
        this.layout = tempLayout;
    }

    public void RotateLeft()
    {
        bool[,] tempLayout = new bool[blockArraySize, blockArraySize];

        for (int x = 0; x < 4; x++)
        {
            for (int y = 0; y < 4; y++)
            {
                tempLayout[x, y] = this.layout[y, 3 - x];
            }
        }

        this.layout = tempLayout;
    }

    public void Draw(SpriteBatch spriteBatch, Texture2D texture)
    {
        for (int y = 0; y < blockArraySize; y++)
        {
            for (int x = 0; x < blockArraySize; x++)
            {
                if (this.layout[y, x])
                {
                    spriteBatch.Draw(texture, new Vector2((float)(blockPosition.X + x) * cellWidth, (float)(blockPosition.Y + y) * cellWidth), this.blockColor);
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
        protected set
        {
            layoutL = value;
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
        protected set
        {
            layoutJ = value;
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
        protected set
        {
            layoutO = value;
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
        protected set
        {
            layoutI = value;
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
        protected set
        {
            layoutS = value;
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
        protected set
        {
            layoutZ = value;
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
        protected set
        {
            layoutT = value;
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
        protected set
        {
            layoutU = value;
        }
    }
}

