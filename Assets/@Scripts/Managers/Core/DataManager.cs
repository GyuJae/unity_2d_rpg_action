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
    public void Init()
    {

    }

    TLoader LoadJson<TLoader, TKey, TValue>(string path) where TLoader : ILoader<TKey, TValue>
    {
        var textAsset = Managers.Resource.Load<TextAsset>($"{path}");
        return JsonConvert.DeserializeObject<TLoader>(textAsset.text);
    }
}
