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
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using AzulEngine.CameraEngine;
using AzulEngine.TileEngine;
using AzulEngine.EngineUtils;
using Microsoft.Xna.Framework.Graphics;

namespace AzulEngine.TextureEngine
{
    /// <summary>
    /// Representa un componente de texturas, encargado de
    /// dibujarlas
    /// </summary>
    public class TextureComponent : AbstractComponent<TextureScene>
    {
        /// <summary>
        /// Inicializa una nueva instancia de la clase AzulEngine.TextureEngine.TextureComponent que recibe como
        /// parametros el objeto Game, la escena, la resolucion base y si es dependiente de la resolucion del sistema.
        /// </summary>
        /// <param name="game">Objeto tipo Game que representa el tipo principal del juego</param>
        /// <param name="scene">Representa un conjunto de capas de baldosas</param>
        /// <param name="baseScreenSize">Resolución base en un sistema de resolución independiente</param>
        /// <param name="resultionIndependent">Indica la resolución del juego si es independiente de la resolución del sistema</param>
        public TextureComponent(Game game, TextureScene scene, Vector2 baseScreenSize, bool resultionIndependent)
            : base(game, scene, baseScreenSize, resultionIndependent)
        { }

        public override void Initialize()
        {
            camera = this.Game.Services.GetService(typeof(Camera2D)) as Camera2D;
            foreach (TextureLayer layer in scene.Layers)
            {
                AbstractLayer currentLayer = layer;
                base.CorrectCamera(ref camera, ref currentLayer);

            }
        }

        public override void Update(GameTime gameTime)
        {
            Camera2D camera = this.Game.Services.GetService(typeof(Camera2D)) as Camera2D;
            foreach (TextureLayer layer in scene.Layers)
            {
                if (!layer.CameraIndependent)
                {
                    if (camera.Changed)
                    {
                        AbstractLayer currentLayer = layer;
                        base.CorrectCamera(ref camera, ref currentLayer);
                    }
                }
                else
                {
                    AbstractLayer currentLayer = layer;
                    base.MoveLayer(ref currentLayer);
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

            foreach (TextureLayer layer in scene.Layers)
            {
                if (layer.Visible)
                {
                    spriteBatch.Draw(layer.Texture,
                                     layer.Position,
                                     null,
                                     Color.White * layer.Transparency,
                                     0, Vector2.Zero,
                                     Vector2.Multiply(camera.Zoom, layer.ZoomScale),
                                     SpriteEffects.None,
                                     0.0f);

                }
            }
            spriteBatch.End();
            base.Draw(gameTime);

        }

    }
}
