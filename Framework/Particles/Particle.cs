using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace OrionFramework;

public class Particle
{
    private readonly ParticleData _data;
    private Vector2 _position;
    private float _lifespanLeft;
    private float _lifespanAmount;
    private Color _color;
    private float _opacity;
    private float _scale;
    private Vector2 _origin;
    private Vector2 _direction;

    public bool IsFinished { get; private set; }

    public Particle(ParticleData data, Vector2 position)
    {
        _data = data;
        _position = position;
        _color = data.ColorStart;
        _opacity = data.OpacityStart;
        _lifespanLeft = data.LifeSpan;
        _lifespanAmount = 1f;

        _origin = new Vector2(_data.Texture.Width / 2, _data.Texture.Height / 2);

        if (_data.Speed != 0f)
        {
            _data.Angle = MathHelper.ToRadians(_data.Angle);
            _direction = new Vector2(MathF.Sin(_data.Angle), -MathF.Cos(_data.Angle));
        }
    }

    public void Update()
    {
        _lifespanLeft -= Time.DeltaTime;

        if (_lifespanLeft <= 0f)
        {
            IsFinished = true;
            return;
        }

        _lifespanAmount = MathHelper.Clamp(_lifespanLeft / _data.LifeSpan, 0, 1);

        _color = Color.Lerp(_data.ColorEnd, _data.ColorStart, _lifespanAmount);
        float opacity_lerp = MathHelper.Lerp(_data.OpacityEnd, _data.OpacityStart, _lifespanAmount);
        _opacity = MathHelper.Clamp(opacity_lerp, 0f, 1f);
        _scale = MathHelper.Lerp(_data.SizeEnd, _data.SizeStart, _lifespanAmount) / _data.Texture.Width;
        _position += _direction * _data.Speed * Time.DeltaTime;
    }

    public void Draw()
    {
        Batcher.DrawTexture(_data.Texture, _position, null, _color * _opacity, 0f, _origin, _scale, SpriteEffects.None);
    }
}