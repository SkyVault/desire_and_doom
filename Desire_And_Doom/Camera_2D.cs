using Desire_And_Doom.ECS;
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
        private readonly Viewport viewport;
        public float Zoom { get; set; }
        public float Rotation { get; set; }

        public Vector2 Position { get; set; }
        public Vector2 Origin { get; set; }

        public float X { get { return Position.X; } set { Position = new Vector2(value, Position.Y); } }
        public float Y { get { return Position.Y; } set { Position = new Vector2(Position.X, value); } }

        private bool scrollable;

        public Camera_2D(Viewport _viewport, bool _scrollable = false)
        {
            this.viewport   = _viewport;
            this.scrollable = _scrollable;

            Zoom = 1;
            Rotation = 0;
            Origin = new Vector2(viewport.Width / 2f, viewport.Height / 2f);
        }

        public void Update(GameTime time)
        {
            Position += Input.It.Mouse_Drag() * 0.5f;
        }

        public void Track(Body body, float smoothing)
        {
            var dx = (X - (body.X + body.Width / 2) + Game1.WIDTH / 2);
            var dy = (Y - (body.Y + body.Height / 2) + Game1.HEIGHT / 2);

            X -= dx * smoothing;
            Y -= dy * smoothing;
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
