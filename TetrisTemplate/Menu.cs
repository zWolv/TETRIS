using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using static System.Formats.Asn1.AsnWriter;
using System.Reflection.Metadata;

namespace TetrisTemplate
{
    internal class Menu
    {

        public Texture2D logo;
        public SpriteFont font;
        public Menu()
        {
            logo = TetrisGame.ContentManager.Load <Texture2D> ("Tetris");
            font = TetrisGame.ContentManager.Load <SpriteFont> ("spriteFont");
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            SpriteBatch.Begin();
            spriteBatch.DrawString(font, "Press enter to play!", new Vector2(400f, 200f), Color.Black);
            spriteBatch.Draw(logo, new Vector2((float)TetrisGame.ScreenSize.X / 2, TetrisGame.ScreenSize.Y / 3), Color.White);
            SpriteBatch.End();
        }
    }
}
