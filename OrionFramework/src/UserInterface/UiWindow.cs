using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace OrionFramework.UserInterface;

public class UiWindow
{
    public Vector2 Position { get; }
    public string Identifier { get; }
    public Texture2D? Skin { get; }

    private readonly List<UiElement> _uiElements = new();

    public UiWindow(string identifier, Vector2 position, string? skin)
    {
        Identifier = identifier;
        Position = position;

        if (skin is not null)
            Skin = AssetManager.LoadAsset<Texture2D>(skin);
    }

    public T GetUiElement<T>(string identifier) where T : UiElement
    {
        return (T)_uiElements.First(x => x.Identifier == identifier);
    }

    public void AddElement(UiElement uiElement)
    {
        _uiElements.Add(uiElement);
    }

    public virtual void Update()
    {
        _uiElements.ForEach(x => x.Update());
    }

    public virtual void Draw()
    {
        _uiElements.ForEach(x => x.Draw());
    }
}