using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AzulEngine.EngineUtils;

namespace AzulEngine.SpriteEngine
{
    /// <summary>
    /// Clase que representa una escena de sprites y que contiene varias capas a dibujar
    /// </summary>
    public class SpriteScene : AbstractScene<SpriteLayer>
    {
        /// <summary>
        /// Inicializa una nueva instancia de la clase AzulEngine.SpriteEngine.SpriteScene que recibe
        /// como parametros una colección de capas
        /// </summary>
        /// <param name="layers">Colección de capas de index AzulEngine.SpriteEngine.SpriteLayer</param>
        public SpriteScene(List<SpriteLayer> layers)
            :base(layers)
        { }

        /// <summary>
        /// Inicializa una nueva instancia de la clase AzulEngine.SpriteEngine.SpriteScene  que no recibe parametros
        /// </summary>
        public SpriteScene()
            : this(new List<SpriteLayer>())
        { }
    }
}
