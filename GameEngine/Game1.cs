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
        Vector2 backgrounScale = Vector2.One;
        public Game1()
            : base()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferHeight = 200;
            graphics.PreferredBackBufferWidth = 200;

            graphics.ApplyChanges();
            this.Window.AllowUserResizing = true;
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

            Vector2 backgrounScale = new Vector2(
            (float)this.Window.ClientBounds.Width / (float)graphics.PreferredBackBufferWidth,
            (float)this.Window.ClientBounds.Height / (float)graphics.PreferredBackBufferHeight);
            TileLayer layer = new TileLayer(cat, map, true, Vector2.Zero, new Vector2(1.0f, 1.0f), backgrounScale);
            this.Window.ClientSizeChanged += new EventHandler<EventArgs>(Window_ClientSizeChanged);
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
                scene.Layers[0].Position = Vector2.Subtract(scene.Layers[0].Position, new Vector2(0, -1f));
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                scene.Layers[0].Position = Vector2.Subtract(scene.Layers[0].Position, new Vector2(0, 1f));
            }
             if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                scene.Layers[0].Position = Vector2.Multiply(Vector2.Subtract(scene.Layers[0].Position, new Vector2(-1f, 0)),scene.Layers[0].ZoomScale);
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                scene.Layers[0].Position = Vector2.Multiply(Vector2.Subtract(scene.Layers[0].Position, new Vector2(1f, 0)),scene.Layers[0].ZoomScale);
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

                //int xmin = 0;
                //int xmax = 0;
                //float xpos = layer.Position.X;
                //float xZoomedSize = layer.ZoomedSize.X;
                //float xZoomedTileSize = layer.ZoomedTileSize.X;

                //if (xpos < 0 && ((xpos + xZoomedSize) > 0 && (xpos + xZoomedSize) <= this.Window.ClientBounds.Width))
                //{
                //    xmin = (int)(-xpos / xZoomedTileSize);
                //    xmax = (int)((xpos + xZoomedSize) / (xZoomedTileSize)) + xmin + 2;
                //}
                //else if (xpos >= 0 && ((xpos + xZoomedSize) <= this.Window.ClientBounds.Width))
                //{
                //    xmin = 0;
                //    xmax = width;             
                //}
                //else if (xpos >= 0 && ((xpos + xZoomedSize) > this.Window.ClientBounds.Width))
                //{
                //    xmin = 0;
                //    xmax = (int)((this.Window.ClientBounds.Width - xpos) / xZoomedTileSize) + 2;
                //}
                //else if (xpos < 0 && ((xpos + xZoomedSize) > this.Window.ClientBounds.Width))
                //{
                //    xmin = (int)(-xpos / xZoomedTileSize);
                //    xmax = (int)(this.Window.ClientBounds.Width / xZoomedTileSize) + xmin + 2;
                //}


                //int ymin = 0;
                //int ymax = 0;
                //if (layer.Position.Y < 0 && ((layer.Position.Y + layer.ZoomedSize.Y) > 0 && (layer.Position.Y + layer.ZoomedSize.Y) <= this.Window.ClientBounds.Height))
                //{
                //    ymin = (int)(-layer.Position.Y / layer.ZoomedTileSize.Y);
                //    ymax = (int)((layer.Position.Y + layer.ZoomedSize.Y) / (layer.ZoomedTileSize.Y)) + ymin + 2;
                //}
                //else if (layer.Position.Y >= 0 && ((layer.Position.Y + layer.ZoomedSize.Y) <= this.Window.ClientBounds.Height))
                //{
                //    ymin = 0;
                //    ymax = height;
                //}
                //else if (layer.Position.Y >= 0 && ((layer.Position.Y + layer.ZoomedSize.Y) > this.Window.ClientBounds.Height))
                //{
                //    ymin = 0;
                //    ymax = (int)((this.Window.ClientBounds.Height - layer.Position.Y) / layer.ZoomedTileSize.Y) + 2;
                //}
                //else if (layer.Position.Y < 0 && ((layer.Position.Y + layer.ZoomedSize.Y) > this.Window.ClientBounds.Height))
                //{
                //    ymin = (int)(-layer.Position.Y / layer.ZoomedTileSize.Y);
                //    ymax = (int)(this.Window.ClientBounds.Height / layer.ZoomedTileSize.Y) + ymin + 2;
                //}
                //xmin = (int)MathHelper.Clamp(xmin, 0, width);
                //xmax = (int)MathHelper.Clamp(xmax, 0, width);
                //ymin = (int)MathHelper.Clamp(ymin, 0, height);
                //ymax = (int)MathHelper.Clamp(ymax, 0, height);

                TileDrawLimits limit = this.GetDrawLimits(layer, this.Window.ClientBounds);

                System.Diagnostics.Debug.WriteLine("xmin: " + limit.XMin + " xmax: " + limit.XMax);
                //System.Diagnostics.Debug.WriteLine("ymin: " + ymin + " ymax: " + ymax);
                TileCatalog catalog = layer.TileCatalog;
                for (int i = limit.XMin; i < limit.XMax; i++)
                {
                    for (int j = limit.YMin; j < limit.XMax; j++)
                    {

                        Rectangle source = catalog.TilePositions[map.GetTile(i, j).Index];
                        //calcular la posicion de cada tile donde corresponde, multiplicando el numero de turno por su tamaño
                        //pe. x = 5 * 10 = 50
                        Vector2 absolutePosition = Vector2.Multiply(new Vector2(i, j), layer.ZoomedTileSize);
                        //calcular la posicion con respecto a la posicion de la capa
                        Vector2 relativePosition = Vector2.Add(layer.Position, absolutePosition);

                        spriteBatch.Draw(catalog.Texture,
                                         relativePosition,
                                         source,
                                         Color.AliceBlue,
                                         0, Vector2.Zero, Vector2.Multiply(layer.Zoom,layer.ZoomScale), SpriteEffects.None, 0.0f);
                    }
                }
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }

        public TileDrawLimits GetDrawLimits(TileLayer layer, Rectangle clientBounds)
        {

            int[] xLimit = this.GetAxisLimit(layer.Position.X, layer.ZoomedSize.X, layer.ZoomedTileSize.X,layer.Lenght.X, clientBounds);
            int[] yLimit = this.GetAxisLimit(layer.Position.Y, layer.ZoomedSize.Y, layer.ZoomedTileSize.Y, layer.Lenght.Y, clientBounds);
            TileDrawLimits tileDrawLimits = new TileDrawLimits(xLimit[0], xLimit[1], yLimit[0], yLimit[1]);
            return tileDrawLimits;
        }

        public int[] GetAxisLimit(float position, float zoomedSize, float zoomedTileSize,int lenght, Rectangle clientBounds)
        {
            int min = 0;
            int max = 0;
            if (position < 0 && ((position + zoomedSize) > 0 && (position + zoomedSize) <= clientBounds.Width))
            {
                min = (int)(-position / zoomedTileSize);
                max = (int)((position + zoomedSize) / (zoomedTileSize)) + min + 2;
            }
            else if (position >= 0 && ((position + zoomedSize) <= clientBounds.Width))
            {
                min = 0;
                max = lenght;
            }
            else if (position >= 0 && ((position + zoomedSize) > clientBounds.Width))
            {
                min = 0;
                max = (int)((this.Window.ClientBounds.Width - position) / zoomedTileSize) + 2;
            }
            else if (position < 0 && ((position + zoomedSize) > clientBounds.Width))
            {
                min = (int)(-position / zoomedTileSize);
                max = (int)(clientBounds.Width / zoomedTileSize) + min + 2;
            }
            min = (int)MathHelper.Clamp(min, 0, lenght);
            max = (int)MathHelper.Clamp(max, 0, lenght);

            return new int[]{min,max};
        }

        void Window_ClientSizeChanged(object sender, EventArgs e)
        {
            this.backgrounScale = new Vector2(
                (float)this.Window.ClientBounds.Width / (float)graphics.PreferredBackBufferWidth,
                (float)this.Window.ClientBounds.Height / (float)graphics.PreferredBackBufferHeight);
            foreach (TileLayer layer in scene.Layers)
            {
                layer.ZoomScale = this.backgrounScale;
            }
        }
    }
}
