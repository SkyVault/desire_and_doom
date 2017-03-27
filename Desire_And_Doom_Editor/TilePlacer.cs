using Desire_And_Doom_Editor.cs.Gui;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TiledSharp;

namespace Desire_And_Doom_Editor.cs
{
    class TilePlacer
    {
        Monogui GUI;
        TileView view;
        Frame frame;

        public Vector2 Selector { get; set; } = Vector2.Zero;

        private int tile_size = 8;
        private Tiled_Map map;
        private SideBarTileSelector tile_selector;
        private List<Tileset> tilesets;

        public TilePlacer(Tiled_Map map,Monogui GUI, TileView view, SideBarTileSelector tile_selector, List<Tileset> tilesets)
        {
            this.map = map;
            this.GUI = GUI;
            this.view = view;
            this.tile_selector = tile_selector;
            this.tilesets = tilesets;

            frame = new Frame(GUI) {
                X = 100,
                Y = 100,
                Width = 8,
                Height = 8,
                FillColor = Color.Red,
                Can_Intersect_Mouse = false
            };
        }

        public void Update(GameTime time)
        {
            var camera = view.Camera;

            var point = camera.ToWorld(Input.It.Mouse_Position());

            point.X = (float)Math.Floor(point.X / 8) * 8;
            point.Y = (float)Math.Floor(point.Y / 8) * 8;

            Selector = point / 8;

            point = camera.ToScreen(point);

            frame.X = point.X;
            frame.Y = point.Y;

            frame.Width = tile_size * camera.Scale;
            frame.Height = tile_size * camera.Scale;
            
            if (Input.It.Mouse_Right() & map != null && Selector.X >= 0 && Selector.Y >= 0)
            {
                foreach (var layer in map.Data.Layers)
                {
                    layer.Tiles[(int)(Selector.X + Selector.Y * map.Data.Width)] = new TmxLayerTile(0, (int)(Selector.X), (int)(Selector.Y));
                }
            }

            if (Input.It.Mouse_Left() && map != null && Selector.X >= 0 && Selector.Y >= 0)
            {
                foreach (var layer in map.Data.Layers)
                {
                    var id = tile_selector.Tile_Selection + 1;
                    layer.Tiles[(int)(Selector.X + Selector.Y * map.Data.Width)] = new TmxLayerTile((uint)id, (int)(Selector.X), (int)(Selector.Y));   
                }
            }
        }
    }
}
