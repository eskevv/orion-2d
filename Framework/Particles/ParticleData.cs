using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Orion;

public struct ParticleData
{
    private static Texture2D _defaultTexture = null!;

    public Texture2D Texture { get; set; } = _defaultTexture ?? AssetManager.GetAsset<Texture2D>("particle");

    public float LifeSpan { get; set; } = 2f;

    public Color ColorStart { get; set; } = Color.FloralWhite;

    public Color ColorEnd { get; set; } = Color.DodgerBlue;

    public float OpacityStart { get; set; } = 1f;

    public float OpacityEnd { get; set; } = 0f;

    public float SizeStart { get; set; } = 8f;

    public float SizeEnd { get; set; } = 8f;

    public float Speed { get; set; } = 100f;

    public float Angle { get; set; } = 0f;

    public ParticleData()
    { }
}