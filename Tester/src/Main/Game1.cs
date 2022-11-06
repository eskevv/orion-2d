using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using OrionFramework.Scene;

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
        
        SceneManager.AddScene(new Scene("town"), parsedEntities);
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
