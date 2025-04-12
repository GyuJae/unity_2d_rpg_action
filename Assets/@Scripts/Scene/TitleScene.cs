using Scripts.Scene;
using UnityEngine;

public sealed class TitleScene : BaseScene
{
    public override ESceneKind SceneKind { get; } = ESceneKind.Title;

    protected override void Awake()
    {
        base.Awake();
           
    }

    public override void Clear()
    {
        throw new System.NotImplementedException();
    }

    void StartLoadAssets()
    {
        Managers.Resource.LoadAllAsync<Object>(ResourceManager.PreLoadTag, (key, count, totalCount) =>
        {
            Debug.Log($"{key} {count}/{totalCount}");

            if (count == totalCount)
            {
                //Managers.Data.Init();
            }
        });
    }
}
