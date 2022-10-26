using System.Collections.Generic;

namespace Orion;

public static class ParticleManager
{
    private static readonly List<Particle> _particles = new();

    private static readonly List<ParticleEmitter> _particleEmitters = new();

    public static void AddParticle(Particle p)
    {
        _particles.Add(p);
    }

    public static void AddParticleEmitter(ParticleEmitter emitter)
    {
        _particleEmitters.Add(emitter);
    }

    public static void Update()
    {
        UpdateParticles();
        UpdateEmitters();
    }

    private static void UpdateParticles()
    {
        _particles.ForEach(x => x.Update());

        _particles.RemoveAll(x => x.IsFinished);
    }

    private static void UpdateEmitters()
    {
        _particleEmitters.ForEach(x => x.Update());
    }

    public static void DrawParticles()
    {
        _particles.ForEach(x => x.Draw());
    }
}