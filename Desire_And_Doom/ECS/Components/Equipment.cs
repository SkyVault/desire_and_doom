using Desire_And_Doom.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desire_And_Doom.ECS.Components
{
    class Equipment : Component
    {
        public Equipable Chest_Piece   { get; set; }
        public Equipable Legs_Piece    { get; set; }
        public Equipable Helmet_Piece  { get; set; }

        public Equipable Left_Hand  { get; set; }
        public Equipable Right_Hand { get; set; }

        public Equipment() : base(Types.Equipment)
        {
        }
    }
}
