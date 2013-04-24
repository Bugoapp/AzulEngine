#region Using Statements
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;
using GameEngine.TileEngine;

#endregion

namespace GameEngine
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Game
    {
        //test
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D texture;
        TileScene scene;
        float zoom = 1.0f;
        public Game1()
            : base()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            texture = this.Content.Load<Texture2D>("tex");
            //this.graphics.IsFullScreen = true;


            TileCatalog cat = new TileCatalog(texture, 15, 15);
            Random rand = new Random(DateTime.Now.Millisecond);
            TileMap map = new TileMap(10, 10);
            for (int i = 0 ; i < 10; i++){
                for (int j = 0; j < 10; j++)
                {
                    map.SetTile(i, j, new Tile(rand.Next(1, cat.TilePositions.Count)));
                }
            }
            TileLayer layer = new TileLayer(cat, map, true, Vector2.Zero);

            scene = new TileScene();
            scene.AddLayer(layer);
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            if(Keyboard.GetState().IsKeyDown(Keys.A)){
                zoom += 0.01f;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Z))
            {
                zoom -= 0.01f;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                scene.Layers[0].Position = Vector2.Subtract(scene.Layers[0].Position, new Vector2(0, -1f));
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                scene.Layers[0].Position = Vector2.Subtract(scene.Layers[0].Position, new Vector2(0, 1f));
            }
             if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                scene.Layers[0].Position = Vector2.Subtract(scene.Layers[0].Position, new Vector2(-1f, 0));
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                scene.Layers[0].Position = Vector2.Subtract(scene.Layers[0].Position, new Vector2(1f, 0));
            }
            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Opaque);

            foreach (TileLayer layer in scene.Layers)
            {
                Point lenght = layer.Lenght;
                int width = lenght.X;
                int height = lenght.Y;
                TileMap map = layer.TileMap;

                int xmin = 0;
                
                //if (xmin <= 0) { 
                //    xmin *= -1; xmin++; 
                //}
                //else { xmin = 0; }

                //int xmax = (int)layer.Position.X + ((int)layer.Position.X < 0 ? layer.Size.X : layer.Size.X * -1);
                int xmax = 0;
                if (layer.Position.X < 0 && ((layer.Position.X + layer.Size.X) > 0 && (layer.Position.X + layer.Size.X) <= this.graphics.PreferredBackBufferWidth))
                {
                    xmin = -(int)layer.Position.X / layer.TileCatalog.Size.X + 1;
                    xmax = ((int)layer.Position.X + layer.Size.X) / layer.TileCatalog.Size.X + xmin;
                }
                else if (layer.Position.X >= 0 && ((layer.Position.X + layer.Size.X) <= this.graphics.PreferredBackBufferWidth))
                {
                    xmin = 0;
                    xmax = width;             
                }
                else if (layer.Position.X >= 0 && ((layer.Position.X + layer.Size.X) > this.graphics.PreferredBackBufferWidth))
                {
                    xmin = 0;
                    xmax = (this.graphics.PreferredBackBufferWidth - (int)layer.Position.X)/layer.TileCatalog.Size.X;
                }
                else if (layer.Position.X < 0 && ((layer.Position.X + layer.Size.X) > this.graphics.PreferredBackBufferWidth))
                {
                    xmin = -(int)layer.Position.X / layer.TileCatalog.Size.X + 1;
                    xmax = this.graphics.PreferredBackBufferWidth / layer.TileCatalog.Size.X + xmin;
                }

                xmin = (int)MathHelper.Clamp(xmin, 0, width);
                xmax = (int)MathHelper.Clamp(xmax, 0, width);

                System.Diagnostics.Debug.WriteLine("xmin: " + xmin + " xmax: " + xmax);
                TileCatalog catalog = layer.TileCatalog;
                for (int i = 0; i < width; i++)
                {
                    for (int j = 0; j < height; j++)
                    {

                        Rectangle source = catalog.TilePositions[map.GetTile(i, j).Index];
                        Vector2 absolutePosition = Vector2.Multiply(new Vector2(i, j), new Vector2(source.Width * zoom, source.Height * zoom));
                        Vector2 relativePosition = Vector2.Add(layer.Position,absolutePosition); 
 
                        spriteBatch.Draw(catalog.Texture,
                                         relativePosition,
                                         source,
                                         Color.AliceBlue,
                                         0, Vector2.Zero, zoom, SpriteEffects.None, 0.0f);
                    }
                }
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
