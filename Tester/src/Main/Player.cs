using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using OrionFramework.Entities;
using OrionFramework.Scene;
using OrionFramework.UserInterface;

namespace Tester.Main;

public class Player : Entity
{
    private IEnumerator<string> _outcomes;

    public Player(Vector2 position) : base(position)
    {
        _outcomes = GetDialogLine().GetEnumerator();
    }

    public override void Update()
    {
        Velocity = Input.RawAxes * 1.2f;
        Position += Velocity;

        Camera.Position += (Position - Camera.Position) * 0.1f;

        if (Input.Pressed(Keys.V))
            SceneManager.AddEntityToScene(new Npc(Position));

        var dialogWindow = SceneManager.UiManager.GetWindow("dialog");
        var dialog = dialogWindow.GetUiElement<DialogBox>("dialogElement");

        if (Input.Pressed(Keys.R))
            _outcomes = GetDialogLine().GetEnumerator();

        if (Input.Pressed(Keys.X))
            dialog.PushText(_outcomes);
    }

    private IEnumerable<string> GetDialogLine()
    {
        yield return "Remove all newline characters from the string.";
        yield return "Remove newlines from the start and end of the string.";
        yield return "Remove newlines from just the start of the string.";
        yield return "And remove newlines from only the end of the string.";
        yield return "Remove all newline characters from the string.";
        yield return "Remove newlines from the start and end of the string.";
        yield return "Remove newlines from just the start of the string.";
        yield return "And remove newlines from only the end of the string.";
    }

    public override void Draw()
    {
        var t = AssetManager.LoadAsset<Texture2D>("npc");
        var s = new Rectangle(0, 0, 16, 16);
        var o = new Vector2(s.Width / 2, s.Height / 2);
        Batcher.DrawTexture(t, Position, s, origin: o);
    }
}