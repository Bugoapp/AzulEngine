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

namespace AzulEngine.TileEngine
{
    /// <summary>
    /// Clase que representa una escena de baldosas y que contiene varias capas a dibujar
    /// </summary>
    public class TileScene
    {

        private List<TileLayer> layers;
        /// <summary>
        /// Obtiene una lista generica que contiene las capas de la escena
        /// </summary>
        public List<TileLayer> Layers
        {
            get { return layers; }
        }

        /// <summary>
        /// Inicializa una nueva instancia de la clase AzulEngine.TileEngine.TileScene que recibe
        /// como parametros una colección de capas y la visibilidad de la escena
        /// </summary>
        /// <param name="layers">Cátalogo de baldosas</param>
        public TileScene(List<TileLayer> layers)
        {
            this.layers = layers;
        }

        /// <summary>
        /// Inicializa una nueva instancia de la clase AzulEngine.TileEngine.TileScene que no recibe parametros
        /// </summary>
        public TileScene()
            :this(new List<TileLayer>())
        { }

        /// <summary>
        /// Metodo que inserta una capa dentro de la colección de capas
        /// </summary>
        /// <param name="layer">Capa a insertar</param>
        public void AddLayer(TileLayer layer)
        {
            this.layers.Add(layer);
        }

        /// <summary>
        /// Metodo que obtiene una capa desde la colección de capas
        /// </summary>
        /// <param name="index">Indice de la capa a obtener</param>
        /// <returns>Retorna una instancia de tipo AzulEngine.TileEngine.Tile</returns>
        public TileLayer GetLayer(int index)
        {
            return this.layers[index];
        }


    }
}
