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

    public T Spawn<T>(Vector3 position, int templateID) where T : BaseObject
    {
        var prefabName = typeof(T).Name;

        var go = Managers.Resource.Instantiate(prefabName);
        go.transform.position = position;

        var obj = go.GetComponent<BaseObject>();

        // TODO Refactoring
        if (obj.ObjectType == EObjectType.Creature)
        {
            if (templateID != 0 && Managers.Data.CreatureDict.TryGetValue(templateID, out var creatureData) == false)
            {
                Debug.LogError($"ObjectManager Spawn Creature Failed! TryGetValue TemplateID : {templateID}");
                return null;
            }

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

            creature.SetInfo(templateID);
        }

        return obj as T;
    }
    // TODO Despawn
}
