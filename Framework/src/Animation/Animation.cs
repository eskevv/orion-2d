using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;

namespace OrionFramework;

public class Animation
{
    // -- Properties / Fields

    private readonly Rectangle[] _srcRects;
    private readonly float[] _frameTimes;
    private readonly Vector2[] _frameOrigins;
    private readonly int _frameCount;
    private readonly Texture2D _texture;
    private readonly Vector2 _offset;

    private int _currentFrame;
    private float _frameTimeLeft;
    private bool _active = true;

    // -- Arrow Helpers

    private Rectangle SrcRect => _srcRects[_currentFrame];
    private Vector2 Origin => _frameOrigins[_currentFrame];

    // -- Initialization

    public Animation(Texture2D texture, FrameData[] data, Vector2? offset)
    {
        _texture = texture;
        _frameCount = data.Length;
        _offset = offset ?? Vector2.Zero;

        _srcRects = new Rectangle[_frameCount];
        _frameTimes = new float[_frameCount];
        _frameOrigins = new Vector2[_frameCount];

        for (int i = 0; i < _frameCount; i++)
        {
            var frame = data[i];
            _srcRects[i] = new Rectangle(frame.X, frame.Y, frame.W, frame.H);
            _frameTimes[i] = frame.Duration / 1000f;
            _frameOrigins[i] = new Vector2(frame.OX, frame.OY);
        }

        _frameTimeLeft = _frameTimes[0];
    }

    // -- Public Interface

    public void AddKeyFrame(Action frameAction)
    { }

    public void Update()
    {
        if (!_active)
            return;

        _frameTimeLeft -= Time.DeltaTime;

        if (_frameTimeLeft <= 0f)
        {
            _currentFrame = (_currentFrame + 1) % _frameCount;
            _frameTimeLeft += _frameTimes[_currentFrame];
        }
    }

    public void Draw(Vector2 position, bool flippedH)
    {
        var effect = flippedH ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
        var origin = flippedH ? new Vector2(SrcRect.Width - Origin.X, Origin.Y) : Origin;
        Batcher.DrawTexture(_texture, position, SrcRect, Color.White, 0f, origin + _offset, 1f, effect);
    }
}