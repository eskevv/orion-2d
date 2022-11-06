using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace OrionFramework.DataStorage;

public static class DataBank
{
    private static Dictionary<string, Dictionary<string, IDataModel>> _dataStorage = new();

    public static void AddDataType<T>(string filePath) where T : IDataModel
    {
        var data = Parse<T>(filePath);
        _dataStorage[typeof(T).Name] = data;
    }

    public static Dictionary<string, T> GetData<T>()
    {
        var newDictionary = new Dictionary<string, T>();
        foreach (var entry in _dataStorage[typeof(T).Name])
            newDictionary[entry.Key] = (T)entry.Value;

        return newDictionary;
    }

    public static T GetData<T>(string item)
    {
        var dataType = _dataStorage[typeof(T).Name];

        return (T)dataType[item];
    }

    private static Dictionary<string, IDataModel> Parse<T>(string filePath) where T : IDataModel
    {
        string text = File.ReadAllText(filePath);
        var data = JsonConvert.DeserializeObject<Dictionary<string, T>>(text);

        var output = new Dictionary<string, IDataModel>();
        if (data is null) return output;
        
        foreach (var entry in data)
            output[entry.Key] = entry.Value;

        return output;
    }
}