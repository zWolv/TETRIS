// gemaakt door Thomas van Egmond en Steijn Hoks
//              8471533              5002311

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;



class Menu
{
    // maak de texture en spritefont aan
    Texture2D logo;
    SpriteFont font;

    public Menu()
    {
        // initialiseer het logo en het font
        logo = TetrisGame.ContentManager.Load<Texture2D>("Tetris");
        font = TetrisGame.ContentManager.Load<SpriteFont>("spelFont");
    }

    // teken de tekst en het plaatje van het menu
    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.DrawString(font, "Press enter to play!", new Vector2(TetrisGame.ScreenSize.X / 3, TetrisGame.ScreenSize.Y - 150), Color.Black);
        spriteBatch.Draw(logo, new Vector2((float)TetrisGame.ScreenSize.X / 9, 10), Color.White);
    }
}

