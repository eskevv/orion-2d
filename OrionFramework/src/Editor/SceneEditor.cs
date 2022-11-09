using Microsoft.Xna.Framework;
using ImGuiNET;
using OrionFramework.Drawing;
using OrionFramework.ImGuiWrapper;

namespace OrionFramework.Editor;

public static class SceneEditor
{
    private static ImGuiRenderer _renderer;
    private static Square? _context;

    public static void Init(Game game)
    {
        _renderer = new ImGuiRenderer(game);
        _renderer.RebuildFontAtlas(); // Required so fonts are available for rendering
    }

    public static void Draw(GameTime gameTime)
    {
        _renderer.BeforeLayout(gameTime); // Must be called prior to calling any ImGui controls
        ImGui.Begin("Squares");
        foreach (var item in SquareDatabase.Squares)
        {
            if (ImGui.TreeNodeEx(item.Name))
            {
                _context = item;
            }
        }

        ImGui.End();

        ImGui.Begin("Properties");
        if (_context is not null)
        {
            ImGui.SliderInt("xPos", ref _context.X, 1, 1000);
            ImGui.SliderInt("yPos", ref _context.Y, 1, 1000);
            ImGui.SliderInt("width", ref _context.Width, 1, 1000);
            ImGui.SliderInt("height", ref _context.Height, 1, 1000);
        }

        ImGui.End();
        _renderer.AfterLayout(); // Must be called after ImGui control calls

        Batcher.Begin();
        foreach (var item in SquareDatabase.Squares)
        {
            Batcher.DrawRect(new Vector2(item.X, item.Y), item.Width, item.Height, item.Tint);
        }
        Batcher.Present();
    }
}