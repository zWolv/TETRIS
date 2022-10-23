// onderdeel van de TetrisTemplate
// edits van Thomas van Egmond en Steijn Hoks
//           8471533              5002311

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

class TetrisGrid
{
    //het grid voor de game
    public Color[,] colorGrid = new Color[20, 10];

    // de standaardkleur voor het grid
    Color defaultColor = Color.White;

    // constantes voor de GetLength functie van arrays
    const int yLength = 0;
    const int xLength = 1;
    // constante hoogte en breedte van een grid-blok
    const int cellSize = 30;

    // counter voor hoeveel rijen er in totaal tijdens een spel zijn gescoord
    int rowCounterForLevel = 0;

    // sprite voor een enkel blok in een grid
    Texture2D emptyCell;

    // de breedte van het grid in blokken
    public int Width { get { return 10; } }

    // de hoogte van het grid in blokken
    public int Height { get { return 20; } }
    

    public TetrisGrid()
    {
        // initialiseer de sprite voor een blok
        emptyCell = TetrisGame.ContentManager.Load<Texture2D>("block");

        //maak het grid schoon
        Clear();
    }

    // voeg een blok toe aan het grid
    public void AddToGrid(Block block)
    {
            for (int y = 0; y < block.layout.GetLength(yLength); y++)
            {
                for (int x = 0; x < block.layout.GetLength(xLength); x++)
                {
                    if (block.layout[y, x])
                    {
                        colorGrid[(int)block.getBlockPosition.Y + y, (int)block.getBlockPosition.X + x] = block.blockColor;
                    }
                }
            }
            block.MoveToStartPosition();
    }


    // check of er collision aanwezig is op de nieuwe positie van een blok
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
                        if(colorGrid[gridBlockPixelPositionY, gridBlockPixelPositionX] != defaultColor)
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

    // kijk of er volle rijen zijn, en pas het grid aan als die er zijn
    public void CheckRow(ref int scoreRows)
    {
        int counter = 0;
        int rowCounterForScore = 0;
        for (int y = 0; y < Height; y++)
        {
            counter = 0;
            for (int x = 0; x < Width; x++)
            {
                if (colorGrid[y, x] == defaultColor)
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

    // property voor de texture (Block class heeft geen eigen texture maar gebruikt dezelfde)
    public Texture2D getTexture
    {
        get
        {
            return emptyCell;
        }
    }

    // property voor totaal aantal rijen gescoord
    public int getLevelRows
    {
        get
        {
            return rowCounterForLevel;
        }
    }

    // duw het grid een positie omlaag tot aan de rij die vol was
    public void LowerGrid(int rowRemoved)
    {
        for(int y = rowRemoved; y > 0 ; y--)
        {
            for(int x = 0; x < Width; x++)
            {
                    colorGrid[y, x] = colorGrid[y - 1, x];
            }
        }
    }


    // check of de naar rechts gedraaide versie van een  blok collision heeft
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

    // maak alle geplaatste blokken grijs bij gameover
    public void GameOverGrayout()
    {
        for (int y = 0; y < Height; y++)
        {
            for (int x = 0; x < Width; x++)
            {
                if (colorGrid[y, x] != defaultColor)
                {
                    colorGrid[y, x] = Color.Gray;
                }
            }
        }
    }

    // check of de game over is
    public bool GameOverCollision(Block block)
    {
        for(int x = 0; x < Width; x++)
        {
            if (colorGrid[0,x] != defaultColor && CheckCollision(block, new Vector2(block.getBlockPosition.X, block.getBlockPosition.Y + 1)))
            {
                GameOverGrayout();
                return true;
            }
        }
        return false;
    }

    // check of de naar links gedraaide versie van een blok collision heft
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

    // check of een blok collision heeft -- CheckCollision() checkt voor een bepaalde positie van een blok, maar kan dus niet de gedraaide versie checken
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
                    || colorGrid[(int)block.getBlockPosition.Y + y, (int)block.getBlockPosition.X + x] != defaultColor)
                {
                    canRotate = false;
                }
            }
        }
        return canRotate;
    }

    // tekent het grid aan de hand van de kleuren in colorGrid
    public void Draw(GameTime gameTime, SpriteBatch spriteBatch, Block block)
    {

        for (int i = 0; i < Height; i++)
        {
            for (int t = 0; t < Width; t++)
            {
                if (colorGrid[i,t] == defaultColor)
                {
                    spriteBatch.Draw(emptyCell, new Vector2((float)t * cellSize, (float)i * cellSize), defaultColor);
                }
                else if (colorGrid[i,t] != defaultColor)
                {
                    spriteBatch.Draw(emptyCell, new Vector2((float)t * cellSize, (float)i * cellSize), colorGrid[i, t]);
                }
            }
        }
        
    }

    // zet alle blokken in het grid naar de standaardkleur
    public void Clear()
    {
        for(int y = 0; y < Height; y++)
        {
            for(int x = 0; x < Width; x++)
            {
                colorGrid[y,x] = defaultColor;
            }
        }
        
    }
}

