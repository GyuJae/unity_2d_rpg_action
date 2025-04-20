using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

public interface ILoader<TKey, TValue>
{
    Dictionary<TKey, TValue> MakeDict();
    bool Validate();
}

public class DataManager
{
    public Dictionary<int, CreatureData> CreatureDict { get; private set; } = new();
    public Dictionary<int, EnvData> EnvDict { get; private set; } = new();

    public void Init()
    {
        CreatureDict = LoadJson<CreatureDataLoader, int, CreatureData>("CreatureData").MakeDict();
        EnvDict = LoadJson<EnvDataLoader, int, EnvData>("EnvData").MakeDict();
    }

    TLoader LoadJson<TLoader, TKey, TValue>(string path) where TLoader : ILoader<TKey, TValue>
    {
        var textAsset = Managers.Resource.Load<TextAsset>($"{path}");
        return JsonConvert.DeserializeObject<TLoader>(textAsset.text);
    }
}
