using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Variations
{
    public class Squares
    {
        static Random random = new Random();
        Color color;
        public Squares()
        {

        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {

        }
    }



    public class L : Squares
    {
        bool[,] layoutL = new bool[,] {
            {false, true, false, false},
            {false, true, false, false},
            {false, true, false, false},
            {false, true, true, false}
        };

        public L()
        {
        }
        //color = new Color.Orange
    }

    public class J : Squares
    {
        bool[,] layoutJ = new bool[,]
        {
            {false, false, true, false},
            {false, false, true, false},
            {false, false, true, false},
            {false, true, true, false}
        };

    public J()
        {

        }
        //color = new Color.DarkBlue;

    }


    public class O : Squares
    {
        bool[,] layoutO = new bool[,]
        {
            {false, false, false, false},
            {false, true, true, false},
            {false, true, true, false},
            {false, false, false, false}
        };

        public O()
        {

        }

        // color = new Color.Yellow;
    }

    public class I : Squares
    {
        bool[,] layoutI = new bool[,]
        {
            {false, true, false, false},
            {false, true, false, false},
            {false, true, false, false},
            {false, true, false, false}
        };

        public I()
        {

        }
        // color = new Color.LightBlue;
    }

    public class S : Squares
    {
        bool[,] layoutS = new bool[,]
        {
            {false, false, false, false},
            {false, false, true, true},
            {true, true, false, false},
            {false, false, false, false}
        };

        public S()
        {

        }
        //color = new Color.LightGreen;

    }

    public class Z : Squares
    {
        bool[,] layoutZ = new bool[,]
        {
            {false, false, false, false},
            {true, true, false, false},
            {false, false, true, true},
            {false, false, false, false}
        };

        public Z()
        {

        }
        // color = new Color.Red;

    }

    public class T : Squares
    {
        bool[,] layoutT = new bool[,]
        {
            {false, false, false, false},
            {false, true, true, true},
            {false, false, true, false},
            {false, false, false, false},
        };

        public T()
        {

        }
        // color = new Color.Purple;

    }

    public class U : Squares 
    {
        bool[,] layoutU = new bool[,]
        {
            {false, false, false, false},
            {true, false, true, false},
            {true, true, true, false},
            {false, false, false, false}
        };

        public U()
        {

        }
    }
}
