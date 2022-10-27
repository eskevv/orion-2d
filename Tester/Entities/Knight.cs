using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Linq;
using System;

namespace Tester;

public class Knight : MovingObject
{
    private int _animationImage;
    private readonly AnimationManager _animator = new();
    private List<Rectangle> _attacks = new();
    private bool _flipped;

    public Knight(float xPos, float yPos, World world) : base(xPos, yPos, world)
    {
        Bounds = new AABB(Position, 8, 8);

        var idle_texture = AssetManager.GetAsset<Texture2D>("knight_green_idle");
        var run_texture = AssetManager.GetAsset<Texture2D>("knight_green_run");
        var attack_texture = AssetManager.GetAsset<Texture2D>("knight_green_attack");

        _animator.AddAnimation(1, new Animation(run_texture, 8, 1, 0.13f));

        var att = new Animation(attack_texture, 13, 1, 0.085f, true);
        att.AddKeyFrame(3, FrameHit);
        att.AddKeyFrame(5, FrameHit);
        att.AddKeyFrame(8, FrameHit);
        att.AddKeyFrame(12, FrameHit);

        _animator.AddAnimation(2, att);
        _animator.AddAnimation(0, new Animation(idle_texture, 4, 1, 0.13f));
        _animationImage = 0;
    }

    public void Update()
    {
        _animationImage = GetAnimationImage();
        _attacks.Clear();
        _animator.Update(_animationImage);

        Velocity = Input.RawAxes * 1.8f;
        UpdatePhysics();

        if (Velocity.X != 0f)
            _flipped = Velocity.X < 0f;

        Camera.Position = Position;

        foreach (var item in GameManager.Loots.Reverse<Loot>())
        {
            var itemBox = new AABB(new Vector2(item.Position.X, item.Position.Y), 8, 8);

            if (Bounds.Overlaps(itemBox))
                GameManager.LootItem(item);
        }
    }

    public void Draw()
    {
        _animator.Draw(Position, false /*, new Vector2(14, 9) */);
        Batcher.DrawRect(Bounds.Center - Bounds.HalfSize, Bounds.HalfSize.X * 2, Bounds.HalfSize.Y * 2, Color.BlueViolet);
        // _attacks.ForEach(x => Batcher.DrawRect(x, Color.GhostWhite));
    }

    public void FrameHit()
    {
        _attacks.Add(new Rectangle((int)Position.X + (_flipped ? -16 : 8), (int)Position.Y, 8, 6));

        foreach (var x in _attacks.Reverse<Rectangle>())
            GameManager.FireCollisions(x);
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