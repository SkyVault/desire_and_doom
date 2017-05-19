using Desire_And_Doom.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using NLua;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desire_And_Doom
{
    class Assets
    {
        private static Assets it;
        private Lua lua;

        private Dictionary<string, List<Rectangle>> quads;
        private Dictionary<string, SpriteFont>      fonts;
        private Dictionary<string, Texture2D>       textures;
        private Dictionary<string, LuaTable>        entities;
        private Dictionary<string, LuaFunction>     lua_functions;
        private Dictionary<string, Animation>       animations;

        public ContentManager Content;

        private Assets() {
            textures        = new Dictionary<string, Texture2D>();
            entities        = new Dictionary<string, LuaTable>();
            fonts           = new Dictionary<string, SpriteFont>();
            quads           = new Dictionary<string, List<Rectangle>>();
            lua_functions   = new Dictionary<string, LuaFunction>();
            animations      = new Dictionary<string, Animation>();

            lua = new Lua();
        }

        public void Add(string id, object t)
        {
            if (t.GetType() == typeof(Texture2D))
                textures.Add(id, t as Texture2D);
            else if (t.GetType() == typeof(SpriteFont))
                fonts.Add(id, t as SpriteFont);
            else if (t.GetType() == typeof(LuaFunction))
                lua_functions.Add(id, t as LuaFunction);
            else if (t.GetType() == typeof(Animation)) {
                animations.Add(id, t as Animation);
            }
        }

        public Texture2D Load_Texture(string name, string id)
        {
            var texture = Content.Load<Texture2D>(name);
            textures.Add(id, texture);
            return texture;
        }

        public bool Has(string id, Type T)
        {
            if (T == typeof(Texture2D))
                return textures.ContainsKey(id);
            return false;
        }

        public void Remove(string id, object T)
        {
            if (T.GetType() == typeof(Texture2D) )
            {
                textures.Remove(id);
                Texture2D texture = (Texture2D) T;
                texture.Dispose();
            }
        }

        public void Add_Table(string file)
        {
            var table = lua.DoFile(file)[0] as LuaTable;
            foreach(string ent in table.Keys)
            {
                entities.Add(ent, table[ent] as LuaTable);
            }
        }
        
        public Animation Generate_Animation(string id,Vector2 start_pos, Vector2 frame_size, int num_frames)
        {
            // TODO: Remove the aquads and rectangle frames
            var aquads = new List<Rectangle>();
            var frames = new List<Animation_Frame>();
            
            for(int i = 0; i < num_frames; i++) {
                var spos = (start_pos + new Vector2(i * frame_size.X, 0));
                aquads.Add(new Rectangle(spos.ToPoint(), frame_size.ToPoint()));
                frames.Add(new Animation_Frame(spos, frame_size));
            }
            quads.Add(id, aquads);

            var animation = new Animation(frames, id);
            animations.Add(id, animation);

            return animation;
        }

        public void Load_Animations_From_Lua(string file)
        {
            LuaTable data = lua.DoFile(file)[0] as LuaTable;
            if (data["generate"] is LuaTable generate)
            {
                foreach (String key in generate.Keys)
                {
                    LuaTable gen_data = generate[key] as LuaTable;
                    int sx = (int)(gen_data[1] as double?);
                    int sy = (int)(gen_data[2] as double?);
                    int fw = (int)(gen_data[3] as double?);
                    int fh = (int)(gen_data[4] as double?);
                    int nm = (int)(gen_data[5] as double?);
                    
                    Animation animation = Generate_Animation(key, new Vector2(sx, sy), new Vector2(fw, fh), nm);

                    if (gen_data["offset_x"] != null )
                    {
                        animation.Offset_X = (float)(gen_data["offset_x"] as double?);
                    }

                    if ( gen_data["offset_y"] != null )
                    {
                        animation.Offset_Y = (float) (gen_data["offset_y"] as double?);
                    }

                    if ( gen_data["left_offset_x"] != null )
                    {
                        animation.Left_Face_Offset += new Vector2((float) (gen_data["left_offset_x"] as double?), 0);
                    }

                    if ( gen_data["right_offset_x"] != null )
                    {
                        animation.Right_Face_Offset += new Vector2((float) (gen_data["right_offset_x"] as double?), 0);
                    }

                    if (gen_data["right_offset_y"] != null )
                    {
                        animation.Right_Face_Offset += new Vector2(0, (float) (gen_data["right_offset_y"] as double?));
                    }

                    if (gen_data["left_offset_y"] != null )
                    {
                        animation.Left_Face_Offset += new Vector2(0, (float) (gen_data["left_offset_y"] as double?));
                    }

                    if (gen_data["speed"] != null )
                    {
                        float speed = (float) (gen_data["speed"] as double?);
                        foreach ( Animation_Frame frame in animation.Frames )
                            frame.Frame_Time = speed;
                    }

                }
            }
        }

        public void Generate_Quads(string id,int imgsize, int twidth, int theight)
        {
            var tile_quads = new List<Rectangle>();
            int ssize = imgsize;
            int tw = twidth;
            int th = theight;
            for (int y = 0; y < ssize / th; y++)
                for (int x = 0; x < ssize / tw; x++)
                    tile_quads.Add(new Rectangle(x * tw, y * th, tw, th));
            quads.Add(id, tile_quads);
        }

        public List<Rectangle> Get_Quads(string id) {
            if (quads.ContainsKey(id) == false)
                throw new Exception("ERROR:: cannot find quads: " + id);
            return quads[id];
        }

        public T Get <T>(string id){
            if (typeof(T) == typeof(Texture2D))
                return (T)(textures[id] as object);
            else if (typeof(T) == typeof(SpriteFont))
                return (T)(fonts[id] as object);
            else if (typeof(T) == typeof(LuaTable))
                return (T)(object)(entities[id]);
            else if (typeof(T) == typeof(Animation))
                return (T)(object)(animations[id]);
            return default(T);
        }

        public static Assets It
        {
            get { if (it == null) it = new Assets(); return it; }
        }
    }
}
