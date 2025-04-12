namespace Scripts.Scene
{
        public sealed class ESceneKind
        {
                string Name { get; }
                int Index { get; }
        
                ESceneKind(int index, string name)
                {
                        Index = index;
                        Name = name;
                }

                public readonly static ESceneKind Title = new(0, "시작 화면");
                public readonly static ESceneKind Game = new(1, "게임 화면");
        
                public override string ToString() => Name;
        }
}
