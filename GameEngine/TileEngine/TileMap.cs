//<Game engine for monogame>
//Copyright (C) <2013>

//This program is free software: you can redistribute it and/or modify
//it under the terms of the GNU General Public License as published by
//the Free Software Foundation, either version 3 of the License, or
//(at your option) any later version.

//This program is distributed in the hope that it will be useful,
//but WITHOUT ANY WARRANTY; without even the implied warranty of
//MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//GNU General Public License for more details.

//You should have received a copy of the GNU General Public License
//along with this program.  If not, see <http://www.gnu.org/licenses/>.

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
        /// Obtiene o establece un arreglo bidimensional de objetos AzulEngine.TileEngine.Tile
        /// </summary>
        /// <returns>Retorna una instancia de un arreglo bidimensional de tipo AzulEngine.TileEngine.Tile</returns>
        internal Tile[][] Map
        {
            get { return map; }
            set { map = value; }
        }

        /// <summary>
        /// Inicializa una nueva instancia de la clase AzulEngine.TileEngine.TileMap que recibe
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
        /// Inicializa una nueva instancia de la clase AzulEngine.TileEngine.TileMap que recibe
        /// un arreglo bidimensional de tipo AzulEngine.TileEngine.Tile
        /// </summary>
        /// <param name="map">arreglo bidimensional de tipo AzulEngine.TileEngine.Tile</param>
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
        /// <returns>Retorna una instancia de tipo AzulEngine.TileEngine.Tile</returns>
        public Tile GetTile(int xPosition, int yPosition)
        {
            return this.map[xPosition][yPosition];
        }

        
    }
}
