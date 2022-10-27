using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
namespace OrionFramework;

public class AnimationManager
{
    private readonly Dictionary<int, Animation> _animations = new();
    private int _lastKey;
    private float _freezeTimer;
    private float _freezeDuration;

    public Animation AddAnimation(int key, Animation animation)
    {
        _animations.Add(key, animation);
        _lastKey = key;
        return _animations[key];
    }

    public void SwitchTexture(Texture2D texture)
    {
        _animations[_lastKey].SwitchTexture(texture);
    }

    public void Freeze(float seconds)
    {
        _animations[_lastKey].Stop();
        _freezeDuration = seconds;
        _freezeTimer = 0f;
    }

    public void Update(int key)
    {
        if (_freezeTimer < _freezeDuration)
        {
            _freezeTimer += Time.DeltaTime;
            return;
        }
        else
        {
            _freezeDuration = 0f;
            _freezeTimer = 0f;
        }

        if (_animations[_lastKey].KeepAnimating)
        {
            _animations[_lastKey].Update();
            return;
        }

        if (_animations.ContainsKey(key))
        {
            if (_lastKey != key)
            {
                _animations[_lastKey].Stop();
                _animations[_lastKey].Reset();
            }
            _animations[key].Start();
            _animations[key].Update();
            _lastKey = key;
        }
        // else if (_lastKey is not null)
        // {
        //     _animations[_lastKey].Stop();
        //     _animations[_lastKey].Reset();
        // }

    }

    public void Draw(Vector2 position, bool flipped, Vector2? origin = null)
    {
        // if (_lastKey is not null)

        _animations[_lastKey].Draw(position, flipped, origin);
    }
}