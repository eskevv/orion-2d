using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;
using System.Collections.Generic;

namespace OrionFramework;

public static class AssetManager 
{
    // -- Properties / Fields

    private static ContentManager _contentManager = null!;

    private static Dictionary<int, IAssetStore> _assetStores = null!;

    // -- Initialization

    internal static void Initialize(ContentManager contentManager)
    {
        _contentManager = contentManager;
        _assetStores = new Dictionary<int, IAssetStore>();

        _assetStores["Texture2D".GetHashCode()] = new AssetStore<Texture2D>();
        _assetStores["SoundEffect".GetHashCode()] = new AssetStore<SoundEffect>();
        _assetStores["SpriteFont".GetHashCode()] = new AssetStore<SpriteFont>();
    }

    // -- Public Interface

    public static void RegisterAsset<T>(string path)
    {
        string assetType = typeof(T).Name;
        var store = (AssetStore<T>)_assetStores[assetType.GetHashCode()];
        store.AddContent((T)_contentManager.Load<T>(path), path.FileName());
    }

    public static T GetAsset<T>(string name)
    {
        string assetType = typeof(T).Name;
        var store = (AssetStore<T>)_assetStores[assetType.GetHashCode()];
        return (T)store.FindAsset(name);
    }

    public static T LoadAsset<T>(string path)
    {
        T asset = _contentManager.Load<T>(path);
        string assetType = typeof(T).Name;
        var store = (AssetStore<T>)_assetStores[assetType.GetHashCode()];
        return store.AddContent(asset, path.FileName());
    }
}
