using Microsoft.Xna.Framework;
using OrionFramework.Entities;

namespace Tester.Main;

public class AnimatedSprite : Entity
{
    public readonly Animator Animator = new();

    protected string CurrentAnimation;
    protected bool SpriteFlipped;

    public AnimatedSprite(string name, Vector2 position, string? tag = null) : base(name, position, tag)
    {
        CurrentAnimation = "idle";
    }

    public override void Update()
    {
        if (Velocity.X != 0)
            SpriteFlipped = Velocity.X < 0;
        
        Animator.Update(CurrentAnimation.GetHashCode());

        Position += Velocity;
    }

    public override void Draw()
    {
        Animator.Draw(Position, SpriteFlipped);
    }
}