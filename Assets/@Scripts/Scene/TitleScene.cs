using System;

public sealed class TitleScene : BaseScene
{
    public override ESceneKind SceneKind { get; } = ESceneKind.Title;

    protected override void Awake()
    {
        base.Awake();

    }

    public override void Clear()
    {
        throw new NotImplementedException();
    }
}
