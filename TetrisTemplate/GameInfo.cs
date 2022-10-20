using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;

namespace TetrisTemplate
{
    class GameInfo
    {
        private SpriteFont scoreDisplay;
        public int scoreRows = 0;
        int score = 0;
        int level = 1;
        int previousLevelRows = 0;

        public GameInfo()
        {
            scoreDisplay = TetrisGame.ContentManager.Load<SpriteFont>("SpelFont");
        }
            
        
        
        public void UpdateScore()
        {
            if(scoreRows > 0)
            {
                score += (int)Math.Pow((double)scoreRows,(double)2) * 10 * level;
            }

            scoreRows = 0;
        }

        public void UpdateLevel(int levelRows)
        {
            if(levelRows >= previousLevelRows + 10)
            {
                previousLevelRows = levelRows;
                level += 1;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(scoreDisplay, "Level: " + level.ToString(), new Vector2(400f, 220f), Color.Black);
        }
    }
}

