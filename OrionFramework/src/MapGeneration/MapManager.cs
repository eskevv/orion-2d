using System;
using System.Collections.Generic;
using System.Linq;
using FirstLight;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using OrionFramework.AssetManagement;
using OrionFramework.Entities;
using OrionFramework.Helpers;
using OrionFramework.Physics;
using OrionFramework.CameraView;
using OrionFramework.Drawing;

namespace OrionFramework.MapGeneration;

public class MapManager
{
    // Properties / Fields

    public List<TiledObjectGroup> Objects = new();
    public int ExtraDrawRange { get; set; } = 100;

    private List<MapTile> _tiles = new();
    private List<MapTile> _notDrawn = new();
    private List<AABB> _tileColliders = new();
    private List<Entity> _instanceEntities = new();
    private Rectangle[] _grid;

    public Vector2 MapSize => new Vector2(_map.Columns, _map.Rows);
    public int TileSize => _map.TileWidth;

    public bool ShowGrid { get; set; }

    private string _levelName;
    private TileMap _map;

    // -- Initialization 

    public void Load(string levelName)
    {
        _levelName = levelName;
        _map = MapLoader.Load($"content/{levelName}.tmx");
        LoadTiles(_map);
        LoadObjects(_map);
        SetMapGrid();
    }

    private void SetMapGrid()
    {
        _grid = new Rectangle[(int)MapSize.X * (int)MapSize.Y];

        for (int x = 0; x < _grid.Length; x++)
        {
            int row = x % (int)MapSize.X;
            int column = x / (int)MapSize.X;
            _grid[x] = new Rectangle(column * TileSize, row * TileSize, TileSize, TileSize);
        }
    }


    private void LoadTiles(TileMap map)
    {
        foreach (var layer in map.TileLayers!)
        {
            int[] data = layer.LayerData.Gids!;

            for (int x = 0; x < data.Length; x++)
            {
                if (data[x] != 0)
                {
                    var tiled_model = map.GetTileData(x, layer);
                    var texture = AssetManager.LoadAsset<Texture2D>(tiled_model!.ImageSource.RemoveExtension());
                    var position = new Vector2(tiled_model.WorldPositionX, tiled_model.WorldPositionY);
                    var srcRect = new Rectangle(tiled_model.SourceX, tiled_model.SourceY, tiled_model.Width,
                        tiled_model.Height);
                    var animation = tiled_model.FrameData is null ? null : GetTileAnimation(tiled_model);

                    _tiles.Add(new MapTile(tiled_model.ImageName, texture, layer.Name, position, srcRect, animation));
                }
            }
        }
    }

    private void LoadObjects(TileMap map)
    {
        foreach (var objectLayer in map.ObjectLayers!)
        {
            Objects.Add(objectLayer);
        }
    }

    public MapTile[] GetTiles(string layer)
    {
        return _tiles.Where(x => x.ParentLayer == layer).ToArray();
    }

    public TiledObject[] GetObjects(string layer)
    {
        return Objects.First(x => x.Name == layer).Objects!;
    }

    private TileAnimation GetTileAnimation(LightTile tiled_model)
    {
        var frames = new TileFrame[tiled_model.FrameData!.Length];
        foreach (var frame in tiled_model.FrameData)
        {
            var src = new Rectangle(frame.SourceX, frame.SourceY, tiled_model.Width, tiled_model.Height);
            var tile_frame = new TileFrame(src, frame.Duration);
        }

        return new TileAnimation(frames);
    }

    // -- Public Interface

    public void ExcludeDrawnLayers(params string[] layers)
    {
        foreach (var layer in layers)
        {
            var tile = _tiles.Find(x => x.ParentLayer == layer);
            if (tile is null)
            {
                Console.WriteLine($"Tile layer: {layer} does not exist in the map.");
                continue;
            }

            _notDrawn.Add(tile);
            _tiles.Remove(tile);
        }
    }

    public void LoadObjects(string objectLayer)
    {
        var walls = _map.ObjectLayers!.First(x => x.Name == objectLayer);

        foreach (var wall in walls.Objects!)
        {
            var center = new Vector2(wall.X + wall.Width / 2, wall.Y + wall.Height / 2);
            _tileColliders.Add(new AABB(center, wall.Width, wall.Height));
        }
    }

    public void Update()
    {
        _tiles.ForEach(x => x.Update());
    }

    public void Draw()
    {
        var drawn_tiles = _tiles
            .Where(x => OrionHelp.InScreenBounds(x.Position, ExtraDrawRange))
            .ToList();

        drawn_tiles.ForEach(x => x.Draw());

        if (ShowGrid)
            foreach (var tile in _grid)
                Batcher.DrawRect(tile, new Color(30, 30, 30, 50));
    }
}