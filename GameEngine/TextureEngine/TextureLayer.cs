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
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using AzulEngine.TileEngine;
using AzulEngine.EngineUtils;

namespace AzulEngine.TextureEngine 
{
    /// <summary>
    /// Clase que representa una capa compuesta de una sola textura
    /// </summary>
    public class TextureLayer : AbstractLayer
    {
        /// <summary>
        /// Obtiene o establece la textura de la textura
        /// </summary>
        public Texture2D Texture { get; set; }

        /// <summary>
        /// Inicializa una nueva instancia de la clase AzulEngine.TileEngine.TextureLayer que permite
        /// crear una instancia completa con transparencia, visibilidad, posición,escala,velocidad, independencia de cámara y dirección de movimiento
        /// </summary>
        /// <param name="texture">Textura de la capa</param>
        /// <param name="transparency">Transparencia de la capa</param>
        /// <param name="visible">Visibilidad de la capa</param>
        /// <param name="position">Posición de la capa</param>
        /// <param name="zoomScale">Escala inicial de la capa</param>
        /// <param name="velocity">Velocidad de desplazamiento de la capa</param>
        /// <param name="cameraIndependent">Indica si la capa es independiente del movimiento de la cámara</param>
        /// <param name="direction">Dirección de desplazamiento de la capa cuando esta es independiente de la cámara</param>
        public TextureLayer(Texture2D texture, float transparency, Boolean visible, Vector2 position, Vector2 zoomScale, Vector2 velocity, bool cameraIndependent, LayerMovementDirection direction)
            : base(transparency, visible, position, zoomScale, velocity, cameraIndependent, direction)
        {
            this.Texture = texture;
        }

        /// <summary>
        /// Inicializa una nueva instancia de la clase AzulEngine.TileEngine.TextureLayer que permite
        /// crear una instancia completa con sus valores por defecto
        /// </summary>
        /// <param name="texture">Textura de la capa</param>
        public TextureLayer(Texture2D texture)
            : this(texture, 1.0f, true, Vector2.Zero, Vector2.One, Vector2.One, false, LayerMovementDirection.None)
        { }

        /// <summary>
        /// Obtiene el Tamaño de la textura
        /// </summary>
        public override Point Size
        {
            get
            {
                int width = this.Texture.Width;
                int height = this.Texture.Height;
                return new Point(width, height);
            }
        }

        /// <summary>
        /// Obtiene el Tamaño de la textura con escala aplicada
        /// </summary>
        public override Vector2 ScaledSize
        {
            get
            {
                Point size = this.Size;
                float width = size.X * this.ScaledSize.X;
                float height = size.Y * this.ScaledSize.Y;
                return new Vector2(width, height);
            }
        }
    }
}
