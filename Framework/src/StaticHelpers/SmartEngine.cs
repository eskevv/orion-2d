using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace OrionFramework;

public static class SmartEngine
{
    public static Texture2D ShadeWhite(Texture2D original)
    {
        var colors = new Color[original.Width * original.Height];
        original.GetData<Color>(colors);

        for (int x = 0; x < colors.Length; x++)
            if (colors[x].A != 0)
                colors[x] = new Color(255, 255, 255, 255);

        var output = new Texture2D(original.GraphicsDevice, original.Width, original.Height);
        output.SetData<Color>(colors);
        return output;
    }
}