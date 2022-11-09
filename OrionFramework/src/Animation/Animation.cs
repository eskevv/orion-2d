using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using OrionFramework.DataStorage;
using OrionFramework.Drawing;

namespace OrionFramework;

public class Animation
{
    // -- Properties / Fields

    private readonly Rectangle[] _srcRects;
    private readonly float[] _frameTimes;
    private readonly Vector2[] _frameOrigins;
    private readonly int _frameCount;
    private readonly Texture2D _texture;
    private readonly List<KeyFrame> _keyFrames = new();

    public int CurrentFrame { get; private set; }
    private float _frameTimeLeft;
    private bool _active = true;

    // -- Arrow Helpers

    private Rectangle SrcRect => _srcRects[CurrentFrame];
    private Vector2 Origin => _frameOrigins[CurrentFrame];
    public bool JustFinished { get; private set; }

    // -- Initialization

    public Animation(Texture2D texture, IReadOnlyList<FrameData> data)
    {
        _texture = texture;
        _frameCount = data.Count;

        _srcRects = new Rectangle[_frameCount];
        _frameTimes = new float[_frameCount];
        _frameOrigins = new Vector2[_frameCount];

        for (var i = 0; i < _frameCount; i++)
        {
            var frame = data[i];
            _srcRects[i] = new Rectangle(frame.X, frame.Y, frame.W, frame.H);
            _frameTimes[i] = frame.Duration / 1000f;
            _frameOrigins[i] = new Vector2(frame.Ox, frame.Oy);
        }

        _frameTimeLeft = _frameTimes[0];
    }

    // -- Public Interface

    public void AddKeyFrame(int frame, Action frameAction)
    {
        _keyFrames.Add(new KeyFrame(frame, frameAction));
    }

    public void Update()
    {
        if (!_active)
            return;

        _frameTimeLeft -= Time.DeltaTime;

        JustFinished = _frameTimeLeft <= 0f && CurrentFrame == _frameCount - 1;
        
        if (_frameTimeLeft <= 0f)
        {
            CurrentFrame = (CurrentFrame + 1) % _frameCount;

            foreach (var keyFrame in _keyFrames)
            {
                if (keyFrame.Frame == CurrentFrame)
                        keyFrame.Call?.Invoke();
            }
            
            _frameTimeLeft += _frameTimes[CurrentFrame];
        }
    }

    public void ScaleFrameSpeed(float scale)
    {
        for (int i = 0; i < _frameTimes.Length; i++)
        {
            _frameTimes[i] /= scale;
        }
    }

    public void Draw(Vector2 position, bool flippedH)
    {
        var effect = flippedH ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
        var origin = flippedH ? new Vector2(SrcRect.Width - Origin.X, Origin.Y) : Origin;
        Batcher.DrawTexture(_texture, position, SrcRect, Color.White, 0f, origin, 1f, effect);
    }
}