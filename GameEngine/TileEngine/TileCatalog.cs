using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace GameEngine.TileEngine
{
    public class TileCatalog
    {

        private Point size;

        public Point Size
        {
            get { return size; }
            set { size = value; }
        }

        private Dictionary<int, Rectangle> cTilePositions;
        public Dictionary<int, Rectangle> TilePositions
        {
            get { return cTilePositions; }
        }

        private Texture2D cTexture;
        public Texture2D Texture
        {
            get { return cTexture; }
        }

        public TileCatalog(Texture2D texture, int width, int height)
        {
            this.cTexture = texture;
            this.cTilePositions = new Dictionary<int, Rectangle>();
            CalculateTilePositions(texture, width, height, ref this.cTilePositions);
            this.Size = new Point(width, height);
        }

        public TileCatalog(Texture2D texture, Dictionary<int, Rectangle> tilePositions, int width, int height)
        {
            this.cTexture = texture;
            this.cTilePositions = TilePositions;
            this.Size = new Point(width, height);
        }

        public void GetTilePosition(ref Tile tile, out Rectangle tilePosition)
        {
            tilePosition = TilePositions[tile.Index];
        }

        public void AddTilePosition(int index, Rectangle tilePosition)
        {
            this.TilePositions.Add(index, tilePosition);
        }

        public static void CalculateTilePositions(Texture2D texture, int width, int height, ref Dictionary<int, Rectangle> tilePositions)
        {
            //verificar si el ancho y alto son divisores del ancho y alto de las texturas
            if (texture.Width % width == 0 && texture.Height % height == 0)
            {
                int liColumns = texture.Width / width;
                int liRows = texture.Height / height;
                int liPosition = 0;
                //empezar a cortar la textura para crear rectangulos
                for (int i = 0; i < liRows; i++)
                {
                    for (int j = 0; j < liColumns; j++)
                    {
                        liPosition++;
                        tilePositions.Add(liPosition, new Rectangle(j * width,i * height, width, height));
                    }
                }
            }
            else
            {
                throw new TileCatalogException();
            }
        }
    }
}
