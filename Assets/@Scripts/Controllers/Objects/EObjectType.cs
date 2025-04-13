public sealed class EObjectType
{
    public readonly static EObjectType Creture = new("Creature");

    EObjectType(string name)
    {
        Name = name;
    }

    string Name { get; }
}
