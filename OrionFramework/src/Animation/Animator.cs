using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace OrionFramework;

public class Animator
{
    private readonly Dictionary<int, Animation> _animations = new();

    private int _currentKey;

    // -- Arrow Helpers

    public bool FinishedPlaying => _animations[_currentKey].JustFinished;
    public int CurrentFrame => _animations[_currentKey].CurrentFrame;

    // -- Public Interface

    public void ScaleAnimationSpeed(int animation, float scale)
    {
        _animations[animation].ScaleFrameSpeed(scale);
    }

    public void AddAnimation(int key, Animation animation)
    {
        _currentKey = key;
        _animations.Add(key, animation);
    }

    public void SwitchTexture(Texture2D texture)
    {
    }

    public void Update(int key)
    {
        _currentKey = key;
        _animations[_currentKey].Update();
    }

    public void Draw(Vector2 position, bool flipped)
    {
        _animations[_currentKey].Draw(position, flipped);
    }
}