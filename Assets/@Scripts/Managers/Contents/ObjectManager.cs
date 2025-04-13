using UnityEngine;

public class ObjectManager
{
    public Transform HeroRoot
    {
        get { return GetRootTransform("@Heroes"); }
    }
    public Transform MonsterRoot
    {
        get { return GetRootTransform("@Monsters"); }
    }

    static Transform GetRootTransform(string name)
    {
        var root = GameObject.Find(name);
        if (root == null)
            root = new GameObject { name = name };
        return root.transform;
    }

    public BaseObject Spawn(Vector3 position, string prefabName)
    {
        var go = Managers.Resource.Instantiate(prefabName, pooling: true);
        go.transform.position = position;

        return go.GetComponent<BaseObject>();
    }

    // TODO Despawn
}
