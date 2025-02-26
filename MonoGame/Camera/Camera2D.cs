using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Reoria.Engine.MonoGame.Camera.Interfaces;

namespace Reoria.Engine.MonoGame.Camera;

public class Camera2D : ICamera2D
{
    protected GraphicsDevice? GraphicsDevice;

    public Vector2 Position { get; protected set; }
    public float Scale { get; protected set; }
    public int TargetScaleFactor { get; protected set; }
    public float AspectRatio { get; protected set; }

    public Matrix Transform { get; protected set; }
    public Rectangle Viewport { get; protected set; }

    public virtual void Initalize(GraphicsDevice graphicsDevice)
    {
        this.GraphicsDevice = graphicsDevice;
        this.Position = Vector2.Zero;
        this.Scale = 1.0f;
        this.TargetScaleFactor = 1;
        this.AspectRatio = 16.0f / 9.0f;

        this.UpdateViewport();
    }

    public virtual void Update()
    {
        this.Transform = Matrix.CreateTranslation(-this.Position.X, -this.Position.Y, 0) *
                         Matrix.CreateScale(this.Scale, this.Scale, 1) *
                         Matrix.CreateTranslation(this.Viewport.Width / 2f, this.Viewport.Height / 2f, 0);
    }

    public virtual void SetTargetScale(int scaleFactor)
    {
        if (scaleFactor < 1)
        {
            scaleFactor = 1;
        }

        this.TargetScaleFactor = scaleFactor;

        this.UpdateViewport();
    }

    public virtual void UpdateViewport()
    {
        if(this.GraphicsDevice is null)
        {
            throw new NullReferenceException(nameof(this.GraphicsDevice));
        }

        int viewportWidth = this.GraphicsDevice.Viewport.Width;
        int viewportHeight = this.GraphicsDevice.Viewport.Height;

        this.AspectRatio = viewportWidth / (float)viewportHeight;

        int baseWidth = viewportWidth / this.TargetScaleFactor;
        int baseHeight = viewportHeight / this.TargetScaleFactor;

        baseWidth = Math.Max(baseWidth, 1920 / 4);
        baseHeight = Math.Max(baseHeight, 1080 / 4);

        this.Scale = Math.Min(viewportWidth / (float)baseWidth, viewportHeight / (float)baseHeight);

        int scaledWidth = (int)(viewportWidth / this.Scale);
        int scaledHeight = (int)(viewportHeight / this.Scale);

        float offsetX = (viewportWidth - (scaledWidth * this.Scale)) / 2f;
        float offsetY = (viewportHeight - (scaledHeight * this.Scale)) / 2f;

        this.Viewport = new Rectangle((int)offsetX, (int)offsetY, scaledWidth, scaledHeight);
        this.Update();
    }

    public virtual void ResizeWindow() => this.UpdateViewport();

    public virtual void Move(Vector2 delta)
    {
        this.Position += delta;
        this.Update();
    }
}
