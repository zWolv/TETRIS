﻿using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;


class GameInfo
{
    private SpriteFont scoreDisplay;
    public int scoreRows = 0;
    int score = 0;
    int level = 1;
    int previousLevelRows = 0;
    Color textColor = Color.Black;

    public GameInfo()
    {
        scoreDisplay = TetrisGame.ContentManager.Load<SpriteFont>("SpelFont");
    }

    public int getLevel
    {
        get
        {
            return level;
        }
    }

    public void UpdateScore()
    {
        if (scoreRows > 0)
        {
            score += (int)Math.Pow((double)scoreRows, (double)2) * 10 * level;
        }

        scoreRows = 0;
    }

    public void UpdateLevel(int levelRows)
    {
        if (levelRows >= previousLevelRows + 10)
        {
            previousLevelRows = levelRows;
            level += 1;
        }
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.DrawString(scoreDisplay, "Score: " + score, new Vector2(400f, 240f), textColor);
        spriteBatch.DrawString(scoreDisplay, "Level: " + level.ToString(), new Vector2(400f, 270f), textColor);
    }
}


