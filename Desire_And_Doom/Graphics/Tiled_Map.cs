using Desire_And_Doom.ECS;
using Desire_And_Doom.ECS.Components;
using Desire_And_Doom.Graphics;
using Desire_And_Doom.Graphics.Particle_Systems;
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
    class Billboard
    {
        public Vector2 Position { get; set; } = Vector2.Zero;
        public Rectangle Region { get; set; } = Rectangle.Empty;
    }

    class Tiled_Map
    {
        private TmxMap              map;
        private Texture2D           texture;
        private List<Rectangle>     quads;
        private Camera_2D           camera;
        private List<Billboard>     billboards;

        public Func<string, float, float, bool> Change_Scene_Callback;

        public bool Has_Sky { get; private set; } = false;

        public Tiled_Map(string name, Camera_2D _camera, World world, Screen level, Particle_World particle_world, PenumbraComponent lighting = null)
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

            map         = new TmxMap(Directory.GetCurrentDirectory() + "/Content/Maps/" + name + ".tmx");
            quads       = Assets.It.Get_Quads("quads");
            texture     = Assets.It.Get<Texture2D>(map.Tilesets[0].Name);
            billboards  = new List<Billboard>();

            if ( map.Properties.ContainsKey("Sky") )
            {
                Has_Sky = map.Properties["Sky"] ==  "true";
            }
            
            Game1.Map_Height_Pixels = map.Height * map.TileHeight;

            for (int i = map.Layers.Count - 1; i >= 0; i--)
            {
                var layer = map.Layers[i];
                if (layer.Properties.ContainsKey("ignore") == true)
                    map.Layers.RemoveAt(i);
            }

            foreach(var layer in map.ObjectGroups)
            {
                foreach ( var obj in layer.Objects )
                {
                    if ( obj.Type == "" )
                    {
                        var physics_engine = (Physics_Engine) world.Get_System<Physics_Engine>();
                        if ( physics_engine != null )
                        {
                            physics_engine.Add_Solid(new RectangleF((float) obj.X, (float) obj.Y, (float) obj.Width, (float) obj.Height));
                        }
                    } else if ( obj.Type == "Particle" )
                    {
                        //if ()
                        Debug.Assert(obj.Properties.ContainsKey("Type"), "ERROR:: Particle object requires a Type property!");
                        var type = obj.Properties["Type"];

                        switch ( type ) {
                            case "Fire_Fly":
                                particle_world.Add(new Fire_Fly_Emitter(new Vector2(0, 0))); break;
                            case "Fire":
                                Console.WriteLine("Hey");
                                particle_world.Add(new Fire_Emitter(new Vector2((float) obj.X, (float) obj.Y))); break;
                            default:
                                Console.WriteLine("WARNING:: unknown particle type: " + type);
                                Console.WriteLine("Did you mean one of these?");
                                Console.Write("1) Fire_Fly\n2) Fire\n3) Blue_Fire");
                                break;
                        }
                    }
                    else if ( obj.Type == "Billboard" )
                    {
                        Debug.Assert(obj.Properties.ContainsKey("Region"), "Billboard object requires the property: Region, for example: x y width height");
                        var str = obj.Properties["Region"];
                        var toks = str.Split(' ');

                        var billboard = new Billboard() {
                            Position = new Vector2((float) obj.X, (float) obj.Y),
                            Region = new Rectangle(int.Parse(toks[0]), int.Parse(toks[1]), int.Parse(toks[2]), int.Parse(toks[3]))
                        };
                        billboards.Add(billboard);
                    }
                    else if ( obj.Type == "Door" )
                    {
                        Debug.Assert(obj.Properties.ContainsKey("Door"), "Door object requires the property: Door, for example: Door 100 563");

                        var str = obj.Properties["Door"];
                        var toks = str.Split(' ');

                        var door = world.Create_Entity();
                        door.Add(new Body(new Vector2((float) obj.X, (float) obj.Y), new Vector2((float) obj.Width, (float) obj.Height)));
                        door.Add(new Physics(Vector2.Zero, Physics.PType.WORLD_INTERACTION));

                        var wi = (World_Interaction) door.Add(new World_Interaction(
                            (self, other) => {
                                var _wi = (World_Interaction) self.Get(Component.Types.World_Interaction);
                                if ( other.Has(Component.Types.Player) )
                                {
                                    if ( Input.It.Is_Key_Pressed(Keys.Z) || GamePad.GetState(PlayerIndex.One).Buttons.A == ButtonState.Pressed )
                                    {
                                        Change_Scene_Callback?.Invoke(_wi.ID, _wi.X, _wi.Y);
                                    }
                                }
                                return true;
                            }));
                        wi.ID = toks[0];
                        wi.X = float.Parse(toks[1]);
                        wi.Y = float.Parse(toks[2]);

                    } else if ( obj.Type == "PointLight" )
                    {
                        // light
                        var scale = 200f;
                        if ( obj.Properties.ContainsKey("Scale") ) scale = float.Parse(obj.Properties["Scale"]);
                        var color = Color.White;
                        if ( obj.Properties.ContainsKey("Color") )
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
                            Position = new Vector2((float) (obj.X + obj.Width / 2), (float) (obj.Y + obj.Height / 2)),
                            Scale = new Vector2(scale),
                            Color = color,
                            Intensity = 1f
                        });
                    }
                    else
                    {
                        world.Create_Entity(
                            Assets.It.Get<LuaTable>(obj.Type),
                            (float) obj.X, (float) obj.Y
                            );
                    }
                }
            }
        }

        public void Destroy()
        {
            billboards.Clear();
        }

        public void Update(GameTime time)
        {
            var camera_pos = camera.Left;
            var controller = camera.Get_Controller();


            if (camera_pos.X < 0)
                controller.Position = new Vector2(-Game1.WIDTH / camera.Zoom, camera.Y);
            if (camera_pos.Y < 0)
                controller.Position = new Vector2(camera.X, -Game1.HEIGHT / camera.Zoom);

            var camera_bottom_right = camera.Left + new Vector2(Game1.WIDTH / camera.Zoom, Game1.HEIGHT / camera.Zoom);
            if (camera_bottom_right.X > map.Width * map.TileWidth)
                controller.Position = new Vector2((map.Width * map.TileWidth) - (Game1.WIDTH / camera.Zoom) * 2, camera.Y);
            if (camera_bottom_right.Y > map.Height * map.TileHeight)
                controller.Position = new Vector2(camera.X, (map.Height * map.TileHeight) - (Game1.HEIGHT / camera.Zoom) * 2);

            //if (camera_bottom_right.Y > map.Height * map.TileHeight)

        }

        public void Draw(SpriteBatch batch)
        {
            Vector2 camera_position = camera.Left;
            //Console.WriteLine(camera_frustum.GetCorners()[0]);

            foreach (var layer in map.Layers)
            {
                var render_layer = 0.0f;
                if (layer.Properties.ContainsKey("layer"))
                    render_layer = float.Parse(layer.Properties["layer"]);
                var sort = false;
                if (layer.Properties.ContainsKey("sort"))
                    sort = bool.Parse(layer.Properties["sort"]);

                int cx = (int)(camera_position.X / 8);
                int cy = (int)(camera_position.Y / 8);
                int cw = (int)((Game1.WIDTH  / camera.Zoom) / 8);
                int ch = (int)((Game1.HEIGHT / camera.Zoom) / 8);

                if (cx < 0) cx = 0;
                if (cy < 0) cy = 0;

                for (int y = cy; y < cy + ch + 2; y++)
                    for (int x = cx; x < cx + cw + 2; x++)
                    {
                        if (x > map.Width - 1)  continue;
                        if (y > map.Height - 1) continue;

                        var tile = layer.Tiles[x + y * map.Width];
                        if (tile.Gid != 0)
                            batch.Draw(texture, new Vector2(x * map.TileWidth, y * map.TileHeight), quads[tile.Gid - 1], Color.White, 0, Vector2.Zero, 1f,SpriteEffects.None, render_layer);
                    }
            }

            foreach (var billboard in billboards)
            {
                var y = billboard.Position.Y + (billboard.Region.Height * 0.8f);
                var layer = 0.3f + (y / Game1.Map_Height_Pixels) * 0.1f;
                batch.Draw(texture, billboard.Position, billboard.Region, Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, layer);
            }
        }
    }
}
