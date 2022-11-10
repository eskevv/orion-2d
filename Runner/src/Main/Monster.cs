using System;
using System.Linq;
using Microsoft.Xna.Framework;
using OrionFramework.Animation;
using OrionFramework.DataStorage;
using OrionFramework.Helpers;
using OrionFramework.Scene;

namespace Tester.Main;

public class Monster : AnimatedSprite
{
    private const float Speed = 0.18f;

    public Monster(string name, Vector2 position, string? tag = null) : base(name, position, tag)
    {
        var data = new AnimationData("bat", Enumerable.Repeat(80, 4).ToArray(), 8, 8, null);
        Animator.AddAnimation("default".GetHashCode(), new Animation(data.Texture, data.Frames));
        CurrentAnimation = "default";
    }

    public override void Update()
    {
        base.Update();
        
        var targetPosition = SceneManager.EntityManager.GetByTag("player")[0].Position;
        Velocity = Position.NormalDirectionTo(targetPosition) * Speed;
    }
}