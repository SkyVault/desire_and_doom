using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Desire_And_Doom_Editor.Gui;
using Microsoft.Xna.Framework.Graphics;

namespace Desire_And_Doom_Editor.cs.Gui
{
    class HToolBar : Panel
    {
        private HBox hbox;

        public HToolBar(Monogui gui, SpriteFont font, GraphicsDevice device, List<Tileset> tilesets) : base(gui, false)
        {
            Scale_Children = false;
            Width = Editor.WIDTH;
            Height = 32;
            FillColor = new Color(50, 50, 50, 150);

            var btn_color = new Color(0, 0, 0, 0);
            var txt_color = Color.White;

            hbox = new HBox(GUI) { FillColor = new Color(0, 0, 0, 0) };

            Button file = (Button)hbox.Add(DropDownList.Create(GUI, font, "File", new List<DropDownItem> {
                new DropDownItem(){Title = "New",  Callback = () => { return false; } },
                new DropDownItem(){Title = "Open", Callback = () => { return false; } },
                new DropDownItem(){Title = "Save", Callback = () => { return false; } },
                new DropDownItem(){Title = "Exit", Callback = () => { Editor.Close = true; return true; } },
            }));
            file.FillColor = btn_color;
            file.TextColor = txt_color;

            Button e = (Button)hbox.Add(DropDownList.Create(GUI, font, "Edit", new List<DropDownItem> {
                new DropDownItem(){Title = "New Tileset", Callback = () => {
                        Tileset.New_Tileset(GUI, tilesets,device);
                        return false;
                    }
                },
                new DropDownItem(){Title = "Undo", Callback = ()=>{ return false; } },
                new DropDownItem(){Title = "Redo", Callback = ()=>{ return false; } },
                new DropDownItem(){Title = "Hello", Callback = ()=>{ return false; } },
            }));
            e.FillColor = btn_color;
            e.TextColor = txt_color;
            
            hbox.Add(new Button(GUI, "View", font)
            {
                Width = 48,
                Height = 32 - 4,
                Y = 2,
                FillColor = btn_color,
                TextColor = txt_color
            });

            hbox.Add(new Button(GUI, "Map", font)
            {
                Width = 48,
                Height = 32 - 4,
                Y = 2,
                FillColor = btn_color,
                TextColor = txt_color
            });
            
        }

        public override void Update(GameTime time)
        {
            Width = Editor.WIDTH;
            base.Update(time);
        }
    }
}
