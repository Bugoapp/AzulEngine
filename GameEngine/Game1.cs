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
        const bool resultionIndependent = false;
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
            camera = new Camera2D(new Vector2(50f,50f), new Vector2(1f));
            
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
            TileMap map = new TileMap(100, 100);
            for (int i = 0 ; i < 100; i++){
                for (int j = 0; j < 100; j++)
                {
                    map.SetTile(i, j, new Tile(rand.Next(1, cat.TilePositions.Count)));
                }
            }

            //Vector2 backgrounScale = new Vector2(
            //(float)this.Window.ClientBounds.Width / (float)graphics.PreferredBackBufferWidth,
            //(float)this.Window.ClientBounds.Height / (float)graphics.PreferredBackBufferHeight);
            TileLayer layer1 = new TileLayer(cat, map, true, new Vector2(100, 100), new Vector2(1.0f, 1.0f), Vector2.One, new Vector2(1f));
            //this.Window.ClientSizeChanged += new EventHandler<EventArgs>(Window_ClientSizeChanged);
            scene = new TileScene();
            scene.AddLayer(layer1);
            foreach (TileLayer layer in scene.Layers)
            {
                TileLayer currentLayer = layer;
                this.CorrectCamera(ref camera, ref currentLayer);
                
            }
            
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

            //int xDirection = 0;
            //int yDirection = 0;
            if (Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                //scene.Layers[0].Position = Vector2.Subtract(scene.Layers[0].Position, Vector2.Multiply(new Vector2(0f, -3f), scene.Layers[0].ZoomScale));
                camera.Position = Vector2.Add(camera.Position, new Vector2(0f, camera.Velocity.Y));
                camera.Changed = true;
                //yDirection = -1;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                //scene.Layers[0].Position = Vector2.Subtract(scene.Layers[0].Position, Vector2.Multiply(new Vector2(0f, 3f), scene.Layers[0].ZoomScale));
                camera.Position = Vector2.Add(camera.Position, new Vector2(0f, -camera.Velocity.Y));
                camera.Changed = true;
                //yDirection = 1;
            }
             if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                //scene.Layers[0].Position = Vector2.Subtract(scene.Layers[0].Position, Vector2.Multiply(new Vector2(-3f, 0), scene.Layers[0].ZoomScale));
                camera.Position = Vector2.Add(camera.Position, new Vector2(camera.Velocity.X, 0f));
                camera.Changed = true;
                //xDirection = -1;
             }
            else if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                //scene.Layers[0].Position = Vector2.Subtract(scene.Layers[0].Position, Vector2.Multiply(new Vector2(3f, 0), scene.Layers[0].ZoomScale));
                camera.Position = Vector2.Add(camera.Position, new Vector2(-camera.Velocity.X, 0f));
                camera.Changed = true;
                //xDirection = 1;
            }

             foreach (TileLayer layer in scene.Layers)
             {
                 if (camera.Changed)
                 {
                     TileLayer currentLayer = layer;
                     //System.Diagnostics.Debug.WriteLine("layer: " + realPosition.ToString() + " camera: " + camera.Position.ToString());
                     this.CorrectCamera(ref camera, ref currentLayer);
                 }
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
                //this.ChangeResolution();
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
                //this.ChangeResolution();
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
                //this.ChangeResolution();
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
                //this.ChangeResolution();
            }

            if (Keyboard.GetState().IsKeyDown(Keys.F))
            {
                this.graphics.ToggleFullScreen();
                Viewport vw = new Viewport(0, 0, graphics.PreferredBackBufferHeight, graphics.PreferredBackBufferWidth);
                this.graphics.GraphicsDevice.Viewport = vw;
                graphics.ApplyChanges();
            }
            // TODO: Add your update logic here
             //System.Diagnostics.Debug.WriteLine("X: " + scene.Layers[0].Position.X + " Y: " + scene.Layers[0].Position.Y);
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);
            Vector3 screenScalingFactor;
            Rectangle clientBounds;
            if (resultionIndependent)
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

            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Opaque, null, null, null, null, globalTransformation);

            foreach (TileLayer tileLayer in scene.Layers)
            {
                if (tileLayer.Visible)
                {
                    Point layerLenght = tileLayer.Lenght;
                    int layerWidth = layerLenght.X;
                    int layerHeight = layerLenght.Y;
                    TileMap tileMap = tileLayer.TileMap;

                    
                    TileDrawLimits drawLimits = this.GetDrawLimits(tileLayer, clientBounds);

                    //System.Diagnostics.Debug.WriteLine("xmin: " + limit.XMin + " xmax: " + limit.XMax);
                    //System.Diagnostics.Debug.WriteLine("ymin: " + ymin + " ymax: " + ymax);
                    TileCatalog tileCatalog = tileLayer.TileCatalog;
                    for (int i = drawLimits.XMin; i < drawLimits.XMax; i++)
                    {
                        for (int j = drawLimits.YMin; j < drawLimits.YMax; j++)
                        {

                            Rectangle sourceTile = tileCatalog.TilePositions[tileMap.GetTile(i, j).Index];
                            //calcular la posicion de cada tile donde corresponde, multiplicando el numero de turno por su tamaño
                            //pe. x = 5 * 10 = 50
                            Vector2 tileAbsolutePosition = Vector2.Multiply(new Vector2(i, j), tileLayer.ZoomedTileSize);
                            //calcular la posicion con respecto a la posicion de la capa
                            Vector2 tileRelativePosition = Vector2.Add(tileLayer.Position, tileAbsolutePosition);

                            spriteBatch.Draw(tileCatalog.Texture,
                                             tileRelativePosition,
                                             sourceTile,
                                             Color.AliceBlue,
                                             0, Vector2.Zero,
                                             Vector2.Multiply(tileLayer.Zoom, tileLayer.ZoomScale),
                                             SpriteEffects.None,
                                             0.0f);
                        }
                    }

                }
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }

        public TileDrawLimits GetDrawLimits(TileLayer layer, Rectangle clientBounds)
        {
            int[] xLimit = this.GetAxisLimit(layer.Position.X, layer.ZoomedSize.X, layer.ZoomedTileSize.X,layer.Lenght.X, clientBounds.Width);
            int[] yLimit = this.GetAxisLimit(layer.Position.Y, layer.ZoomedSize.Y, layer.ZoomedTileSize.Y, layer.Lenght.Y, clientBounds.Height);
            TileDrawLimits tileDrawLimits = new TileDrawLimits(xLimit[0], xLimit[1], yLimit[0], yLimit[1]);
            return tileDrawLimits;
        }

        public int[] GetAxisLimit(float position, float zoomedSize, float zoomedTileSize,int lenght, int bound)
        {
            int min = 0;
            int max = 0;
            if (position < 0 && ((position + zoomedSize) > 0 && (position + zoomedSize) <= bound))
            {
                min = (int)(-position / zoomedTileSize);
                max = (int)((position + zoomedSize) / (zoomedTileSize)) + min + 2;
            }
            else if (position >= 0 && ((position + zoomedSize) <= bound))
            {
                min = 0;
                max = lenght;
            }
            else if (position >= 0 && ((position + zoomedSize) > bound))
            {
                min = 0;
                max = (int)((bound - position) / zoomedTileSize) + 2;
            }
            else if (position < 0 && ((position + zoomedSize) > bound))
            {
                min = (int)(-position / zoomedTileSize);
                max = (int)(bound / zoomedTileSize) + min + 2;
            }
            min = (int)MathHelper.Clamp(min, 0, lenght);
            max = (int)MathHelper.Clamp(max, 0, lenght);

            return new int[]{min,max};
        }

        public void CorrectCamera(ref Camera2D camera, ref TileLayer layer)
        {
            Vector2 displacementRatio = Vector2.Divide(layer.Velocity, camera.Velocity);
            Vector2 realDisplacement = Vector2.Multiply(camera.Position, displacementRatio);
            Vector2 realPosition = Vector2.Subtract(layer.Origin, realDisplacement);
            camera.Changed = false;
            layer.Position = realPosition;
        }
    }
}
