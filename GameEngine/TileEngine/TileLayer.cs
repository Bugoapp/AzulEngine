using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

//introducir zoom en la camara y no en la capa
//introducir bandera que indique usar camara o use movimiento independiente

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
        public Vector2 Velocity { get; set; }
        public float transparency;
        public float Transparency
        {
            get { return this.transparency; }

            set
            {
                transparency = MathHelper.Clamp(value, 0, 1f);
            }
        }
        private Vector2 origin;

        public Vector2 Origin
        {
            get { return origin; }
        }

        public TileLayer(TileCatalog tileCatalog, TileMap tileMap,float transparency, Boolean visible, Vector2 position, Vector2 zoom, Vector2 zoomScale, Vector2 velocity)
        {
            this.TileCatalog = tileCatalog;
            this.TileMap = tileMap;
            this.transparency = transparency;
            this.Visible = visible;
            this.Position = this.origin = position;
            this.Zoom = zoom;
            this.ZoomScale = zoomScale;
            this.Velocity = velocity;
        }

        public TileLayer(TileCatalog tileCatalog, TileMap tileMap)
            : this(tileCatalog, tileMap,1.0f, true, Vector2.Zero, Vector2.One, Vector2.One, Vector2.One)
        { }

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
