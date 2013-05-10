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

namespace AzulEngine.CameraEngine
{
    /// <summary>
    /// Clase que representa una camara de juego
    /// </summary>
    public class Camera2D
    {
        /// <summary>
        /// Obtiene o establece la velocidad de la cámara
        /// </summary>
        public Vector2 Velocity { get; set; }

        /// <summary>
        /// Obtiene o establece la posición cámara
        /// </summary>
        public Vector2 Position { get; set; }

        /// <summary>
        /// Obtiene o establece si la cámara ha cambiado de posición
        /// </summary>
        public bool Changed { get; set; }

        /// <summary>
        /// Obtiene o establece el zoom de la cámara
        /// </summary>
        public Vector2 Zoom { get; set; }

        /// <summary>
        /// Inicializa una nueva instancia de la clase AzulEngine.CameraEngine
        /// </summary>
        /// <param name="position">Posición de la cámara</param>
        /// <param name="velocity">Velocidad de la cámara</param>
        /// <param name="zoom">Zoom de la cámara</param>
        public Camera2D(Vector2 position, Vector2 velocity, Vector2 zoom)
        {
            this.Position = position;
            this.Velocity = velocity;
            this.Zoom = zoom;
            this.Changed = false;
        }

        /// <summary>
        /// Inicializa una nueva instancia de la clase AzulEngine.CameraEngine sin parametros
        /// </summary>
        public Camera2D()
            : this(Vector2.Zero, Vector2.One, Vector2.One)
        {  }
    }
}
