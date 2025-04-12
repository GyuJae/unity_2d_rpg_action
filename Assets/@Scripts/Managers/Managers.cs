using UnityEngine;

public class Managers : MonoBehaviour
{
    const string GameObjectName = "@Managers";

    static Managers _instance;
    readonly PoolManager pool = new();


    readonly ResourceManager resource = new();
    readonly UIManager ui = new();
    static Managers Instance
    {
        get
        {
            Init();
            return _instance;
        }
    }

    public static ResourceManager Resource
    {
        get { return Instance?.resource; }
    }
    public static PoolManager Pool
    {
        get { return Instance?.pool; }
    }
    public static UIManager UI
    {
        get { return Instance?.ui; }
    }

    static void Init()
    {
        if (_instance != null) return;

        var go = GameObject.Find(GameObjectName);

        if (go == null)
        {
            go = new GameObject { name = GameObjectName };
            go.AddComponent<Managers>();
        }

        DontDestroyOnLoad(go);
        _instance = go.GetComponent<Managers>();
    }
}
