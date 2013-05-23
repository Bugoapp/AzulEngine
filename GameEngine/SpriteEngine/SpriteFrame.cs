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

namespace AzulEngine.SpriteEngine
{
    /// <summary>
    /// Clase que representa una cuadro de un frame
    /// </summary>
    public class SpriteFrame
    {
        /// <summary>
        /// Obtiene o establece el índice del cuadro dentro de la categoría de sprites.
        /// </summary>
        public Int32 Index { get; set; }

        /// <summary>
        /// Inicializa una nueva instancia de la clase AzulEngine.SpriteEngine.SpriteFrame
        /// con el patrón especificado que indica el índice del cuadro.
        /// </summary>
        /// <param name="type">Provides a snapshot of timing values.</param>
        public SpriteFrame(Int32 type)
        {
            this.Index = type;
        }
    }
}
