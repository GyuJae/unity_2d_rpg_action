using System;
using UnityEngine;

public class Managers : MonoBehaviour
{
    const string GameObjectName = "@Managers";

    static Managers _instance;
    static Managers Instance { get { Init(); return _instance; } }

    #region Core
    readonly ResourceManager resource = new();
    readonly PoolManager pool = new();
    
    public static ResourceManager Resource { get { return Instance?.resource; } }
    public static PoolManager Pool { get { return Instance?.pool; } }
    #endregion
    
    static void Init()
    {
        if (_instance != null) return;
        
        GameObject go = GameObject.Find(GameObjectName);

        if (go == null)
        {
            go = new GameObject { name = GameObjectName };
            go.AddComponent<Managers>();
        }

        DontDestroyOnLoad(go);
        _instance = go.GetComponent<Managers>();
    }
}
