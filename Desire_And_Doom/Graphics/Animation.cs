using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desire_And_Doom.Graphics
{
    class Animation
    {
        public List<Animation_Frame> Frames { get; set; }
        public string ID { get; private set; }

        public Animation(List<Animation_Frame> _frames, string _id)
        {
            Frames = _frames;
            ID = _id;
        }
    }
}
