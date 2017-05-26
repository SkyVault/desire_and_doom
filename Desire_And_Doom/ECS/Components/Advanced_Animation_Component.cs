using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Desire_And_Doom.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Desire_And_Doom.ECS.Components
{
    class Advanced_Animation_Component : Sprite
    {
        public Dictionary<string, Animation> Animations{get; private set;}
        public string Current_Animation_ID {get;set;} = "";
        public int Current_Frame = 0;
        public bool Playing_Animation{get;set;} = false;
        public bool Animation_Finished{get;set;} = false;
        public float Timer {get; set;} = 0;
        // public bool Play_Animation {get;set;} = false;

        public void Request_Animation_Playback(string animation_id){

            if (Animations.ContainsKey(animation_id) == false){
                Console.WriteLine($"WARNING:: Component doesnt contain the animation {animation_id}");
            }

            if (Playing_Animation) return;

            Animation_Finished = false;
            Playing_Animation = true;
            Current_Animation_ID = animation_id;
            Current_Frame = 0;
        }
        public void Force_Animation_Playback(string animation_id){
            if (Animations.ContainsKey(animation_id) == false){
                Console.WriteLine($"WARNING:: Component doesnt contain the animation {animation_id}");
            }

            Animation_Finished = false;
            Playing_Animation = true;
            Current_Animation_ID = animation_id;
            Current_Frame = 0;
        }

        public void Stop() => Playing_Animation = false;

        public Animation Get_Current_Animation() => Animations[Current_Animation_ID];
        public Animation_Frame Get_Current_Frame() => Get_Current_Animation().Frames[Current_Frame];
        public void Check_Bounds(){
            if (Current_Frame > Get_Current_Animation().Frames.Count - 1)
                Current_Frame  = 0;
            if (Current_Frame < 0)
                Current_Frame  = Get_Current_Animation().Frames.Count - 1;
        }
        public Advanced_Animation_Component(Texture2D _texture, List<string> animation_ids) : base(_texture, new Rectangle())
        {
            // We extend sprite, so we have to say what the type is
            Type = Types.Advanced_Animation;
            Animations = new Dictionary<string, Animation>();

            if (animation_ids.Count < 1) {
                throw new Exception("ERROR::ADVANCED::ANIMATION::COMPONENT:: this component requires at least one animation!");
            }            

            Current_Animation_ID = animation_ids.First();
            foreach(var id in animation_ids){
                var animation = Assets.It.Get<Animation>(id);
                Animations.Add(animation.ID, animation);
            }

        }
    }
}
