using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Tester.Entities;

public class Character : Entity
{
    protected bool FacingRight { get; set; }
    protected float Speed { get; set; }

    protected Character(Vector2 position, CharacterModel data)
    {
        Position = position;
        Speed = data.Speed;
        Texture = AssetManager.LoadAsset<Texture2D>(data.Texture);
    }

    public override void Draw()
    {
        var effect = FacingRight ? SpriteEffects.None : SpriteEffects.FlipHorizontally;

        if (Texture is not null)
            Batcher.DrawTexture(Texture, Position, effect: effect);
    }

    public override void Update()
    {
    }
}