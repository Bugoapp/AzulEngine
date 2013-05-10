using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace AzulEngine.TileEngine
{
    /// <summary>
    /// Clase que representa un mapa de baldosas
    /// </summary>
    public class TileMap
    {
        private Tile[][] map;
        /// <summary>
        /// Obtiene o establece un arreglo bidimensional de objetos GameEngine.TileEngine.Tile
        /// </summary>
        /// <returns>Retorna una instancia de un arreglo bidimensional de tipo GameEngine.TileEngine.Tile</returns>
        internal Tile[][] Map
        {
            get { return map; }
            set { map = value; }
        }

        /// <summary>
        /// Inicializa una nueva instancia de la clase GameEngine.TileEngine.TileMap que recibe
        /// los parametros de ancho y alto del mapa
        /// </summary>
        /// <param name="width">Cátalogo de baldosas</param>
        /// <param name="height">Mapa de baldosas</param>
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

        /// <summary>
        /// Inicializa una nueva instancia de la clase GameEngine.TileEngine.TileMap que recibe
        /// un arreglo bidimensional de tipo GameEngine.TileEngine.Tile
        /// </summary>
        /// <param name="map">arreglo bidimensional de tipo GameEngine.TileEngine.Tile</param>
        public TileMap(Tile[][] map)
        {
            this.map = map;
        }

        /// <summary>
        /// Metodo que establece una baldosa dentro de la colección de baldosas
        /// </summary>
        /// <param name="xPosition">Número de fila donde se posicionará la baldosa</param>
        /// <param name="yPosition">Número de columna donde se posicionará la baldosa</param>
        /// <param name="tile">Baldosa a insertar dentro de la colección</param>
        public void SetTile(int xPosition, int yPosition, Tile tile)
        {
            this.map[xPosition][yPosition] = tile;
        }

        /// <summary>
        /// Metodo que obtiene una baldosa dentro de la colección de baldosas
        /// </summary>
        /// <param name="xPosition">Número de fila donde se posiciona la baldosa</param>
        /// <param name="yPosition">Número de columna donde se posiciona la baldosa</param>
        /// <returns>Retorna una instancia de tipo GameEngine.TileEngine.Tile</returns>
        public Tile GetTile(int xPosition, int yPosition)
        {
            return this.map[xPosition][yPosition];
        }

        
    }
}
