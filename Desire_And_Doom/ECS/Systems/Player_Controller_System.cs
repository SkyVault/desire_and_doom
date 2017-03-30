using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using static Desire_And_Doom.ECS.Component;
using Desire_And_Doom.Utils;

namespace Desire_And_Doom.ECS
{
    class Player_Controller_System : System
    {
        Camera_2D camera;
        public Player_Controller_System(Camera_2D _camera) : base(Types.Body, Types.Player, Types.Physics)
        {
            this.camera = _camera;
        }

        public override void Load(Entity entity)
        {
            var physics = (Physics)entity.Get(Types.Physics);
            physics.Callback = (self, o) =>
            {
                if (o.Has(Types.Item))
                {
                    var inv = (Invatory)entity.Get(Types.Invatory);
                    inv.Add_Item(o);
                    o.Destroy();
                }

                return false;
            };
        }

        public override void Update(GameTime time, Entity entity)
        {
            var body        = (Body)entity.Get(Types.Body);
            var physics     = (Physics)entity.Get(Types.Physics);
            var sprite      = (Animated_Sprite)entity.Get(Types.Animation);
            var invatory    = (Invatory)entity.Get(Types.Invatory);
            var player      = (Player)entity.Get(Types.Player);

            if (player.State != Player.Action_State.ATTACKING)
                sprite.Current_Animation_ID = "player-idle";

            if (Messanger.It.Top() == "Dialog_Open")    player.Can_Move = false;
            if (Messanger.It.Top() == "Dialog_Closed")  player.Can_Move = true;
            if (Debug_Console.Open) player.Can_Move = false;

            var state = GamePad.GetState(PlayerIndex.One);
            var x_axis = state.ThumbSticks.Left.X;
            var y_axis = state.ThumbSticks.Left.Y;
            var dash = (state.Triggers.Left > 0.2f);
            var action = state.Buttons.A == ButtonState.Pressed;
            var attack = state.Buttons.X == ButtonState.Pressed;

            string player_run_anim = "player-claymore-run";

            if (player.Can_Move && player.State != Player.Action_State.ATTACKING && player.State != Player.Action_State.DASHING)
            {
                if (Input.It.Is_Key_Down(Keys.Space))
                {
                    body.Z = 1;
                }
                else body.Z = 0;
                
                if (Input.It.Is_Key_Down(Keys.Left) || x_axis < 0)
                {
                    sprite.Current_Animation_ID = player_run_anim;
                    physics.Apply_Force(10, Physics.Deg_To_Rad(180));
                    sprite.Scale = new Vector2(-1, sprite.Scale.Y);
                }

                if (Input.It.Is_Key_Down(Keys.Right) || x_axis > 0)
                {
                    sprite.Current_Animation_ID = player_run_anim;
                    physics.Apply_Force(10, Physics.Deg_To_Rad(0));
                    sprite.Scale = new Vector2(1, sprite.Scale.Y);
                }

                if (Input.It.Is_Key_Down(Keys.Up) || y_axis > 0)
                {
                    physics.Apply_Force(10, Physics.Deg_To_Rad(270));
                    sprite.Current_Animation_ID = player_run_anim;
                }

                if (Input.It.Is_Key_Down(Keys.Down) || y_axis < 0)
                {
                    sprite.Current_Animation_ID = player_run_anim;
                    physics.Apply_Force(10, Physics.Deg_To_Rad(90));
                }
            }

            if (Input.It.Is_Key_Pressed(Keys.I))
            {
                invatory.Draw = !invatory.Draw;
            }

            if (player.Can_Move)
            {
                if (Input.It.Is_Key_Pressed(Keys.LeftShift) == true || dash == true)
                {
                    if (player.Dash_Timer <= 0 && player.State != Player.Action_State.DASHING)
                    {
                        player.State = Player.Action_State.DASHING;
                        var direction = physics.Direction;
                        physics.Velocity = Vector2.Zero;
                        physics.Apply_Force(300, direction);
                        player.Dash_Timer = player.Max_Dash_Time;
                    }
                }

                if (player.Dash_Timer <= 0 && player.State == Player.Action_State.DASHING)
                {
                    player.State = Player.Action_State.RUNNING;
                }

                if (Input.It.Is_Key_Down(Keys.X) || attack && player.State != Player.Action_State.ATTACKING)
                {
                    sprite.Current_Animation_ID = "player-attack";
                    sprite.Current_Frame = 0;
                    player.State = Player.Action_State.ATTACKING;
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

            camera.Track(body, 0.1f);
        }

        public override void Draw(SpriteBatch batch, Entity entity)
        {
        }
    }
}
