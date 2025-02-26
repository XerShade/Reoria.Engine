using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Xna.Framework;
using Reoria.Engine.Base.Common;
using Reoria.Engine.MonoGame.Interfaces;
using Reoria.Engine.StateMachines.Interfaces;
using Reoria.Game.StateMachines.GameStates.Interfaces;
using System.Diagnostics;

namespace Reoria.Engine.MonoGame;

public class XnaGameServer : Disposable, IXnaGame
{
    protected readonly GameServiceContainer services;
    protected readonly ILogger<IXnaGame> logger;
    protected readonly IConfiguration configuration;
    protected readonly IGameStateMachine stateMachine;
    protected readonly Stopwatch stopwatch;

    protected double fixedTimeStep;
    protected double fixedTimeAccumulator = 0.0;
    protected int maxFixedUpdatesPerFrame;
    private bool tickGameLoop;
    private bool isRunning;

    public IGameStateMachine StateMachine => this.stateMachine;
    public GameServiceContainer Services => this.services;

    public XnaGameServer(IServiceProvider serviceProvider)
    {
        this.logger = serviceProvider.GetRequiredService<ILogger<IXnaGame>>();
        this.configuration = serviceProvider.GetRequiredService<IConfiguration>();
        this.stateMachine = serviceProvider.GetRequiredService<IGameStateMachine>();

        this.services = new GameServiceContainer();
        this.stopwatch = new Stopwatch();
        this.isRunning = false;

        this.stateMachine.AttachToWindow(this);

        this.LoadPerformanceSettings();
    }

    protected virtual void LoadPerformanceSettings()
    {
        this.fixedTimeStep = 1.0 / 60.0;
        this.maxFixedUpdatesPerFrame = 5;
    }

    public void SetPerformanceSettings(int fixedUpdateRate, int maxFixedSteps, int newMaxFPS = 0, bool enableVSync = false)
    {
        this.fixedTimeStep = 1.0 / Math.Clamp(fixedUpdateRate, 1, 240);
        this.maxFixedUpdatesPerFrame = Math.Clamp(maxFixedSteps, 1, 10);
    }

    public void Run()
    {
        if (this.isRunning)
        {
            this.logger.LogWarning("Something attempted to run the server thread while it is already running, aborting this attempt.");
            return;
        }

        GameTime gameTime = new(TimeSpan.Zero, TimeSpan.Zero, false);
        double previousElapsedTime = 0.0;

        this.isRunning = true;
        this.tickGameLoop = true;
        this.stopwatch.Restart();

        while (this.tickGameLoop)
        {
            double totalElapsedTime = this.stopwatch.Elapsed.TotalSeconds;
            double deltaTime = totalElapsedTime - previousElapsedTime;
            previousElapsedTime = totalElapsedTime;

            gameTime.TotalGameTime = TimeSpan.FromSeconds(totalElapsedTime);
            gameTime.ElapsedGameTime = TimeSpan.FromSeconds(deltaTime);

            this.fixedTimeAccumulator += deltaTime;
            int fixedUpdateCount = 0;

            while (this.fixedTimeAccumulator >= this.fixedTimeStep && fixedUpdateCount < this.maxFixedUpdatesPerFrame)
            {
                this.FixedUpdate(new GameTime(gameTime.TotalGameTime, TimeSpan.FromSeconds(this.fixedTimeStep)));
                this.fixedTimeAccumulator -= this.fixedTimeStep;
                fixedUpdateCount++;
            }

            this.Update(gameTime);

            Thread.Sleep(1);
        }

        this.stopwatch.Stop();
        this.isRunning = false;
    }

    public void Exit() => this.tickGameLoop = false;

    protected virtual void FixedUpdate(GameTime gameTime) => this.stateMachine.FixedUpdate(gameTime);

    protected virtual void Update(GameTime gameTime) => this.stateMachine.Update(gameTime);
}
