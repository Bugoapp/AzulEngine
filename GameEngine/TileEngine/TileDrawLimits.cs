using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace GameEngine.TileEngine
{
    public class TileDrawLimits
    {
        public int XMin { get; set; }
        public int YMin { get; set; }
        public int XMax { get; set; }
        public int YMax { get; set; }

        public TileDrawLimits(int xMin, int xMax, int yMin, int yMax)
        {
            this.XMin = xMin;
            this.YMin = yMin;
            this.XMax = xMax;
            this.YMax = yMax;
        }

        public TileDrawLimits()
            :this(0,0,0,0)
        {}
    }
}
