using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Myra;
using Myra.Graphics2D.UI;

namespace Desire_Tiles
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Editor : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch batch;

        private Desktop host;
        private Top_Menu menu;
        private Right_Side_Bar right_side_bar;

        public static readonly int WIDTH = 1280;
        public static readonly int HEIGHT = 640;

        private static bool exit = false;
        public static void Exit_Editor()
        {
            exit = true;
        }

        public Editor()
        {
            graphics = new GraphicsDeviceManager(this)
            {
                PreferredBackBufferWidth    = WIDTH,
                PreferredBackBufferHeight   = HEIGHT,
                SynchronizeWithVerticalRetrace = true
            };
            graphics.ApplyChanges();

            IsMouseVisible = true;

            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            MyraEnvironment.Game = this;

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            batch = new SpriteBatch(GraphicsDevice);

            var hsplit = new HorizontalSplitPane {};

            var grid = new Grid
            {
                RowSpacing = 8,
                ColumnSpacing = 8
            };

            grid.ColumnsProportions.Add(new Grid.Proportion(Grid.ProportionType.Auto));
            grid.ColumnsProportions.Add(new Grid.Proportion(Grid.ProportionType.Auto));
            grid.RowsProportions.Add(new Grid.Proportion(Grid.ProportionType.Auto));
            grid.RowsProportions.Add(new Grid.Proportion(Grid.ProportionType.Auto));
            grid.RowsProportions.Add(new Grid.Proportion(Grid.ProportionType.Auto));
            hsplit.Widgets.Add(grid);

            menu = new Top_Menu(grid);
            right_side_bar = new Right_Side_Bar(hsplit);

            host = new Desktop();
            host.Widgets.Add(hsplit);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if (exit)
                Exit();

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            host.Bounds = new Rectangle(0, 0, GraphicsDevice.PresentationParameters.BackBufferWidth, GraphicsDevice.PresentationParameters.BackBufferHeight);
            host.Render();

            base.Draw(gameTime);
        }
    }
}
