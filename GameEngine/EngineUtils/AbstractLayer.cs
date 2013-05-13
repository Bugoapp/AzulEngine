//<AzulEngine - AzulEngine - Game engine for monogame>
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

namespace AzulEngine.EngineUtils
{
    /// <summary>
    /// Clase abstracta que representa una capa
    /// </summary>
    public abstract class AbstractLayer
    {
        /// <summary>
        /// Obtiene o establece la visibilidad de la capa
        /// </summary>
        public virtual Boolean Visible { get; set; }

        /// <summary>
        /// Obtiene o establece la posición de la capa
        /// </summary>
        public virtual Vector2 Position { get; set; }

        /// <summary>
        /// Obtiene o establece la escala de la capa
        /// </summary>
        public virtual Vector2 ZoomScale { get; set; }

        /// <summary>
        /// Obtiene o establece la velocidad de desplazamiento de la capa
        /// </summary>
        public virtual Vector2 Velocity { get; set; }

        private float transparency;
        /// <summary>
        /// Obtiene o establece la transparencia de la capa
        /// </summary>
        public virtual float Transparency
        {
            get { return this.transparency; }

            set
            {
                transparency = MathHelper.Clamp(value, 0, 1f);
            }
        }

        private Vector2 origin;
        /// <summary>
        /// Obtiene el origen de la capa
        /// </summary>
        public virtual  Vector2 Origin
        {
            get { return origin; }
        }

        private bool cameraIndependent;
        /// <summary>
        /// Obtiene la bandera que indica si la capa es independiente del movimiento de la cámara
        /// </summary>
        public virtual bool CameraIndependent
        {
            get { return cameraIndependent; }
        }

        private LayerMovementDirection direction;
        /// <summary>
        /// Obtiene la dirección de movimiento de la capa
        /// </summary>
        public virtual LayerMovementDirection Direction
        {
            get { return direction; }
        }

        /// <summary>
        /// Constructor de la clase abstracta AzulEngine.TileEngine.AbstractLayer que permite
        /// crear una instancia completa con transparencia, visibilidad, posición,escala,velocidad, independencia de cámara y dirección de movimiento
        /// </summary>
        /// <param name="transparency">Transparencia de la capa</param>
        /// <param name="visible">Visibilidad de la capa</param>
        /// <param name="position">Posición de la capa</param>
        /// <param name="zoomScale">Escala inicial de la capa</param>
        /// <param name="velocity">Velocidad de desplazamiento de la capa</param>
        /// <param name="cameraIndependent">Indica si la capa es independiente del movimiento de la cámara</param>
        /// <param name="direction">Dirección de desplazamiento de la capa cuando esta es independiente de la cámara</param>
        public AbstractLayer(float transparency, Boolean visible, Vector2 position, Vector2 zoomScale, Vector2 velocity, bool cameraIndependent, LayerMovementDirection direction)
        {
            this.transparency = transparency;
            this.Visible = visible;
            this.Position = this.origin = position;
            this.ZoomScale = zoomScale;
            this.Velocity = velocity;
            this.cameraIndependent = cameraIndependent;
            this.direction = direction;
        }

        /// <summary>
        /// Constructor de la clase abstracta AzulEngine.TileEngine.AbstractLayer que permite
        /// crear una instancia con valores pore defecto
        /// </summary>
        public AbstractLayer()
            : this(1.0f, true, Vector2.Zero, Vector2.One, Vector2.One, false, LayerMovementDirection.None)
        { }


        /// <summary>
        /// Obtiene el Tamaño de la capa
        /// </summary>
        public abstract Point Size { get; }

        /// <summary>
        /// Obtiene el Tamaño de la capa con escala aplicada
        /// </summary>
        public abstract Vector2 ScaledSize { get; }

    }
}
