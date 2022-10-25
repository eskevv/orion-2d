using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace DewInterface;

public class Troll
{
    private static Texture2D _texture = null!;
    private static Texture2D _textureHit = null!;
    public static Vector2 _position;
    public static Rectangle _hitRect;
    private static readonly AnimationManager _animation = new();
    private static bool _wasHit;
    private static float _textureTime;
    private static float _hitDuration = 0.1f;
    private static float _freezeDuration = 0.4f;

    public Troll(float xPos, float yPos)
    {
        _texture = AssetManager.GetAsset<Texture2D>("troll_idleA");
        _textureHit = SmartEngine.ShadeWhite(_texture);

        _position = new Vector2(xPos, yPos);
        _hitRect = new Rectangle((int)xPos, (int)yPos, 16, 16);
        _animation.AddAnimation(0, new Animation(_texture, 4, 1, 0.15f));
    }

    public static void TakeDamage()
    {
        _wasHit = true;
        _textureTime = 0f;
        _animation.Freeze(_freezeDuration);
        _animation.SwitchTexture(_textureHit);
    }

    public void Update()
    {
        _animation.Update(0);
        _hitRect = new Rectangle((int)_position.X, (int)_position.Y, 16, 16);

        if (_wasHit)
            TextureTimer();
    }

    private void TextureTimer()
    {
        _textureTime += Time.DeltaTime;

        if (_textureTime >= _hitDuration)
        {
            _wasHit = false;
            _animation.SwitchTexture(_texture);
            _textureTime = 0f;
        }
    }

    public void Draw()
    {
        _animation.Draw(_position);

        // Batcher.DrawRect(_hitRect, Color.Crimson);
    }
}