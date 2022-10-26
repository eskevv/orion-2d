using Microsoft.Xna.Framework.Graphics;

namespace Orion;

class Item
{
    // -- Properties / Fields

    public string Name { get; private set; }

    public Texture2D Image { get; private set; }

    public int Value { get; private set; }

    // -- Initialization

    public Item(string name, string imageSrc, int value)
    {
        Name = name;
        Image = AssetManager.GetAsset<Texture2D>(imageSrc);
        Value = value;
    }
}