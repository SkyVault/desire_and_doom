using Comora;
using Desire_And_Doom_Editor.cs;
using Desire_And_Doom_Editor.cs.Gui;
using Desire_And_Doom_Editor.Gui;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Input.InputListeners;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desire_And_Doom_Editor
{
    public class Editor : Game
    {
        public static int WIDTH = 800;
        public static int HEIGHT = 600;

        public static int Map_Height_Pixels { get; set; }

        GraphicsDeviceManager graphics;
        SpriteBatch batch;
        TileView camera;

        Tiled_Map map;
        TilePlacer placer;

        Monogui GUI;
        List<Tileset> tilesets;

        public static bool Close = false;
        public Editor()
        {
            graphics = new GraphicsDeviceManager(this)
            {
                PreferredBackBufferWidth = WIDTH,
                PreferredBackBufferHeight = HEIGHT
            };
            Window.AllowUserResizing = true;

            graphics.ApplyChanges();

            this.IsMouseVisible = true;

            var keyboard_listener = new KeyboardListener(new KeyboardListenerSettings());
            keyboard_listener.KeyTyped += Key_Typed;
            keyboard_listener.KeyPressed += Key_Pressed;

            Content.RootDirectory = "Content/";
            Components.Add(new InputListenerComponent(this, keyboard_listener));
        }

        protected void Key_Pressed(object sender, KeyboardEventArgs args)
        {
            GUI.Key_Pressed(args.Key);
        }

        protected void Key_Typed(object sender, KeyboardEventArgs args)
        {
            GUI.Key_Typed((char)args.Character);
        }

        protected override void Initialize()
        {
            GUI = new Monogui();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            Assets.It.Generate_Quads("quads", 512, 8, 8);

            Assets.It.Add<Texture2D>("tiles_main", Content.Load<Texture2D>("tiles_main"));
            Assets.It.Add<Texture2D>("entities", Content.Load<Texture2D>("entities"));
            Assets.It.Add<Texture2D>("items", Content.Load<Texture2D>("items"));
            Assets.It.Add<Texture2D>("Tiles_1", Content.Load<Texture2D>("Tiles_1"));
            Assets.It.Add<Texture2D>("gui", Content.Load<Texture2D>("gui"));
            Assets.It.Add<SpriteFont>("font", Content.Load<SpriteFont>("font"));
            var font = Assets.It.Get<SpriteFont>("font");

            batch   = new SpriteBatch(GraphicsDevice);
            camera  = new TileView(GraphicsDevice);
            map     = new Tiled_Map("Text1", camera);

            tilesets = new List<Tileset>();

            var toolbar = new HToolBar(GUI, font, GraphicsDevice, tilesets);
            var sidebar = new SideBarTileSelector(GUI, GraphicsDevice,tilesets) {
                Y = toolbar.Bottom,
                Width = 200,
                Height = HEIGHT - toolbar.Height,
                X = WIDTH - 200,
                Snap_Side = Panel.Side.RIGHT,
                FillColor = toolbar.FillColor
            };

            placer  = new TilePlacer(map,GUI, camera, sidebar, tilesets);

            base.LoadContent();
        }

        protected override void Update(GameTime gameTime)
        {
            WIDTH = Window.ClientBounds.Width;
            HEIGHT = Window.ClientBounds.Height;

            GUI.Update(gameTime);
            Input.It.Update(gameTime);

            //Console.WriteLine(tilesets.Count);

            if (!GUI.Mouse_On_Any_Element)
            {
                placer.Update(gameTime);
                camera.Update_Mouse_Panning(gameTime);
            }
            base.Update(gameTime);

            if (Close) Exit();
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.DarkGray);

            batch.Begin(camera.Camera, SpriteSortMode.Deferred, null, SamplerState.PointClamp);
            //batch.DrawString(Assets.It.Get<SpriteFont>("font"), "Hello", Vector2.Zero, Color.White);
            map.Draw(batch);
            batch.End();

            batch.Begin(SpriteSortMode.Immediate, null, SamplerState.PointClamp, null, null, null, null);
            //batch.DrawString(Assets.It.Get<SpriteFont>("font"), "Hello", Vector2.Zero, Color.White);
            //E.Draw_Rect(batch);
            GUI.Draw(batch);
            batch.End();

            base.Draw(gameTime);
        }
    }
}
