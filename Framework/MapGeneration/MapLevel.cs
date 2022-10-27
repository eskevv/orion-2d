using FirstLight;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Linq;

namespace OrionFramework;

public class MapLevel
{
    // Properties / Fields

    private string _levelName;
    private TileMap _map;

    public List<LightTile> Tiles { get; private set; } = new();
    public List<AABB> TileColliders { get; private set; } = new();

    // -- Initialization 

    public MapLevel(string levelName)
    {
        _levelName = levelName;
        _map = MapLoader.Load($"content/{levelName}.tmx");
        LoadTiles(_map);
    }

    private void LoadTiles(TileMap map)
    {
        foreach (var layer in map.TileLayers!)
        {
            int[] data = layer.LayerData.Gids!;

            for (int x = 0; x < data.Length; x++)
                if (data[x] != 0)
                    Tiles.Add(map.GetTileData(data[x], x, layer.Width)!);
        }
    }

    // -- Public Interface

    public void LoadObjects(string objectLayer)
    {
        var walls = _map.ObjectLayers!.First(x => x.Name == objectLayer);

        foreach (var wall in walls.Objects!)
        {
            var center = new Vector2(wall.X + wall.Width / 2, wall.Y + wall.Height / 2);
            TileColliders.Add(new AABB(center, wall.Width, wall.Height));
        }
    }

    public void Draw()
    {
        foreach (var tile in Tiles)
        {
            var image = AssetManager.GetAsset<Texture2D>(tile.ImageName);
            var position = new Vector2(tile.WorldPositionX, tile.WorldPositionY);
            var srcRect = new Rectangle(tile.SourceX, tile.SourceY, tile.Width, tile.Height);
            Batcher.DrawTexture(image, position, srcRect, Color.White);
        }
    }
}