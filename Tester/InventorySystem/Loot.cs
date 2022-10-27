using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Tester;

public class Loot
{
    // -- Properties / Fields

    private Color _color = Color.White;
    private float _scale = 0.333f;
    private float _transitionDuration = 0.8f;
    private float _transitionTime;
    private bool _transitionIncrease;
    private bool _glow;

    public Vector2 Position { get; set; }

    public Item Item { get; set; }

    // -- Initialization

    public Loot(Vector2 position, Item item, bool glow = false)
    {
        Position = position;
        Item = item;
        _glow = glow;
    }

    public void Update()
    {
        GlowEffect();
    }

    private void GlowEffect()
    {
        if (!_glow)
            return;

        _transitionTime += _transitionIncrease ? Time.DeltaTime : -Time.DeltaTime;

        _color = Color.Lerp(Color.YellowGreen, Color.LimeGreen, _transitionTime / _transitionDuration);

        if (_transitionTime >= _transitionDuration)
            _transitionIncrease = false;
        if (_transitionTime <= 0f)
            _transitionIncrease = true;
    }

    // -- Public Interface

    public void Draw()
    {
        var texture = AssetManager.GetAsset<Texture2D>("shard_01j");
        Batcher.DrawTexture(texture, Position, null, _color, 0f, null, _scale, SpriteEffects.None);
    }
}