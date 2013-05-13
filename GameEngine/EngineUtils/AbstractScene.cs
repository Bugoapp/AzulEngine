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
    /// Clase abstracta que representa una escena y que contiene varias capas a dibujar
    /// </summary>
    /// <typeparam name="T">Representa el tipo de la capa a utilizar</typeparam>
    public class AbstractScene<T>
    {

        protected List<T> layers;
        /// <summary>
        /// Obtiene una lista generica que contiene las capas de la escena
        /// </summary>
        public virtual List<T> Layers
        {
            get { return layers; }
        }

        /// <summary>
        /// Constructor de la clase abstracta AzulEngine.EngineUtils.AbstractScene que recibe
        /// como parametros una colección de capas
        /// </summary>
        /// <param name="layers">Colección de capas de tipo T</param>
        public AbstractScene(List<T> layers)
        {
            this.layers = layers;
        }

        /// <summary>
        /// Constructor de la clase abstracta AzulEngine.EngineUtils.AbstractScene que no recibe parametros
        /// </summary>
        public AbstractScene()
            : this(new List<T>())
        { }


        /// <summary>
        /// Metodo que inserta una capa dentro de la colección de capas
        /// </summary>
        /// <param name="layer">Capa a insertar</param>
        public void AddLayer(T layer)
        {
            this.layers.Add(layer);
        }

        /// <summary>
        /// Metodo que obtiene una capa desde la colección de capas
        /// </summary>
        /// <param name="index">Indice de la capa a obtener</param>
        /// <returns>Retorna una instancia de tipo T</returns>
        public T GetLayer(int index)
        {
            return this.layers[index];
        }
    }
}
