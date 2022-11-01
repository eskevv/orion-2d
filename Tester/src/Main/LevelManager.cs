using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System;
using System.IO;
using System.Net;
using Newtonsoft.Json;
using Tester.Data.Models;
using Tester.Entities;

namespace OrionFramework;

public class LevelManager
{
    private Dictionary<string, Type> _instances = new();

    private readonly MapLevel _mapLevel = new("forest_00");
    private SpriteFont _font = AssetManager.LoadAsset<SpriteFont>("gameFont");

    public LevelManager()
    {
        var dataP = File.ReadAllText("Data/projectiles.json");
        var dataC = File.ReadAllText("Data/characters.json");

        var projectiles = JsonConvert.DeserializeObject<Dictionary<string, ProjectileModel>>(dataP);
        var characters = JsonConvert.DeserializeObject<Dictionary<string, CharacterModel>>(dataC);

        var dataPDictionary = new Dictionary<string, IDataModel>();
        foreach (var entry in projectiles!)
            dataPDictionary[entry.Key] = entry.Value;

        var dataCDictionary = new Dictionary<string, IDataModel>();
        foreach (var entry in characters!)
            dataCDictionary[entry.Key] = entry.Value;

        DataBank.AddDataType<ProjectileModel>(dataPDictionary);
        DataBank.AddDataType<CharacterModel>(dataCDictionary);

        var barbarians = _mapLevel.GetObjects("barbarians");
        var player = _mapLevel.GetObjects("player");

        foreach (var b in barbarians)
        {
            var characterData = DataBank.GetData<CharacterModel>("barbarian");
            EntityManager.Add(new Enemy(new(b.X, b.Y), characterData));
        }

        foreach (var p in player)
        {
            var characterData = DataBank.GetData<CharacterModel>("barbarian");
            var playerEntity = new Player(new(p.X, p.Y), characterData)
            {
                Tag = "player"
            };
            EntityManager.Add(playerEntity);
        }
    }

    public static Entity[] FindEntites(string tag)
    {
        return EntityManager.GetByTag(tag);
    }

    public void Update()
    {
        _mapLevel.Update();
        EntityManager.Update();

        var playerPosition = EntityManager.GetByTag("player")[0].Position;

        Camera.Position -= (Camera.Position - playerPosition) * 0.08f;
    }

    public void Draw()
    {
        _mapLevel.Draw();
        EntityManager.Draw();
    }
}