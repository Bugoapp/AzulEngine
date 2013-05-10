using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using AzulEngine.CameraEngine;

namespace AzulEngine.TileEngine
{
    /// <summary>
    /// Representa un componente de baldosas, encargado de
    /// dibujar las capas y sus componentes
    /// </summary>
    public class TileComponent: DrawableGameComponent
    {
        /// <summary>
        /// Obtiene la cámara de visualización
        /// </summary>
        private Camera2D camera;

        private TileScene tileScene;
        /// <summary>
        /// Obtiene la escena que representa un conjunto de capas de baldosas
        /// </summary>
        public TileScene TileScene
        {
            get { return tileScene; }
        }

        private Vector2 baseScreenSize;
        /// <summary>
        /// Obtiene o establece la resolución base en un sistema de resolución independiente
        /// </summary>
        public Vector2 BaseScreenSize
        {
            get { return baseScreenSize; }
            set { baseScreenSize = value; }
        }

        /// <summary>
        /// Obtiene o establece si la resolución interna es independiente de la resolución del sistema
        /// </summary>
        public Boolean ResultionIndependent { get; set; }


        /// <summary>
        /// Inicializa una nueva instancia de la clase GameEngine.TileEngine.TileComponent que recibe como
        /// parametros el objeto Game, la escena, la resolucion base y si es dependiente de la resolucion del sistema.
        /// </summary>
        /// <param name="game">Objeto tipo Game que representa el tipo principal del juego</param>
        /// <param name="tileScene">Representa un conjunto de capas de baldosas</param>
        /// <param name="baseScreenSize">Resolución base en un sistema de resolución independiente</param>
        /// <param name="resultionIndependent">Indica la resolución del juego si es independiente de la resolución del sistema</param>
        public TileComponent(Game game, TileScene tileScene, Vector2 baseScreenSize, bool resultionIndependent)
            :base(game)
        {
            this.tileScene = tileScene;
            this.baseScreenSize = baseScreenSize;
            this.ResultionIndependent = resultionIndependent;
        }

        public override void Initialize()
        {
            camera = this.Game.Services.GetService(typeof(Camera2D)) as Camera2D;
            foreach (TileLayer layer in tileScene.Layers)
            {
                TileLayer currentLayer = layer;
                this.CorrectCamera(ref camera, ref currentLayer);
                
            }
        }

        public override void Update(GameTime gameTime)
        {
            Camera2D camera = this.Game.Services.GetService(typeof(Camera2D)) as Camera2D;
            foreach (TileLayer layer in tileScene.Layers)
            {
                if (!layer.CameraIndependent)
                {
                    if (camera.Changed)
                    {
                        TileLayer currentLayer = layer;
                        this.CorrectCamera(ref camera, ref currentLayer);
                    }
                }
                else{
                        TileLayer currentLayer = layer;
                        this.MoveLayer(ref currentLayer);           
                }
            }
        }

        public override void Draw(GameTime gameTime)
        {

            SpriteBatch spriteBatch = Game.Services.GetService(typeof(SpriteBatch)) as SpriteBatch;
            Vector3 screenScalingFactor;
            Rectangle clientBounds;
            if (this.ResultionIndependent)
            {
                float horScaling = (float)this.GraphicsDevice.PresentationParameters.BackBufferWidth / baseScreenSize.X;
                float verScaling = (float)this.GraphicsDevice.PresentationParameters.BackBufferHeight / baseScreenSize.Y;
                screenScalingFactor = new Vector3(horScaling, verScaling, 1);
                clientBounds = new Rectangle(0, 0, (int)this.baseScreenSize.X, (int)this.baseScreenSize.Y);
            }
            else
            {
                screenScalingFactor = new Vector3(1, 1, 1);
                clientBounds = new Rectangle(0, 0, (int)this.GraphicsDevice.Viewport.Width, (int)this.GraphicsDevice.Viewport.Height);

            }
            Matrix globalTransformation = Matrix.CreateScale(screenScalingFactor);

            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null, globalTransformation);

            foreach (TileLayer tileLayer in tileScene.Layers)
            {
                if (tileLayer.Visible)
                {
                    Point layerLenght = tileLayer.Lenght;
                    int layerWidth = layerLenght.X;
                    int layerHeight = layerLenght.Y;
                    TileMap tileMap = tileLayer.TileMap;


                    TileDrawLimits drawLimits = this.GetDrawLimits(tileLayer, clientBounds);

                    TileCatalog tileCatalog = tileLayer.TileCatalog;
                    for (int j = drawLimits.YMin; j < drawLimits.YMax; j++)
                    {
                        for (int i = drawLimits.XMin; i < drawLimits.XMax; i++)
                        {

                            Rectangle sourceTile = tileCatalog.TilePositions[tileMap.GetTile(i, j).Index];
                            //calcular la posicion de cada tile donde corresponde, multiplicando el numero de turno por su tamaño
                            //pe. x = 5 * 10 = 50
                            Vector2 tileAbsolutePosition = Vector2.Multiply( Vector2.Multiply(new Vector2(i, j), tileLayer.ScaledTileSize),camera.Zoom) ;
                            //calcular la posicion con respecto a la posicion de la capa
                            Vector2 tileRelativePosition = Vector2.Add(tileLayer.Position, tileAbsolutePosition);

                            spriteBatch.Draw(tileCatalog.Texture,
                                             tileRelativePosition,
                                             sourceTile,
                                             Color.White * tileLayer.transparency,
                                             0, Vector2.Zero,
                                             Vector2.Multiply(camera.Zoom, tileLayer.ZoomScale),
                                             SpriteEffects.None,
                                             0.0f);
                        }
                    }

                }
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }

        /// <summary>
        /// Método de optimización de dibujado en pantalla que devuelve los límites de dibujo de baldosas
        /// </summary>
        /// <param name="layer">Capa de baldosas que se va a limitar</param>
        /// <param name="clientBounds">Rectángulo que representa los limites que definirán el cálculo</param>
        /// <returns>Retorna un objeto de tipo GameEngine.TileEngine.TileDrawLimits</returns>
        public TileDrawLimits GetDrawLimits(TileLayer layer, Rectangle clientBounds)
        {
            int[] xLimit = this.GetAxisLimit(layer.Position.X, layer.ScaledSize.X * camera.Zoom.X, layer.ScaledTileSize.X * camera.Zoom.X, layer.Lenght.X, clientBounds.Width);
            int[] yLimit = this.GetAxisLimit(layer.Position.Y, layer.ScaledSize.Y * camera.Zoom.Y, layer.ScaledTileSize.Y * camera.Zoom.Y, layer.Lenght.Y, clientBounds.Height);
            TileDrawLimits tileDrawLimits = new TileDrawLimits(xLimit[0], xLimit[1], yLimit[0], yLimit[1]);
            return tileDrawLimits;
        }

        /// <summary>
        /// Método de optimización que calcula los límites de dibujado de baldosas tanto para el eje x como el eje y
        /// </summary>
        /// <param name="position">Posición actual de la capa de baldosas</param>
        /// <param name="zoomedScaledSize">Tamaño de la capa con zoom y escala aplicadas</param>
        /// <param name="zoomedScaledTileSize">Tamaño de la baldosa con zoom y escala aplicadas</param>
        /// <param name="lenght">Longitud del eje</param>
        /// <param name="bound">Longitud del rectangulo limitador de un eje</param>
        /// <returns>Retorna un arreglo de enteros que representan los límites máximos y mínimos de un eje determinado</returns>
        public int[] GetAxisLimit(float position, float zoomedScaledSize, float zoomedScaledTileSize, int lenght, int bound)
        {
            int min = 0;
            int max = 0;
            if (position < 0 && ((position + zoomedScaledSize) > 0 && (position + zoomedScaledSize) <= bound))
            {
                min = (int)(-position / zoomedScaledTileSize);
                max = (int)((position + zoomedScaledSize) / (zoomedScaledTileSize)) + min + 2;
            }
            else if (position >= 0 && ((position + zoomedScaledSize) <= bound))
            {
                min = 0;
                max = lenght;
            }
            else if (position >= 0 && ((position + zoomedScaledSize) > bound))
            {
                min = 0;
                max = (int)((bound - position) / zoomedScaledTileSize) + 2;
            }
            else if (position < 0 && ((position + zoomedScaledSize) > bound))
            {
                min = (int)(-position / zoomedScaledTileSize);
                max = (int)(bound / zoomedScaledTileSize) + min + 2;
            }
            min = (int)MathHelper.Clamp(min, 0, lenght);
            max = (int)MathHelper.Clamp(max, 0, lenght);

            return new int[] { min, max };
        }


        /// <summary>
        /// Posiciona los elementos en pantalla de acuerdo la la posición de la cámara
        /// </summary>
        /// <param name="camera">Cámara de visualiación</param>
        /// <param name="layer">Capa de baldosas</param>
        public void CorrectCamera(ref Camera2D camera, ref TileLayer layer)
        {
            Vector2 displacementRatio = Vector2.Divide(layer.Velocity, camera.Velocity);
            Vector2 realDisplacement = Vector2.Multiply(camera.Position, displacementRatio);
            Vector2 realPosition = Vector2.Subtract(layer.Origin, realDisplacement);
            layer.Position = realPosition;
        }

        /// <summary>
        /// Mueve la capa de baldosas automaticamente en una dirección determinada por cada fracción de tiempo
        /// </summary>
        /// <param name="layer">Referencia a una capa de baldosas</param>
        public void MoveLayer(ref TileLayer layer)
        {
            switch (layer.Direction)
            {
                case TileLayerMovementDirection.Up:
                    layer.Position = Vector2.Add(layer.Position, new Vector2(0, -layer.Velocity.Y));
                    break;
                case TileLayerMovementDirection.Down:
                    layer.Position = Vector2.Add(layer.Position, new Vector2(0, layer.Velocity.Y));
                    break;
                case TileLayerMovementDirection.Left:
                    layer.Position = Vector2.Add(layer.Position, new Vector2(-layer.Velocity.X, 0));
                    break;
                case TileLayerMovementDirection.Right:
                    layer.Position = Vector2.Add(layer.Position, new Vector2(layer.Velocity.X, 0));
                    break;
                case TileLayerMovementDirection.LowerLeft:
                    layer.Position = Vector2.Add(layer.Position, new Vector2(-layer.Velocity.X, layer.Velocity.Y));
                    break;
                case TileLayerMovementDirection.LowerRigth:
                    layer.Position = Vector2.Add(layer.Position, new Vector2(layer.Velocity.X, layer.Velocity.Y));
                    break;
                case TileLayerMovementDirection.UpperLeft:
                    layer.Position = Vector2.Add(layer.Position, new Vector2(-layer.Velocity.X, -layer.Velocity.Y));
                    break;
                case TileLayerMovementDirection.UpperRight:
                    layer.Position = Vector2.Add(layer.Position, new Vector2(layer.Velocity.X, -layer.Velocity.Y));
                    break;
            }
        }

    }
}
