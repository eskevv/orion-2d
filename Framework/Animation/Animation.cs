using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System;

namespace DewInterface;

public class Animation
{
    private Texture2D _texture;
    private readonly List<Rectangle> _srcRects = new();
    private readonly Dictionary<int, KeyFrame> _keyFrames = new();
    private readonly int _frames;
    private readonly float _frameTime;
    private float _frameTimeLeft;
    private int _currentFrame;
    private bool _active = true;
    private bool _playingFull;
    private bool _finished;

    // -- Arrow Helpers
    public bool KeepAnimating => _playingFull && !_finished;

    public Animation(Texture2D texture, int framesX, int framesY, float frameTime, bool playingFull = false, int row = 1)
    {
        _texture = texture;
        _frameTime = frameTime;
        _frameTimeLeft = frameTime;
        _frames = framesX;
        _playingFull = playingFull;

        int frameWidth = (int)(_texture.Width / framesX);
        int frameHeight = (int)(_texture.Height / framesY);

        for (int i = 0; i < _frames; i++)
            _srcRects.Add(new Rectangle(i * frameWidth, (row - 1) * frameHeight, frameWidth, frameHeight));
    }

    public void SwitchTexture(Texture2D newTexture)
    {
        _texture = newTexture;
    }

    public void AddKeyFrame(int frame, Action keyAction)
    {
        _keyFrames.Add(frame, new KeyFrame(frame, keyAction));
    }

    public void Stop()
    {
        _active = false;
    }

    public void Start()
    {
        _active = true;
    }

    public void Reset()
    {
        _currentFrame = 0;
        _frameTimeLeft = _frameTime;
    }

    public void Update()
    {
        if (!_active)
            return;

        _frameTimeLeft -= Time.DeltaTime;
        _finished = _currentFrame == _frames - 1 && _frameTimeLeft <= 0f;

        if (_frameTimeLeft <= 0f)
        {
            _frameTimeLeft += _frameTime;
            _currentFrame = (_currentFrame + 1) % _frames;

            if (_keyFrames.ContainsKey(_currentFrame))
                _keyFrames[_currentFrame].FrameEvent?.Invoke();
        }
    }

    public void Draw(Vector2 position)
    {
        var src = _srcRects[_currentFrame];
        Batcher.DrawTexture(_texture, position, src, Color.White, 0f, null, 1f, 0);
    }
}