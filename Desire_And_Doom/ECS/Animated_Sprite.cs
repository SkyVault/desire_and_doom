using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Desire_And_Doom.ECS
{
    class Animated_Sprite : Sprite
    {
        public Dictionary<string, float> Animation_IDs { get; set; }

        public string Current_Animation_ID { get; set; }
        public int Current_Frame { get; set; } = 0;

        public float Timer { get; set; } = 0f;

        public Animated_Sprite(Texture2D _texture, Dictionary<string, float> ids) : base(_texture, new Rectangle())
        {
            Animation_IDs = ids;
            Current_Animation_ID = Animation_IDs.Keys.First();
        }
    }
}
