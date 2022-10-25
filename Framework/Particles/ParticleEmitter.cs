using Microsoft.Xna.Framework;

namespace DewInterface;

public class ParticleEmitter
{
    private readonly ParticleEmitterData _data;

    private float _intervalLeft;

    private readonly IEmitter _emitter;

    public ParticleEmitter(IEmitter emitter, ParticleEmitterData data)
    {
        _emitter = emitter;
        _data = data;
        _intervalLeft = _data.Interval;
    }

    private void Emit(Vector2 position)
    {
        ParticleData data = _data.Data;
        data.LifeSpan = GameMath.RandomFloat(_data.LifeSpanMin, _data.LifeSpanMax);
        data.Speed = GameMath.RandomFloat(_data.SpeedMin, _data.SpeedMax);

        float r = GameMath.NextFloat() * 2 - 1;
        data.Angle += _data.AngleVariance * r;

        ParticleManager.AddParticle(new Particle(data, position));
    }

    public void Update()
    {
        _intervalLeft -= Time.DeltaTime;

        while (_intervalLeft <= 0f)
        {
            _intervalLeft += _data.Interval;
            var position = _emitter.EmitPosition;

            for (int i = 0; i < _data.EmitCount; i++)
                Emit(position);                
        }
    }
}