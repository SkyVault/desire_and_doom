using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desire_And_Doom.ECS
{
    class Body : Component
    {
        public Vector2 Position { get; set; }
        public Vector2 Size { get; set; }

        public float X { get { return Position.X; } set { Position = new Vector2(value, Position.Y); } }
        public float Y { get { return Position.Y; } set { Position = new Vector2(Position.X, value); } }
        public float Z { get; set; } = 0;

        public float Width  { get { return Size.X; } set { Size = new Vector2(value, Size.Y); } }
        public float Height { get { return Size.Y; } set { Size = new Vector2(Size.X, value); } }

        public float Angle_To_Other(Body other) => (float)(Math.Atan2(other.Y - Y, other.X - X));
        public float Angle_To_Other(Vector2 other) => (float)(Math.Atan2(other.Y - Y, other.X - X));

        public bool Contains(Body other)
        {
            return (
                X + Width > other.X && 
                X < other.X + other.Width && 
                Y + Height > other.Y && 
                Y < other.Y + other.Height
                ) && other.Z == Z;
        }

        public Body(Vector2 _position, Vector2 _size) : base(Types.Body)
        {
            Position = _position;
            Size = _size;
        }
    }
}
