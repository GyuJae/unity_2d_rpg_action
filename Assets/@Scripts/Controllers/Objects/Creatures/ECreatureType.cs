public class ECreatureType
{
    public readonly static ECreatureType Hero = new("Hero");
    public readonly static ECreatureType Monster = new("Monster");

    ECreatureType(string name)
    {
        Name = name;
    }

    string Name { get; }
}
