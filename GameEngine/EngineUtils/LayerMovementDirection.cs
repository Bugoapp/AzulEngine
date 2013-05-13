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

namespace AzulEngine.EngineUtils
{
    /// <summary>
    /// Enumeración que Representa las direcciones de movimiento de una capa de baldosas
    /// </summary>
    public enum LayerMovementDirection
    {
        /// <summary>
        /// No tiene movimiento
        /// </summary>
        None = 0,
        /// <summary>
        /// Movimiento hacia arriba
        /// </summary>
        Up = 1,
        /// <summary>
        /// Movimiento hacia abajo
        /// </summary>
        Down = 2,
        /// <summary>
        /// Movimiento hacia la izquierda
        /// </summary>
        /// 
        Left = 3,
        /// <summary>
        /// Movimiento hacia la derecha
        /// </summary>
        Right = 4,
        /// <summary>
        /// Movimiento hacia la arriba y la izquierda
        /// </summary>
        UpperLeft = 5,
        /// <summary>
        /// Movimiento hacia la arriba y la derecha
        /// </summary>
        UpperRight = 6,
        /// <summary>
        /// Movimiento hacia la abajo y la izquierda
        /// </summary>
        LowerLeft = 7,
        /// <summary>
        /// Movimiento hacia la abajo y la derecha
        /// </summary>
        LowerRight = 8
    }
}
