using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using static Desire_And_Doom.ECS.Component;
using Desire_And_Doom.ECS.Components;
using Desire_And_Doom.Graphics;
using Desire_And_Doom.Gui;

namespace Desire_And_Doom.ECS
{
    class Player_Controller_System : System
    {
        GameCamera camera;
        Particle_World particle_world;
        Invatory_Container invatory_container;
        Invatory_Manager invatory_manager;

        bool show_overlay_gui = false;
        const float dash_speed = 500;

        public Player_Controller_System(GameCamera _camera, Particle_World particle_world, Invatory_Manager invatory_manager) : base(Types.Body, Types.Player, Types.Physics)
        {
            this.invatory_manager = invatory_manager;
            this.camera = _camera;
            this.particle_world = particle_world;
        }

        public override void Load(Entity entity)
        {
            // add dust emitter
            //var body = (Body) entity.Get(Types.Body);
            //entity.Add(new Entity_Particle_Emitter(
            //    new Dust_Emitter(body.Position, true),
            //    particle_world, 
            //    true
            //    ));
            invatory_container = new Invatory_Container();

            var invatory = (Invatory) entity.Get(Types.Invatory);
            if (invatory != null )
            {
                // add the invatory to the manager
                invatory_manager.Add(invatory);
            }
            
            var physics = (Physics)entity.Get(Types.Physics);
            physics.Blacklisted_Collision_Tags.Add("Player-Hit");
            physics.Callback = (self, o) =>
            {

                if (o.Has_Tag("Enemy"))
                {
                    var health = (Health)self.Get(Types.Health);
                    health.Hurt(1, true);
                }

                if (o.Has(Types.Item))
                {
                    var item = (Item)o.Get(Types.Item);
                    if (item.Can_Collect)
                    {
                        var inv = (Invatory)entity.Get(Types.Invatory);
                        if (inv.Has_Space())
                        {
                            inv.Add_Item(o);
                            o.Destroy();
                        }
                    }
                }

                return false;
            };

            var equipment = (Equipment) entity.Get(Types.Equipment);
            if ( equipment != null )
            {
                equipment.Left_Hand = new Items.Equipable(Items.Equipable.Equipment_Type.WEAPON) {
                    Run_Animation_ID = "player-claymore-run",
                    Idle_Animation_ID = "player-claymore-idle",
                    Use_Animation_ID = "player-claymore-attack",
                    ID = "Claymore"
                };
            }
        }

        public override void Destroy(Entity entity)
        {
            base.Destroy(entity);

            invatory_manager.Remove((Invatory) entity.Get(Types.Invatory));
        }

        public void Create_Sword_Hitbox(Physics physics, Body body)
        {
            var side = physics.Velocity.X > 0 ? 1 : -1;

            var hit_size = 16;
            var hit = World_Ref.Create_Entity();
            hit.Tags.Add("Player-Hit");
            hit.Add(new Body(new Vector2(body.Position.X + hit_size * side, body.Position.Y - body.Height / 2 - hit_size), new Vector2(hit_size * 1.8f, hit_size * 2)));
            hit.Add(new Timed_Destroy(0.1f));
            var phy = (Physics) hit.Add(new Physics(Vector2.Zero, Physics.PType.DYNAMIC));
            phy.Blacklisted_Collision_Tags.Add("Player");

            hit.Update = (self) => {

                var h_body = (Body) self.Get(Types.Body);
                h_body.Position = body.Position + new Vector2(physics.Velocity.X > 0 ? hit_size : -hit_size, -h_body.Height + 16);

                return true;
            };
        }

        public override void Update(GameTime time, Entity entity)
        {
            // get all of the needed components
            var body        = (Body)entity.Get(Types.Body);
            var physics     = (Physics)entity.Get(Types.Physics);
            var sprite      = (Animated_Sprite)entity.Get(Types.Animation);
            var invatory    = (Invatory)entity.Get(Types.Invatory);
            var player      = (Player)entity.Get(Types.Player);
            var equipment   = (Equipment) entity.Get(Types.Equipment);
            var dust        = (Entity_Particle_Emitter) entity.Get(Types.Entity_Particle_Emitter);

            // default the animation state to be the idle animation
            if (player.State != Player.Action_State.ATTACKING)
                sprite.Current_Animation_ID = "player-idle";

            // check if a dialog box opened up
            //if (Messanger.It.Top() == "Dialog_Open")    player.Can_Move = false;
            //if (Messanger.It.Top() == "Dialog_Closed")  player.Can_Move = true;
            //if (Debug_Console.Open) player.Can_Move = false;
            //else player.Can_Move = true;

            // get input 
            var state = GamePad.GetState(PlayerIndex.One);
            var x_axis = state.ThumbSticks.Left.X;
            var y_axis = state.ThumbSticks.Left.Y;
            var dash = (state.Triggers.Left > 0.2f);
            var action = state.Buttons.A == ButtonState.Pressed;
            var attack = state.Buttons.X == ButtonState.Pressed;

            string player_run_anim = "player-run";
            if (equipment.Left_Hand != null )
            {
                if ( player.State != Player.Action_State.ATTACKING )
                    sprite.Current_Animation_ID = equipment.Left_Hand.Idle_Animation_ID;
                player_run_anim = equipment.Left_Hand.Run_Animation_ID;
            }

            if (player.Can_Move && player.State != Player.Action_State.DASHING && player.State != Player.Action_State.IN_INVATORY)
            {
                var mult = (player.State == Player.Action_State.ATTACKING) ? player.Attack_Walk_Speed_Multiplyer : 1;
                var speed = 10 * mult;
                
                if (Input.It.Is_Key_Down(Keys.Space))
                {
                    body.Z = 1;
                }
                else body.Z = 0;
                
                if (Input.It.Is_Key_Down(Keys.Left) || x_axis < 0)
                {
                    if (player.State != Player.Action_State.ATTACKING)
                        sprite.Current_Animation_ID = player_run_anim;
                    physics.Apply_Force(speed, Physics.Deg_To_Rad(180));
                    sprite.Scale = new Vector2(-1, sprite.Scale.Y);
                }

                if (Input.It.Is_Key_Down(Keys.Right) || x_axis > 0)
                {
                    if (player.State != Player.Action_State.ATTACKING)
                        sprite.Current_Animation_ID = player_run_anim;
                    physics.Apply_Force(speed, Physics.Deg_To_Rad(0));
                    sprite.Scale = new Vector2(1, sprite.Scale.Y);
                }

                if (Input.It.Is_Key_Down(Keys.Up) || y_axis > 0)
                {
                    physics.Apply_Force(speed * 0.8f, Physics.Deg_To_Rad(270));
                    if (player.State != Player.Action_State.ATTACKING)
                        sprite.Current_Animation_ID = player_run_anim;
                }

                if (Input.It.Is_Key_Down(Keys.Down) || y_axis < 0)
                {
                    if (player.State != Player.Action_State.ATTACKING)
                        sprite.Current_Animation_ID = player_run_anim;
                    physics.Apply_Force(speed * 0.8f, Physics.Deg_To_Rad(90));
                }
            }

            if (Input.It.Is_Key_Pressed(Keys.C))
            {
                sprite.Force_Play_Animation("player-smoke");
            }
        
            // Controll
            if (player.Can_Move)
            {
                if (Input.It.Is_Key_Pressed(Keys.LeftShift) == true || dash == true)
                {
                    if (player.Dash_Timer <= 0 && player.State != Player.Action_State.DASHING)
                    {
                        player.State = Player.Action_State.DASHING;
                        var direction = physics.Direction;
                        physics.Velocity = Vector2.Zero;
                        physics.Apply_Force(dash_speed, direction);
                        player.Dash_Timer = player.Max_Dash_Time;
                    }
                }

                if (player.Dash_Timer <= 0 && player.State == Player.Action_State.DASHING)
                {
                    player.State = Player.Action_State.RUNNING;
                }

                if ((Input.It.Is_Key_Down(Keys.X) || GamePad.GetState(PlayerIndex.One).IsButtonDown(Buttons.X)) && player.State != Player.Action_State.ATTACKING)
                {
                    if ( equipment.Left_Hand != null )
                    {
                        sprite.Current_Animation_ID = "player-attack";
                        sprite.Current_Animation_ID = equipment.Left_Hand.Use_Animation_ID;
                        sprite.Current_Frame = 0;
                        player.State = Player.Action_State.ATTACKING;

                        // create the collideable
                        Create_Sword_Hitbox(physics, body);
                    }
                }
                else
                {
                    if (player.State == Player.Action_State.ATTACKING)
                    {
                        if (sprite.Animation_End) {
                            player.State = Player.Action_State.IDLE;
                        }
                    }
                }
            }
            player.Dash_Timer -= (float)time.ElapsedGameTime.TotalSeconds;

            // (TEMP): toggle the equipment
            if ( Input.It.Is_Key_Pressed(Keys.E) ) {
                if (equipment.Left_Hand != null)
                    equipment.Left_Hand = null;
                else {
                    // TODO: Clean this up so that i dont hard code the player equipment
                    equipment.Left_Hand = new Items.Equipable(Items.Equipable.Equipment_Type.WEAPON) {
                        Run_Animation_ID = "player-claymore-run",
                        Idle_Animation_ID = "player-claymore-idle",
                        Use_Animation_ID = "player-claymore-attack",
                        ID = "Claymore"
                    };
                }
            }

            // handle the dust emitter
            if ( dust != null )
            {
                dust.Active = physics.Current_Speed > 1.2f;
                dust.Offset = new Vector2(dust.Offset.X, -8);
                if ( physics.Vel_X > 0 )
                {
                    dust.Offset = new Vector2(-4, dust.Offset.Y);
                }
                else
                {
                    dust.Offset = new Vector2(-8, dust.Offset.Y);
                }
            }

            camera.Track(body, 0.1f);
        }

        public override void Constant_Update(GameTime time, Entity entity)
        {
            base.Constant_Update(time, entity);
            var player = (Player)entity.Get(Types.Player);

            if ( Input.It.Is_Key_Down(Keys.LeftControl) && Input.It.Is_Key_Pressed(Keys.P) )
            {
                DesireAndDoom.Toggle_Pause();
            }
            
            // Toggle the gui and invatory screen
            var invatory = (Invatory) entity.Get(Types.Invatory);
            if ( Input.It.Is_Key_Pressed(Keys.Q) )
            {
                show_overlay_gui = !show_overlay_gui;
                invatory_manager.Showing = show_overlay_gui;

                if (show_overlay_gui)
                    player.State = Player.Action_State.IN_INVATORY;
                else
                    player.State = Player.Action_State.IDLE;

                //DesireAndDoom.Toggle_Pause();
            }

            //if (Game1.Game_State == Game1.State.PAUSED && invatory.Draw)
            //    invatory_container.Update(time);
        }

        public override void Draw(SpriteBatch batch, Entity entity)
        {
            var gui     = Assets.It.Get<Texture2D>("gui");
            var health  = (Health)entity.Get(Types.Health);

            var margin = 4;
            Vector2 health_pos = camera.Screen_To_World(new Vector2(
                DesireAndDoom.ScreenWidth - (16 * health.Amount * DesireAndDoom.SCALE) - (margin * health.Amount * DesireAndDoom.SCALE), 
                16
                ));

            for (int i = 0; i < health.Amount; i++)
            {
                if (i > 0) health_pos += new Vector2(16 + margin, 0);
                batch.Draw(
                    gui,
                    health_pos,
                    new Rectangle(0, 24, 16, 16),
                    Color.White,
                    0f,
                    Vector2.Zero,
                    1f,
                    SpriteEffects.None,
                    1f
                    );
            }
        }

        public override void UIDraw(SpriteBatch batch, GameCamera camera, Entity entity)
        {
            base.UIDraw(batch, camera, entity);
            if ( !show_overlay_gui ) return;

            var gui = Assets.It.Get<Texture2D>("gui");



            int y_offset = 32;

            var region = new Rectangle(24, 0, 24, 24);
            batch.Draw(gui, new Rectangle(96, 32+y_offset, 48, 48), region, new Color(0, 0, 0, 100));
            batch.Draw(gui, new Rectangle(96, 32+ y_offset + 48 + 2, 48, 48), region, new Color(0, 0, 0, 100));

            // left hand box
            batch.Draw(gui, new Rectangle(96 - 48 - 2, 32 + 48 + 2 + y_offset, 48, 48), region, new Color(0, 0, 0, 100));
            // right hand box
            batch.Draw(gui, new Rectangle(96 + 48 + 2, 32 + 48 + 2 + y_offset, 48, 48), region, new Color(0, 0, 0, 100));

            batch.Draw(gui, new Rectangle(96, 32 + 48 * 2 + 4 + y_offset, 48, 48), region, new Color(0, 0, 0, 100));

            var equipment = (Equipment) entity.Get(Types.Equipment);
            var font = (SpriteFont) Assets.It.Get<SpriteFont>("font");
            var inv = (Invatory) entity.Get(Types.Invatory);

            // draw money
            var items = Assets.It.Get<Texture2D>("items");
            var coin_region = new Rectangle(0, 0, 8, 8);
            var coin_pos = new Vector2(16, 16);
            batch.Draw(items, coin_pos, coin_region, Color.White, 0, Vector2.Zero, 4, SpriteEffects.None, 1);
            batch.DrawString(font, inv.Money.ToString(),coin_pos + new Vector2(48, 0), Color.White);

            // left hand (TODO): finish the rest of the boxes
            if ( equipment.Left_Hand != null )
                batch.DrawString(font, equipment.Left_Hand.ID, new Vector2(96 - 48 - 2, 32 + 48 + 2 + y_offset), Color.White, 0, Vector2.Zero, 0.5f, SpriteEffects.None, 1);

            // right hand


        }
    }
}
