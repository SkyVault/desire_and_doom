using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Desire_And_Doom.ECS
{
    class Player_Controller_System : System
    {
        Camera_2D camera;
        public Player_Controller_System(Camera_2D _camera) : base(typeof(Body), typeof(Player), typeof(Physics))
        {
            this.camera = _camera;
        }

        public override void Update(GameTime time, Entity entity)
        {
            var body    = (Body)entity.Get<Body>();
            var physics = (Physics)entity.Get<Physics>();
            var sprite = (Animated_Sprite)entity.Get<Animated_Sprite>();

            sprite.Current_Animation_ID = "player-idle";
            if (Input.It.Is_Key_Down(Keys.Left))
            {
                sprite.Current_Animation_ID = "player-run";
                physics.Apply_Force(10, Physics.Deg_To_Rad(180));
                sprite.Scale = new Vector2(-1,sprite.Scale.Y);
            }

            if (Input.It.Is_Key_Down(Keys.Right))
            {
                sprite.Current_Animation_ID = "player-run";
                physics.Apply_Force(10, Physics.Deg_To_Rad(0));
                sprite.Scale = new Vector2(1, sprite.Scale.Y);
            }

            if (Input.It.Is_Key_Down(Keys.Up))
            {
                physics.Apply_Force(10, Physics.Deg_To_Rad(270));
                sprite.Current_Animation_ID = "player-run";
            }

            if (Input.It.Is_Key_Down(Keys.Down))
            {
                sprite.Current_Animation_ID = "player-run";
                physics.Apply_Force(10, Physics.Deg_To_Rad(90));
            }

            camera.Track(body, 0.1f);
        }

        public override void Draw(SpriteBatch batch, Entity entity)
        {
        }
    }
}
