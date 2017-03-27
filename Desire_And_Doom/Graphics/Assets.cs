using Desire_And_Doom.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
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

        public void Add_Table(string file)
        {
            var table = lua.DoFile(file)[0] as LuaTable;
            foreach(string ent in table.Keys)
            {
                entities.Add(ent, table[ent] as LuaTable);
            }
        }
        
        public void Generate_Animation(string id,Vector2 start_pos, Vector2 frame_size, int num_frames)
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

        public List<Rectangle> Get_Quads(string id) { return quads[id]; }

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
