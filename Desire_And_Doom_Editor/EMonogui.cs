using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using MonoGame.Extended.ViewportAdapters;
using MonoGame.Extended;
using MonoGame.Extended.BitmapFonts;
using System.IO;
using Desire_And_Doom_Editor.Gui;
using Microsoft.Xna.Framework.Input;

namespace Desire_And_Doom_Editor
{
    class Monogui
    {

        List<Element> elements;

        public bool Mouse_On_Any_Element { get; private set; } = false;

        public Monogui()
        {
            elements = new List<Element>();
        }

        public void Key_Pressed(Keys key)
        {
            elements.ForEach(e => {
                e.Key_Pressed(key);
            });
        }
        
        public void Key_Typed(char charactor)
        {
            elements.ForEach(e => {
                e.Key_Typed(charactor);
            });
        }
        
        public bool Mouse_On_Layer_Sorted(Element e)
        {
            if (!e.Mouse_In_Bounds()) return false;
            foreach(var element in elements)
            {
                if (element.Can_Intersect_Mouse)
                    if (element.Intersects(e) && element.Layer > e.Layer && element.Mouse_In_Bounds())
                        return false;
            }
            return true;
        }

        public void Add(Element e)
        {
            elements.Add(e);
        }

        public void Update(GameTime time)
        {
            elements = elements.OrderBy((e) => e.Layer).ToList();

            Mouse_On_Any_Element = false;

            for (int i = elements.Count - 1; i >= 0; i--)
                if (elements[i].Remove)
                    elements.RemoveAt(i);

            for (int i = 0; i < elements.Count; i++)
            {
                var element = elements[i];
                if (element.Mouse_In_Bounds() && element.Can_Intersect_Mouse)
                    Mouse_On_Any_Element = true;
                element.Update(time);
            }
        }

        public void Draw(SpriteBatch batch)
        {
            foreach(var element in elements)
            {
                if (!element.Hide) element.Draw(batch);
            }
        }
    }
}
