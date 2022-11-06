using Microsoft.Xna.Framework;

namespace OrionFramework;

public class TileAnimation
{
    private readonly TileFrame[] _frames;
    private int _currentFrame;
    private float _frameTime;
    private float _timeLeft;

    public Rectangle SrcRect => _frames[_currentFrame].SrcRect;

    public TileAnimation(TileFrame[] frames)
    {
        _frames = frames;
    }

    public void Update()
    {
        _timeLeft -= Time.DeltaTime;

        if (_timeLeft <= 0f)
        {
            _currentFrame = (_currentFrame + 1) % _frames.Length;
            _timeLeft += _frames[_currentFrame].Duration;
        }
    }
}