// gemaakt door Thomas van Egmond en Steijn Hoks
//              8471533              5002311


using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

class Block
{
    // de positie van een blok
    protected Vector2 blockPosition;
    // de hoogte en breedte van een cel op het grid en dus van een blokdeel
    const int cellWidth = 30;

    // constantes voor de GetLength functie van arrays
    protected const int xLength = 1;
    protected const int yLength = 0;

    // de layout van een blok
    protected bool[,] _layout = null;
    // de kleur van een blok
    protected Color _blockColor = Color.White;

    
    public Block(Vector2 blockPos)
    {
        // initialiseer de positie, kleur en layout van een blok
        blockPosition = blockPos;
        blockColor = _blockColor;
        layout = _layout;
    }

    // layout property van een blok
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

    // kleur property van een blok
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

    // positie property van een blok
    public Vector2 getBlockPosition
    {
        get
        {
            return blockPosition;
        }
    }

    // beweeg blok 1 positie omlaag op het grid
    public void MoveDown()
    {
        blockPosition.Y += 1;
    }

    // beweeg blok 1 positie naar links op het grid
    public void MoveLeft()
    {
        blockPosition.X -= 1;
    }

    // beweeg blok 1 positie naar rechts op het grid
    public void MoveRight()
    {
        blockPosition.X += 1;
    }

    // zet een blok op de default startpositie op het grid
    public void MoveToStartPosition()
    {
        blockPosition = new Vector2(4, 0);
    }

    // roteer een blok naar rechts
    public void RotateRight()
    {
        bool[,] tempLayout = new bool[layout.GetLength(yLength), layout.GetLength(xLength)];


        for (int x = 0; x < layout.GetLength(xLength); x++)
        {
            for (int y = 0; y < layout.GetLength(yLength); y++)
            {
                tempLayout[x, y] = layout[(layout.GetLength(yLength) - 1) - y, x];
            }
        }
        layout = tempLayout;
    }

    // roteer een blok naar links
    public void RotateLeft()
    {
        bool[,] tempLayout = new bool[layout.GetLength(1), layout.GetLength(0)];

        for (int x = 0; x < layout.GetLength(1); x++)
        {
            for (int y = 0; y < layout.GetLength(0); y++)
            {
                tempLayout[x, y] = layout[y, (layout.GetLength(xLength) - 1) - x];
            }
        }
        layout = tempLayout;
    }

    // teken een blok, op basis van de layout
    public void Draw(SpriteBatch spriteBatch, Texture2D texture)
    {
        for (int y = 0; y < layout.GetLength(yLength); y++)
        {
            for (int x = 0; x < layout.GetLength(xLength); x++)
            {
                if (layout[y, x])
                {
                    spriteBatch.Draw(texture, new Vector2((float)(blockPosition.X + x) * cellWidth, (float)(blockPosition.Y + y) * cellWidth), blockColor);
                }
            }
        }
    }
}


// alleen comments bij vorm L, andere vormen zijn hetzelfde opgebouwd
class L : Block {

    // layout voor vorm L
    bool[,] layoutL = new bool[,]
    {
        {false, true, false},
        {false, true, false},
        {false, true, true}
    };

    // kleur voor vorm L
    Color colorL = Color.Orange;

    public L(Vector2 blockPos)
    // zet positie met constructor hoofdklasse Block
    :base(blockPos)
    {
        // zet de kleur en layout goed voor vorm L
        blockColor = colorL;
        layout = layoutL;
    }
}

class J : Block
{
    bool[,] layoutJ = new bool[,]
    {
        {false, false, true},
        {false, false, true},
        {false, true, true}
    };

    Color colorJ = Color.DarkBlue;

    public J(Vector2 blockPos)
    :base(blockPos)
    {
        blockColor = colorJ;
        layout = layoutJ;
    }
}

class O : Block
{
    bool[,] layoutO = new bool[,]
    {
        {true, true},
        {true, true}
    };

    Color colorO = Color.Yellow;

    public O(Vector2 blockPos)
    :base(blockPos)
    {
        blockColor = colorO;
        layout = layoutO;
    }
}

class I : Block
{
    bool[,] layoutI = new bool[,]
    {
        {false, true, false, false},
        {false, true, false, false},
        {false, true, false, false},
        {false, true, false, false}
    };

    Color colorI = Color.LightBlue;

    public I(Vector2 blockPos)
    :base(blockPos)
    {
        blockColor = colorI;
        layout = layoutI;
    }
}

class S : Block
{
    bool[,] layoutS = new bool[,]
    {
        {false, true, false},
        {false, true, true},
        {false, false, true}
    };

    Color colorS = Color.LightGreen;

    public S(Vector2 blockPos)
    :base(blockPos)
    {
        blockColor = colorS;
        layout = layoutS;
    }
}

class Z : Block
{
    bool[,] layoutZ = new bool[,]
    {
        {true, true, false},
        {false, true, true},
        {false, false, false}
    };

    Color colorZ = Color.Red;

    public Z(Vector2 blockPos)
    :base(blockPos)
    {
        blockColor = colorZ;
        layout = layoutZ;
    }
}

class T : Block
{
    bool[,] layoutT = new bool[,]
    {
        {false, true, false},
        { true, true, true},
        {false, false, false}
    };

    Color colorT = Color.Purple;

    public T(Vector2 blockPos)
    :base(blockPos)
    {
        blockColor = colorT;
        layout = layoutT;
    }
}

class U : Block
{

    bool[,] layoutU = new bool[,]
    {
        {false, false, false},
        {true, false, true},
        {true, true, true}
    };

    Color colorU = Color.Pink;

    public U(Vector2 blockPos)
    :base(blockPos)
    {
        blockColor = colorU;
        layout = layoutU;
    }
}