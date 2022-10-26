using System;
using Microsoft.Xna.Framework;

namespace Orion;

public class ParticleEmitter
{
    private readonly ParticleEmitterData _data;
    private float _intervalLeft;
    private readonly IEmitter _emitter;
    private int _emitCount;
    private bool _active;
    private int _maxEmits;

    public ParticleEmitter(IEmitter emitter, ParticleEmitterData data)
    {
        _emitter = emitter;
        _data = data;
        _intervalLeft = _data.Interval;
    }

    public void Emit(Vector2 position)
    {
        ParticleData data = _data.Data;
        data.LifeSpan = GameMath.RandomFloat(_data.LifeSpanMin, _data.LifeSpanMax);
        data.Speed = GameMath.RandomFloat(_data.SpeedMin, _data.SpeedMax);

        float r = GameMath.NextFloat() * 2 - 1;
        data.Angle += _data.AngleVariance * r;

        ParticleManager.AddParticle(new Particle(data, position));
        _emitCount++;
    }

    public void Update()
    {
        if (!_active)
            return;
            
        _intervalLeft -= Time.DeltaTime;

        while (_intervalLeft <= 0f)
        {
            _intervalLeft += _data.Interval;
            var position = _emitter.EmitPosition;

            for (int i = 0; i < _data.EmitCount; i++)
                Emit(position);                
        }

        if (_emitCount >= _maxEmits)
            _active = false;
    }

    internal void Burst()
    {
        _maxEmits += _data.EmitCount;
        _active = true;
    }
}