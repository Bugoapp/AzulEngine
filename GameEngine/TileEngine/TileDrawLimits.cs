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
        /// Inicializa una nueva instancia de la clase GameEngine.TileEngine.TileDrawLimits que recibe como
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
        /// Inicializa una nueva instancia de la clase GameEngine.TileEngine.TileDrawLimits 
        /// </summary>
        public TileDrawLimits()
            :this(0,0,0,0)
        {}
    }
}
