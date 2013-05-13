using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AzulEngine.EngineUtils
{
    /// <summary>
    /// Enumeración que representa las anclas para los componentes de texturas
    /// </summary>
    public enum Anchor
    {
        /// <summary>
        /// Sin anclaje
        /// </summary>  
        None = 0,
        /// <summary>
        /// Anclado arriba y a la izquierda
        /// </summary>        
        UpperLeft = 1,
        /// <summary>
        /// Anclado arriba y a la derecha
        /// </summary>  
        UpperRight = 2,
        /// <summary>
        /// Anclado abajo y a la izquierda
        /// </summary>  
        LowerLeft = 3,
        /// <summary>
        /// Anclado abajo y a la derecha
        /// </summary>  
        LowerRight = 4
    }
}
