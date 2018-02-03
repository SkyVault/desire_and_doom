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
            IN_INVATORY,
            NUM_ACTION_STATES
        }

        public int Combo_Counter = 0;
        public float Dash_Timer = 0;
        public float Max_Dash_Time { get => 1f; }
        public float Attack_Walk_Speed_Multiplyer { get => 0.25f; }

        public Action_State State { get; set; } = Action_State.IDLE;

        public Player() : base(Types.Player)
        {
        }
    }
}
