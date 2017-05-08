using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desire_And_Doom.ECS.Components
{
    class Character : Component
    {
        public Character() : base(Types.Character)
        {
        }

        public string Name  { get; set; } = "Unnamed";
        public int Age      { get; set; } = 20;

        public float Defence { get; set; } = 0;

        public float Long_Sward_Skill   { get; set; } = 0.5f; // half the swords attack
        public float Short_Sward_Skill  { get; set; } = 0.5f;
        public float Archery_Skill      { get; set; } = 0.5f;

        public float Fear { get; set; } = 0;
    }
}
