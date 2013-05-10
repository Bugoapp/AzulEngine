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
        /// Inicializa una nueva instancia de la clase GameEngine.TileEngine.TileCatalogException
        /// </summary>
        public TileCatalogException()
            : base("El ancho y el alto seleccionados deben ser divisores del ancho y alto de la textura respectivamente")
        { }

    }
}
