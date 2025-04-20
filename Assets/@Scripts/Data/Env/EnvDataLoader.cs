using System;
using System.Collections.Generic;
using System.Linq;

[Serializable]
public class EnvDataLoader : ILoader<int, EnvData>
{
    public List<EnvData> envs = new();

    public Dictionary<int, EnvData> MakeDict()
    {
        return envs.ToDictionary(env => env.DataId);
    }

    public bool Validate()
    {
        throw new NotImplementedException();
    }
}
