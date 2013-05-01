using System;
using System.Collections.Generic;
//class
namespace GameEngine.TileEngine
{
    public class Tile
    {
        public Int32 Index { get; set; }

        public Tile(Int32 tipo)
        {
            this.Index = tipo;
        }
    }
}
