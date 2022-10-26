using System.Collections.Generic;

namespace Orion;

class AssetStore<T> : IAssetStore
{
    // -- Properties / Fields

    private readonly Dictionary<string, T> _assets;

    // -- Initialization

    public AssetStore()
    {
        _assets = new Dictionary<string, T>();
    }

    // -- Public Interface

    public void AddContent(T asset, string name) =>
        _assets[name] = asset;

    public T FindAsset(string name) =>
        _assets[name];
}
