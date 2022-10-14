using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;



    public class BlockVariations
    {
        static Random random = new Random();
        Color color;

        List<BlockVariations> variations  = new List<BlockVariations>();
        public BlockVariations()
        {

        }

        public List<BlockVariations> blockList
        {
            get
            {
                return variations;
            }
        }

        public virtual bool layout(int x, int y)
        {
            return false;
        }
        public void addBlocks(string blockType) {

            switch(blockType)
            {
                case ("L"):
                    variations.Add(new L());
                    break;
                case ("J"):
                    variations.Add(new J());
                    break;
                case ("O"):
                    variations.Add(new O());
                    break;
                case ("T"):
                    variations.Add(new T());
                    break;
                case ("S"):
                    variations.Add(new S());
                    break;
                case ("Z"):
                    variations.Add(new Z());
                    break;
                case ("I"):
                    variations.Add(new I());
                    break;
                case ("U"):
                    variations.Add(new U());
                    break;
                default:
                    break;
            }
            
            
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {

        }
    }



    public class L : BlockVariations
    {
        bool[,] layoutL = new bool[,] {
            {false, true, false, false},
            {false, true, false, false},
            {false, true, true, false},
            {false, false, false, false}
        };

        public L()
        {
        }
        //color = new Color.Orange

        public override bool layout(int x, int y)
        {
            return layoutL[y,x];
        }
        
    }

    public class J : BlockVariations
    {
        bool[,] layoutJ = new bool[,]
{
            {false, false, true, false},
            {false, false, true, false},
            {false, true, true, false},
            {false, false, false, false}
};

        public J()
        {

        }
        //color = new Color.DarkBlue;



        public override bool layout(int x, int y)
        {
            return layoutJ[y, x];
        }
    }


    public class O : BlockVariations
    {
        bool[,] layoutO = new bool[,]
        {
            {false, true, true, false},
            {false, true, true, false},
            {false, false, false, false},
            {false, false, false, false}
        };

        public O()
        {

        }
        // color = new Color.Yellow;

        

        public override bool layout(int x, int y)
        {
            return layoutO[y, x];
        }
    }

    public class I : BlockVariations
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

        public override bool layout(int x, int y)
        {
            return layoutI[y, x];
        }
    }

    public class S : BlockVariations
    {

        bool[,] layoutS = new bool[,]
        {
            {false, false, true, true},
            {false, true, true, false},
            {false, false, false, false},
            {false, false, false, false}
        };

        public S()
        {

        }
        //color = new Color.LightGreen;

        public override bool layout(int x, int y)
        {
            return layoutS[y, x];
        }
    }

    public class Z : BlockVariations
    {
        bool[,] layoutZ = new bool[,]
        {
            {true, true, false, false},
            {false, true, true, false},
            {false, false, false, false},
            {false, false, false, false}
        };

        public Z()
        {

        }
        // color = new Color.Red;

        public override bool layout(int x, int y)
        {
            return layoutZ[y, x];
        }
    }

    public class T : BlockVariations
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

        public override bool layout(int x, int y)
        {
            return layoutT[y, x];
        }
    }

    public class U : BlockVariations 
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

        public override bool layout(int x, int y)
        {
            return layoutU[y, x];
        }
    }

