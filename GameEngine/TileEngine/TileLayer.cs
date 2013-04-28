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
        public Vector2 Zoom { get; set; }
        public Vector2 ZoomScale { get; set; }
        public TileLayer(TileCatalog tileCatalog, TileMap tileMap, Boolean visible, Vector2 position, Vector2 zoom, Vector2 zoomScale)
        {
            this.TileCatalog = tileCatalog;
            this.TileMap = tileMap;
            this.Visible = visible;
            this.Position = position;
            this.Zoom = zoom;
            this.ZoomScale = zoomScale;
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

        public Vector2 ZoomedTileSize
        {
            get
            {
                float width = this.TileCatalog.Size.X * this.Zoom.X * this.ZoomScale.X;
                float height = this.TileCatalog.Size.Y * this.Zoom.Y * this.ZoomScale.Y;
                return new Vector2(width, height);
            }
        }

        public Vector2 ZoomedSize
        {
            get
            {
                Vector2 zoomedTileSize = this.ZoomedTileSize;
                Point lenght = this.Lenght;
                float width = lenght.X * zoomedTileSize.X;
                float height = lenght.Y * zoomedTileSize.Y;
                return new Vector2(width, height);
            }
        }

        public Point TileSize
        {
            get
            {
                return this.TileCatalog.Size;
            }
        }
    }
}
