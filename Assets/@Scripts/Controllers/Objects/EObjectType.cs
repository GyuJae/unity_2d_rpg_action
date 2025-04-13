public sealed class EObjectType
{
    public readonly static EObjectType Creature = new("Creature");

    EObjectType(string name)
    {
        Name = name;
    }

    string Name { get; }
}
