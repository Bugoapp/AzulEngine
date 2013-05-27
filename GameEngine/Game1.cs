#region Using Statements
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;
using AzulEngine.TileEngine;
using AzulEngine.CameraEngine;
using AzulEngine.EngineUtils;
using AzulEngine.TextureEngine;
using AzulEngine.SpriteEngine;

#endregion

namespace AzulEngine
{
    /// <summary>
    /// This is the main index for your game
    /// </summary>
    public class Game1 : Game
    {
        //test
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D texture1;
        Texture2D texture2;
        TileScene scene;
        Vector2 backgrounScale = Vector2.One;
        Vector2 baseScreenSize;
        const bool resultionIndependent = false;
        Camera2D camera; 
        public Game1()
            : base()
        {
            int resolutionCount = OpenTK.DisplayDevice.GetDisplay(OpenTK.DisplayIndex.Default).AvailableResolutions.Count;
            OpenTK.DisplayResolution dv = OpenTK.DisplayDevice.GetDisplay(OpenTK.DisplayIndex.Default).AvailableResolutions[resolutionCount -1 ] ;
            
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            baseScreenSize = new Vector2(dv.Width, dv.Height);
            graphics.PreferredBackBufferWidth = 800;
            graphics.PreferredBackBufferHeight = 600;
            graphics.IsFullScreen = false;
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
            camera = new Camera2D(new Vector2(0f, 0f), new Vector2(1f), new Vector2(3f));

            this.Services.AddService(typeof(SpriteBatch), spriteBatch);
            this.Services.AddService(typeof(Camera2D), camera);

            texture1 = this.Content.Load<Texture2D>("tex");
            TileCatalog cat1 = new TileCatalog(texture1, 15, 15);
  
            Random rand = new Random(DateTime.Now.Millisecond);
            TileMap map1 = new TileMap(100, 100);
            for (int i = 0; i < 100; i++)
            {
                for (int j = 0; j < 100; j++)
                {
                    map1.SetTile(i, j, new Tile(rand.Next(1, cat1.TilePositions.Count)));
                }
            }

            TileLayer layer1 = new TileLayer(cat1, map1, 0.5f, false, new Vector2(0, 0), new Vector2(1f, 1f), new Vector2(3f), false, LayerMovementDirection.None);

            texture2 = this.Content.Load<Texture2D>("tiles2");

            TileCatalog cat2 = new TileCatalog(texture2, 48, 48);
            TileMap map2 = new TileMap(10, 500);
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 500; j++)
                {
                    map2.SetTile(i, j, new Tile(rand.Next(1, cat2.TilePositions.Count)));
                }
            }
            TileLayer layer2 = new TileLayer(cat2, map2, 1.0f, false, new Vector2(0, 0), new Vector2(1f, 1f), new Vector2(3f), true, LayerMovementDirection.Up);

            scene = new TileScene();
            scene.AddLayer(layer2);
            scene.AddLayer(layer1);

            TileComponent component = new TileComponent(this, scene, baseScreenSize, resultionIndependent);
            this.Components.Add(component);

            TextureLayer tLayer1 = new TextureLayer(this.texture1, 1f, false, new Vector2(20f), Vector2.One, new Vector2(1.5f,1.5f), true, Anchor.LowerRight);
            TextureLayer tLayer2 = new TextureLayer(this.texture2, 0.5f, false, new Vector2(10f), Vector2.One, new Vector2(5f), true, Anchor.LowerLeft);
            
            TextureScene tScene = new TextureScene();
            tScene.AddLayer(tLayer1);
            tScene.AddLayer(tLayer2);
            TextureComponent tComponent = new TextureComponent(this, tScene, baseScreenSize, resultionIndependent);          
            this.Components.Add(tComponent);

            texture1 = this.Content.Load<Texture2D>("megax");
            SpriteCatalog scatalog = new SpriteCatalog(texture1, 36, 42);

            SpriteSequence[] spriteSecuences = new SpriteSequence[2];

            SpriteSequence spriteSecuence1 = new SpriteSequence(7, 0);
            spriteSecuence1.StepTime = 400;
            spriteSecuence1.SetFrame(0,new SpriteFrame(1));
            spriteSecuence1.SetFrame(1, new SpriteFrame(1));
            spriteSecuence1.SetFrame(2, new SpriteFrame(1));
            spriteSecuence1.SetFrame(3, new SpriteFrame(1));
            spriteSecuence1.SetFrame(4, new SpriteFrame(2));
            spriteSecuence1.SetFrame(5, new SpriteFrame(3));
            spriteSecuence1.SetFrame(6, new SpriteFrame(1));
            spriteSecuences[0] = spriteSecuence1;

            SpriteSequence spriteSecuence2 = new SpriteSequence(10, 0);
            spriteSecuence2.StepTime = 90;
            spriteSecuence2.SetFrame(0, new SpriteFrame(5));
            spriteSecuence2.SetFrame(1, new SpriteFrame(6));
            spriteSecuence2.SetFrame(2, new SpriteFrame(7));
            spriteSecuence2.SetFrame(3, new SpriteFrame(8));
            spriteSecuence2.SetFrame(4, new SpriteFrame(9));
            spriteSecuence2.SetFrame(5, new SpriteFrame(10));
            spriteSecuence2.SetFrame(6, new SpriteFrame(11));
            spriteSecuence2.SetFrame(7, new SpriteFrame(12));
            spriteSecuence2.SetFrame(8, new SpriteFrame(13));
            spriteSecuence2.SetFrame(9, new SpriteFrame(14));

            spriteSecuences[1] = spriteSecuence2;

            SpriteLayer spLayer = new SpriteLayer(scatalog, spriteSecuences, 1.0f, true, new Vector2(10f), Vector2.One, Vector2.Zero, SpriteEffects.None, true, Anchor.None);
            spLayer.CurrentSequence = 2;
            SpriteScene spScene = new SpriteScene();
            spScene.AddLayer(spLayer);
            SpriteComponent spComponent = new SpriteComponent(this, spScene, baseScreenSize, resultionIndependent);
            this.Components.Add(spComponent);


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

                camera.Zoom = Vector2.Add(camera.Zoom, new Vector2(0.01f, 0.01f));
                
            }


            if (Keyboard.GetState().IsKeyDown(Keys.Z))
            {

                camera.Zoom = Vector2.Subtract(camera.Zoom, new Vector2(0.01f, 0.01f));
                
                
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
            //spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);
            //spriteBatch.Draw(texture,
            //     Vector2.Zero,
            //     null,
            //     Color.White,
            //     0, Vector2.Zero,
            //     1.0f,
            //     SpriteEffects.None,
            //     0.0f);
            //spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
