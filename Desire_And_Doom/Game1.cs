using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using MonoGame.Extended.Timers;
using MonoGame.Extended;
using System;
using Desire_And_Doom.ECS;

namespace Desire_And_Doom
{
    public class Game1 : Game
    {
        public static int WIDTH     = 800;
        public static int HEIGHT    = 480;

        public static readonly int TileWidth        = 32;
        public static readonly int TileHeight       = 15;
        public static readonly int OddRowOffset     = 16;
        public static readonly int HeightTIleOff    = 16;

        Tiled_Map map;

        GraphicsDeviceManager graphics;
        SpriteBatch batch;
        
        Camera_2D camera;
        FramesPerSecondCounter fps_counter;
        World world;
        
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);

            graphics.PreferredBackBufferWidth   = WIDTH;
            graphics.PreferredBackBufferHeight  = HEIGHT;
            graphics.ApplyChanges();

            this.IsMouseVisible = true;

            Content.RootDirectory = "Content";
            
        }

        protected override void Initialize()
        {
            Assets.It.Generate_Quads("quads", 512, 16, 16);
            Assets.It.Generate_Animation("player-run", new Vector2(0, 32), new Vector2(20, 32), 5);
            Assets.It.Generate_Animation("player-idle", new Vector2(0, 0), new Vector2(20, 32), 4);

            camera = new Camera_2D(GraphicsDevice.Viewport, true);
            fps_counter = new FramesPerSecondCounter(10);

            camera.Zoom = 3f;

            world = new World();
            world.Add_System<Sprite_Renderer_System>(new Sprite_Renderer_System());
            world.Add_System<Player_Controller_System>(new Player_Controller_System(camera));
            world.Add_System<Animation_Renderer_System>(new Animation_Renderer_System());
            world.Add_System<Physics_Engine>(new Physics_Engine(world));

            base.Initialize();
        }

        protected override void LoadContent()
        {
            batch = new SpriteBatch(GraphicsDevice);

            Assets.It.Add<Texture2D>("tiles_main", Content.Load<Texture2D>("tiles_main"));
            Assets.It.Add<Texture2D>("entities", Content.Load<Texture2D>("entities"));
            Assets.It.Add<SpriteFont>("font", Content.Load<SpriteFont>("font"));

            var e = world.Create_Entity();
            e.Add<Body>(new Body(new Vector2(10, 10), new Vector2(20, 32)));
            e.Add<Physics>(new Physics(Vector2.Zero));
            e.Add<Animated_Sprite>(new Animated_Sprite(Assets.It.Get<Texture2D>("entities"), new Dictionary<string, float>() {
                {"player-run", 0.1f},
                {"player-idle", 0.1f},
            }));
            e.Add<Player>(new Player());

            var r = new Random(DateTime.Now.Second);
            for (int i = 0; i < 15; i++)
            {
                var _e = world.Create_Entity();
                var x = r.Next(64, 500);
                var y = r.Next(64, 500);
                _e.Add<Body>(new Body(new Vector2(x, y), new Vector2(20, 32)));
                _e.Add<Sprite>(new Sprite(Assets.It.Get<Texture2D>("entities"), new Rectangle(20, 0, 20, 32)));
                _e.Add<Physics>(new Physics(Vector2.Zero));
                var p = (Physics)_e.Get<Physics>();
            }

            map = new Tiled_Map("test_ortho");
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            Input.It.Update(gameTime);
            camera.Update(gameTime);
            world.Update(gameTime);
            

            fps_counter.Update((float)gameTime.ElapsedGameTime.TotalSeconds);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            batch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp, DepthStencilState.DepthRead, null, null, camera.View_Matrix);
            map.Draw(batch);
            world.Draw(batch);
            batch.End();

            // gui
            batch.Begin();
            var font = Assets.It.Get<SpriteFont>("font");
            batch.DrawString(font, fps_counter.CurrentFramesPerSecond.ToString(), new Vector2(10, 10), Color.Red);
            batch.End();

            base.Draw(gameTime);
        }
    }
}
