using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.IO;
using System;
using Newtonsoft.Json;

namespace OrionFramework;

public static class DataBank
{
    private static Dictionary<string, Dictionary<string, IDataModel>> _dataStorage = new();
    private static Dictionary<string, AnimationData> _animations = new();

    public static void AddAnimation(string key, Texture2D texture, FrameData[] data, Vector2? offset) =>
        _animations[key] = new(texture, data, offset);

    public static void AddDataType<T>(Dictionary<string, IDataModel> data)
    {
        _dataStorage[typeof(T).Name] = data;
    }
    
    public static Dictionary<string, T> GetData<T>()
    {
        var newDictionary = new Dictionary<string, T>();
        foreach (var entry in _dataStorage[typeof(T).Name])
        {
            newDictionary[entry.Key] = (T)entry.Value;
        }

        return newDictionary;
    }
    
    public static T GetData<T>(string item)
    {
        var itemSearched = _dataStorage[typeof(T).Name];

        return (T)itemSearched[item];
    }
    
    public static Animation GetAnimation(string key)
    {
        var data = _animations[key];
        return new Animation(data.Texture, data.Frames, data.OriginOffset);
    }

    public static void LoadAtlas(string filePath, string textureName, bool invertedYOrigin = false)
    {
        string animation_json = File.ReadAllText(filePath);
        var character_objects = JsonConvert.DeserializeObject<Dictionary<string, CharacterData>>(animation_json);
        if (character_objects is null)
            throw new Exception("File not correctly formated for 'CharacterData' type.");

        var texture = AssetManager.LoadAsset<Texture2D>(textureName);

        foreach (var animation in character_objects)
        {
            if (animation.Key == "_meta")
                continue;

            var data = animation.Value;
            var frames = invertedYOrigin ? GetInvertedFrames(data.Frames) : data.Frames.ToArray();
            var offset = new Vector2(data.offsetX, data.offsetY);

            _animations[animation.Key] = new(texture, frames, offset);
        }
    }

    private static FrameData[] GetInvertedFrames(List<FrameData> frames)
    {
        var output = new List<FrameData>();

        int normalX = frames[0].OX;
        int normalY = frames[0].OY;

        foreach (var f in frames)
        {
            var added = f;
            added.OX = normalX + (normalX - f.OX);
            added.OY = normalY + (normalY - f.OY);
            output.Add(added);
        }

        return output.ToArray();
    }
}