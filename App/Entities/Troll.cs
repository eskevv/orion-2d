using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Orion;

public class Troll
{
    private Texture2D _texture = null!;
    private Texture2D _textureHit = null!;
    public Vector2 _position;
    public Rectangle _hitRect;
    private readonly AnimationManager _animation = new();
    private bool _wasHit;
    private float _textureTime;
    private float _hitDuration = 0.1f;
    private float _freezeDuration = 0.4f;
    private ParticleEmitter _emitter;

    public Troll(float xPos, float yPos)
    {
        _texture = AssetManager.GetAsset<Texture2D>("troll_idleA");
        _textureHit = SmartEngine.ShadeWhite(_texture);

        _position = new Vector2(xPos, yPos);
        _hitRect = new Rectangle((int)xPos - 8, (int)yPos - 8, 16, 16);
        _animation.AddAnimation(0, new Animation(_texture, 4, 1, 0.15f));

        _emitter = new ParticleEmitter(new StaticEmitter(_position.X, _position.Y), new ParticleEmitterData()
        {
            EmitCount = 10,
            Interval = 0.01f,
            SpeedMax = 30f,
            SpeedMin = 25f,
            AngleVariance = 180,
            Data = new ParticleData()
            {
                ColorStart = Color.Crimson,
                ColorEnd = Color.WhiteSmoke,
                SizeStart = 3f,
                SizeEnd = 8f,
                OpacityEnd = 1f,
                LifeSpan = 0.1f,
            }
        });
    }

    public void TakeDamage()
    {
        _wasHit = true;
        _textureTime = 0f;
        _animation.Freeze(_freezeDuration);
        _animation.SwitchTexture(_textureHit);
        _emitter.Burst();
    }

    public void Update()
    {
        _animation.Update(0);
        _hitRect = new Rectangle((int)_position.X - 8, (int)_position.Y - 8, 16, 16);

        if (_wasHit)
            TextureTimer();

        _emitter.Update();
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
        _animation.Draw(_position, false);
        // Batcher.DrawRect(_hitRect, Color.Crimson);
    }
}