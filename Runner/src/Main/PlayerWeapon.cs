using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using OrionFramework.Animation;
using OrionFramework.AssetManagement;
using OrionFramework.DataStorage;
using OrionFramework.Drawing;
using OrionFramework.Entities;
using OrionFramework.Helpers;
using OrionFramework.Input;
using OrionFramework.UserInterface;

namespace Tester.Main;

public class EntityWeapon
{
    private Vector2 _position = new(1600 / 2, 900 / 2);
    private Texture2D _texture = AssetManager.LoadAsset<Texture2D>("sword_1");
    private float _scale = 1f;
    private Vector2 _origin = new(8, 15);
    private float _rotation;
    private SpriteEffects _effect = SpriteEffects.None;
    private Animator _animator = new();

    private bool _inAction;
    private float _actionRotation;
    private float _swingSpeed = MathHelper.ToRadians(8f);
    private bool _returning;
    private float _actualRot;
    private float _returnAngle;
    private string _direction;
    private Entity _owner;
    private bool _animatorActive;

    public EntityWeapon(Entity owner)
    {
        _owner = owner;
        var texture = AssetManager.LoadAsset<Texture2D>("slash");

        int[] frames = Enumerable.Repeat(90, 3).ToArray();

        var data = new AnimationData("slash", frames, 0, 0, null);

        _animator.AddAnimation("default".GetHashCode(), new Animation(data.Texture, data.Frames));
    }

    public void Update()
    {
        if (_animatorActive)
            _animator.Update("default".GetHashCode());

        if (_animator.CurrentFrame == 2)
        {
            _animator.Restart();
            _animatorActive = false;
        }

        _position = _owner.Position;

        float toDegrees = MathHelper.ToDegrees(_rotation);
        _actualRot = toDegrees < 0f ? 180 + -(-180 - toDegrees) : toDegrees;

        if (!_inAction && !_returning)
        {
            _rotation = _position.AngleToPoint(Input.WorldCursor);
            _effect = _rotation <= -1.57 || _rotation >= 1.57 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
            _scale = _rotation <= -1.57 || _rotation >= 1.57 ? -1f : 1f;
        }

        if (Input.Pressed(MouseButton.LeftButton) && !_inAction)
        {
            _inAction = true;
            _actionRotation = _rotation <= -1.57 || _rotation >= 1.57 ? -_swingSpeed : _swingSpeed;
            _returnAngle = _actualRot;
            _direction = _rotation <= -1.57 || _rotation >= 1.57 ? "left" : "right";
            _animatorActive = true;
        }

        if (_inAction)
        {
            _rotation += _actionRotation;
            if (_rotation < -3.14 || _rotation > 3.14)
                _rotation = -_rotation;
        }

        if (_inAction && _actionRotation < 0f && (_rotation is > -1 and < 1) && !_returning)
        {
            _actionRotation = -_actionRotation;
            _returning = true;
        }

        if (_inAction && _actionRotation > 0f && (_rotation is < -2.14f or > 2.14f) && !_returning)
        {
            _actionRotation = -_actionRotation;
            _returning = true;
        }

        if (_returning && _direction == "left" && _actualRot > _returnAngle)
        {
            _inAction = false;
            _returning = false;
        }

        if (_returning && _direction == "right")
        {
            _actualRot = _actualRot < 270 ? _actualRot + 360 : _actualRot;
            if (_actualRot < _returnAngle)
            {
                _inAction = false;
                _returning = false;
            }
        }
    }

    public void Draw()
    {
        Batcher.DrawTexture(_texture, _position, null, Color.White, _rotation, _origin, _scale, _effect);

        if (_animatorActive)
            _animator.Draw(_position + new Vector2(_owner.FacingRight ? 10 : -10, -8), !_owner.FacingRight);
    }
}