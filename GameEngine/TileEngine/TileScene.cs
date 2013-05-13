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
using AzulEngine.EngineUtils;

namespace AzulEngine.TileEngine
{
    /// <summary>
    /// Clase que representa una escena de baldosas y que contiene varias capas a dibujar
    /// </summary>
    public class TileScene : AbstractScene<TileLayer>
    {
        /// <summary>
        /// Inicializa una nueva instancia de la clase AzulEngine.TileEngine.TileScene que recibe
        /// como parametros una colección de capas
        /// </summary>
        /// <param name="layers">Colección de capas de tipo AzulEngine.TileEngine.TileLayer</param>
        public TileScene(List<TileLayer> layers)
            :base(layers)
        { }

        /// <summary>
        /// Inicializa una nueva instancia de la clase AzulEngine.TileEngine.TileScene que no recibe parametros
        /// </summary>
        public TileScene()
            :this(new List<TileLayer>())
        { }


    }
}
