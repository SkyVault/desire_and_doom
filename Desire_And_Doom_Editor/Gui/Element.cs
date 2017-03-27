using Desire_And_Doom_Editor;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using MonoGame.Extended.Shapes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desire_And_Doom_Editor.Gui
{
    class Element
    {
        protected Monogui GUI;

        public float X      { get; set; } = 0f;
        public float Y      { get; set; } = 0f;
        public float Width  { get; set; } = 0f;
        public float Height { get; set; } = 0f;
        
        public RectangleF Shape { get => new RectangleF(X, Y, Width, Height); }

        public List<Element> Children { get; } = new List<Element>();
        public Element Parent { get; set; }

        public Vector2 Position { get => new Vector2(X, Y); }
        public Vector2 Local_Position { get {
                if (Parent != null)
                    return Position + Parent.Local_Position;
                return Position;
            } }

        public Color FillColor = Color.DarkSlateGray;
        public float Layer = 0.5f;

        public bool Focus { get; set; }     = false;
        public bool Active { get; set; }    = true;
        public bool Hide { get; set; }      = false;

        public void Hide_Toggle()
        {
            Hide = !Hide;
            Children.ForEach(a => a.Hide_Toggle());
        }

        public void Toggle()
        {
            Active = !Active;
            foreach (var child in Children) child.Toggle();
        }

        public bool Remove { get; set; } = false;
        public bool Can_Intersect_Mouse { get; set; } = true;

        public Element(Monogui gui)
        {
            this.GUI = gui;
            GUI.Add(this);
        }

        public Vector2 Size     { get => new Vector2(Width, Height); }
        public Vector2 Center   { get => Local_Position + Size / 2;  }

        public float Left {
            get => Local_Position.X;
        }

        public float Right
        {
            get => Local_Position.X + Width;
        }

        public float Top
        {
            get => Local_Position.Y;
        }

        public float Bottom
        {
            get => Local_Position.Y + Height;
        }

        public bool Mouse_In_Bounds()
        {
            Vector2 mpos = Input.It.Mouse_Position();
            return mpos.X > Left && mpos.X < Right && mpos.Y > Top && mpos.Y < Bottom;
        }

        public float Local_Left
        {
            get => X;
        }

        public float Local_Right
        {
            get => X + Width;
        }

        public float Local_Top
        {
            get => Y;
        }

        public float Local_Bottom
        {
            get => Y + Height;
        }
        
        public Element Add(Element child)
        {
            Children.Add(child);
            child.Parent = this;
            child.Layer = Layer + 0.1f;
            return child;
        }

        public bool Intersects(Element e)
        {
            var x = Local_Position.X;
            var y = Local_Position.Y;
            return (e.Right > x && e.Left < Right) &&
                   (e.Bottom > y && e.Top < Bottom);
        }

        public void Draw_Rect(SpriteBatch batch)
        {
            var image = Assets.It.Get<Texture2D>("gui");
            batch.Draw(image, new Rectangle((int)Local_Position.X, (int)Local_Position.Y, (int)Width, (int)Height),new Rectangle(0, 0, image.Width, image.Height), FillColor, 0, Vector2.Zero, SpriteEffects.None, 1);
        }

        public virtual void Key_Pressed(Keys key) { }
        public virtual void Key_Typed(char charactor) { }
        public virtual void Draw(SpriteBatch batch){ Draw_Rect(batch); }
        public virtual void Update(GameTime time) {
            Children.ForEach(e => Active = this.Active);
        }

        public void HideAll()
        {
            Hide = true;
            Children.ForEach(a => a.HideAll());
        }

        public void RemoveAll()
        {
            Remove = true;
            Children.ForEach(e => e.RemoveAll());
        }
    }
}
