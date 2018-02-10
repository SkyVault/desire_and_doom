using Desire_And_Doom.ECS;

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
        public bool Performed_Combo = true;
        public bool Dying = false;
        public bool Going_To_Menu = false;

        public float Dash_Timer = 0;
        public float Max_Dash_Time { get => 1f; }
        public float Attack_Walk_Speed_Multiplyer { get => 0.25f; }

        public Action_State State { get; set; } = Action_State.IDLE;

        public Player() : base(Types.Player)
        {
        }
    }
}
