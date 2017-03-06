using System;
using Microsoft.Xna.Framework;

namespace Desire_And_Doom.ECS
{
    class Physics_Engine : System
    {
        private World world;
        public Physics_Engine(World world) : base(typeof(Body), typeof(Physics))
        {
            this.world = world;
        }

        public override void Update(GameTime time, Entity entity)
        {
            var body = (Body)entity.Get<Body>();
            var physics = (Physics)entity.Get<Physics>();

            float dt = (float)time.ElapsedGameTime.TotalSeconds;

            var x_body = new Body(new Vector2(body.X + physics.Vel_X * dt, body.Y), body.Size);
            var y_body = new Body(new Vector2(body.X, body.Y + physics.Vel_Y * dt), body.Size);

            var bodies = world.Get_All_With_Component<Physics>();
            foreach(var other in bodies)
            {
                if (other.UUID == entity.UUID) continue;
                var o_physics   = (Physics)other.Get<Physics>();
                var o_body      = (Body)other.Get<Body>();

                if (o_body.Contains(x_body)) { x_body = body; }
                if (o_body.Contains(y_body)) { y_body = body; }
                
            }

            physics.Velocity *= physics.Friction;
            physics.Direction = (float)Math.Atan2(body.Y - (body.Y + physics.Vel_Y * dt),body.X - (body.X + physics.Vel_X * dt)) + Physics.Deg_To_Rad(180);

            physics.Current_Speed = Vector2.Distance(body.Position, physics.Velocity * dt);

            body.X = x_body.X;
            body.Y = y_body.Y;
        }
    }
}
