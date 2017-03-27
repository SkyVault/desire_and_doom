using Desire_And_Doom_Editor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desire_And_Doom_Editor
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            using (var game = new Editor())
                game.Run();
        }
    }
}
