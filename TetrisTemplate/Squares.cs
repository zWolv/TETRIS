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


        public L()
        {

        }
        //color = new Color.Orange;

    }

    public class J : Squares
    {

       public J()
        {

        }
        //color = new Color.DarkBlue;

    }


    public class O : Squares
    {

        public O()
        {

        }

        // color = new Color.Yellow;
    }

    public class I : Squares
    {

        public I()
        {

        }
        // color = new Color.LightBlue;
    }

    public class S : Squares
    {
    
        public S()
        {

        }
        //color = new Color.LightGreen;

    }

    public class Z : Squares
    {

        public Z()
        {

        }
        // color = new Color.Red;

    }

    public class T : Squares
    {

        public T()
        {

        }
        // color = new Color.Purple;

    }
}
