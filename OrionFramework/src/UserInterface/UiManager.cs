using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework.Graphics;

namespace OrionFramework.UserInterface;

public class UiManager
{
    private readonly List<UiWindow> _uiWindows = new();

    public static readonly SpriteFont UniversalFont = AssetManager.LoadAsset<SpriteFont>("gameFont");

    public T GetWindow<T>(string identifier) where T : UiWindow
    {
        return (T)_uiWindows.First(x => x.Identifier == identifier);
    }
    
    public UiWindow GetWindow(string identifier)
    {
        return _uiWindows.First(x => x.Identifier == identifier);
    }

    public void AddUiWindow(UiWindow uiWindow)
    {
        _uiWindows.Add(uiWindow);
    }

    public void Update()
    {
        _uiWindows.ForEach(x => x.Update());
    }

    public void Draw()
    {
        _uiWindows.ForEach(x => x.Draw());
    }
}