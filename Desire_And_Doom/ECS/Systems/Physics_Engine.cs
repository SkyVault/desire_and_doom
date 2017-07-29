using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using static Desire_And_Doom.ECS.Component;
using System.Collections.Generic;
using Desire_And_Doom.Utils;
using Desire_And_Doom.ECS.Components;
using Desire_And_Doom.Graphics;

namespace Desire_And_Doom.ECS
{
    class Polygon
    {
        public Vector2 Position { get; set; }
        public List<Vector2> Points { get; set; }
    }

    class Line
    {
      public Vector2 Start, End;
    }

    class Polygon_Collision_Result
    {
        public bool Will_Intersect { get; set; } = false;
        public bool Intersect { get; set; } = false;
        public Vector2 Minimum_Translation_Vector { get; set; } = Vector2.Zero;
    }

    class Physics_Engine : System
    {
        private World world;
        private List<RectangleF> solids;
        private List<Polygon> polygons;
        
        public Physics_Engine(World world) : base(Types.Body, Types.Physics)
        {
            this.world = world;
            this.solids = new List<RectangleF>();
            this.polygons = new List<Polygon>();
        }

        public void Project_Polygon(Vector2 axis, Polygon polygon, ref float min, ref float max)
        {
            float dot_product = Vector2.Dot(axis, polygon.Points[0]);
            min = dot_product;
            max = dot_product;
            foreach (var v in polygon.Points)
            {
                dot_product = Vector2.Dot(v, axis);
                if (dot_product < min) min = dot_product;
                else if (dot_product > max) max = dot_product;
            }
        }

        public float Interval_Distance(float min_a, float max_a, float min_b, float max_b)
        {
            if (max_a < min_b) return min_a - max_a;
            else return min_a - max_b;
        }

        public bool Line_Intersection(Vector2 a1, Vector2 a2, Vector2 b1, Vector2 b2)
        {
            Vector2 intersection = Vector2.Zero;
            Vector2 b = Vector2.Subtract(a2, a1);
            Vector2 d = Vector2.Subtract(b2, b1);
            float b_dot_perp = b.X * d.Y - b.Y * d.X;
            if (b_dot_perp == 0) return false;

            Vector2 c = Vector2.Subtract(b1, a1);
            float t = (c.X * d.Y - c.Y * d.X) / b_dot_perp;
            if (t < 0 || t > 1) return false;

            float u = (c.X * b.Y - c.Y * b.X) / b_dot_perp;
            if (u < 0 || u > 1) return false;

            intersection = a1 + Vector2.Multiply(b, t);
            return true;
        }

        public bool Body_In_Polygon(Polygon poly, Body body)
        {
            Line left_wall = new Line
            {
                Start = body.Position,
                End = new Vector2(body.X, body.Y + body.Height)
            };

            Line bottom_wall = new Line
            {
                Start = new Vector2(body.X, body.Y + body.Height),
                End = new Vector2(body.X + body.Width, body.Y + body.Height)
            };

            Line right_wall = new Line
            {
                Start = new Vector2(body.X + body.Width, body.Y),
                End = new Vector2(body.X + body.Width, body.Y + body.Height)
            };

            Line top_wall = new Line
            {
                Start = body.Position,
                End = new Vector2(body.X + body.Width, body.Y)
            };

            for (int j = 0; j < poly.Points.Count; j += 2)
            {
                Vector2 start = poly.Points[j] + poly.Position;
                int end_index = j + 1;
                if (end_index > poly.Points.Count - 1)
                    end_index = 0;
                Vector2 end = poly.Points[end_index] + poly.Position;

                // check intersections
                if (Line_Intersection(left_wall.Start, left_wall.End, start, end) ||
                    Line_Intersection(bottom_wall.Start, bottom_wall.End, start, end) ||
                    Line_Intersection(right_wall.Start, right_wall.End, start, end) ||
                    Line_Intersection(top_wall.Start, top_wall.End, start, end))
                {
                    return true;
                }
            }
            return false;
        }

        public Polygon Add_Polygon(Polygon p)
        {
            polygons.Add(p);
            return p;
        }

        public RectangleF Add_Solid(RectangleF solid)
        {
            solids.Add(solid);
            return solid;
        }

        public void Clear_Solids()
        {
            solids.Clear();
        }

        public override void Update(GameTime time, Entity entity)
        {
            var body = (Body)entity.Get(Types.Body);
            var physics = (Physics)entity.Get(Types.Physics);

            float dt = (float)time.ElapsedGameTime.TotalSeconds;

            var x_body = new Body(new Vector2(body.X + physics.Vel_X * dt, body.Y), body.Size);
            var y_body = new Body(new Vector2(body.X, body.Y + physics.Vel_Y * dt), body.Size);
            y_body.Z = x_body.Z = body.Z;

            void handle_interaction(Entity other)
            {
                if (other.Has(Types.World_Interaction) == false) return;
                var o_interaction = (World_Interaction)other.Get(Types.World_Interaction);
                if (o_interaction.UType == World_Interaction.Update_Type.LAMBDA)
                    o_interaction?.Update(other, entity);
                else
                    o_interaction?.Lua_Update.Call(other, entity);
            }

            if (physics.Other != null) physics.Other = null;
            if (!entity.Has(Types.Item) && !entity.Has(Types.World_Interaction))
            {
                var bodies = world.Get_All_With_Component(Types.Physics);
                foreach (var other in bodies)
                {

                    if (other.UUID == entity.UUID) continue;
                    var o_physics = (Physics)other.Get(Types.Physics);
                    var o_body = (Body)other.Get(Types.Body);

                    if (o_physics.Handle_Collisions == false) continue;

                    if (o_physics.Physics_Type == Physics.PType.DYNAMIC || o_physics.Physics_Type == Physics.PType.STATIC)
                    {
                        bool blacklisted = physics.Contains_Blacklisted_Tag(other.Tags);
                        bool coll = false;
                        
                        if (o_body.Contains(x_body) && !blacklisted) {
                            handle_interaction(other);
                            x_body = body; coll = true;
                        }
                        if (o_body.Contains(y_body) && !blacklisted) {
                            handle_interaction(other);
                            y_body = body; coll = true;
                        }

                        if (coll && !blacklisted)
                        {
                            physics.Other = other;
                            physics.Callback?.Invoke(entity, other);
                        }

                    }else if (o_physics.Physics_Type == Physics.PType.WORLD_INTERACTION)
                    {
                        if (o_body.Contains(body))
                        {
                            var o_interaction = (World_Interaction)other.Get(Types.World_Interaction);
                            if (o_interaction.UType == World_Interaction.Update_Type.LAMBDA)
                                o_interaction?.Update(other, entity);
                            else
                                o_interaction?.Lua_Update.Call(other, entity);
                        }
                    }
                }

                foreach(var solid in solids)
                {
                    var o_body = new Body(solid.Location, solid.Size);

                    if (o_body.Contains(x_body)) { x_body = body; }
                    if (o_body.Contains(y_body)) { y_body = body; }
                }
                
                foreach(var poly in polygons)
                {
                    // get the lines of the rectangle
                    if (Body_In_Polygon(poly, x_body)) { x_body = body; }
                    if (Body_In_Polygon(poly, y_body)) { y_body = body; }
                }
            }
            
            physics.Velocity *= physics.Friction;

            physics.Direction = (float)Math.Atan2(body.Y - (body.Y + physics.Vel_Y * dt),body.X - (body.X + physics.Vel_X * dt)) + Physics.Deg_To_Rad(180);

            physics.Current_Speed = Vector2.Distance(body.Position,body.Position + physics.Velocity * dt);

            body.X = x_body.X;
            body.Y = y_body.Y;
            
        }

        //public override void UIDraw(SpriteBatch batch, Entity entity)
        //{
        //    base.Draw(batch, entity);

        //    if ( !Game1.DEBUG ) return;

        //    Draw_Solids(batch, camera);
        //}

        public override void UIDraw(SpriteBatch batch, Camera_2D camera, Entity entity)
        {
            base.UIDraw(batch, camera, entity);

            if ( !DesireAndDoom.DEBUG ) return;

            Draw_Solids(batch, camera);
        }

        public void Draw_Solids(SpriteBatch batch, Camera_2D camera)
        {
            var gui = (Texture2D) Assets.It.Get<Texture2D>("gui");
            solids.ForEach(s => {
                //batch.Draw(gui, new Rectangle((int)s.X, (int)s.Y, (int)s.Width, (int)s.Height), new Rectangle(24, 0, 24, 24), new Color(0, 0, 0, 10));
                var proj = camera.World_To_Screen(new Vector2(s.X, s.Y));
                Debug_Drawing.Draw_Line_Rect(batch, proj.X, proj.Y, s.Width * camera.Zoom, s.Height * camera.Zoom, Color.Red);
            });
            
            polygons.ForEach(p =>
            {
                for (int i = 0; i < p.Points.Count; i += 2)
                {
                    var start = camera.World_To_Screen(p.Points[i] + p.Position);
                    var other_index = i + 1;
                    if (other_index > p.Points.Count - 1)
                    {
                        other_index = 0;
                    }

                    var end = camera.World_To_Screen(p.Points[other_index] + p.Position);
                    Debug_Drawing.Draw_Line(batch, start, end, Color.Red);
                }
            });

            var bodies = World_Ref.Get_All_With_Component(Types.Physics);
            foreach(var entity in bodies )
            {
                var physics = (Physics) entity.Get(Types.Physics);
                var body = (Body) entity.Get(Types.Body);
                //batch.Draw(gui, new Rectangle((int) body.X, (int) body.Y, (int) body.Width, (int) body.Height), new Rectangle(24, 0, 24, 24), new Color(0, 0, 0, 10));

                // TODO: make it so that it turns red if it is collideing
                var proj = camera.World_To_Screen(new Vector2(body.X, body.Y));

                var color = Color.LimeGreen;
                if ( physics.Other != null )
                    color = Color.Red;

                Debug_Drawing.Draw_Line_Rect(batch, proj.X, proj.Y, body.Width * camera.Zoom, body.Height * camera.Zoom, color);
            }

        }
        
    }
}
