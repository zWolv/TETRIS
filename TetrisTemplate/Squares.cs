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
        

    public J()
        {

        }
        //color = new Color.DarkBlue;

        bool[,] layoutJ = new bool[,]
        {
            {false, false, true, false},
            {false, false, true, false},
            {false, false, true, false},
            {false, true, true, false}
        };
    }


    public class O : Squares
    {
       

        public O()
        {

        }
        // color = new Color.Yellow;

        bool[,] layoutO = new bool[,]
       {
            {false, false, false, false},
            {false, true, true, false},
            {false, true, true, false},
            {false, false, false, false}
       };
    }

    public class I : Squares
    {
        

        public I()
        {

        }
        // color = new Color.LightBlue;

        bool[,] layoutI = new bool[,]
        {
            {false, true, false, false},
            {false, true, false, false},
            {false, true, false, false},
            {false, true, false, false}
        };
    }

    public class S : Squares
    {
        

        public S()
        {

        }
        //color = new Color.LightGreen;

        bool[,] layoutS = new bool[,]
        {
            {false, false, false, false},
            {false, false, true, true},
            {true, true, false, false},
            {false, false, false, false}
        };
    }

    public class Z : Squares
    {

        public Z()
        {

        }
        // color = new Color.Red;

        bool[,] layoutZ = new bool[,]
        {
            {false, false, false, false},
            {true, true, false, false},
            {false, false, true, true},
            {false, false, false, false}
        };
    }

    public class T : Squares
    {
        

        public T()
        {

        }
        // color = new Color.Purple;

        bool[,] layoutT = new bool[,]
        {
            {false, false, false, false},
            {false, true, true, true},
            {false, false, true, false},
            {false, false, false, false},
        };
    }

    public class U : Squares 
    {
        
        public U()
        {

        }

        bool[,] layoutU = new bool[,]
        {
            {false, false, false, false},
            {true, false, true, false},
            {true, true, true, false},
            {false, false, false, false}
        };
    }
}
