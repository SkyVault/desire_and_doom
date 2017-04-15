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
        List<Func<GameTime,bool>> tasks;
        int current_task = 0;
        public bool Done { get; set; }

        public Tasker(params Func<GameTime,bool>[] tasks){
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
            if (!Done)
                tasks[current_task]?.Invoke(time);
        }
    }
}
