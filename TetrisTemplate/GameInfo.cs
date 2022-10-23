// gemaakt door Thomas van Egmond en Steijn Hoks
//              8471533              5002311

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;


class GameInfo
{
    // het font voor de tekst
    SpriteFont scoreDisplay;
    // het aantal rijen dat gescoord is
    public int scoreRows = 0;
    // de score
    int score = 0;
    // het level
    int level = 1;
    //hoeveel rijen er in totaal gescoord zijn
    int previousLevelRows = 0;
    // kleur van de tekst
    Color textColor = Color.Black;
    // positie van de score en het level
    Vector2 scorePos = new Vector2(400f, 240f);
    Vector2 levelPos = new Vector2(400f, 270f);

    public GameInfo()
    {
        // initialiseer het font
        scoreDisplay = TetrisGame.ContentManager.Load<SpriteFont>("SpelFont");
    }

    // property om het level te krijgen
    public int getLevel
    {
        get
        {
            return level;
        }
    }

    // update de score op basis van scoreRows
    public void UpdateScore()
    {
        if (scoreRows > 0)
        {
            score += (int)Math.Pow((double)scoreRows, (double)2) * 10 * level;
        }
        scoreRows = 0;
    }

    //  update het level op basis van hoeveel rijen er gescoord zijn
    public void UpdateLevel(int levelRows)
    {
        if (levelRows >= previousLevelRows + 10)
        {
            previousLevelRows = levelRows;
            level += 1;
        }
    }

    // teken de score en het level
    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.DrawString(scoreDisplay, "Score: " + score, scorePos, textColor);
        spriteBatch.DrawString(scoreDisplay, "Level: " + level, levelPos, textColor);
    }
}


