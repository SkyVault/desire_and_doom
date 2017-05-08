using Myra.Graphics2D.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desire_Tiles
{
    class Top_Menu
    {
        HorizontalMenu Menu { get; set; }
        public Top_Menu(Grid grid)
        {
            Menu = new HorizontalMenu
            {
                GridPositionX   = 0,
                GridPositionY   = 0,
                //WidthHint = Editor.WIDTH
            };

            Menu.Items.Add(new MenuItem { Text = "File", Id = "file" });
            Menu.Items.Add(new MenuItem { Text = "Edit", Id = "edit" });
            Menu.Items.Add(new MenuItem { Text = "View", Id = "view" });
            Menu.Items.Add(new MenuItem { Text = "Tilesheets" });

            var file = Menu.FindMenuItemById("file");
            file.Items.Add(new MenuItem { Text = "open" });
            file.Items.Add(new MenuItem { Text = "save" });
            file.Items.Add(new MenuItem { Text = "save as" });
            file.Items.Add(new MenuItem { Text = "export" });

            var exit = new MenuItem { Text = "exit" };
            exit.Selected += (s, a) => {
                Editor.Exit_Editor();
            };
            file.Items.Add(exit);

            grid.Widgets.Add(Menu);
        }
    }
}
