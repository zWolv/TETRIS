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



    public class L : Squares
    {


        public L()
        {

        }
    }

    public class J : Squares
    {

       public J()
        {

        }
    }


    public class O : Squares
    {

        public O()
        {

        }

    }

    public class I : Squares
    {

        public I()
        {

        }

    }


}
