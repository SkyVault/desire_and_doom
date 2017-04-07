using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desire_And_Doom.Graphics
{
    abstract class Particle
    {

        public float Life_Max   { get; set; } = 10f;
        public float Life       { get; set; } = 0f;

        public Color Color { get; set; } = Color.White;
        
        public bool     Remove { get; set; } = false;

        public SpriteEffects Flip { get; set; } = SpriteEffects.None;

        public Rectangle Region { get; set; }
        public Texture2D Image  { get; set; }

        public Vector2 Position { get; set; }
        public float X { get { return Position.X; } set { Position = new Vector2(value, Position.Y); } }
        public float Y { get { return Position.Y; } set { Position = new Vector2(Position.X, value); } }

        public Vector2 Size { get; set; }
        public float Width  { get { return Size.X; } set { Size = new Vector2(value, Size.Y); } }
        public float Height { get { return Size.Y; } set { Size = new Vector2(Size.X, value); } }

        public Vector2 Velocity { get; set; } = Vector2.Zero;
        public float Vel_X { get { return Velocity.X; } set { Velocity = new Vector2(value, Velocity.Y); } }
        public float Vel_Y { get { return Velocity.Y; } set { Velocity = new Vector2(Velocity.X, value); } }

        public float Direction { get; set; } = 0f;
        public Vector2 Friction  { get; set; } = Vector2.One;

        public float Speed { get; set; } = 10f;
        public float Scale { get; set; } = 1f;
        public float Rotation { get; set; } = 0f;

        public float Transparency {
            get => this.Color.A / 255f;
            set => Color = new Color(Color.R, Color.G, Color.B, (byte) (255 * value));
        }

        public Particle()
        {
            Life = Life_Max;
        }

        public    void Destroy()                    => Remove = true;
        protected void Scale_Down(float by = 0.99f) => Scale *= by;
        protected void Apply_Friction()             => Velocity *= Friction;

        protected void Move_In_Direction() =>
            Velocity = new Vector2((float)Math.Cos(Direction), (float)Math.Sin(Direction)) * Speed;

        protected void Find_Direction() =>
            Direction = (float)Math.Atan2(Y - Vel_Y, X - Vel_X);

        protected void Apply_Velocity(GameTime time) =>
            Position += Velocity * (float)time.ElapsedGameTime.TotalSeconds;

        protected float Lerp(float a, float b, float time)  => (1f - time) * a + time * b;
        protected float Lerp2(float a, float b, float time) => a + (b - a) * time;

        public void Fade_Out()
        {
            if (Life < Life_Max / 2) return;
            var t = (1 / (Life / (Life_Max)) - 1);
            //Console.WriteLine(t);
            Transparency = Lerp(Transparency, 0, t);
        }

        public void Fade_In()
        {
            //if (Life > Life_Max / 2) return;
            Transparency = Lerp2(Transparency, 1, 0.16f);
        }

        protected void Countdown_Life(GameTime time)
        {
            Life -= (float)time.ElapsedGameTime.TotalSeconds;
        }

        public abstract void Update(GameTime time);

        public virtual void Draw(SpriteBatch batch)
        {
            batch.Draw(
                Image, 
                Position + new Vector2(Region.Width/2, Region.Width / 2), 
                Region, 
                new Color (this.Color.R/255f, this.Color.G/255f, this.Color.B/255f, Transparency), 
                Rotation, 
                new Vector2(Region.Width / 2, Region.Height /2), Scale, 
                Flip, 
                0.4f
                );
        }
    }
}
