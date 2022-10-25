using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
namespace DewInterface;

public class Knight : MovingObject
{
    private int _animationImage;
    private readonly AnimationManager _animation = new();
    private List<Rectangle> _attacks = new();

    public Knight(float xPos, float yPos, World world) : base(world)
    {
        Position = new Vector2(xPos, yPos);
        Bounds = new AABB(new Vector2(200, 200), 10, 10);
        BoundsOffset = new Vector2(5f, 10f);

        var idle_texture = AssetManager.GetAsset<Texture2D>("knight_green_idle");
        var run_texture = AssetManager.GetAsset<Texture2D>("knight_green_run");
        var attack_texture = AssetManager.GetAsset<Texture2D>("knight_green_attack");

        _animation.AddAnimation(0, new Animation(idle_texture, 4, 1, 0.13f));
        _animation.AddAnimation(1, new Animation(run_texture, 8, 1, 0.13f));

        var att = new Animation(attack_texture, 13, 1, 0.1f, true);
        att.AddKeyFrame(3, FrameHit);
        att.AddKeyFrame(5, FrameHit);
        att.AddKeyFrame(8, FrameHit);
        att.AddKeyFrame(12, FrameHit);

        _animation.AddAnimation(2, att);
    }

    public void Update()
    {
        _animationImage = GetAnimationImage();
        _attacks.Clear();
        _animation.Update(_animationImage);

        Velocity = Input.RawAxes * 0.8f;
        UpdatePhysics();
        Position = Bounds.Center;
    }

    public void Draw()
    {
        _animation.Draw(Position - BoundsOffset);
        // Batcher.DrawRect((Bounds.Center - Bounds.HalfSize), Bounds.AsRectangle.Width, Bounds.AsRectangle.Height, Color.BlueViolet);
        _attacks.ForEach(x => Batcher.DrawRect(x, Color.GhostWhite));
    }

    public void FrameHit()
    {
        _attacks.Add(new Rectangle((int)Position.X + 10, (int)Position.Y + 3, 8, 6));

        foreach (var x in _attacks.Reverse<Rectangle>())
        {
            if (x.Intersects(Troll._hitRect))
            {
                HitEffect();
                Troll.TakeDamage();
            }
        }
    }

    private void HitEffect()
    {
        var particle = new ParticleData()
        {
            ColorStart = Color.DarkRed,
            ColorEnd = Color.WhiteSmoke,
            SizeStart = 11f,
            SizeEnd = 3f,
            OpacityEnd = 0.6f,
            Angle = 0,
            Speed = 30f,
            LifeSpan = 1f,
        };

        ParticleManager.AddParticle(new Particle(particle, Position + new Vector2(16f, 13f)));
    }

    private int GetAnimationImage()
    {
        if (Input.Pressed(Keys.V))
            return 2;
        else if (Input.RawAxes != Vector2.Zero)
            return 1;

        return 0;
    }
}