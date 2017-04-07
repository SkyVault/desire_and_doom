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

        public Health(int total) : base(Types.Health)
        {
            Ammount = total;
        }

        public void Hurt(int dammage) => Ammount -= dammage;
        public void Heal(int health)  => Ammount += health;
    }
}
