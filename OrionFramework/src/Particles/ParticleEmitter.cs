using Microsoft.Xna.Framework;
using OrionFramework.Helpers;
using OrionFramework.Particles.Emitters;

namespace OrionFramework.Particles;

public class ParticleEmitter
{
    private readonly ParticleEmitterData _data;
    private float _intervalLeft;
    private readonly IEmitter _emitter;
    public bool Active { get; set; }

    public ParticleEmitter(IEmitter emitter, ParticleEmitterData data)
    {
        _emitter = emitter;
        _data = data;
        _intervalLeft = _data.Interval;
    }

    private void Emit(Vector2 position)
    {
        ParticleData data = _data.Data;
        data.LifeSpan = OrionHelp.RandomFloat(_data.LifeSpanMin, _data.LifeSpanMax);
        data.Speed = OrionHelp.RandomFloat(_data.SpeedMin, _data.SpeedMax);

        float r = OrionHelp.NextFloat() * 2 - 1;
        data.Angle += _data.AngleVariance * r;

        ParticleManager.AddParticle(new Particle(data, position));
    }

    public void Update()
    {
        if (!Active)
            return;

        _intervalLeft -= Time.Time.DeltaTime;

        while (_intervalLeft <= 0f)
        {
            _intervalLeft += _data.Interval;
            var position = _emitter.EmitPosition;

            for (var i = 0; i < _data.EmitCount; i++)
                Emit(position);
        }
    }

    public void Burst(Vector2? position = null)
    {
        for (var i = 0; i < _data.EmitCount; i++)
            Emit(position ?? _emitter.EmitPosition);
    }
}