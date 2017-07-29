using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiledSharp;

namespace Desire_And_Doom
{
    class Iso_Tiled_Map
    {
        private TmxMap map;

        public Iso_Tiled_Map(string name)
        {
            map = new TmxMap("Content/Maps/" + name);
        }

        public static Vector2 Cart_To_Iso(Vector2 cart_coords)
        {
            var iso_coords = Vector2.Zero;
            iso_coords.X = (cart_coords.X - cart_coords.Y);
            iso_coords.Y = (cart_coords.X + cart_coords.Y) / 2;
            return iso_coords;
        }

        public static Vector2 Iso_To_Cart(Vector2 iso_coords)
        {
            var card_coords = Vector2.Zero;
            card_coords.X = (2 * iso_coords.Y + iso_coords.X) / 2;
            card_coords.Y = (2 * iso_coords.X - iso_coords.X) / 2;
            return card_coords;
        }

        public void Render(SpriteBatch batch)
        {
            var image = Assets.It.Get<Texture2D>("tiles");
            var quads = Assets.It.Get_Quads("quads");
            var layer_num = 1;
            foreach(var layer in map.Layers)
            {
                for (int y = 0; y < (int)(map.Height); y++)
                {
                    for (int x = 0; x < (int)(map.Width); x++)
                    {
                        if (layer.Tiles[x + y * map.Width].Gid != 0)
                        {
                            float brightness = 1;
                            batch.Draw(
                                image,
                                Cart_To_Iso( 
                                    new Vector2(
                                        x * map.TileWidth / 2 + DesireAndDoom.WIDTH / 2, 
                                        y * map.TileHeight)), 
                                quads[layer.Tiles[x + y * map.Width].Gid - 1], 
                                new Color (brightness, brightness, brightness)
                                );
                        }
                    }
                }
                layer_num++;
            }
        }
    }
}
