using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Transactions;
using static System.Reflection.Metadata.BlobBuilder;

class Blocks
{
    const int cellWidth = 30;
    protected bool[,] blockArray = new bool[4,4];
    static Random random = new Random();
    Color color;
    Vector2 blockPosition;

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
        if (inputHelper.KeyPressed(Microsoft.Xna.Framework.Input.Keys.Left))
        {
            blockPosition.X -= 1;
        }

        if (inputHelper.KeyPressed(Microsoft.Xna.Framework.Input.Keys.Right))
        {
            blockPosition.X += 1;
        }

        if (inputHelper.KeyPressed(Microsoft.Xna.Framework.Input.Keys.Down))
        {
            blockPosition.Y += 1;
        }

        if (inputHelper.KeyPressed(Microsoft.Xna.Framework.Input.Keys.A))
        {
            for (int i = 0; i < 4; i++)
            {
                for(int j = 0; j < 4; j++)
                {
                    currentBlock.layout[i, j] = currentBlock.layout[j, i];
                }
            }
        }
    }


    public void Update()
    {
        //blockPosition = new Vector2(0, 0);
    }

    public void Draw(SpriteBatch spriteBatch, Texture2D texture)
    {
        for(int i = 0; i < 4; i++)
        {
            for(int j = 0; j < 4; j++)
            {
                if (currentBlock.layout[i,j])
                {
                    spriteBatch.Draw(texture, new Vector2((float) (blockPosition.X + i) * cellWidth, (float) (blockPosition.Y + j) * cellWidth), Color.Red);
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

