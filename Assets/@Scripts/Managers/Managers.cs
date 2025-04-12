using System;
using UnityEngine;

public class Managers : MonoBehaviour
{
    const String GameObjectName = "@Managers";

    static Managers _instance;
    public static Managers Instance { get { Init(); return _instance; } }

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
