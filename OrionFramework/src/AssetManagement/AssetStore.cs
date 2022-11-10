using System.Collections.Generic;

namespace OrionFramework.AssetManagement;

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

    public T AddContent(T asset, string name) =>
        _assets[name] = asset;

    public T FindAsset(string name) =>
        _assets[name];
}
