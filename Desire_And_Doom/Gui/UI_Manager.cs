using Desire_And_Doom.ECS;
using Desire_And_Doom.ECS.Components;
using Desire_And_Doom.Graphics;
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

        public Vector2 Offset { get; private set; } = Vector2.Zero;

        public Dictionary<string, Action> MenuActions;
        public Entity Player { get; set; } = null;
        private World entity_world;
        private PrimitivesBatch primitives;

        public static readonly float MARGIN = 32;
        public static readonly float CELL_SIZE = 64;
        public static readonly float CELL_MARGIN = 8;
        public static readonly Color CELL_COLOR = new Color(0, 0, 0, 0.75f);

        public UI_Manager(World _entity_world, PrimitivesBatch _primitives)
        {
            invatories = new List<Invatory>();
            entity_world = _entity_world;
            primitives = _primitives;

            MenuActions = new Dictionary<string, Action>
            {
                {"Drop", ()=>{
                    invatories.First().Drop_Item(selector.X, selector.Y);
                }},
                {"Move", ()=>{ Console.WriteLine("Moving!"); }},
                {"Info", ()=>{ Console.WriteLine("Info!"); }},
            };
        }
        public void Update(GameTime time)
        {
            var players = entity_world.Get_All_With_Component(Component.Types.Player);
            if (players.Count > 0) Player = players.First();
            if (Player != null && Player.Remove) Player = null;
            if (Player == null) return;

            if (Input.It.Is_Key_Pressed(Keys.Q))
            {
                Showing = !Showing;
            }
        }
        
        public void DrawPlayerHealth(SpriteBatch batch)
        {
            var gui     = Assets.It.Get<Texture2D>("gui");
            // Draw the players health
            var health  = (Health)Player.Get(Types.Health);

            var margin = CELL_MARGIN;
            var health_pos = new Vector2(MARGIN) + Offset;
            var scale = 4;

            for (int i = 0; i < health.Amount; i++)
            {
                if (i > 0) health_pos += (new Vector2(scale * 16 + margin, 0));
                batch.Draw(
                    gui,
                    health_pos,
                    new Rectangle(0, 24, 16, 16),
                    Color.White,
                    0f,
                    Vector2.Zero,
                    scale,
                    SpriteEffects.None,
                    1f
                    );
            }
        }
        public void DrawPlayerInvatory(SpriteBatch batch)
        {
            var invatory = (Invatory)Player.Get(Types.Invatory);
            var center = new Vector2(DesireAndDoom.ScreenWidth, DesireAndDoom.ScreenHeight) / 2.0f;
            var starting_pos = new Vector2(
                center.X - (invatory.W * (CELL_SIZE + CELL_MARGIN)) / 2.0f,
                center.Y - (invatory.H * (CELL_SIZE + CELL_MARGIN)) / 2.0f
                );
            
            for (int i = 0; i < invatory.H; i++)
                for (int j = 0; j < invatory.W; j++)
                {
                    // Draw the rectangle
                    primitives.DrawFilledRect(
                        starting_pos + new Vector2((CELL_SIZE + CELL_MARGIN) * j, (CELL_SIZE + CELL_MARGIN) * i),
                        new Vector2(CELL_SIZE),
                        CELL_COLOR,
                        0,
                        0.91f
                        );

                    var cell = invatory.Get(i, j);
                    if (cell != null)
                    {
                        // Draw the item over the rectangle
                    }
                }
        }

        public void UIDraw(SpriteBatch batch)
        {
            if (Player != null && Player.Remove) Player = null;
            if (Player == null) return;

            DrawPlayerHealth(batch);

            if (!Showing) return;

            DrawPlayerInvatory(batch);
        }
    }
}
