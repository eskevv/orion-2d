using Microsoft.Xna.Framework.Graphics;

namespace Tester;

public class Item
{
    // -- Properties / Fields

    public string Name { get; private set; }

    public Texture2D Image { get; private set; }

    public string ImageName { get; private set; }

    public int Value { get; private set; }

    // -- Initialization

    public Item(string name, string imageSrc, int value)
    {
        ImageName = imageSrc;
        Name = name;
        Image = AssetManager.GetAsset<Texture2D>(imageSrc);
        Value = value;
    }
}