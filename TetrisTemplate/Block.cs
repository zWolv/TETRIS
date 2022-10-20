using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.ComponentModel;
using System.Xml.Schema;

class Block
{
    protected Vector2 blockPosition;
    const int cellWidth = 30;
    protected const int xLength = 1;
    protected const int yLength = 0;

    bool[,] _layout;
    Color _blockColor;
    // Blocktypes
    bool[,] layoutL = new bool[,] {
            {false, true, false},
            {false, true, false},
            {false, true, true}
        };

    bool[,] layoutJ = new bool[,]
        {
            {false, false, true},
            {false, false, true},
            {false, true, true}
        };

    bool[,] layoutO = new bool[,]
    {
            {true, true},
            {true, true}
    };

    bool[,] layoutI = new bool[,]
    {
            {false, true, false, false},
            {false, true, false, false},
            {false, true, false, false},
            {false, true, false, false}
    };

    bool[,] layoutS = new bool[,]
    {
            {false, true, false},
            {false, true, true},
            {false, false, true}
    };

    bool[,] layoutZ = new bool[,]
    {
            {true, true, false},
            {false, true, true},
            {false, false, false}
    };

    bool[,] layoutT = new bool[,]
    {
            {false, true, false},
            { true, true, true},
            {false, false, false}
    };

    bool[,] layoutU = new bool[,]
    {
            {false, false, false},
            {true, false, true},
            {true, true, true}
    };



    //Blockcolors
    Color colorL = Color.Orange;
    Color colorJ = Color.DarkBlue;
    Color colorO = Color.Yellow;
    Color colorI = Color.LightBlue;
    Color colorS = Color.LightGreen;
    Color colorZ = Color.Red;
    Color colorT = Color.Purple;
    Color colorU = Color.Pink;

    public Block(Vector2 blockPos, int blockType)
    // public Block(int blockType, Vector2 blockPos)
    {
        switch (blockType)
        {
            case (0):
                blockPosition = blockPos;
                _layout = layoutL;
                _blockColor = colorL;
                break;
            case (1):
                blockPosition = blockPos;
                _layout = layoutJ;
                _blockColor = colorJ;
                break;
            case (2):
                blockPosition = blockPos;
                _layout = layoutO;
                _blockColor = colorO;
                break;
            case (3):
                blockPosition = blockPos;
                _layout = layoutT;
                _blockColor = colorT;
                break;
            case (4):
                blockPosition = blockPos;
                _layout = layoutS;
                _blockColor = colorS;
                break;
            case (5):
                blockPosition = blockPos;
                _layout = layoutZ;
                _blockColor = colorZ;
                break;
            case (6):
                blockPosition = blockPos;
                _layout = layoutT;
                _blockColor = colorT;
                break;
            case (7):
                blockPosition = blockPos;
                _layout = layoutU;
                _blockColor = colorU;
                break;
            default:
                break;
        }
    }

    public bool[,] layout
    {
        get
        {
            return _layout;
        }
        private set
        {
            _layout = value;
        }
    }

    public virtual Color blockColor
    {
        get
        {
            return _blockColor;
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