using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Tester.Data.Models;

namespace Tester.Entities;

public class Player : Character
{
    public Player(Vector2 position, CharacterModel data) : base(position, data)
    {
    }

    public override void Update()
    {
        Velocity = Input.RawAxes * Speed;

        if (Velocity.X != 0f)
            FacingRight = Velocity.X > 0f;

        Position += Velocity;

        if (Input.Pressed(Keys.A))
        {
            var direction = new Vector2(FacingRight ? 1f : -1f, 0f);
            var data = DataBank.GetData<ProjectileModel>("arrow");
            EntityManager.Add(new Arrow(Position + new Vector2(10f, 10f), direction, data));
        }
    }
}