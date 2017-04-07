using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desire_And_Doom.ECS.Components
{
    class Enemy : Component
    {
        public List<string> Drop_Items { get; set; }
        public float Experience { get; set; } = 0;

        public Enemy(List<string> _drop_items) : base(Types.Enemy)
        {
            Drop_Items = _drop_items;
        }
    }
}
