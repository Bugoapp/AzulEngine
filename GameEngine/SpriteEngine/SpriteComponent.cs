using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AzulEngine.CameraEngine;
using Microsoft.Xna.Framework;

namespace AzulEngine.SpriteEngine
{
    public class SpriteComponent
    {
        protected Camera2D camera;
        /// <summary>
        /// Obtiene la cámara de visualización
        /// </summary>
        public virtual Camera2D Camera
        {
            get { return camera; }
        }



        protected Vector2 baseScreenSize;
        /// <summary>
        /// Obtiene o establece la resolución base en un sistema de resolución independiente
        /// </summary>
        public virtual Vector2 BaseScreenSize
        {
            get { return baseScreenSize; }
            set { baseScreenSize = value; }
        }


        /// <summary>
        /// Obtiene o establece si la resolución interna es independiente de la resolución del sistema
        /// </summary>
        public virtual Boolean ResultionIndependent { get; set; }
    }
}
