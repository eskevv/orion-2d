using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using OrionFramework.BaseEngine;
using OrionFramework.Scene;
using OrionFramework.UserInterface;

// TODO: scene entity initializer interfaces, scene scripts(on load, on update)
// TODO: IDrawable, Ui editing language, EngineEditor
// TODO: Dialog box word fits in current page before erase

namespace Tester.Main;

public class Game1 : Engine
{
    public Game1()
    {
        Content.RootDirectory = "Content";
        IsMouseVisible = false;
        TargetElapsedTime = TimeSpan.FromSeconds(1d / 120);
        ScreenClear = new Color(40, 40, 40);
        Window.IsBorderless = true;
    }

    protected override void Initialize()
    {
        base.Initialize();

        Screen.PreferredBackBufferWidth = 1600;
        Screen.PreferredBackBufferHeight = 900;
        Screen.ApplyChanges();

        Camera.Zoom = 2f;
        Camera.SetLimits(0, Screen.Width, 0, Screen.Height * 4);
        Camera.Position = Vector2.Zero;

        var parsedEntities = new Dictionary<string, Type>()
        {
            ["npcs"] = typeof(Npc),
            ["player"] = typeof(Player)
        };

        SceneManager.AddScene(new Scene("town"), new TypeSceneInitializer(parsedEntities));

        var dialog = new DialogBox("dialogElement", new Rectangle(300, 300, 600, 100));
        var dialogWindow = new UiWindow("dialog", new Vector2(), null);
        dialogWindow.AddElement(dialog);

        SceneManager.AddUiWindowToScene(dialogWindow);
    }

    protected override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        SceneManager.Update();
    }

    protected override void Draw(GameTime gameTime)
    {
        base.Draw(gameTime);
        SceneManager.Draw();
    }
}