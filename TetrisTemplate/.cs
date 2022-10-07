using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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



    public class LShape : Squares
    {


        public LShape()
        {

        }
    }

    public class InvertedLShape : Squares
    {

       public InvertedLShape()
        {

        }
    }


    public class SquareShape : Squares
    {

        public SquareShape()
        {

        }

    }

    public class SquareLine : Squares
    {

        public SquareLine()
        {

        }

    }


}
