using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Tester.Data.Models;

namespace Tester.Entities;

public class Arrow : Entity
{
    private readonly float _speed;
    private readonly Vector2 _direction;
    private readonly float _rotation;
    private readonly Vector2 _origin;

    private float _lifeSpan;
    private float _timeActive;
    private float _damage;

    public Arrow(Vector2 position, Vector2 direction, ProjectileModel data)
    {
        Position = position;
        _direction = direction;
        Texture = AssetManager.LoadAsset<Texture2D>("arrow");
        float amount = MathHelper.PiOver2;
        _origin = new Vector2(Texture!.Width / 2f, Texture!.Height / 2f);
        _speed = data.Speed;
        _lifeSpan = data.LifeSpan;
        _damage = data.Damage;
        _rotation = direction.X < 0 ? -amount : amount;
    }

    public override void Update()
    {
        Position += _direction * _speed;

        _timeActive += Time.DeltaTime;
        if (_timeActive >= _lifeSpan)
            Active = false;
    }

    public override void Draw()
    {
        if (Texture is not null)
            Batcher.DrawTexture(Texture, Position, rotation: _rotation, origin: _origin);
    }
}