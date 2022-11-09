using Microsoft.Xna.Framework;
using OrionFramework.DataStorage;

namespace Tester.Main;

public class Player : AnimatedSprite
{
    private const float Speed = 0.6f;

    private EntityState _state = EntityState.Idle;

    private EntityWeapon _weapon;

    public Player(string name, Vector2 position, string? tag = null) : base(name, position, tag)
    {
        _weapon = new EntityWeapon(this);
        
        var idleAnimation = DataBank.GetData<AnimationData>("knight_idle");
        var runAnimation = DataBank.GetData<AnimationData>("knight_run");
        Animator.AddAnimation("idle".GetHashCode(), new Animation(idleAnimation.Texture, idleAnimation.Frames));
        Animator.AddAnimation("run".GetHashCode(), new Animation(runAnimation.Texture, runAnimation.Frames));
    }

    public override void Update()
    {
        _weapon.Update();
            
        switch (_state)
        {
            case EntityState.Idle:
                Idle();
                break;
            case EntityState.Running:
                Running();
                break;
        }

        base.Update();
        

        Camera.Position += (Position - Camera.Position) * 0.6f;
    }

    private void Idle()
    {
        CurrentAnimation = "idle";

        Velocity = Input.RawAxes * Speed;

        if (Velocity != Vector2.Zero)
            _state = EntityState.Running;
    }

    private void Running()
    {
        CurrentAnimation = "run";

        Velocity = Input.RawAxes * Speed;

        if (Velocity == Vector2.Zero)
            _state = EntityState.Idle;
    }

    public override void Draw()
    {
        base.Draw();
        
        _weapon.Draw();
    }
}