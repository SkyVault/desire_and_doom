﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using static Desire_And_Doom.ECS.Component;
using Desire_And_Doom.ECS.Components;
using Desire_And_Doom.Graphics;
using Desire_And_Doom.Gui;
using Desire_And_Doom.Screens;
using Desire_And_Doom.Items;
using System;

namespace Desire_And_Doom.ECS
{
    class Player_Controller_System : System
    {
        private readonly Equipable Claymore = new Equipable(Equipable.Equipment_Type.WEAPON) {
            Run_Animation_ID = "player-claymore-run",
            Idle_Animation_ID = "player-claymore-idle",
            Use_Animation_ID = "player-claymore-attack-1",
            ID = "Claymore"
        };

        GameCamera camera;
        Particle_World particle_world;
        Invatory_Container invatory_container;
        Screen_Manager screen_manager;

        bool show_overlay_gui = false;
        const float dash_speed = 500;

        public Player_Controller_System(GameCamera _camera, Particle_World particle_world, Screen_Manager screen) : base(Types.Body, Types.Player, Types.Physics)
        {
            this.screen_manager = screen;
            this.camera = _camera;
            this.particle_world = particle_world;
        }

        public override void Load(Entity entity)
        {
            invatory_container = new Invatory_Container();
            var invatory = (Invatory) entity.Get(Types.Invatory);
            
            var physics = (Physics)entity.Get(Types.Physics);
            physics.Blacklisted_Collision_Tags.Add("Player-Hit");

            physics.Callback = (self, o) =>
            {
                if (o.Has_Tag("Enemy"))
                {
                    var health = (Health)self.Get(Types.Health);
                    health.Hurt(1, true);
                }

                if (o.Has_Tag("Heal") && invatory.Has_Space())
                {
                    var health = (Health)self.Get(Types.Health);
                    if (health.Amount < 4) health.Heal(1);
                }

                if (o.Has(Types.Item))
                {
                    var item = (Item)o.Get(Types.Item);
                    if (item.Can_Collect)
                    {
                        var inv = (Invatory)entity.Get(Types.Invatory);
                        if (inv.Has_Space() || o.Has_Tag("Coin"))
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
                equipment.Left_Hand = Claymore;
            }
        }

        public override void Destroy(Entity entity)
        {
            base.Destroy(entity);
        }

        public void Create_Sword_Hitbox(World world, Physics physics, Body body)
        {
            var side = (int)physics.FacingSide;

            var hit_size = 16;
            var hit = World_Ref.Create_Entity();
            hit.Tags.Add("Player-Hit");

            hit.Add(new Body(new Vector2(
                body.Center.X + (side < 0 ? hit_size * side * 2 : 0), 
                body.Position.Y - body.Height / 2 - hit_size), 

                new Vector2(
                    hit_size * 2.3f, 
                    hit_size * 2
                )));

            hit.Add(new Timed_Destroy(0.3f));

            var phy = (Physics) hit.Add(new Physics(Vector2.Zero, Physics.PType.DYNAMIC));
            phy.Blacklisted_Collision_Tags.Add("Player");

            hit.Update = (self) => {

                var h_body = (Body) self.Get(Types.Body);
                var _side = (int)physics.FacingSide;

                h_body.Position = new Vector2(
                    body.Center.X + (side < 0 ? hit_size * side * 2 : 0),
                    body.Position.Y - body.Height / 2 - hit_size
                );

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

            //sprite.Color = new Color(1f, 1f, 1f, 0.2f);

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

            if (Input.It.Is_Key_Pressed(Keys.P))
            {
                global::System.Console.WriteLine($"Player Pos: {body.X} {body.Y}.");
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
            if (player.Dying) player.Can_Move = false;
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

                var _attack = Input.It.Is_Key_Pressed(Keys.X) || Input.It.Is_Gamepad_Button_Pressed(Buttons.X);
                if (_attack) player.Combo_Counter++;

                if (_attack && player.State != Player.Action_State.ATTACKING)
                {
                    player.Combo_Counter = 0;
                    if ( equipment.Left_Hand != null )
                    {
                        sprite.Current_Animation_ID = equipment.Left_Hand.Use_Animation_ID;
                        sprite.Current_Frame = 0;
                        player.State = Player.Action_State.ATTACKING;

                        // create the collideable
                        Create_Sword_Hitbox(World_Ref, physics, body);
                    }
                }
                else
                {
                    if (player.State == Player.Action_State.ATTACKING)
                    {
                        if (sprite.Current_Frame == 6 - 1 )
                        {
                            if (player.Combo_Counter == 0)
                            {
                                player.State = Player.Action_State.IDLE;
                                player.Combo_Counter = 0;
                                player.Performed_Combo = false;
                            }
                        }

                        if (sprite.Current_Frame == 10 - 1 && player.Combo_Counter > 0 && player.Performed_Combo == false)
                        {
                            Create_Sword_Hitbox(World_Ref, physics, body);
                            player.Performed_Combo = true;
                        }

                        if (sprite.Animation_End) {
                            player.Performed_Combo = false;
                            player.Combo_Counter = 0;
                            player.State = Player.Action_State.IDLE;
                        }
                    }
                }
            }
            player.Dash_Timer -= (float)time.ElapsedGameTime.TotalSeconds;


            // Dealing with the killing of the player
            if (entity.Get(Component.Types.Health) is Health health){
                if (health.Amount <= 0 && !player.Dying)
                {
                    player.Dying = true;
                    player.Going_To_Menu = false;
                    sprite.Force_Play_Animation("player-die");
                    return;
                }
            }

            if (player.Dying && sprite.Animation_End && !player.Going_To_Menu)
            {
                player.Going_To_Menu = true;
                sprite.Force_Play_Animation("player-die");
                sprite.Current_Frame = sprite.Animations[sprite.Current_Animation_ID].Frames.Count - 1;
                sprite.Playing = false;
                screen_manager.Goto_Screen("Menu", true);
            }

            // (TEMP): toggle the equipment
            if ( Input.It.Is_Key_Pressed(Keys.E) ) {
                if (equipment.Left_Hand != null)
                    equipment.Left_Hand = null;
                else {
                    // TODO: Clean this up so that i dont hard code the player equipment
                    equipment.Left_Hand = Claymore;
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
            if ( Input.It.Is_Key_Pressed(Keys.Q) || Input.It.Is_Gamepad_Button_Pressed(Buttons.Y))
            {
                show_overlay_gui = !show_overlay_gui;

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

            //var gui = Assets.It.Get<Texture2D>("gui");

            //int y_offset = 32;

            //var region = new Rectangle(24, 0, 24, 24);
            //batch.Draw(gui, new Rectangle(96, 32+y_offset, 48, 48), region, new Color(0, 0, 0, 100));
            //batch.Draw(gui, new Rectangle(96, 32+ y_offset + 48 + 2, 48, 48), region, new Color(0, 0, 0, 100));

            //// left hand box
            //batch.Draw(gui, new Rectangle(96 - 48 - 2, 32 + 48 + 2 + y_offset, 48, 48), region, new Color(0, 0, 0, 100));
            //// right hand box
            //batch.Draw(gui, new Rectangle(96 + 48 + 2, 32 + 48 + 2 + y_offset, 48, 48), region, new Color(0, 0, 0, 100));

            //batch.Draw(gui, new Rectangle(96, 32 + 48 * 2 + 4 + y_offset, 48, 48), region, new Color(0, 0, 0, 100));

            //var equipment = (Equipment) entity.Get(Types.Equipment);
            //var font = (SpriteFont) Assets.It.Get<SpriteFont>("font");
            //var inv = (Invatory) entity.Get(Types.Invatory);

            //// draw money
            //var items = Assets.It.Get<Texture2D>("items");
            //var coin_region = new Rectangle(0, 0, 8, 8);
            //var coin_pos = new Vector2(16, 16);
            //batch.Draw(items, coin_pos, coin_region, Color.White, 0, Vector2.Zero, 4, SpriteEffects.None, 1);
            //batch.DrawString(font, inv.Money.ToString(),coin_pos + new Vector2(48, 0), Color.White);

            //// left hand (TODO): finish the rest of the boxes
            //if ( equipment.Left_Hand != null )
            //    batch.DrawString(font, equipment.Left_Hand.ID, new Vector2(96 - 48 - 2, 32 + 48 + 2 + y_offset), Color.White, 0, Vector2.Zero, 0.5f, SpriteEffects.None, 1);

            //// right hand


        }
    }
}
