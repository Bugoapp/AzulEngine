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
using GameEngine.CameraEngine;

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
        Vector2 backgrounScale = Vector2.One;
        Vector2 baseScreenSize;
        const bool resultionIndependent = true;
        Camera2D camera; 
        public Game1()
            : base()
        {
       
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            baseScreenSize = new Vector2(1280, 1024);
            graphics.PreferredBackBufferWidth = 320;
            graphics.PreferredBackBufferHeight = 280;
            graphics.IsFullScreen = false;
            this.Window.AllowUserResizing = true;
            graphics.ApplyChanges();           
            
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
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
            camera = new Camera2D(new Vector2(0f, 0f), new Vector2(1f));

            this.Services.AddService(typeof(SpriteBatch), spriteBatch);
            this.Services.AddService(typeof(Camera2D), camera);

            texture = this.Content.Load<Texture2D>("tex");
            TileCatalog cat1 = new TileCatalog(texture, 15, 15);
            Random rand = new Random(DateTime.Now.Millisecond);
            TileMap map1 = new TileMap(1000, 1000);
            for (int i = 0; i < 1000; i++)
            {
                for (int j = 0; j < 1000; j++)
                {
                    map1.SetTile(i, j, new Tile(rand.Next(1, cat1.TilePositions.Count)));
                }
            }

            TileLayer layer1 = new TileLayer(cat1, map1, 0.5f, true, new Vector2(100, 100), new Vector2(1.0f, 1.0f), Vector2.One, new Vector2(3f));

            texture = this.Content.Load<Texture2D>("tiles2");

            TileCatalog cat2 = new TileCatalog(texture, 48, 48);
            TileMap map2 = new TileMap(200, 200);
            for (int i = 0; i < 200; i++)
            {
                for (int j = 0; j < 200; j++)
                {
                    map2.SetTile(i, j, new Tile(rand.Next(1, cat2.TilePositions.Count)));
                }
            }
            TileLayer layer2 = new TileLayer(cat2, map2, 1.0f, true, new Vector2(0, 0), new Vector2(1.0f, 1.0f), new Vector2(0.1f, 0.1f), new Vector2(1f));

            scene = new TileScene();
            scene.AddLayer(layer2);
            scene.AddLayer(layer1);

            //foreach (TileLayer layer in scene.Layers)
            //{
            //    TileLayer currentLayer = layer;
            //    this.CorrectCamera(ref camera, ref currentLayer);

            //}
            TileComponent component = new TileComponent(this, scene, baseScreenSize, resultionIndependent);
            this.Components.Add(component);

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
                foreach (TileLayer layer in scene.Layers)
                {
                    layer.Zoom = Vector2.Add(layer.Zoom, new Vector2(0.01f, 0.01f));
                }
            }


            if (Keyboard.GetState().IsKeyDown(Keys.Z))
            {
                foreach (TileLayer layer in scene.Layers)
                {
                    layer.Zoom = Vector2.Subtract(layer.Zoom, new Vector2(0.01f, 0.01f));
                }
                
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                camera.Position = Vector2.Add(camera.Position, new Vector2(0f, camera.Velocity.Y));
                camera.Changed = true;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                camera.Position = Vector2.Add(camera.Position, new Vector2(0f, -camera.Velocity.Y));
                camera.Changed = true;
            }
             if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                camera.Position = Vector2.Add(camera.Position, new Vector2(camera.Velocity.X, 0f));
                camera.Changed = true;
             }
            else if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                camera.Position = Vector2.Add(camera.Position, new Vector2(-camera.Velocity.X, 0f));
                camera.Changed = true;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.D1))
            {
                graphics.PreferredBackBufferHeight = 480;
                graphics.PreferredBackBufferWidth = 640;
                if (!graphics.IsFullScreen)
                {
                    Viewport vw = new Viewport(0, 0, 640, 480);
                    this.graphics.GraphicsDevice.Viewport = vw;
                }

                graphics.ApplyChanges();
            }
            if (Keyboard.GetState().IsKeyDown(Keys.D2))
            {
                graphics.PreferredBackBufferHeight = 600;
                graphics.PreferredBackBufferWidth = 800;
                if (!graphics.IsFullScreen)
                {
                    Viewport vw = new Viewport(0, 0, 800, 600);
                    this.graphics.GraphicsDevice.Viewport = vw;
                }

                graphics.ApplyChanges();
            }
            if (Keyboard.GetState().IsKeyDown(Keys.D3))
            {
                graphics.PreferredBackBufferHeight = 768;
                graphics.PreferredBackBufferWidth = 1024;
                if (!graphics.IsFullScreen)
                {
                    Viewport vw = new Viewport(0, 0, 1024, 768);
                    this.graphics.GraphicsDevice.Viewport = vw;
                }

                graphics.ApplyChanges();
            }
            if (Keyboard.GetState().IsKeyDown(Keys.D4))
            {
                graphics.PreferredBackBufferHeight = 1024;
                graphics.PreferredBackBufferWidth = 1280;
                if (!graphics.IsFullScreen)
                {
                    Viewport vw = new Viewport(0, 0, 1280, 1024);
                    this.graphics.GraphicsDevice.Viewport = vw;
                }

                graphics.ApplyChanges();
            }

            if (Keyboard.GetState().IsKeyDown(Keys.F))
            {
                this.graphics.ToggleFullScreen();
                Viewport vw = new Viewport(0, 0, graphics.PreferredBackBufferHeight, graphics.PreferredBackBufferWidth);
                this.graphics.GraphicsDevice.Viewport = vw;
                graphics.ApplyChanges();
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
            GraphicsDevice.Clear(Color.White);
            base.Draw(gameTime);
        }
    }
}
