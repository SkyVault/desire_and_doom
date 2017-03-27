using Desire_And_Doom.ECS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desire_And_Doom
{
    class Player : Component
    {
        public bool Can_Move { get; set; } = true;

        public enum Action_State
        {
            IDLE,
            RUNNING,
            ATTACKING,
            DASHING,
            NUM_ACTION_STATES
        }

        public float Dash_Timer = 0;
        public float Max_Dash_Time = 0.3f;

        public Action_State State { get; set; } = Action_State.IDLE;

        public Player() : base(Types.Player)
        {
        }
    }
}
