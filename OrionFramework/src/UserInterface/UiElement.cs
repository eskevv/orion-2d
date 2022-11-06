using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace OrionFramework.UserInterface;

public abstract class UiElement
{
    public string Identifier { get; }
    public Vector2 Position { get; set; }
    public Texture2D? Skin { get; set; }

    public UiElement(string identifier)
    {
        Identifier = identifier;
    }
    
    public abstract void Update();

    public abstract void Draw();
}