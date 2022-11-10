namespace OrionFramework.Particles;

public struct ParticleEmitterData
{
    // -- Fields 

    public ParticleData Data = new();

    public float AngleVariance = 45f;

    public float LifeSpanMin = 0.1f;

    public float LifeSpanMax = 2f;

    public float SpeedMin = 10f;

    public float SpeedMax = 100f;

    public float Interval = 1f;

    public int EmitCount = 1;

    // -- Initialization

    public ParticleEmitterData()
    { }
}
