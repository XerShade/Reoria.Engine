using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Reoria.Engine.MonoGame.Camera.Interfaces;

public interface ICamera2D
{
    float AspectRatio { get; }
    Vector2 Position { get; }
    float Scale { get; }
    int TargetScaleFactor { get; }
    Matrix Transform { get; }
    Rectangle Viewport { get; }

    void Initalize(GraphicsDevice graphicsDevice);
    void Move(Vector2 delta);
    void ResizeWindow();
    void SetTargetScale(int scaleFactor);
    void Update();
    void UpdateViewport();
}
