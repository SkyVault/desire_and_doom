using Desire_And_Doom.ECS;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Desire_And_Doom.ECS.Component;

namespace Desire_And_Doom.Gui
{
    class UI_Manager
    {
        List<Invatory> invatories;
        public int Square_Size { get; set; } = 24;

        Point selector = Point.Zero;

        public bool Showing { get; set; } = false;
        private bool ShowMenu { get; set; } = false;
        private int menu_selector = 0;

        public Vector2 Offset { get; private set; } = Vector2.Zero;

        public Dictionary<string, Action> MenuActions;
        public Entity Player { get; set; } = null;
        private World entity_world;

        public UI_Manager(World _entity_world)
        {
            invatories = new List<Invatory>();
            entity_world = _entity_world;

            MenuActions = new Dictionary<string, Action>
            {
                {"Drop", ()=>{
                    invatories.First().Drop_Item(selector.X, selector.Y);
                }},
                {"Move", ()=>{ Console.WriteLine("Moving!"); }},
                {"Info", ()=>{ Console.WriteLine("Info!"); }},
            };
        }

        

        public void Add(Invatory inv)
        {
            invatories.Add(inv);
        }

        public void Remove(Invatory inv)
        {
            invatories.Remove(inv);
        }

        public void Update(GameTime time)
        {
            var players = entity_world.Get_All_With_Component(Component.Types.Player);
            if (players.Count > 0) Player = players.First();
            if (Player.Remove) Player = null;

            var total_width = 0;
            invatories.ForEach(i => total_width += (i.W * Square_Size));

            if ( Showing )
            {
                var left = Input.It.Is_Key_Pressed(Keys.Left) || Input.It.Is_Gamepad_Button_Pressed(Buttons.LeftThumbstickLeft);
                var right = Input.It.Is_Key_Pressed(Keys.Right) || Input.It.Is_Gamepad_Button_Pressed(Buttons.LeftThumbstickRight);
                var down = Input.It.Is_Key_Pressed(Keys.Down) || Input.It.Is_Gamepad_Button_Pressed(Buttons.LeftThumbstickDown);
                var up = Input.It.Is_Key_Pressed(Keys.Up) || Input.It.Is_Gamepad_Button_Pressed(Buttons.LeftThumbstickUp);

                if (left)
                {
                    selector += new Point(-1, 0);
                    ShowMenu = false;
                }

                if (right)
                {
                    selector += new Point(1, 0);
                    ShowMenu = false;
                }

                if ( !ShowMenu && up) selector += new Point(0, -1);
                if ( !ShowMenu && down) selector += new Point(0, 1);

                if (selector.X < 0)
                {
                    selector.X = invatories.First().W - 1;
                    selector.Y--;
                }
                if (selector.X > invatories.First().W - 1)
                {
                    selector.Y++;
                    selector.X = 0;
                }

                if (selector.Y < 0) selector.Y = invatories.First().H - 1;
                if (selector.Y > invatories.First().H - 1) selector.Y = 0;

                if (Input.It.Is_Key_Pressed(Keys.Z) || Input.It.Is_Gamepad_Button_Pressed(Buttons.A))
                {
                    if (ShowMenu)
                    {
                        MenuActions[MenuActions.ElementAt(menu_selector).Key]?.Invoke();
                        menu_selector = 0;
                    }

                    ShowMenu = !ShowMenu;
                }

                if (ShowMenu)
                {
                    if (up)
                    {
                        menu_selector--;
                    }

                    if (down)
                    {
                        menu_selector++;
                    }

                    if (menu_selector < 0) menu_selector = MenuActions.Count - 1;
                    if (menu_selector > MenuActions.Count - 1) menu_selector = 0;
                }
            }
            else
            {
                ShowMenu = false;
            }
        }

        public void UIDraw(SpriteBatch batch)
        {
            if ( !Showing ) return;

            int index = 0;
            foreach (var invatory in invatories )
            {
                var gui     = (Texture2D) Assets.It.Get<Texture2D>("gui");
                var font    = (SpriteFont)Assets.It.Get<SpriteFont>("font");
                var offset  = new Vector2(256, 64) + Offset;
                var size    = 64;

                for ( int y = 0; y < invatory.H; y++ )
                    for ( int x = 0; x < invatory.W; x++ )
                    {
                        var pos = new Vector2(x * 16, y * 16) + offset;
                        var region = new Rectangle(24, 0, 24, 24);
                        
                        //draw grid square
                        batch.Draw(
                            gui, 
                            new Rectangle(
                                (int) offset.X + size * x, 
                                (int) offset.Y + size * y, 
                                size, 
                                size), 
                            new Rectangle(24, 0, 24, 24), 
                            new Color(0, 0, 0, 100), 
                            0, 
                            Vector2.Zero, 
                            SpriteEffects.None, 
                            0.3f);

                        if (selector.X == x && selector.Y == y)
                        {
                            batch.Draw(
                                gui, 
                                new Rectangle(
                                    (int)offset.X + size * x, 
                                    (int)offset.Y + size * y, 
                                    size, 
                                    size), 
                                new Rectangle(24, 0, 24, 24), 
                                new Color(0, 0, 0, 100));

                            if (this.ShowMenu)
                            {
                                var text_height = font.MeasureString("Hello").Y;

                                batch.Draw(
                                    gui, 
                                    new Rectangle(
                                        (int)offset.X + size * x + size, 
                                        (int)offset.Y + size * y, 
                                        size * 2, 
                                        (int)text_height * (int)MenuActions.Count), 
                                    new Rectangle(24, 0, 24, 24), 
                                    new Color(0, 0.5f, 0.5f, 1f), 
                                    0,
                                    Vector2.Zero, 
                                    SpriteEffects.None, 
                                    0.98f);


                                int text_y = 0;
                                foreach (var action in MenuActions)
                                {
                                    if (text_y == menu_selector)
                                    {
                                        batch.Draw(
                                        gui,
                                        new Rectangle(
                                            (int)offset.X + size * x + size,
                                            (int)offset.Y + size * y + (int)(text_y * text_height),
                                            size * 2,
                                            (int)text_height),
                                        new Rectangle(24, 0, 24, 24),
                                        new Color(0, 0.0f, 0.0f, 0.5f),
                                        0,
                                        Vector2.Zero,
                                        SpriteEffects.None,
                                        0.99f);
                                    }

                                    batch.DrawString(
                                        font,
                                        action.Key, 
                                        new Vector2(offset.X + size * x + size, offset.Y + size * y + (text_y++ * text_height)),
                                        Color.White,
                                        0,
                                        Vector2.Zero,
                                        1,
                                        SpriteEffects.None,
                                        1f);
                                }
                            }
                        }

                        var item = invatory.Get(y, x);

                        if ( item != null )
                        {
                            var spr = (Sprite) item.Get(Types.Sprite);
                            if ( spr != null )
                            {
                                batch.Draw(
                                    spr.Texture,
                                    new Vector2((float) offset.X + size * x, (float) offset.Y + size * y),
                                    spr.Quad,
                                    Color.White,
                                    0f,
                                    Vector2.Zero,
                                    4,
                                    SpriteEffects.None,
                                    0.5f
                                    );
                            }
                        }
                    }
                index++;
            }
        }
    }
}
