//<AzulEngine - Game engine for monogame>
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
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace AzulEngine.TileEngine
{

    /// <summary>
    /// Representa una catálogo de baldosas 
    /// </summary>
    public class TileCatalog
    {
        private Point size;
        /// <summary>
        /// Obtiene o establece el tamaño de una baldosa individual
        /// </summary>
        public Point Size
        {
            get { return size; }
            set { size = value; }
        }

        private Dictionary<int, Rectangle> tilePositions;
        /// <summary>
        /// Obtiene una colección de los rectángulos que prepresentan la posición de cada baldosa del catálogo
        /// </summary>
        public Dictionary<int, Rectangle> TilePositions
        {
            get { return tilePositions; }
        }

        private Texture2D texture;
        /// <summary>
        /// Obtiene la textura que se utiliza para generar el catálogo
        /// </summary>
        public Texture2D Texture
        {
            get { return texture; }
        }

        /// <summary>
        /// Inicializa una nueva instancia de la clase AzulEngine.TileEngine.TileCatalog que recibe como
        /// parametros la textura, el ancho y alto de la baldosa.
        /// </summary>
        /// <param name="texture">Textura que define el catálogo</param>
        /// <param name="width">Ancho de una baldosa</param>
        /// <param name="height">Alto de una baldosa</param>
        public TileCatalog(Texture2D texture, int width, int height)
        {
            this.texture = texture;
            this.tilePositions = new Dictionary<int, Rectangle>();
            CalculateTilePositions(texture, width, height, ref this.tilePositions);
            this.Size = new Point(width, height);
        }

        /// <summary>
        /// Inicializa una nueva instancia de la clase AzulEngine.TileEngine.TileCatalog que recibe como
        /// parametros la textura, una colección de rectángulos,el ancho y alto de la baldosa.
        /// </summary>
        /// <param name="texture">Textura que define el catálogo</param>
        /// <param name="tilePositions">Diccionario que almacena el rectángulo de posición de cada baldosa</param>
        /// <param name="width">Ancho de una baldosa</param>
        /// <param name="height">Alto de una baldosa</param>
        public TileCatalog(Texture2D texture, Dictionary<int, Rectangle> tilePositions, int width, int height)
        {
            this.texture = texture;
            this.tilePositions = tilePositions;
            this.Size = new Point(width, height);
        }

        /// <summary>
        /// Obtiene el rectángulo de la posicion de una baldosa dentro de la textura 
        /// dada una referencia de la misma
        /// </summary>
        /// <param name="tile">Referencia de la baldosa de la que se obtendra la posición </param>
        /// <param name="tilePosition">Valor de salida que contiene el rectángulo de posición</param>
        public void GetTilePosition(Tile tile, out Rectangle tilePosition)
        {
            tilePosition = TilePositions[tile.Index];
        }


        /// <summary>
        /// Agrega o modifica una nuevo índice a la colección de rectángulos de posición de baldosas
        /// </summary>
        /// <param name="index">Indice de la baldosa</param>
        /// <param name="tilePosition">Rectángulo que representa la posición de la baldosa</param>
        public void AddTilePosition(int index, Rectangle tilePosition)
        {
            this.TilePositions.Add(index, tilePosition);
        }


        /// <summary>
        /// Calcula las posiciones de las baldosas y las agrega dentro de la colecciones de rectángulos de posición, 
        /// evitando crear la colección de manera manual.
        /// </summary>
        /// <param name="texture">Textura de la que se calculan los rectángulos de posición</param>
        /// <param name="width">Ancho de la baldosa</param>
        /// <param name="height">Alto de la baldosa</param>
        /// <param name="tilePositions">referencia de la colección de rectangulos de posición a devolver</param>
        public static void CalculateTilePositions(Texture2D texture, int width, int height, ref Dictionary<int, Rectangle> tilePositions)
        {
            //verificar si el ancho y alto son divisores del ancho y alto de las texturas
            if (texture.Width % width == 0 && texture.Height % height == 0)
            {
                int liColumns = texture.Width / width;
                int liRows = texture.Height / height;
                int liPosition = 0;
                //empezar a cortar la textura para crear rectángulos
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
