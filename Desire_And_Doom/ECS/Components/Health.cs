using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desire_And_Doom.ECS.Components
{
    class Health : Component
    {
        public int Ammount { get; set; } = 3;
        public bool Should_Die { get => Ammount <= 0; }

        public float Shield_Timer{ get; set; } = 0;
        public float Max_Shield_Timer { get; set; } = 1f;

        public Health(int total) : base(Types.Health)
        {
            Ammount = total;
        }

        public void Hurt(int dammage, bool shield = false) {
            if (shield)
            {
                if (Shield_Timer <= 0)
                {
                    Console.WriteLine("Health");
                    Shield_Timer = Max_Shield_Timer;
                    Ammount -= dammage;
                }
            }else
                Ammount -= dammage;
        }

        public void Heal(int health)  => Ammount += health;
    }
}
