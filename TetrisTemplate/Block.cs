using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

class Block
{
    protected Vector2 blockPosition;
    const int cellWidth = 30;
    protected const int xLength = 1;
    protected const int yLength = 0;

    protected bool[,] _layout = null;
    protected Color _blockColor = Color.White;

    // Blocktypes
    protected bool[,] layoutL = new bool[,]
    {
        {false, true, false},
        {false, true, false},
        {false, true, true}
    };

    protected bool[,] layoutJ = new bool[,]
    {
        {false, false, true},
        {false, false, true},
        {false, true, true}
    };

    protected bool[,] layoutO = new bool[,]
    {
            {true, true},
            {true, true}
    };

    protected bool[,] layoutI = new bool[,]
    {
            {false, true, false, false},
            {false, true, false, false},
            {false, true, false, false},
            {false, true, false, false}
    };

    protected bool[,] layoutS = new bool[,]
    {
            {false, true, false},
            {false, true, true},
            {false, false, true}
    };

    protected bool[,] layoutZ = new bool[,]
    {
            {true, true, false},
            {false, true, true},
            {false, false, false}
    };

    protected bool[,] layoutT = new bool[,]
    {
            {false, true, false},
            { true, true, true},
            {false, false, false}
    };

    protected bool[,] layoutU = new bool[,]
    {
            {false, false, false},
            {true, false, true},
            {true, true, true}
    };

    //Blockcolors
    protected Color colorL = Color.Orange;
    protected Color colorJ = Color.DarkBlue;
    protected Color colorO = Color.Yellow;
    protected Color colorI = Color.LightBlue;
    protected Color colorS = Color.LightGreen;
    protected Color colorZ = Color.Red;
    protected Color colorT = Color.Purple;
    protected Color colorU = Color.Pink;

    public Block(Vector2 blockPos)
    {
        blockPosition = blockPos;
        blockColor = _blockColor;
        layout = _layout;
    }

    public bool[,] layout
    {
        get
        {
            return _layout;
        }
        protected set
        {
            _layout = value;
        }
    }

    public Color blockColor
    {
        get
        {
            return _blockColor;
        }
        protected set
        {
            _blockColor = value;
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

    public void MoveToStartPosition()
    {
        blockPosition = new Vector2(4, 0);
    }

    public void RotateRight()
    {
        bool[,] tempLayout = new bool[this.layout.GetLength(yLength), this.layout.GetLength(xLength)];


        for (int x = 0; x < this.layout.GetLength(xLength); x++)
        {
            for (int y = 0; y < this.layout.GetLength(yLength); y++)
            {
                tempLayout[x, y] = this.layout[(this.layout.GetLength(yLength) - 1) - y, x];
            }
        }
        this.layout = tempLayout;
    }

    public void RotateLeft()
    {
        bool[,] tempLayout = new bool[this.layout.GetLength(1), this.layout.GetLength(0)];

        for (int x = 0; x < this.layout.GetLength(1); x++)
        {
            for (int y = 0; y < this.layout.GetLength(0); y++)
            {
                tempLayout[x, y] = this.layout[y, (this.layout.GetLength(xLength) - 1) - x];
            }
        }

        this.layout = tempLayout;
    }

    public virtual void Draw(SpriteBatch spriteBatch, Texture2D texture)
    {
        for (int y = 0; y < this.layout.GetLength(yLength); y++)
        {
            for (int x = 0; x < this.layout.GetLength(xLength); x++)
            {
                if (this.layout[y, x])
                {
                    spriteBatch.Draw(texture, new Vector2((float)(blockPosition.X + x) * cellWidth, (float)(blockPosition.Y + y) * cellWidth), this.blockColor);
                }
            }
        }
    }
}

class L : Block
{
    public L(Vector2 blockPos)
    :base(blockPos)
    {
        this.blockColor = colorL;
        this.layout = layoutL;
    }
}

class J : Block
{

    public J(Vector2 blockPos)
    :base(blockPos)
    {
        this.blockColor = colorJ;
        this.layout = layoutJ;
    }
}

class O : Block
{

    public O(Vector2 blockPos)
    :base(blockPos)
    {
        this.blockColor = colorO;
        this.layout = layoutO;
    }
}

class I : Block
{

    public I(Vector2 blockPos)
    :base(blockPos)
    {
        this.blockColor = colorI;
        this.layout = layoutI;
    }
}

class S : Block
{

    public S(Vector2 blockPos)
    :base(blockPos)
    {
        this.blockColor = colorS;
        this.layout = layoutS;
    }
}

class Z : Block
{

    public Z(Vector2 blockPos)
    :base(blockPos)
    {
        this.blockColor = colorZ;
        this.layout = layoutZ;
    }
}

class T : Block
{

    public T(Vector2 blockPos)
    :base(blockPos)
    {
        this.blockColor = colorT;
        this.layout = layoutT;
    }
}

class U : Block
{

    public U(Vector2 blockPos)
    :base(blockPos)
    {
        this.blockColor = colorU;
        this.layout = layoutU;
    }
}