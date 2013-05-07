using System;
using System.Collections.Generic;

namespace GameEngine.TileEngine
{
    /// <summary>
    /// Representa una baldosa de una capa de baldosas
    /// </summary>
    public class Tile
    {
        /// <summary>
        /// Obtiene o establece el índice de la baldosa dentro de la categoría de baldosas.
        /// </summary>
        public Int32 Index { get; set; }

        /// <summary>
        /// Inicializa una nueva instancia de la clase GameEngine.TileEngine.Tile 
        /// con el patrón especificado que indica el índice de la baldosa.
        /// </summary>
        /// <param name="tipo">Provides a snapshot of timing values.</param>
        public Tile(Int32 tipo)
        {
            this.Index = tipo;
        }
    }
}
