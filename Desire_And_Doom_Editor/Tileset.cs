using Desire_And_Doom_Editor.cs.Gui;
using Desire_And_Doom_Editor.Gui;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Desire_And_Doom_Editor.cs
{
    class Tileset
    {
        public int TileWidth    { get; private set; }
        public int TileHeight   { get; private set; }

        public Texture2D Image { get; private set; }
        public string Path { get; private set; }

        public Tileset(int _width, int _height, Texture2D _image)
        {
            TileWidth = _width;
            TileHeight = _height;
            Image = _image;
            Path = _image.Name;
        }

        public static void New_Tileset(Monogui GUI, List<Tileset> tilesets, GraphicsDevice device)
        {
            // todo open new tileset ui window
            // user inputs the data
            // then it creates the tile set
            var font = Assets.It.Get<SpriteFont>("font");

            var window = new Window(GUI, 300, 200, "New Tileset");

            var lbl1 = window.Add(new Gui.Label(GUI, font, "Tile Width: ") { Y = window.Bar_Height + 16, X = 48, Height = font.MeasureString("w").Y });
            var lbl2 = window.Add(new Gui.Label(GUI, font, "Tile Height: ") { Y = lbl1.Local_Bottom + 8, X = lbl1.Local_Left, Height = font.MeasureString("w").Y });

            var imp1 = (NumericInputBox)window.Add(new NumericInputBox(GUI) { X = lbl1.Local_Right + 96, Y = lbl1.Local_Top });
            var imp2 = (NumericInputBox)window.Add(new NumericInputBox(GUI) { X = lbl1.Local_Right + 96, Y = lbl2.Local_Top });

            var label = window.Add(new Gui.Label(GUI, font, "Image Path") { X = lbl2.Local_Left + 8, Y = imp2.Local_Bottom + 16 });
            //var selector = window.Add(new FileSelector(GUI, "file", "Image Files|*.png;*.jpg;*.bmp") { X = 16, Y = label.Local_Bottom + 16 });
            var input = (TextInput)window.Add(
                new TextInput(GUI) { X = 8, Y = label.Local_Bottom + 16, Layer = 1, Width = 256 }
            );

            var open_dialog_btn = window.Add(new Desire_And_Doom_Editor.Gui.Button(GUI, "...", font) {
                X = input.Local_Right, Y = input.Local_Top, Width = 32, Height = input.Height,
                FillColor = new Color(0, 150, 255, 100), TextColor = Color.Black,
                Callback = () =>
                {
                    OpenFileDialog dialog = new OpenFileDialog();
                    dialog.Filter = "Image Files|*.png;*.jpg;*.bmp";
                    DialogResult result = dialog.ShowDialog();

                    if (result == DialogResult.OK)
                    {
                        var path = dialog.InitialDirectory + dialog.FileName;
                        input.Text = path;
                    }

                    return true;
                }
            });

            var okay = window.Add(new Desire_And_Doom_Editor.Gui.Button(GUI, "Okay", font) {
                X = 300 - 64,
                Y = 200 - 32,
                Width = 64,
                Height = 32,

                Callback = () =>
                {
                    if (File.Exists(input.Text) && imp1.Text.Length > 0 && imp2.Text.Length > 0)
                    {
                        var stream = new FileStream(input.Text, FileMode.Open);
                        var image = Texture2D.FromStream(device, stream);
                        var tileset = new Tileset((int)imp1.Get_Value(), (int)imp2.Get_Value(), image);
                        //tileset.
                        window.RemoveAll();

                        tilesets.Add(tileset);

                        stream.Close();
                    }
                    return true;
                }
            });
        }
    }
}
