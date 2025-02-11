using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Reoria.Engine.MonoGame.Interfaces;
using XnaGameBase = Microsoft.Xna.Framework.Game;

namespace Reoria.Engine.MonoGame;

public abstract class XnaGame : XnaGameBase, IXnaGame
{
    protected readonly GraphicsDeviceManager Graphics;
    protected SpriteBatch? SpriteBatch;

    protected double fixedTimeStep;
    protected double fixedTimeAccumulator = 0.0;

    protected TimeSpan targetElapsedTime;
    protected int maxFPS;
    protected int maxFixedUpdatesPerFrame;
    protected bool useVSync;

    public XnaGame()
    {
        this.Graphics = new GraphicsDeviceManager(this)
        {
            PreferredBackBufferWidth = 1280,
            PreferredBackBufferHeight = 720
        };

        this.Content.RootDirectory = "Content";
        this.IsMouseVisible = true;

        this.LoadPerformanceSettings();
        this.ApplyPerformanceSettings();
    }

    protected virtual void LoadPerformanceSettings()
    {
        this.maxFPS = 120;
        this.fixedTimeStep = 1.0 / 60.0;
        this.maxFixedUpdatesPerFrame = 5;
        this.useVSync = true;
    }

    public void SetPerformanceSettings(int fixedUpdateRate, int maxFixedSteps, int newMaxFPS, bool enableVSync)
    {
        this.maxFPS = Math.Clamp(newMaxFPS, 1, 240);
        this.fixedTimeStep = 1.0 / Math.Clamp(fixedUpdateRate, 1, 240);
        this.maxFixedUpdatesPerFrame = Math.Clamp(maxFixedSteps, 1, 10);
        this.useVSync = enableVSync;
    }

    protected virtual void ApplyPerformanceSettings()
    {
        this.Graphics.SynchronizeWithVerticalRetrace = this.useVSync;
        this.IsFixedTimeStep = false;

        this.targetElapsedTime = this.useVSync ? TimeSpan.Zero : TimeSpan.FromSeconds(1.0 / this.maxFPS);
        this.Graphics.ApplyChanges();
    }

    protected override void Initialize()
    {
        this.SpriteBatch = new SpriteBatch(this.GraphicsDevice);
        base.Initialize();
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
            Keyboard.GetState().IsKeyDown(Keys.Escape))
        {
            this.Exit();
        }

        double deltaTime = gameTime.ElapsedGameTime.TotalSeconds;
        this.fixedTimeAccumulator += deltaTime;

        int fixedUpdateCount = 0;

        while (this.fixedTimeAccumulator >= this.fixedTimeStep && fixedUpdateCount < this.maxFixedUpdatesPerFrame)
        {
            this.FixedUpdate(new GameTime(gameTime.TotalGameTime, TimeSpan.FromSeconds(this.fixedTimeStep)));
            this.fixedTimeAccumulator -= this.fixedTimeStep;
            fixedUpdateCount++;
        }

        base.Update(gameTime);

        if (!this.useVSync)
        {
            this.ThrottleFPS();
        }
    }

    protected virtual void FixedUpdate(GameTime gameTime)
    {

    }

    protected virtual void ThrottleFPS()
    {
        double frameTime = this.targetElapsedTime.TotalSeconds;
        double elapsed = this.TargetElapsedTime.TotalSeconds;

        if (elapsed < frameTime)
        {
            int sleepTime = (int)((frameTime - elapsed) * 1000.0);
            if (sleepTime > 0)
            {
                Thread.Sleep(sleepTime);
            }
        }
    }

    protected override void Draw(GameTime gameTime)
    {
        this.GraphicsDevice.Clear(Color.CornflowerBlue);

        base.Draw(gameTime);
    }
}
