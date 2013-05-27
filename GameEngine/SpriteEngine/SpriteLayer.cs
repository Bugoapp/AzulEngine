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
using AzulEngine.EngineUtils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AzulEngine.SpriteEngine
{
    /// <summary>
    /// Clase que representa una capa compuesta de varias secuencias de sprites
    /// </summary>
    public class SpriteLayer : AbstractLayer
    {
        /// <summary>
        /// Obtiene o establece el catálogo de sprites
        /// </summary>
        public SpriteCatalog SpriteCatalog { get; set; }

        /// <summary>
        /// Obtiene o establece una colección de secuencias de cuadros
        /// </summary>
        public SpriteSequence[] SpriteSequences { get; set; }


        private int currentSequenceIndex;
        /// <summary>
        /// Obtiene o establece el indice de la secuencia actual a dibujar
        /// </summary>
        public int CurrentSequence {
            get
            {
                return this.currentSequenceIndex;
            }
            set
            {
                this.currentSequenceIndex = (int)MathHelper.Clamp(value, 0, this.SpriteSequences.Length);
            }
        }

        /// <summary>
        /// Obtiene o establece el ancla de la capa
        /// </summary>
        public Anchor Anchor { get; set; }


        /// <summary>
        /// Obtiene o establece el tiempo acumulado que se utiliza para calcular el tiempo de cambio de un cuadro en la secuencia.
        /// </summary>
        private float TotalElapsedTime; 

        /// <summary>
        /// Obtiene o establece un efecto de tipo Microsoft.Xna.Framework.Graphics.SpriteEffect, que 
        /// rota la imagen 180 Grados
        /// </summary>
        public SpriteEffects SpriteEffects;

        /// <summary>
        /// Inicializa una nueva instancia de la clase AzulEngine.SpriteEngine.SpriteLayer que permite
        /// crear una instancia completa con transparencia, visibilidad, posición,escala,velocidad, independencia de cámara y dirección de movimiento
        /// </summary>
        /// <param name="spriteCatalog">Catálogo de cuadros</param>
        /// <param name="spriteSecuence">Colección de secuencia de cuadros</param>
        /// <param name="transparency">Transparencia de la capa</param>
        /// <param name="visible">Visibilidad de la capa</param>
        /// <param name="position">Posición de la capa</param>
        /// <param name="zoomScale">Escala inicial de la capa</param>
        /// <param name="velocity">Velocidad de desplazamiento de la capa</param>
        /// <param name="spriteEffects">Efecto de rotación sobre el sprite</param>
        /// <param name="cameraIndependent">Indica si la capa es independiente del movimiento de la cámara</param>
        /// <param name="direction">Dirección de desplazamiento de la capa cuando esta es independiente de la cámara</param>
        public SpriteLayer(SpriteCatalog spriteCatalog, SpriteSequence[] spriteSecuence, float transparency, Boolean visible, Vector2 position, Vector2 zoomScale, Vector2 velocity, SpriteEffects spriteEffects, bool cameraIndependent, LayerMovementDirection direction)
            : base(transparency, visible, position, zoomScale, velocity, cameraIndependent, direction)
        {
            this.SpriteCatalog = spriteCatalog;
            this.SpriteSequences = spriteSecuence;
            this.Anchor = Anchor.None;
            this.SpriteEffects = spriteEffects;
            this.TotalElapsedTime = 0f;
        }

        /// <summary>
        /// Inicializa una nueva instancia de la clase AzulEngine.SpriteEngine.SpriteLayer que permite
        /// crear una instancia completa con transparencia, visibilidad, posición,escala,velocidad, independencia de cámara y un anclaje de eje
        /// </summary>
        /// <param name="spriteCatalog">Catálogo de cuadros</param>
        /// <param name="spriteSecuence">Colección de secuencia de cuadros</param>
        /// <param name="transparency">Transparencia de la capa</param>
        /// <param name="visible">Visibilidad de la capa</param>
        /// <param name="position">Posición de la capa</param>
        /// <param name="zoomScale">Escala inicial de la capa</param>
        /// <param name="velocity">Velocidad de desplazamiento de la capa</param>
        /// <param name="spriteEffects">Efecto de rotación sobre el sprite</param>
        /// <param name="cameraIndependent">Indica si la capa es independiente del movimiento de la cámara</param>
        /// <param name="anchor">Anclaje del sprite</param>
        public SpriteLayer(SpriteCatalog spriteCatalog, SpriteSequence[] spriteSecuence, float transparency, Boolean visible, Vector2 position, Vector2 zoomScale, Vector2 velocity, SpriteEffects spriteEffects, bool cameraIndependent, Anchor anchor)
            : base(transparency, visible, position, zoomScale, velocity, cameraIndependent, LayerMovementDirection.None)
        {
            this.SpriteCatalog = spriteCatalog;
            this.SpriteSequences = spriteSecuence;
            this.Anchor = anchor;
            this.SpriteEffects = spriteEffects;
            this.TotalElapsedTime = 0f;
        }

        /// <summary>
        /// Inicializa una nueva instancia de la clase AzulEngine.SpriteEngine.SpriteLayer que permite
        /// crear una instancia solo con un cátalogo y una colección de secuencias
        /// </summary>
        /// <param name="spriteCatalog">Cátalogo de cuadros </param>
        /// <param name="spriteSecuence">Secuencia de cuadros</param>
        public SpriteLayer(SpriteCatalog spriteCatalog, SpriteSequence[] spriteSecuence)
            : this(spriteCatalog, spriteSecuence, 1.0f, true, Vector2.Zero, Vector2.One, Vector2.One, SpriteEffects.None, false, LayerMovementDirection.None)
        {  }

        /// <summary>
        /// Obtiene el Tamaño de la capa
        /// </summary>
        public override Point Size
        {
            get
            {
                int width = this.SpriteCatalog.Size.X;
                int height = this.SpriteCatalog.Size.Y;
                return new Point(width, height);
            }
        }

        /// <summary>
        /// Obtiene el Tamaño de la capa con escala aplicada
        /// </summary>
        public override Vector2 ScaledSize
        {
            get
            {
                Point size = this.Size;
                float width = size.X * this.ZoomScale.X;
                float height = size.Y * this.ZoomScale.Y;
                return new Vector2(width, height);
            }
        }

        public SpriteSequence GetCurrentSequence()
        {
            return this.SpriteSequences[this.currentSequenceIndex - 1];
        }

        public bool SpriteCollide(IGameElement gameElement)
        {
            return false;
        }

        public void UpdateFrame(float elapsedTime)
        {
            this.TotalElapsedTime += elapsedTime;
            SpriteSequence spriteSequence = this.SpriteSequences[this.CurrentSequence - 1];
            if (this.TotalElapsedTime > spriteSequence.StepTime)
            {
                this.TotalElapsedTime -= spriteSequence.StepTime;
                spriteSequence.NextFrame();
            }

        }

    }
}
