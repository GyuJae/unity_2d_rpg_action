public sealed class ESceneKind
{
    public readonly static ESceneKind Title = new("TitleScene");
    public readonly static ESceneKind Game = new("GameScene");

    ESceneKind(string name)
    {
        Name = name;
    }

    string Name { get; }


    public string GetName()
    {
        return Name;
    }
}
