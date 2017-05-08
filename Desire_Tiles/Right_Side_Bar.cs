using Myra.Graphics2D.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desire_Tiles
{
    class Right_Side_Bar
    {
        public Right_Side_Bar(HorizontalSplitPane split)
        {
            var vsplit = new VerticalSplitPane {
                
            };

            split.Widgets.Add(vsplit);
            split.SetSplitterPosition(0, 0.75f);
            

        }
    }
}
