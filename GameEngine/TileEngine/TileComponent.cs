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
using Microsoft.Xna.Framework.Graphics;
using AzulEngine.CameraEngine;
using AzulEngine.EngineUtils;

namespace AzulEngine.TileEngine
{
    /// <summary>
    /// Representa un componente de baldosas, encargado de
    /// dibujar las capas y sus componentes
    /// </summary>
    public class TileComponent : AbstractComponent<TileScene>
    {
        /// <summary>
        /// Inicializa una nueva instancia de la clase AzulEngine.TileEngine.TileComponent que recibe como
        /// parametros el objeto Game, la escena, la resolucion base y si es dependiente de la resolucion del sistema.
        /// </summary>
        /// <param name="game">Objeto tipo Game que representa el tipo principal del juego</param>
        /// <param name="scene">Representa un conjunto de capas de baldosas</param>
        /// <param name="baseScreenSize">Resolución base en un sistema de resolución independiente</param>
        /// <param name="resultionIndependent">Indica la resolución del juego si es independiente de la resolución del sistema</param>
        public TileComponent(Game game, TileScene scene, Vector2 baseScreenSize, bool resultionIndependent)
            : base(game, scene, baseScreenSize, resultionIndependent)
        { }

        public override void Initialize()
        {
            camera = this.Game.Services.GetService(typeof(Camera2D)) as Camera2D;
            foreach (TileLayer layer in scene.Layers)
            {
                AbstractLayer currentLayer = layer;
                this.CorrectCamera(ref camera, ref currentLayer);               
            }
        }

        public override void Update(GameTime gameTime)
        {
            Camera2D camera = this.Game.Services.GetService(typeof(Camera2D)) as Camera2D;
            foreach (TileLayer layer in scene.Layers)
            {
                if (!layer.CameraIndependent)
                {
                    if (camera.Changed)
                    {
                        AbstractLayer currentLayer = layer;
                        this.CorrectCamera(ref camera, ref currentLayer);
                    }
                }
                else{
                    AbstractLayer currentLayer = layer;
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
            foreach (TileLayer layer in scene.Layers)
            {
                if (layer.Visible)
                {
                    Point layerLenght = layer.Lenght;
                    TileMap tileMap = layer.TileMap;

                    TileDrawLimits drawLimits = this.GetDrawLimits(layer, clientBounds);

                    TileCatalog tileCatalog = layer.TileCatalog;
                    for (int j = drawLimits.YMin; j < drawLimits.YMax; j++)
                    {
                        for (int i = drawLimits.XMin; i < drawLimits.XMax; i++)
                        {
                            Rectangle sourceTile = tileCatalog.TilePositions[tileMap.GetTile(i, j).Index];
                            //calcular la posicion de cada tile donde corresponde, multiplicando el numero de turno por su tamaño
                            //pe. x = 5 * 10 = 50
                            Vector2 tileAbsolutePosition = Vector2.Multiply( Vector2.Multiply(new Vector2(i, j), layer.ScaledTileSize),camera.Zoom) ;
                            //calcular la posicion con respecto a la posicion de la capa
                            Vector2 tileRelativePosition = Vector2.Add(layer.Position, tileAbsolutePosition);

                            spriteBatch.Draw(tileCatalog.Texture,
                                             tileRelativePosition,
                                             sourceTile,
                                             Color.White * layer.Transparency,
                                             0, Vector2.Zero,
                                             Vector2.Multiply(camera.Zoom, layer.ZoomScale),
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
        /// <returns>Retorna un objeto de tipo AzulEngine.TileEngine.TileDrawLimits</returns>
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

    }
}
