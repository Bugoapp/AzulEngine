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
    /// Clase que representa los limites de una capa de baldosas a dibujar
    /// </summary>
    public class TileDrawLimits
    {
        /// <summary>
        /// Límite mínimo en el eje x
        /// </summary>
        public int XMin { get; set; }
        /// <summary>
        /// Límite mínimo en el eje y
        /// </summary>
        public int YMin { get; set; }
        /// <summary>
        /// Límite máximo en el eje x
        /// </summary>
        public int XMax { get; set; }
        /// <summary>
        /// Límite máximo en el eje y
        /// </summary>
        public int YMax { get; set; }

        /// <summary>
        /// Inicializa una nueva instancia de la clase AzulEngine.TileEngine.TileDrawLimits que recibe como
        /// parametros los limite mínimo y máximo de los eje x y y.
        /// </summary>
        /// <param name="xMin">Límite mínimo en el eje x</param>
        /// <param name="xMax">Límite máximo en el eje x</param>
        /// <param name="yMin">Límite mínimo en el eje y</param>
        /// <param name="yMax">Límite máximo en el eje y</param>
        public TileDrawLimits(int xMin, int xMax, int yMin, int yMax)
        {
            this.XMin = xMin;
            this.YMin = yMin;
            this.XMax = xMax;
            this.YMax = yMax;
        }

        /// <summary>
        /// Inicializa una nueva instancia de la clase AzulEngine.TileEngine.TileDrawLimits 
        /// </summary>
        public TileDrawLimits()
            :this(0,0,0,0)
        {}
    }
}
