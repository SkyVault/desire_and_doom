using Desire_And_Doom.ECS;
using Desire_And_Doom.ECS.Components;
using Desire_And_Doom.Screens;
using Desire_And_Doom.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using NLua;
using Penumbra;
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
        private Camera_2D           camera;

        public Func<string, float, float, bool> Change_Scene_Callback;

        public Tiled_Map(string name, Camera_2D _camera, World world, Screen level, PenumbraComponent lighting = null)
        {
            this.camera = _camera;
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
            
            Game1.Map_Height_Pixels = map.Height * map.TileHeight;

            foreach(var layer in map.ObjectGroups)
            {
                foreach(var obj in layer.Objects)
                {
                    if (obj.Type == "")
                    {
                        var physics_engine = (Physics_Engine)world.Get_System<Physics_Engine>();
                        if (physics_engine != null)
                        {
                            physics_engine.Add_Solid(new RectangleF((float)obj.X, (float)obj.Y, (float)obj.Width, (float)obj.Height));
                        }
                    }
                    else if (obj.Type == "Door")
                    {
                        if (obj.Properties.ContainsKey("Door") == false)
                            throw new Exception("Door object requires the property: Door, for example: Door 100 563");
                        var str = obj.Properties["Door"];
                        var toks = str.Split(' ');

                        var door = world.Create_Entity();
                        door.Add(new Body(new Vector2((float)obj.X, (float)obj.Y), new Vector2((float)obj.Width, (float)obj.Height)));
                        door.Add(new Physics(Vector2.Zero, Physics.PType.WORLD_INTERACTION));

                        var wi = (World_Interaction)door.Add(new World_Interaction(
                            (self, other) => {
                                var _wi = (World_Interaction)self.Get(Component.Types.World_Interaction);
                                if (other.Has(Component.Types.Player))
                                {
                                    if (Input.It.Is_Key_Pressed(Keys.Z))
                                    {
                                        Change_Scene_Callback?.Invoke(_wi.ID, _wi.X, _wi.Y);
                                    }
                                }
                                return true;
                            }));
                        wi.ID = toks[0];
                        wi.X = float.Parse(toks[1]);
                        wi.Y = float.Parse(toks[2]);
                        
                    }else if (obj.Type == "PointLight")
                    {
                        // light
                        var scale = 200f;
                        if (obj.Properties.ContainsKey("Scale")) scale = float.Parse(obj.Properties["Scale"]);
                        var color = Color.White;
                        if (obj.Properties.ContainsKey("Color"))
                        {
                            string color_string = obj.Properties["Color"];
                            var tokens = color_string.Split(' ');
                            color = new Color(
                                byte.Parse(tokens[0]),
                                byte.Parse(tokens[1]),
                                byte.Parse(tokens[2])
                                );
                        }

                        lighting.Lights.Add(new PointLight() {
                            Position = new Vector2((float)(obj.X + obj.Width / 2), (float)(obj.Y + obj.Height / 2)),
                            Scale = new Vector2(scale),
                            Color = color,
                            Intensity = 1f
                        });
                    }
                    else 
                    {
                        world.Create_Entity(
                            Assets.It.Get<LuaTable>(obj.Type),
                            (float)obj.X, (float)obj.Y
                            );
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

                for (int y = 0; y < map.Height; y++)
                    for (int x = 0; x < map.Width; x++)
                    {
                        //if (sort)
                        //{
                        //    //render_layer = (y / (Game1.Map_Height_Pixels));
                        //    //Console.WriteLine(y * map.TileHeight);
                        //}

                        if (x > map.Width - 1)  continue;
                        if (y > map.Height - 1) continue;

                        var tile = layer.Tiles[x + y * map.Width];
                        if (tile.Gid != 0)
                            batch.Draw(texture, new Vector2(x * map.TileWidth, y * map.TileHeight), quads[tile.Gid - 1], Color.White, 0, Vector2.Zero, 1f,SpriteEffects.None, render_layer);
                    }
            }
        }
    }
}
