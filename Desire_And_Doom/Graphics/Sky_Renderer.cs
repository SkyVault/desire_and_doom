using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desire_And_Doom.Graphics
{
    class Sky_Renderer
    {
        Texture2D image;
        Texture2D BG_Image;

        bool show_bg = false;

        const int num_skytiles_wide = 10;
        const int num_skytiles_high = 20;

        List<Vector2> skytiles;
        List<float> scales;
        List<float> speeds;

        public Sky_Renderer(Texture2D image, bool bg_image = false)
        {
            this.image = image;
            skytiles = new List<Vector2>();
            scales = new List<float>();
            speeds = new List<float>();

            show_bg = bg_image;

            if (show_bg)
                BG_Image = Assets.It.Get<Texture2D>("Background_2");

            var rnd = new Random();
            for (int i = 0; i < num_skytiles_high; i++)
                for(int j = 0; j < num_skytiles_wide; j++)
                {
                    skytiles.Add(new Vector2(j * image.Width + (i % 2 == 0 ? image.Width / 4 : 0) + rnd.Next() % 20, i * image.Height / 1.7f + rnd.Next() % 20));
                    scales.Add((float)rnd.NextDouble() + 0.2f);
                    speeds.Add((float)rnd.NextDouble() * 2);
                }
        }

        public void Update(GameTime time)
        {
            for(int i = 0; i < skytiles.Count; i++)
            {
                var pos = skytiles[i];
                pos = new Vector2(pos.X + (float)time.ElapsedGameTime.TotalSeconds * 3f * speeds[i], pos.Y);
                if (pos.X - image.Width > (num_skytiles_wide - 2) * image.Width / 2)
                {
                    pos.X = -image.Width;
                    if ((pos.Y / image.Height) % 2 == 0)
                        pos.X += image.Width / 2;
                }


                skytiles[i] = pos;
            }
        }

        public void Draw(SpriteBatch batch)
        {
            //foreach(var pos in skytiles)
            for (int i = skytiles.Count - 1; i >= 0; i--)
            {
                var pos = skytiles[i];
                batch.Draw(
                    image, 
                    new Vector2(pos.X, pos.Y), 
                    new Rectangle(0, 0, image.Width, image.Height), 
                    new Color(1, 1, 1, 0.5f), 
                    0, 
                    Vector2.Zero, 
                    scales[i], 
                    SpriteEffects.None, 
                    0.01f);
            }

            if (show_bg)
            {
                batch.Draw(
                    BG_Image,
                    new Rectangle(0, 0, BG_Image.Width, BG_Image.Height),
                    new Rectangle(0, 0, BG_Image.Width, BG_Image.Height),
                    Color.White,
                    0,
                    Vector2.Zero,
                    SpriteEffects.None,
                    0.03f
                    );
            }
        }
    }
}
