using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System;
using Desire_And_Doom.ECS;
using NLua;
using System.IO;
using Newtonsoft.Json;
using Desire_And_Doom.Screens;
using Penumbra;
using Desire_And_Doom.Utils;
using Desire_And_Doom.ECS.Systems;
using MonoGame.Extended.Input.InputListeners;
using Desire_And_Doom.Graphics;
using Desire_And_Doom.Gui;

namespace Desire_And_Doom
{
    public class Game1 : Game
    {
        public enum State
        {
            PLAYING,
            PAUSED,
        }

        public static State Game_State { get; set;  } = State.PLAYING;
        public static void Toggle_Pause()
        {
            if ( Game1.Game_State == Game1.State.PLAYING )
                Game1.Game_State = Game1.State.PAUSED;
            else Game1.Game_State = Game1.State.PLAYING;
        }

        public static void Request_Pause()  => Game_State = State.PAUSED;
        public static void Request_Resume() => Game_State = State.PLAYING;
        
        public static int WIDTH     = 1280;
        public static int HEIGHT    = 700;

        // TEMP
        private bool skip_intro_animation = false;

        public static int Map_Height_Pixels = 0;
        public static bool DEBUG = false;

        public static bool SHOULD_QUIT = false;
        public static readonly float SCALE = 3f;

        GraphicsDeviceManager graphics;
        SpriteBatch batch;
        
        Camera_2D           camera;
        World               world;
        Monogui             gui;
        Screen_Manager      screen_manager;
        Particle_World      particle_world;
        PenumbraComponent   penumbra;
        Debug_Console       console;
        Lua                 lua;
        RenderTarget2D      scene;
        Physics_Engine      physics_engine;
        Invatory_Manager    invatory_manager;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this) {
                PreferredBackBufferWidth = WIDTH,
                PreferredBackBufferHeight = HEIGHT,
                SynchronizeWithVerticalRetrace = true
            };

            var width   = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            var height  = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
            Window.AllowUserResizing    = true;
            Window.AllowAltF4           = true;
            Window.Position = new Point(width / 2 - WIDTH / 2, 0);
            //Window.Position = new Point(0, 0);
            graphics.ApplyChanges();

            this.IsMouseVisible = true;

            Content.RootDirectory = "Content";
            Assets.It.Content = Content;

            lua = new Lua();

            // initialize penumbra lighting
            penumbra    = new PenumbraComponent(this);
            console     = new Debug_Console(this, lua);

            Components.Add(console);
            Components.Add(penumbra);
        }

        protected override void Initialize()
        {
            Assets.It.Generate_Quads("quads", 512, 8, 8);

            Assets.It.Load_Animations_From_Lua("Lua_World/Animation.lua");
            
            //var frames = Assets.It.Get_Quads("player-attack");
            camera = new Camera_2D(GraphicsDevice, true);
            screen_manager = new Screen_Manager();

            camera.Zoom = SCALE;

            world           = new World(penumbra);
            particle_world  = new Particle_World();
            invatory_manager= new Invatory_Manager();

            //UserInterface.Initialize(Content, BuiltinThemes.hd);

            world.Add_System<Sprite_Renderer_System>(new Sprite_Renderer_System());
            world.Add_System<Player_Controller_System>(new Player_Controller_System(camera, particle_world, invatory_manager));
            world.Add_System<Animation_Renderer_System>(new Animation_Renderer_System());
            physics_engine = (Physics_Engine)world.Add_System<Physics_Engine>(new Physics_Engine(world));
            
            world.Add_System<Invatory_System>(new Invatory_System(invatory_manager));
            world.Add_System<AI_System>(new AI_System());
            world.Add_System<Light_Emitter_System>(new Light_Emitter_System());
            world.Add_System<World_Interaction_System>(new World_Interaction_System());
            world.Add_System<Lua_Function_System>(new Lua_Function_System(lua, camera));
            world.Add_System<Timed_Destroy_System>(new Timed_Destroy_System());
            world.Add_System<Particle_Emitter_System>(new Particle_Emitter_System());
            world.Add_System<Enemy_System>(new Enemy_System());
            world.Add_System<Multipart_Animation_System>(new Multipart_Animation_System());
            world.Add_System<Item_System>(new Item_System());
            gui = new Monogui();

            var npc_system = (Npc_System)world.Add_System<Npc_System>(new Npc_System(this, graphics, invatory_manager));
            //npc_system.Text_Console.Initialize();

            penumbra.Initialize();

            // initialize keylisteners
            var keyboard_listener = new KeyboardListener(new KeyboardListenerSettings());
            Components.Add(new InputListenerComponent(this, keyboard_listener));

            keyboard_listener.KeyTyped += console.Key_Typed;
            
            scene = new RenderTarget2D(graphics.GraphicsDevice, WIDTH, HEIGHT, false, SurfaceFormat.Color, DepthFormat.None, 2, RenderTargetUsage.DiscardContents);

            base.Initialize();
        }

        public void Quit()
        {
            Exit();
        }

        protected override void LoadContent()
        {
            batch = new SpriteBatch(GraphicsDevice);

            Assets.It.Add("entities",       Content.Load<Texture2D>("entities"));
            Assets.It.Add("items",          Content.Load<Texture2D>("items"));
            Assets.It.Add("Tiles_1",        Content.Load<Texture2D>("Tiles_1"));
            Assets.It.Add("gui",            Content.Load<Texture2D>("gui"));
            Assets.It.Add("sky_1",          Content.Load<Texture2D>("sky"));
            Assets.It.Add("Boss_Texture",   Content.Load<Texture2D>("boss_1"));
            Assets.It.Add("Charactors",     Content.Load<Texture2D>("Charactors"));
            Assets.It.Add("font",           Content.Load<SpriteFont>("font"));
            Assets.It.Add("gfont", Content.Load<SpriteFont>("GFont"));

            Assets.It.Generate_Rectangle(GraphicsDevice, "gui-rect", 512, 512);

            Assets.It.Add_Table("Lua_World/items.lua");
            Assets.It.Add_Table("Lua_World/Player.lua");
            Assets.It.Add_Table("Lua_World/Enemies.lua");
            Assets.It.Add_Table("Lua_World/Entities.lua");
            Assets.It.Add_Table("Lua_World/Npcs.lua");
            Assets.It.Add_Table("Lua_World/Behaviors/Enemy_Ai.lua");
            Assets.It.Add_Table("Lua_World/Dialog.lua");

            screen_manager.Register(new Level_1_Screen(world, camera, penumbra, particle_world, physics_engine, lua));
            screen_manager.Register(new Boss_Room_1(world, camera, penumbra, particle_world, physics_engine, Content, lua));
            screen_manager.Register(new Menu_Screen(screen_manager, penumbra, camera));
            screen_manager.Register(new Intro_Logos_Screen(screen_manager, camera, penumbra));

            if ( skip_intro_animation == false )
                screen_manager.Goto_Screen("Logo");
            else
                screen_manager.Goto_Screen("Level 1");
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if (SHOULD_QUIT) Quit();
            
            Timers.It.Update(gameTime);
            Input.It.Update(gameTime);

            camera.Update(gameTime);
            world.Update(gameTime);
            screen_manager.Update(gameTime);
            invatory_manager.Update(gameTime);
            //UserInterface.Update(gameTime);

            if (Input.It.Is_Key_Pressed(Keys.P)) DEBUG = !DEBUG;

            if (Game_State == State.PLAYING)
                particle_world.Update(gameTime);

            base.Update(gameTime);

            Messanger.It.Clear();
        }

        protected override void Draw(GameTime gameTime)
        {
            penumbra.BeginDraw();
            penumbra.Transform = camera.View_Matrix;

            GraphicsDevice.Clear(new Color(105, 205, 241, 255));


            // main draw
            batch.Begin(SpriteSortMode.FrontToBack, null, SamplerState.PointClamp, DepthStencilState.DepthRead, null, null, camera.View_Matrix);
                world.Draw(batch);
                screen_manager.Draw(batch);

                //batch.Draw(scene, Vector2.Zero, Color.White);

                particle_world.Draw(batch);
                batch.End();

                batch.Begin(SpriteSortMode.FrontToBack, BlendState.Additive, SamplerState.PointClamp, DepthStencilState.DepthRead, null, null, camera.View_Matrix);
                particle_world.Additive_Draw(batch);
            batch.End();

            // filtereds
            batch.Begin(SpriteSortMode.FrontToBack, null, SamplerState.AnisotropicClamp, DepthStencilState.DepthRead, null, null, camera.View_Matrix);
                screen_manager.FilteredDraw(batch);
            batch.End();

            penumbra.Draw(gameTime);

            // gui
            if (DEBUG)
            {
                batch.Begin();
                var font = Assets.It.Get<SpriteFont>("font");
                batch.End();
            }
            
            batch.Begin(SpriteSortMode.FrontToBack, null, SamplerState.PointClamp);
                world.UIDraw(batch, camera);
                gui.Draw(batch);
                invatory_manager.UIDraw(batch);
                console.Draw(batch);
            
                if ( DEBUG )
                {
                    float frameRate = 1f / (float) gameTime.ElapsedGameTime.TotalSeconds;
                    batch.DrawString(Assets.It.Get<SpriteFont>("font"), frameRate.ToString(), new Vector2(10, 10), Color.BurlyWood);
                }
            batch.End();

            //UserInterface.Draw(batch);

            //base.Draw(gameTime);
        }
    }
}
