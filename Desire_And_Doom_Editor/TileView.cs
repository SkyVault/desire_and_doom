using Comora;
using Desire_And_Doom_Editor;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desire_And_Doom_Editor
{

    class TileView
    {
        private readonly Viewport viewport;
        public float Zoom { get; set; }
        public float Rotation { get; set; }

        public Vector2 Position { get; set; }
        public Vector2 Origin { get; set; }

        public float X { get { return Position.X; } set { Position = new Vector2(value, Position.Y); } }
        public float Y { get { return Position.Y; } set { Position = new Vector2(Position.X, value); } }

        private bool scrollable;

        private Vector2 scroll_offset = Vector2.Zero;
        private Vector2 camera_start = Vector2.Zero;
        private bool dragging = false;

        private float scroll_wheel_delta = 0;
        private float last_scroll_wheel = 0;

        private Camera camera;

        public TileView(GraphicsDevice device, bool _scrollable = false)
        {
            this.viewport   = device.Viewport;
            this.scrollable = _scrollable;
            this.camera = new Camera(device);
            this.camera.LoadContent(device);

            Zoom = 1;
            Rotation = 0;
            Origin = new Vector2(viewport.Width / 2f, viewport.Height / 2f);

            //camera.Grid.IsVisible = false;
        }

        public Vector2 Scroll_Offset { get => scroll_offset; }

        public Vector2 Screen_To_World(Vector2 position)
        {
            return position * Zoom;
        }

        public void Update_Mouse_Panning(GameTime time)
        {
            if (Input.It.Mouse_Middle_Pressed())
            {
                dragging = true;
                camera_start = ((-Input.It.Mouse_Position() / camera.Scale) - camera.Position);
            }

            if (dragging)
            {
                camera.Position = ((-Input.It.Mouse_Position() / camera.Scale) - camera_start);
            }

            var sw = Mouse.GetState().ScrollWheelValue;
            if (last_scroll_wheel != sw && !camera.IsAnimated)
            {
                var delta = sw - last_scroll_wheel;
                if (delta > 0)
                    this.camera.Zoom(TimeSpan.FromSeconds(0.2), this.camera.Scale * 1.2f);
                else
                    this.camera.Zoom(TimeSpan.FromSeconds(0.2), this.camera.Scale * 0.8f);
                last_scroll_wheel = sw;
            }

            float speed = 400 / camera.Scale;
            if (Input.It.Is_Key_Down(Keys.A))
                camera.Position += new Vector2(-speed * (float)time.ElapsedGameTime.TotalSeconds, 0);

            if (Input.It.Is_Key_Down(Keys.D))
                camera.Position += new Vector2(speed * (float)time.ElapsedGameTime.TotalSeconds, 0);

            if (Input.It.Is_Key_Down(Keys.W))
                camera.Position += new Vector2(0, -speed * (float)time.ElapsedGameTime.TotalSeconds);

            if (Input.It.Is_Key_Down(Keys.S))
                camera.Position += new Vector2(0, speed * (float)time.ElapsedGameTime.TotalSeconds);

            if (!Input.It.Mouse_Middle())
            {
                dragging = false;
            }
            camera.Update(time);
        }

        public void Update(GameTime time)
        {
        }
        
        public Camera Camera
        {
            get => camera;
        }
    }
}
