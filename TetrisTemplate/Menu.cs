using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using static System.Formats.Asn1.AsnWriter;

namespace TetrisTemplate
{
    internal class Menu
    {

        public Texture2D logo;
        public SpriteFont font;

        public Menu()
        {
            this.logo = logo;
            this.font = font;
        }

        public void LoadContent(ContentManager content)
        {
            logo = content.Load<Texture2D>("Tetris");
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(font, "Press enter to play!", new Vector2(400f, 200f), Color.Black);
            spriteBatch.Draw(logo, new Vector2((float)TetrisGame.ScreenSize.X / 2, TetrisGame.ScreenSize.Y / 3), Color.White);
        }
    }
}
