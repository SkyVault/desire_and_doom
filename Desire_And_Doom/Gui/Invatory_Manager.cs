using Desire_And_Doom.ECS;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Desire_And_Doom.ECS.Component;

namespace Desire_And_Doom.Gui
{
    class Invatory_Manager
    {
        List<Invatory> invatories;
        public int Square_Size { get; set; } = 24;

        Point selector = Point.Zero;

        public bool Showing { get; set; } = false;

        public Invatory_Manager()
        {
            invatories = new List<Invatory>();
        }


        public void Add(Invatory inv)
        {
            invatories.Add(inv);
        }

        public void Remove(Invatory inv)
        {
            invatories.Remove(inv);
        }

        public void Update(GameTime time)
        {
            var total_width = 0;
            invatories.ForEach(i => total_width += (i.W * Square_Size));

            if ( Showing )
            {
                if ( Input.It.Is_Key_Pressed(Keys.Left) ) selector += new Point(-1, 0);
                if ( Input.It.Is_Key_Pressed(Keys.Right) ) selector += new Point(1, 0);
                if ( Input.It.Is_Key_Pressed(Keys.Up) ) selector += new Point(0, -1);
                if ( Input.It.Is_Key_Pressed(Keys.Down) ) selector += new Point(0, 1);
            }

            for (int i = 0; i < invatories.Count; i++ )
            {
                var inv = invatories[i];
                inv.Offset = new Vector2(Game1.WIDTH / 2, 32);

                // position the invatory correctly
                if ( i > 0 )
                {
                    inv.Offset -= new Vector2(total_width + invatories[i - 1].W * Square_Size, 0);
                }
            }
        }

        public void UIDraw(SpriteBatch batch)
        {
            if ( !Showing ) return;
            foreach(var invatory in invatories )
            {
                var gui     = (Texture2D) Assets.It.Get<Texture2D>("gui");
                var offset  = invatory.Offset;
                var size    = 48;

                for ( int y = 0; y < invatory.H; y++ )
                    for ( int x = 0; x < invatory.W; x++ )
                    {
                        var pos = new Vector2(x * 16, y * 16) + offset;
                        var region = new Rectangle(24, 0, 24, 24);

                        batch.Draw(gui, new Rectangle((int) offset.X + size * x, (int) offset.Y + size * y, size, size), new Rectangle(24, 0, 24, 24), new Color(0, 0, 0, 100));

                        var item = invatory.Get(y, x);

                        if ( item != null )
                        {
                            var spr = (Sprite) item.Get(Types.Sprite);
                            if ( spr != null )
                            {
                                batch.Draw(
                                    spr.Texture,
                                    new Vector2((float) offset.X + size * x, (float) offset.Y + size * y),
                                    spr.Quad,
                                    Color.White,
                                    0f,
                                    Vector2.Zero,
                                    4,
                                    SpriteEffects.None,
                                    1
                                    );
                            }
                        }
                    }
            }
        }
    }
}
