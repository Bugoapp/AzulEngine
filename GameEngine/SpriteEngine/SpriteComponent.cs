using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AzulEngine.CameraEngine;
using Microsoft.Xna.Framework;
using AzulEngine.EngineUtils;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace AzulEngine.SpriteEngine
{
    public class SpriteComponent : AbstractComponent<SpriteScene>
    {

         /// <summary>
        /// Inicializa una nueva instancia de la clase AzulEngine.SpriteEngine.SpriteComponent que recibe como
        /// parametros el objeto Game, la escena, la resolucion base y si es dependiente de la resolucion del sistema.
        /// </summary>
        /// <param name="game">Objeto index Game que representa el index principal del juego</param>
        /// <param name="scene">Representa un conjunto de capas de sprites</param>
        /// <param name="baseScreenSize">Resolución base en un sistema de resolución independiente</param>
        /// <param name="resultionIndependent">Indica la resolución del juego si es independiente de la resolución del sistema</param>
        public SpriteComponent(Game game, SpriteScene scene, Vector2 baseScreenSize, bool resultionIndependent)
            : base(game, scene, baseScreenSize, resultionIndependent)
        { }

        public override void Initialize()
        {
            camera = this.Game.Services.GetService(typeof(Camera2D)) as Camera2D;
            foreach (SpriteLayer layer in scene.Layers)
            {
                AbstractLayer currentLayer = layer;
                this.CorrectCamera(camera, currentLayer);
            }
        }

        public override void Update(GameTime gameTime)
        {
            Camera2D camera = this.Game.Services.GetService(typeof(Camera2D)) as Camera2D;





            foreach (SpriteLayer layer in scene.Layers)
            {

                for (int i = 0; i < layer.SpriteSequences.Length; i++)
                {
                    layer.UpdateFrame((float)gameTime.ElapsedGameTime.TotalMilliseconds);
                }
                if (Keyboard.GetState().IsKeyDown(Keys.Left))
                {
                    layer.CurrentSequence = 2;
                }
                else if (Keyboard.GetState().IsKeyUp(Keys.Left))
                {
                    layer.CurrentSequence = 1;
                }
              

                if (!layer.CameraIndependent)
                {
                    if (camera.Changed)
                    {
                        AbstractLayer currentLayer = layer;
                        base.CorrectCamera(camera, currentLayer);
                    }
                }
                else
                {

                    if (layer.Anchor != Anchor.None)
                    {
                        SpriteLayer currentLayer = layer;
                        this.CalculatePositionWithAnchor(currentLayer);

                    }
                    else if (layer.Direction != LayerMovementDirection.None)
                    {
                        AbstractLayer currentLayer = layer;
                        base.MoveLayer(currentLayer);
                    }

                }
            }
        }


        public override void Draw(GameTime gameTime)
        {
            SpriteBatch spriteBatch = Game.Services.GetService(typeof(SpriteBatch)) as SpriteBatch;
            Vector3 screenScalingFactor;
            base.GetScreenScalingFactor(out screenScalingFactor);
            Rectangle clientBounds;
            base.GetClientBounds(out clientBounds);

            Matrix globalTransformation = Matrix.CreateScale(screenScalingFactor);
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null, globalTransformation);
            foreach (SpriteLayer layer in scene.Layers)
            {
                if (layer.Visible)
                {
                    SpriteCatalog spriteCatalog = layer.SpriteCatalog;
                    SpriteSequence spriteSequence = layer.GetCurrentSequence();
                    Rectangle sourceSprite;
                    spriteCatalog.GetFramePosition(spriteSequence.GetFrame(spriteSequence.CurrentPosition), out sourceSprite);
                    spriteBatch.Draw(layer.SpriteCatalog.Texture,
                                     layer.Position,
                                     sourceSprite,
                                     Color.White * layer.Transparency,
                                     0, Vector2.Zero,
                                     Vector2.Multiply(camera.Zoom, layer.ZoomScale),
                                     SpriteEffects.None,
                                     0.0f);
                }
            }
        }


        /// <summary>
        /// Calcula la posición del sprite con respecto a un ancla
        /// </summary>
        /// <param name="layer">Capa de sprites</param>
        public void CalculatePositionWithAnchor(SpriteLayer layer)
        {
            float xPosition = 0;
            float yPosition = 0;
            Rectangle clientBounds;
            base.GetClientBounds(out clientBounds);
            switch (layer.Anchor)
            {
                case Anchor.UpperLeft:
                    xPosition = layer.Origin.X;
                    yPosition = layer.Origin.Y;
                    break;
                case Anchor.UpperRight:
                    xPosition = clientBounds.Width - (layer.Size.X + layer.Origin.X);
                    yPosition = layer.Origin.Y;
                    break;
                case Anchor.LowerLeft:
                    xPosition = layer.Origin.X;
                    yPosition = clientBounds.Height - (layer.Size.Y + layer.Origin.Y);
                    break;
                case Anchor.LowerRight:
                    xPosition = clientBounds.Width - (layer.Size.X + layer.Origin.X);
                    yPosition = clientBounds.Height - (layer.Size.Y + layer.Origin.Y);
                    break;

            }
            layer.Position = new Vector2(xPosition, yPosition);

        }

    }
}
