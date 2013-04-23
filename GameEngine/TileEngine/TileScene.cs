using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameEngine.TileEngine
{
    public class TileScene
    {
        private bool Visible { get; set; }

        private List<TileLayer> layers;

        public List<TileLayer> Layers
        {
            get { return layers; }
        }

        public TileScene(List<TileLayer> layers, bool visible)
        {
            this.layers = layers;
            this.Visible = visible;
        }

        public TileScene()
            :this(new List<TileLayer>(),true)
        { }


        public void AddLayer(TileLayer layer)
        {
            this.layers.Add(layer);
        }

        public TileLayer GetLayer(int index)
        {
            return this.layers[index];
        }


    }
}
