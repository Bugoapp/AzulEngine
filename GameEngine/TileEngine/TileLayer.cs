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
using Microsoft.Xna.Framework;
using AzulEngine.EngineUtils;

namespace AzulEngine.TileEngine
{
    /// <summary>
    /// Clase que representa una capa de baldosas
    /// </summary>
    public class TileLayer : AbstractLayer
    {
        /// <summary>
        /// Obtiene o establece el catálogo de baldosas
        /// </summary>
        public TileCatalog TileCatalog { get; set; }

        /// <summary>
        /// Obtiene o establece el mapa de baldosas
        /// </summary>
        public TileMap TileMap { get; set; }

        /// <summary>
        /// Inicializa una nueva instancia de la clase AzulEngine.TileEngine.TileLayer que permite
        /// crear una instancia completa con transparencia, visibilidad, posición, escala, velocidad, independencia de cámara y dirección de movimiento
        /// </summary>
        /// <param name="tileCatalog">Catálogo de baldosas</param>
        /// <param name="tileMap">Mapa de baldosas</param>
        /// <param name="transparency">Transparencia de la capa</param>
        /// <param name="visible">Visibilidad de la capa</param>
        /// <param name="position">Posición de la capa</param>
        /// <param name="zoomScale">Escala inicial de la capa</param>
        /// <param name="velocity">Velocidad de desplazamiento de la capa</param>
        /// <param name="cameraIndependent">Indica si la capa es independiente del movimiento de la cámara</param>
        /// <param name="direction">Dirección de desplazamiento de la capa cuando esta es independiente de la cámara</param>
        public TileLayer(TileCatalog tileCatalog, TileMap tileMap, float transparency, Boolean visible, Vector2 position, Vector2 zoomScale, Vector2 velocity, bool cameraIndependent, LayerMovementDirection direction)
            : base(transparency, visible, position, zoomScale, velocity, cameraIndependent, direction)
        {
            this.TileCatalog = tileCatalog;
            this.TileMap = tileMap;
        }

        /// <summary>
        /// Inicializa una nueva instancia de la clase AzulEngine.TileEngine.TileLayer que permite
        /// crear una instancia solo con un cátalogo y un mapa de baldosas
        /// </summary>
        /// <param name="tileCatalog">Cátalogo de baldosas</param>
        /// <param name="tileMap">Mapa de baldosas</param>
        public TileLayer(TileCatalog tileCatalog, TileMap tileMap)
            : this(tileCatalog, tileMap, 1.0f, true, Vector2.Zero, Vector2.One, Vector2.One, false, LayerMovementDirection.None)
        { }

        /// <summary>
        /// longitud de la capa definida en el número de baldosas en el eje x y y
        /// </summary>
        public Point Lenght
        {
            get{
                int width = this.TileMap.Map.GetLength(0);
                int height = this.TileMap.Map[0].Length;
                return new Point(width, height);           
            }
        }

        /// <summary>
        /// Obtiene el Tamaño de la capa
        /// </summary>
        public override Point Size
        {
            get{
                Point lenght = this.Lenght;
                int width = lenght.X * this.TileCatalog.Size.X;
                int height = lenght.Y * this.TileCatalog.Size.Y;
                return new Point(width, height);          
            }
        }

        /// <summary>
        /// Obtiene el Tamaño de la baldosa con escala aplicada
        /// </summary>
        public Vector2 ScaledTileSize
        {
            get
            {            
                float width = this.TileCatalog.Size.X  * this.ZoomScale.X;
                float height = this.TileCatalog.Size.Y * this.ZoomScale.Y;
                return new Vector2(width, height);
            }
        }
        
        /// <summary>
        /// Obtiene el Tamaño de la capa con escala aplicada
        /// </summary>
        public override Vector2 ScaledSize
        {
            get
            {
                Vector2 zoomedTileSize = this.ScaledTileSize;
                Point lenght = this.Lenght;
                float width = lenght.X * zoomedTileSize.X;
                float height = lenght.Y * zoomedTileSize.Y;
                return new Vector2(width, height);
            }
        }

        /// <summary>
        /// Obtiene el Tamaño de una baldosa individual.
        /// </summary>
        public Point TileSize
        {
            get
            {
                return this.TileCatalog.Size;
            }
        }
    }
}
