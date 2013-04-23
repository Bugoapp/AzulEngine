using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace GameEngine.TileEngine
{
    public class TileMap
    {
        private Tile[][] map;
        public TileMap(int width, int height)
        {
            this.map = new Tile[width][];
            for (int i = 0; i < width; i++)
            {
                this.map[i] = new Tile[height];
                for (int j = 0; j < height; j++)
                {
                    this.map[i][j] = new Tile(0);
                }
            }
        }

        public TileMap(Tile[][] map)
        {

            this.map = map;
        }

        public void SetTile(int xPosition, int yPosition, Tile tile)
        {
            this.map[xPosition][yPosition] = tile;
        }

        public Tile GetTile(int xPosition, int yPosition)
        {
            return this.map[xPosition][yPosition];
        }

        public Point GetLenght()
        {
            int width = this.map.GetLength(0);
            int height = this.map[0].Length;
            return new Point(width, height);
        }

    }
}
