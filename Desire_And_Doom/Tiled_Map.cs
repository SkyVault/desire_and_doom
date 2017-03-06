using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiledSharp;

namespace Desire_And_Doom
{
    class Tiled_Map
    {
        private TmxMap              map;
        private Texture2D           texture;
        private List<Rectangle>     quads;

        public Tiled_Map(string name)
        {
            var current = Directory.GetCurrentDirectory();
            if (!File.Exists(current + "/Content/Maps/" + name + ".tmx"))
            {
                var files = Directory.GetFiles(current + "/Content/Maps/");
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
        }

        public void Draw(SpriteBatch batch)
        {
            foreach (var layer in map.Layers)
            {
                for (int y = 0; y < map.Height; y++)
                    for (int x = 0; x < map.Width; x++)
                    {
                        var tile = layer.Tiles[x + y * map.Width];
                        if (tile.Gid != 0)
                            batch.Draw(texture, new Vector2(x * map.TileWidth, y * map.TileHeight), quads[tile.Gid - 1], Color.White);
                    }
            }
        }
    }
}
