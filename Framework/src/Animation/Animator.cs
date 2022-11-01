using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
namespace OrionFramework;

public class Animator
{
    private readonly Dictionary<int, Animation> _animations = new();

    public Animation AddAnimation(int key, Animation animation)
    {
        return animation;
    }

    public void SwitchTexture(Texture2D texture)
    {
    }

    public void Update(int key)
    {
    }

    public void Draw(Vector2 position, bool flipped)
    {
    }
}