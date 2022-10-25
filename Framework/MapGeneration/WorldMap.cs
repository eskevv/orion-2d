using FirstLight;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Linq;
using System;
namespace DewInterface;

public class WorldMap
{
    public List<LightTile> Tiles { get; private set; }

    /// <summary>Loads a map from a tmx file. Note: content must be placed under root: 'Content'.</summary>
    public WorldMap(string levelName)
    {
        var map = MapLoader.Load($"content/{levelName}.tmx");
        Tiles = LoadTiles(map);
        LoadWallObjects(map);
    }

    private List<LightTile> LoadTiles(TileMap map)
    {
        var output = new List<LightTile>();

        foreach (var layer in map.TileLayers!)
        {
            int[] data = layer.LayerData.Gids!;
            for (int x = 0; x < data.Length; x++)
            {
                if (data[x] != 0)
                {
                    LightTile tile = map.GetTileData(data[x], x, layer.Width)!;
                    output.Add(tile);
                }
            }
        }

        return output;
    }

    private void LoadWallObjects(TileMap map)
    {
        var walls = map.ObjectLayers!.First(x => x.Name == "wall_collisions");

        var created_objects = new List<Rectangle>();

        foreach (var wall in walls.Objects!)
            created_objects.Add(new Rectangle((int)wall.X, (int)wall.Y, (int)wall.Width, (int)wall.Height));

        GameManager.Walls = created_objects;
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