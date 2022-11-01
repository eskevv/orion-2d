using Microsoft.Xna.Framework;

namespace Tester.Entities;

public class Enemy : Character
{
    public Enemy(Vector2 position, CharacterModel data) : base(position, data)
    {
    }

    public override void Draw()
    {
        Batcher.DrawTexture(Texture, Position);
    }

    public override void Update()
    {
    }
}