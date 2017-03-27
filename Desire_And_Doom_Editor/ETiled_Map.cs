using Desire_And_Doom_Editor;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using NLua;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiledSharp;
using MonoGame.Extended.Shapes;

namespace Desire_And_Doom_Editor
{
    class Tiled_Map
    {
        private TmxMap              map;
        private Texture2D           texture;
        private List<Rectangle>     quads;
        private TileView camera;

        private bool grid = true;

        public TmxMap Data { get => map; }

        public Tiled_Map(string name, TileView _camera)
        {
            this.camera = _camera;
            var current = Directory.GetCurrentDirectory();
            if (!File.Exists(current + "/Content/Maps/" + name + ".tmx"))
            {
               var files = Directory.GetFiles(current + "/Content/Maps");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("ERROR:: no map named {0} in directory, did you mean one of these?", name);
                foreach (var file in files)
                {
                    var toks = file.Split('\\', '/');
                    Console.WriteLine(toks.Last());
                }

                Console.ForegroundColor = ConsoleColor.White;
                Console.ReadKey();
            }

            map     = new TmxMap(Directory.GetCurrentDirectory() + "/Content/Maps/" + name + ".tmx");
            quads   = Assets.It.Get_Quads("quads");
            texture = Assets.It.Get<Texture2D>(map.Tilesets[0].Name);

            Editor.Map_Height_Pixels = map.Height * map.TileHeight;

            foreach(var layer in map.ObjectGroups)
            {
                foreach(var obj in layer.Objects)
                {
                    if (obj.Type == "")
                    {

                    }
                    else 
                    {
                        
                    }
                }
            }
        }

        public void Draw(SpriteBatch batch)
        {


            foreach (var layer in map.Layers)
            {
                var render_layer = 0.0f;
                if (layer.Properties.ContainsKey("layer"))
                    render_layer = float.Parse(layer.Properties["layer"]);
                var sort = false;
                if (layer.Properties.ContainsKey("sort"))
                    sort = bool.Parse(layer.Properties["sort"]);

                for (int y = 0; y < map.Width; y++)
                    for (int x = 0; x < map.Height; x++)
                    {
                        if (sort)
                        {
                            //render_layer = (y / (Game1.Map_Height_Pixels));
                            //Console.WriteLine(y * map.TileHeight);
                        }

                        if (x > map.Width - 1)  continue;
                        if (y > map.Height - 1) continue;

                        var tile = layer.Tiles[x + y * map.Width];
                        if (tile.Gid != 0)
                            batch.Draw(texture, new Vector2(x * map.TileWidth, y * map.TileHeight), quads[tile.Gid - 1], Color.White, 0, Vector2.Zero, 1,SpriteEffects.None, render_layer);
                    }
            }
            
            if (grid)
            {
                //var color = new Color(Color.White, 0.2f);
                //for (int y = 0; y < map.Height; y++)
                //{
                //    batch.DrawLine(0, y * map.TileHeight, map.Width * map.TileWidth, y * map.TileHeight, color, 1);
                //}

                //for (int x = 0; x < map.Width; x++)
                //{
                //    batch.DrawLine(x * map.TileWidth, 0, x * map.TileWidth, map.Height * map.TileHeight, color, 1);
                //}
            }
        }
    }
}
