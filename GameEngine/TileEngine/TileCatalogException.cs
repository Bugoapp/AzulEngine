using System;
using System.Collections.Generic;

namespace GameEngine.TileEngine
{
    class TileCatalogException : Exception
    {
        public TileCatalogException()
            : base("El ancho y el alto seleccionados deben ser divisores del ancho y alto de la textura respectivamente")
        { }

    }
}
