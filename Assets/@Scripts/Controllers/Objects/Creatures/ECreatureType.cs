public class ECreatureType
{
    public readonly static ECreatureType Hero = new("Hero", "@Heroes");
    public readonly static ECreatureType Monster = new("Monster", "@Monsters");

    ECreatureType(string name, string rootObjName)
    {
        Name = name;
        RootObjName = rootObjName;
    }

    public string Name { get; }
    public string RootObjName { get; }
}
