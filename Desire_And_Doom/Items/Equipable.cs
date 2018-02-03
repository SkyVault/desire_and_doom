using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desire_And_Doom.Items
{
    class Equipable
    {
        public enum Equipment_Type
        {
            NONE,
            WEAPON,
            SHEILD,
            CHEST,
            LEGS,
            BOOTS,
            HELMET,
        }

        public Equipment_Type Type          { get; set; } = Equipment_Type.NONE;
        public string Run_Animation_ID      { get; set; } = "";
        public string Use_Animation_ID      { get; set; } = "";
        public string Idle_Animation_ID     { get; set; } = "";
        public string ID                    { get; set; } = "";

        public Equipable(Equipment_Type _type)
        {
            Type = _type;
        }
    }

    class Weapon : Equipable
    {
        public Weapon() : base(Equipment_Type.WEAPON)
        {
        }
    }
}
