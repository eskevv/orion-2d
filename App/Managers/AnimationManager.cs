using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
namespace DewInterface;

public class AnimationManager
{
    private readonly Dictionary<object, Animation> _animations = new();
    private object _lastKey = null!;
    private float _freezeTimer;
    private float _freezeDuration;

    public Animation AddAnimation(object key, Animation animation)
    {
        _animations.Add(key, animation);
        _lastKey ??= key;
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

    public void Update(object key)
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
            _animations[key].Start();
            _animations[key].Update();
            _lastKey = key;
        }
        else if (_lastKey is not null)
        {
            _animations[_lastKey].Stop();
            _animations[_lastKey].Reset();
        }

        if (key != _lastKey)
        {
            _animations[_lastKey!].Reset();
        }
    }

    public void Draw(Vector2 position)
    {
        if (_lastKey is not null)
            _animations[_lastKey].Draw(position);
    }
}