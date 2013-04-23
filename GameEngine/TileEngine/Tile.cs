using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
