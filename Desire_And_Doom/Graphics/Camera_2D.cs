using Desire_And_Doom.ECS;
using Desire_And_Doom.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desire_And_Doom
{

    class Camera_2D
    {
        private readonly Camera2D camera;

        public float Zoom { get; set; }
        public float Rotation { get; set; }

        public Vector2 Position { get => camera.Position; }
        public Vector2 Origin { get => camera.Origin; }

        public float X { get { return Position.X; } }
        public float Y { get { return Position.Y; } }

        public Camera_2D(GraphicsDevice device, bool _scrollable = false)
        {
            
            Zoom = 1;
            Rotation = 0;
            
            camera = new Camera2D(device);
            
        }

        public void Update(GameTime time)
        {
        }

        public void Track(Body body, float smoothing)
        {
            var dx = (X - (body.X + body.Width / 2) + Game1.WIDTH / 2);
            var dy = (Y - (body.Y + body.Height / 2) + Game1.HEIGHT / 2);

            camera.Move(new Vector2(-dx * smoothing,-dy * smoothing));


            //camera.Position = new Vector2((float)Math.Floor(camera.Position.X), (float)Math.Floor(camera.Position.Y));
            //Console.WriteLine(bounds.X);
        }

        public Vector2 World_To_Screen(Vector2 point)
        {
            return camera.WorldToScreen(point);
        }

        public Vector2 Screen_To_World(Vector2 point)
        {
            return camera.ScreenToWorld(point);
        }

        public Matrix View_Matrix
        {
            get
            {
                return
                    Matrix.CreateTranslation(new Vector3(-Position, 0.0f)) *
                    Matrix.CreateTranslation(new Vector3(-Origin, 0.0f)) *
                    Matrix.CreateRotationZ(Rotation) *
                    Matrix.CreateScale(Zoom, Zoom, 1) *
                    Matrix.CreateTranslation(new Vector3(Origin, 0.0f));
            }
        }
    }
}
