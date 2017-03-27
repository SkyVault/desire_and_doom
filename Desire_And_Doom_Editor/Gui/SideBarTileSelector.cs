using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;

namespace Desire_And_Doom_Editor.cs.Gui
{
    class SideBarTileSelector : Panel
    {
        GraphicsDevice device;

        List<Tileset> tilesets;

        float zoom = 1;
        float last_scroll_wheel = 0;

        private Vector2 drag_start = Vector2.Zero;
        private Vector2 frame_position = Vector2.Zero;
        private bool dragging = false;

        private Vector2 Selector_Position = Vector2.Zero;

        public int Tile_Selection { get {
                if (tilesets.Count == 0) return 0;
                var tileset = tilesets.Last();
                
                

                return 0;
            }}

        public SideBarTileSelector(Monogui gui, GraphicsDevice device, List<Tileset> tilesets) : base(gui, true)
        {
            Scale_Children = true;
            this.tilesets = tilesets;
            this.device = device;
        }

        public override void Update(GameTime time)
        {
            base.Update(time);
            
            if (tilesets.Count > 0 && Mouse_In_Bounds() && Input.It.Mouse_Position().X > Local_Position.X + 16)
            {
                var size = tilesets.Last().TileWidth * zoom;

                var image = tilesets.Last().Image;
                var mpos = (Input.It.Mouse_Position() - Position - new Vector2(16, 0)) / zoom;

                Selector_Position.X = (float)(Math.Floor(mpos.X / size) * size);
                Selector_Position.Y = (float)(Math.Floor(mpos.Y / size) * size);

                var sw = Mouse.GetState().ScrollWheelValue;
                if (last_scroll_wheel != sw)
                {
                    var delta = sw - last_scroll_wheel;
                    //zoom += delta / 1000;
                    if (delta > 0)
                        zoom *= 1.08f;
                    else
                        zoom *= 0.92f;

                    last_scroll_wheel = sw;
                }

                if (Input.It.Mouse_Middle_Pressed())
                {
                    dragging = true;
                    drag_start = ((-Input.It.Mouse_Position() / zoom) - frame_position);
                }

                if (dragging)
                {
                    frame_position = ((-Input.It.Mouse_Position() / zoom) - drag_start);
                }

                if (!Input.It.Mouse_Middle())
                {
                    dragging = false;
                }

                if (Input.It.Mouse_Left_Pressed())
                {

                }
            }
        }

        public override void Draw(SpriteBatch batch)
        {
            Height = Editor.HEIGHT;
            
            if (tilesets.Count > 0)
            {
                var tileset = tilesets.Last();
                var image   = tileset.Image;
                var width   = (float)image.Width;
                var height  = (float)image.Height;
                var x_offset = 16;

                width *= zoom;

                if (frame_position.X < 0) frame_position.X = 0;
                if (frame_position.Y < 0) frame_position.Y = 0;
                
                var viewport = new Rectangle((int)frame_position.X, (int)frame_position.Y, (int)width, (int)height);
                
                batch.Draw(image,new Vector2(x_offset, 0) + Local_Position, viewport,Color.White, 0 , Vector2.Zero, zoom, SpriteEffects.None, 1);

                batch.DrawRectangle(new RectangleF((Selector_Position.X) + X + x_offset, (Selector_Position.Y) + Y, tileset.TileWidth / (image.Width / width), tileset.TileHeight / (image.Width / width)), Color.Red, 2);
                //device.Viewport = def;
            }

            base.Draw(batch);
        }
    }
}
