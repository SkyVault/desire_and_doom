using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Entity_Editor
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch batch;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this) {
                PreferredBackBufferWidth = 800,
                PreferredBackBufferHeight = 800,
                SynchronizeWithVerticalRetrace = true // Enable VSYNC
            };

            Window.AllowAltF4 = true;
            Window.AllowUserResizing = false;
            Window.Position = new Point(
                    (GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width / 2),
                    (GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height / 2) - (800 / 2)
                );
            graphics.ApplyChanges();
            
            Content.RootDirectory = "Content";
        }
        protected override void Initialize()
        {
            base.Initialize();
        }
        protected override void LoadContent()
        {
            batch = new SpriteBatch(GraphicsDevice);
        }

        protected override void UnloadContent()
        {
        }
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            base.Update(gameTime);
        }
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            base.Draw(gameTime);
        }
    }
}
