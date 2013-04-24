using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace GameEngine.TileEngine
{
    public class TileLayer
    {
        public TileCatalog TileCatalog { get; set; }
        public TileMap TileMap { get; set; }
        public Boolean Visible {get; set;}
        public Vector2 Position {get; set;}


        public TileLayer(TileCatalog tileCatalog, TileMap tileMap, Boolean visible, Vector2 position)
        {
            this.TileCatalog = tileCatalog;
            this.TileMap = tileMap;
            this.Visible = visible;
            this.Position = position;
        }


        public Point Lenght
        {
            get{
                int width = this.TileMap.Map.GetLength(0);
                int height = this.TileMap.Map[0].Length;
                return new Point(width, height);           
            }

        }

        public Point Size
        {
            get{
                Point lenght = this.Lenght;
                int width = lenght.X * this.TileCatalog.Size.X;
                int height = lenght.Y * this.TileCatalog.Size.Y;
                return new Point(width, height);          
            }

        }

    }
}
