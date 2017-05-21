using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desire_And_Doom.Utils
{
    class Tasker
    {
        List<Action<GameTime>> tasks;
        int current_task = 0;
        public bool Done { get; set; }

        public Tasker(params Action<GameTime>[] tasks){
            this.tasks = tasks.ToList();
        }

        public void Next()
        {
            current_task++;
            if ( current_task == tasks.Count )
                Done = true;
        }

        public void Update(GameTime time)
        {
            if (tasks.Count == 0) return;
            if (!Done)
                tasks[current_task]?.Invoke(time);
        }
    }
}
