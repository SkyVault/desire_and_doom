using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desire_And_Doom.ECS
{
    class Item : Component
    {
        public string Description { get; set; } = "Just an item";
        public bool Can_Collect { get; set; } = false;
        public float Collect_Timer { get; set; } = 0;
        public Item() : base(Types.Item)
        {
        }
    }
}
