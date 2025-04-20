public sealed class EObjectType
{
    public readonly static EObjectType Creature = new("Creature");
    public readonly static EObjectType HeroCamp = new("HeroCamp");

    EObjectType(string name)
    {
        Name = name;
    }

    string Name { get; }
}
