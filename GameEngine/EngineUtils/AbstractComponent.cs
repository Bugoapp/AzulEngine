using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AzulEngine.CameraEngine;
using Microsoft.Xna.Framework;

namespace AzulEngine.EngineUtils
{
    /// <summary>
    /// Representa un componente astracto
    /// </summary>
    /// <typeparam name="T">Representa el tipo de la escena a utilizar</typeparam>
    public class AbstractComponent<T> : DrawableGameComponent
    {
        protected Camera2D camera;
        /// <summary>
        /// Obtiene la cámara de visualización
        /// </summary>
        public virtual Camera2D Camera
        {
            get { return camera; }
        }

        protected T scene;
        /// <summary>
        /// Obtiene la escena que representa un conjunto de capas
        /// </summary>
        public virtual T Scene
        {
            get { return scene; }
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
        /// <summary>
        /// Constructor de la clase abstracta AzulEngine.EngineUtils.AbstractComponent que recibe como
        /// parametros el objeto Game, la escena, la resolucion base y si es dependiente de la resolucion del sistema.
        /// </summary>
        /// <param name="game">Objeto tipo Game que representa el tipo principal del juego</param>
        /// <param name="scene">Representa un conjunto de capas</param>
        /// <param name="baseScreenSize">Resolución base en un sistema de resolución independiente</param>
        /// <param name="resultionIndependent">Indica la resolución del juego si es independiente de la resolución del sistema</param>
        public AbstractComponent(Game game, T scene, Vector2 baseScreenSize, bool resultionIndependent)
            :base(game)
        {
            this.scene = scene;
            this.baseScreenSize = baseScreenSize;
            this.ResultionIndependent = resultionIndependent;
        }

        /// <summary>
        /// Posiciona los elementos en pantalla de acuerdo la la posición de la cámara
        /// </summary>
        /// <param name="camera">Cámara de visualiación</param>
        /// <param name="layer">Capa de texturas</param>
        public void CorrectCamera(Camera2D camera, AbstractLayer layer)
        {
            Vector2 displacementRatio = Vector2.Divide(layer.Velocity, camera.Velocity);
            Vector2 realDisplacement = Vector2.Multiply(camera.Position, displacementRatio);
            Vector2 realPosition = Vector2.Subtract(layer.Origin, realDisplacement);
            layer.Position = realPosition;
        }

        /// <summary>
        /// Mueve la capa automaticamente en una dirección determinada por cada fracción de tiempo
        /// </summary>
        /// <param name="layer">Capa de texturas</param>
        public void MoveLayer(AbstractLayer layer)
        {
            switch (layer.Direction)
            {
                case LayerMovementDirection.Up:
                    layer.Position = Vector2.Add(layer.Position, new Vector2(0, -layer.Velocity.Y));
                    break;
                case LayerMovementDirection.Down:
                    layer.Position = Vector2.Add(layer.Position, new Vector2(0, layer.Velocity.Y));
                    break;
                case LayerMovementDirection.Left:
                    layer.Position = Vector2.Add(layer.Position, new Vector2(-layer.Velocity.X, 0));
                    break;
                case LayerMovementDirection.Right:
                    layer.Position = Vector2.Add(layer.Position, new Vector2(layer.Velocity.X, 0));
                    break;
                case LayerMovementDirection.LowerLeft:
                    layer.Position = Vector2.Add(layer.Position, new Vector2(-layer.Velocity.X, layer.Velocity.Y));
                    break;
                case LayerMovementDirection.LowerRight:
                    layer.Position = Vector2.Add(layer.Position, new Vector2(layer.Velocity.X, layer.Velocity.Y));
                    break;
                case LayerMovementDirection.UpperLeft:
                    layer.Position = Vector2.Add(layer.Position, new Vector2(-layer.Velocity.X, -layer.Velocity.Y));
                    break;
                case LayerMovementDirection.UpperRight:
                    layer.Position = Vector2.Add(layer.Position, new Vector2(layer.Velocity.X, -layer.Velocity.Y));
                    break;
            }
        }

        /// <summary>
        /// Obtiene los límites de la ventana cliente teniendo en cuenta la independencia de la resolución
        /// </summary>
        /// <param name="clientBounds">Referencia a un rectángulo que contendrá los límites de la ventana cliente</param>
        public void GetClientBounds(out Rectangle clientBounds)
        {

            if (this.ResultionIndependent)
            {
                clientBounds = new Rectangle(0, 0, (int)this.baseScreenSize.X, (int)this.baseScreenSize.Y);
            }
            else
            {
                clientBounds = new Rectangle(0, 0, (int)this.GraphicsDevice.Viewport.Width, (int)this.GraphicsDevice.Viewport.Height);
            }
        }

        /// <summary>
        /// Obtiene el factor de escala de la pantalla
        /// </summary>
        /// <param name="screenScalingFactor">Referencia a un Vector3 que contendrá el factor de escalamiento global</param>
        public void GetScreenScalingFactor(out Vector3 screenScalingFactor)
        {
            if (this.ResultionIndependent)
            {
                float horScaling = (float)this.GraphicsDevice.PresentationParameters.BackBufferWidth / baseScreenSize.X;
                float verScaling = (float)this.GraphicsDevice.PresentationParameters.BackBufferHeight / baseScreenSize.Y;
                screenScalingFactor = new Vector3(horScaling, verScaling, 1);
            }
            else
            {
                screenScalingFactor = Vector3.One;
            }
        }
    }
}
