using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace OrionFramework;

public struct ParticleData
{
    public Texture2D Texture = AssetManager.GetAsset<Texture2D>("particle");

    public float LifeSpan = 2f;

    public Color ColorStart = Color.FloralWhite;

    public Color ColorEnd = Color.DodgerBlue;

    public float OpacityStart = 1f;

    public float OpacityEnd = 0f;

    public float SizeStart = 8f;

    public float SizeEnd = 8f;

    public float Speed = 100f;

    public float Angle = 0f;

    public ParticleData()
    { }
}