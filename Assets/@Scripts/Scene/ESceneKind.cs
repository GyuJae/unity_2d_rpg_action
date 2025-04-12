namespace Scripts.Scene
{
    public sealed class ESceneKind
    {
        public readonly static ESceneKind Title = new(0, "TitleScene");
        public readonly static ESceneKind Game = new(1, "GameScene");

        ESceneKind(int index, string name)
        {
            Index = index;
            Name = name;
        }

        string Name { get; }
        int Index { get; }

        public string GetName()
        {
            return Name;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
