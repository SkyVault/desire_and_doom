using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
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

        private Dictionary<string, Texture2D> textures;
        private Dictionary<string, SpriteFont> fonts;
        private Dictionary<string, List<Rectangle>> quads;

        private Assets() {
            textures = new Dictionary<string, Texture2D>();
            fonts = new Dictionary<string, SpriteFont>();
            quads = new Dictionary<string, List<Rectangle>>();
        }

        public void Add<T>(string id, T t)
        {
            if (t.GetType() == typeof(Texture2D))
                textures.Add(id, t as Texture2D);
            else if (t.GetType() == typeof(SpriteFont))
                fonts.Add(id, t as SpriteFont);
        }

        public void Generate_Animation(string id,Vector2 start_pos, Vector2 frame_size, int num_frames)
        {
            var aquads = new List<Rectangle>();
            for(int i = 0; i < num_frames; i++) {
                aquads.Add(new Rectangle((start_pos + new Vector2(i * frame_size.X, 0)).ToPoint(), frame_size.ToPoint()));
            }
            quads.Add(id, aquads);
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
            return default(T);
        }

        public static Assets It
        {
            get { if (it == null) it = new Assets(); return it; }
        }
    }
}
