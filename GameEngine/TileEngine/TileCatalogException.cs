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

namespace AzulEngine.TileEngine
{
    /// <summary>
    /// Representa un error cuando se cargan texturas que tiene un tamaño que no coincide
    /// con los tamaños de baldosa especificados
    /// </summary>
    class TileCatalogException : Exception
    {
        /// <summary>
        /// Inicializa una nueva instancia de la clase AzulEngine.TileEngine.TileCatalogException
        /// </summary>
        public TileCatalogException()
            : base("El ancho y el alto seleccionados deben ser divisores del ancho y alto de la textura respectivamente")
        { }

    }
}
