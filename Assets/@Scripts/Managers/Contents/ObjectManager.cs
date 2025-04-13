using System.Collections.Generic;
using UnityEngine;

public class ObjectManager
{
    public HashSet<Hero> Heroes { get; } = new();
    public HashSet<Monster> Monsters { get; } = new();

    public Transform HeroRoot
    {
        get { return GetRootTransform(ECreatureType.Hero.RootObjName); }
    }
    public Transform MonsterRoot
    {
        get { return GetRootTransform(ECreatureType.Monster.RootObjName); }
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

        var obj = go.GetComponent<BaseObject>();

        // TODO Refactoring
        if (obj.ObjectType == EObjectType.Creature)
        {
            var creature = go.GetComponent<Creature>();
            if (creature.Type == ECreatureType.Hero)
            {
                obj.transform.SetParent(HeroRoot);
                Heroes.Add(creature as Hero);
            }
            else if (creature.Type == ECreatureType.Monster)
            {
                obj.transform.SetParent(MonsterRoot);
                Monsters.Add(creature as Monster);
            }
        }

        return obj;
    }
    // TODO Despawn
}
