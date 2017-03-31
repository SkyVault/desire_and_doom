using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Desire_And_Doom.Graphics;

namespace Desire_And_Doom.ECS
{
    class Animated_Sprite : Sprite
    {
        public Dictionary<string, Animation> Animations { get; private set; }
        public string Current_Animation_ID { get; set; } = "";
        public int Current_Frame { get; set; } = 0;
        public bool Animation_End { get; set; } = false;

        //public Texture2D Texture { get; private set; }
        public float Timer { get; set; } = 0;

        public Animated_Sprite(Texture2D _texture, List<string> anim_ids) : base(_texture, new Rectangle())
        {
            Type = Types.Animation;

            Animations = new Dictionary<string, Animation>();
            Texture = _texture;

            if (anim_ids.Count < 1)  throw new Exception("ERROR:: no anim ids!");

            Current_Animation_ID = anim_ids[0];
            foreach (var id in anim_ids)
            {
                var anim = Assets.It.Get<Animation>(id);
                Animations.Add(anim.ID, anim);
            }
        }
    }
}
