using System;
using System.Collections.Generic;
using System.Linq;

[Serializable]
public class CreatureDataLoader : ILoader<int, CreatureData>
{
    public List<CreatureData> Creatures = new();

    public Dictionary<int, CreatureData> MakeDict()
    {
        return Creatures.ToDictionary(creature => creature.DataId);
    }

    public bool Validate()
    {
        throw new NotImplementedException();
    }
}
