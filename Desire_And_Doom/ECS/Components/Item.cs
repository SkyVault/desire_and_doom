﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desire_And_Doom.ECS
{
    class Item : Component
    {
        public Item() : base(Types.Item)
        {
        }
    }
}
