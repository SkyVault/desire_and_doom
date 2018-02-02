using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desire_And_Doom.ECS.Components
{
    class Health : Component
    {
        public int Amount { get; set; } = 3;
        public bool Should_Die { get => Amount <= 0; }

        public float Shield_Timer{ get; set; } = 0;
        public float Max_Shield_Timer { get; set; } = 1f;

        public Health(int total) : base(Types.Health)
        {
            Amount = total;
        }

        public void Hurt(int dammage, bool shield = false) {
            if (shield)
            {
                if (Shield_Timer <= 0)
                {
                    Shield_Timer = Max_Shield_Timer;
                    Amount -= dammage;
                }
            }else
                Amount -= dammage;
        }

        public void Heal(int health)  => Amount += health;
    }
}
