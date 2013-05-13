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
using AzulEngine.EngineUtils;

namespace AzulEngine.TextureEngine
{
    /// <summary>
    /// Clase que representa una escena de texturas y que contiene varias capas a dibujar
    /// </summary>
    public class TextureScene : AbstractScene<TextureLayer>
    {
        /// <summary>
        /// Inicializa una nueva instancia de la clase AzulEngine.TextureEngine.TextureScene que recibe
        /// como parametros una colección de capas
        /// </summary>
        /// <param name="layers">Colección de capas de tipo AzulEngine.TextureEngine.TextureLayer</param>
        public TextureScene(List<TextureLayer> layers)
            :base(layers)
        {}

        /// <summary>
        /// Inicializa una nueva instancia de la clase AzulEngine.TextureEngine.TextureLayer que no recibe parametros
        /// </summary>
        public TextureScene()
            : this(new List<TextureLayer>())
        { }

    }
}
