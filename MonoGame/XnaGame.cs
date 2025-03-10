using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Reoria.Engine.MonoGame.Interfaces;
using XnaGameBase = Microsoft.Xna.Framework.Game;
#region System.Windows.Forms Compaitbility
using ButtonState = Microsoft.Xna.Framework.Input.ButtonState;
using Keys = Microsoft.Xna.Framework.Input.Keys;
#endregion
#region System.Drawing Compatibility
using Color = Microsoft.Xna.Framework.Color;
using Rectangle = Microsoft.Xna.Framework.Rectangle;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Reoria.Engine.StateMachines.GameStates.Interfaces;
using Reoria.Engine.MonoGame.Camera.Interfaces;
#endregion

namespace Reoria.Engine.MonoGame;

public abstract class XnaGame : XnaGameBase, IXnaGame
{
    protected readonly GraphicsDeviceManager graphics;
    protected readonly ILogger<IXnaGame> logger;
    protected readonly IConfiguration configuration;
    protected readonly IGameStateMachine stateMachine;
    protected readonly ICamera2D camera;
    protected SpriteBatch? spriteBatch;

    protected double fixedTimeStep;
    protected double fixedTimeAccumulator = 0.0;

    protected TimeSpan targetElapsedTime;
    protected int maxFPS;
    protected int maxFixedUpdatesPerFrame;
    protected bool useVSync;

    public IGameStateMachine StateMachine => this.stateMachine;
    public Vector2 GetWindowSize() => new(this.Window.ClientBounds.Width, this.Window.ClientBounds.Height);
    public ICamera2D GetCamera() => this.camera;

    public XnaGame(IServiceProvider serviceProvider)
    {
        this.logger = serviceProvider.GetRequiredService<ILogger<IXnaGame>>();
        this.configuration = serviceProvider.GetRequiredService<IConfiguration>();
        this.stateMachine = serviceProvider.GetRequiredService<IGameStateMachine>();
        this.camera = serviceProvider.GetRequiredService<ICamera2D>();

        this.graphics = new GraphicsDeviceManager(this)
        {
            PreferredBackBufferWidth = 1280,
            PreferredBackBufferHeight = 720
        };

        this.Content.RootDirectory = "Assets";

        this.stateMachine.AttachToWindow(this);

        this.Window.ClientSizeChanged += (o, e) => this.camera.ResizeWindow();

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
        this.graphics.SynchronizeWithVerticalRetrace = this.useVSync;
        this.IsFixedTimeStep = false;
        this.IsMouseVisible = true;

        this.targetElapsedTime = this.useVSync ? TimeSpan.Zero : TimeSpan.FromSeconds(1.0 / this.maxFPS);
        this.graphics.ApplyChanges();
    }

    protected override void Initialize()
    {
        this.spriteBatch = new SpriteBatch(this.GraphicsDevice);

        this.camera.Initalize(this.GraphicsDevice);

        base.Initialize();
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
            Keyboard.GetState().IsKeyDown(Keys.Escape))
        {
            this.Exit();
        }

        #region Temporary Debug Code
        if (Keyboard.GetState().IsKeyDown(Keys.W))
        {
            this.camera.Move(new(0, -1));
        }
        if (Keyboard.GetState().IsKeyDown(Keys.S))
        {
            this.camera.Move(new(0, 1));
        }
        if (Keyboard.GetState().IsKeyDown(Keys.A))
        {
            this.camera.Move(new(-1, 0));
        }
        if (Keyboard.GetState().IsKeyDown(Keys.D))
        {
            this.camera.Move(new(1, 0));
        }
        if (Keyboard.GetState().IsKeyDown(Keys.PageDown))
        {
            this.camera.SetTargetScale(1);
        }
        if (Keyboard.GetState().IsKeyDown(Keys.PageUp))
        {
            this.camera.SetTargetScale(2);
        }
        #endregion

        double deltaTime = gameTime.ElapsedGameTime.TotalSeconds;
        this.fixedTimeAccumulator += deltaTime;

        int fixedUpdateCount = 0;

        while (this.fixedTimeAccumulator >= this.fixedTimeStep && fixedUpdateCount < this.maxFixedUpdatesPerFrame)
        {
            this.FixedUpdate(new GameTime(gameTime.TotalGameTime, TimeSpan.FromSeconds(this.fixedTimeStep)));
            this.fixedTimeAccumulator -= this.fixedTimeStep;
            fixedUpdateCount++;
        }

        this.stateMachine.Update(gameTime);

        base.Update(gameTime);

        if (!this.useVSync)
        {
            this.ThrottleFPS();
        }
    }

    protected virtual void FixedUpdate(GameTime gameTime) => this.stateMachine.FixedUpdate(gameTime);

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

        if(this.spriteBatch != null)
        {
            this.spriteBatch.Begin(
                samplerState: SamplerState.PointClamp,
                sortMode: SpriteSortMode.FrontToBack,
                transformMatrix: this.camera.Transform);

            #region Temporary Debug Code
            this.DrawDebugAxis(this.spriteBatch);
            #endregion

            this.stateMachine.Draw(gameTime, this.spriteBatch, this.Content);

            this.spriteBatch.End();
        }

        this.camera.UpdateViewport();

        base.Draw(gameTime);
    }

    private void DrawDebugAxis(SpriteBatch spriteBatch)
    {
        Texture2D pixel = new(this.GraphicsDevice, 1, 1);
        pixel.SetData([Color.White]);

        Vector2 center = new(0, 0);
        int gridSize = 16;
        int gridRange = 16 * 100;

        for (int x = -gridRange; x <= gridRange; x += gridSize)
        {
            if(x != 0)
            {
                spriteBatch.Draw(pixel, new Rectangle(x, -gridRange, 1, gridRange * 2), Color.Gray * 0.5f); // Vertical lines
            }
            else
            {
                spriteBatch.Draw(pixel, new Rectangle(x, -gridRange, 1, gridRange * 2), Color.Blue); // Vertical lines
            }
        }

        for (int y = -gridRange; y <= gridRange; y += gridSize)
        {
            if(y != 0)
            {
                spriteBatch.Draw(pixel, new Rectangle(-gridRange, y, gridRange * 2, 1), Color.Gray * 0.5f); // Horizontal lines
            }
            else
            {
                spriteBatch.Draw(pixel, new Rectangle(-gridRange, y, gridRange * 2, 1), Color.Green); // Horizontal lines
            }
        }

        spriteBatch.Draw(pixel, new Rectangle((int)center.X - 3, (int)center.Y - 3, 7, 7), Color.Red);
    }
}
