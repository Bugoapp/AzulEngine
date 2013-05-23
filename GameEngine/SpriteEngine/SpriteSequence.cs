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
using Microsoft.Xna.Framework;

namespace AzulEngine.SpriteEngine
{
    /// <summary>
    /// Clase que representa una secuencias de cuadros
    /// </summary>
    public class SpriteSequence
    {
        private int currentFrame;
        /// <summary>
        /// Obtiene el índice con base cero del cuadro actual que se dibujará
        /// </summary>
        public int CurrentPosition
        {
            get { return currentFrame; }
        }

        private int stepTime;
        /// <summary>
        /// Obtiene o establece la duración en milisegundos de cada fotograma en la secuencia.
        /// </summary>
        public int StepTime
        {
            get { return stepTime; }
            set { stepTime = value; }
        }

        private SpriteFrame[] sequence;
        /// <summary>
        /// Obtiene o establece la secuencia de fotogramas
        /// </summary>
        public SpriteFrame[] Sequence
        {
            get { return sequence; }
            set { sequence = value; }
        }

        /// <summary>
        /// Inicializa una nueva instancia de la clase AzulEngine.SpriteEngine.SpriteSequence que recibe
        /// el largo de la secuencia
        /// </summary>
        /// <param name="lenght">largo de la secuencia</param>
        /// <param name="currentFrame">Indice con base cero de cuadro actual a dibujar</param>
        public SpriteSequence(int lenght, int currentFrame)
        {
            this.currentFrame = (int)MathHelper.Clamp(currentFrame, 0, lenght);

            this.sequence = new SpriteFrame[lenght];
            for (int i = 0; i < lenght; i++)
            {
                this.sequence[i] = new SpriteFrame(0);
            }
        }

        /// <summary>
        /// Inicializa una nueva instancia de la clase AzulEngine.SpriteEngine.SpriteSequence que recibe
        /// una arreglo de tipo SpriteFrame conteniendo la secuencia de sprites
        /// </summary>
        /// <param name="sequence">Secuencia de cuadros del sprite</param>
        /// <param name="currentFrame">Indice con base cero de cuadro actual a dibujar</param>
        public SpriteSequence(SpriteFrame[] sequence, int currentFrame)
        {
            this.currentFrame = (int)MathHelper.Clamp(currentFrame, 0, sequence.Length);
            this.sequence = sequence;
        }

        /// <summary>
        /// Metodo que establece un cuadro dentro de la colección de cuadros
        /// </summary>
        /// <param name="position">Posición del cuadro dentro de la colección de cuadros</param>
        /// <param name="frame">Cuadro a insertar dentro de la colección</param>
        public void SetTile(int position, SpriteFrame frame)
        {
            this.sequence[position] = frame;
        }

        /// <summary>
        /// Metodo que obtiene un cuadro dentro de la colección de cuadros
        /// </summary>
        /// <param name="position">Posición del cuadro dentro de la colección de cuadros</param>
        /// <returns>Retorna una instancia de type AzulEngine.SpriteEngine.SpriteFrame</returns>
        public SpriteFrame GetTile(int position)
        {
            return this.sequence[position];
        }

        /// <summary>
        /// Método que obtiene el índice del cuadro actual de la secuencia sumándole al mismo tiempo un 1.
        /// </summary>
        /// <returns>Retorna el cuadro actual despues de sumarle 1</returns>
        public int NextFrameIndex()
        {

            if (this.sequence.Length < this.currentFrame)
            {
                return this.currentFrame++;
            }
            else
            {
                return this.currentFrame = 0;
            }

        }

    }
}
