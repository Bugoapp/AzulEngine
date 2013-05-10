using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace AzulEngine.CameraEngine
{
    public class Camera2D
    {

        public Vector2 Velocity { get; set; }
        public Vector2 Position { get; set; }
        public bool Changed { get; set; }
        public Vector2 Zoom { get; set; }

        public Camera2D(Vector2 position, Vector2 velocity, Vector2 zoom)
        {
            this.Position = position;
            this.Velocity = velocity;
            this.Zoom = zoom;
            this.Changed = false;
        }

        public Camera2D()
            : this(Vector2.Zero, Vector2.One, Vector2.One)
        {  }
    }
}
