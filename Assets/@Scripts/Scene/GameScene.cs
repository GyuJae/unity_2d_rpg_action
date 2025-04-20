using System;
using Unity.VisualScripting;
using UnityEngine;

public sealed class GameScene : BaseScene
{
    public override ESceneKind SceneKind { get; } = ESceneKind.Game;

    protected override void Awake()
    {
        base.Awake();

        var map = Managers.Resource.Instantiate("BaseMap");
        map.transform.position = Vector3.zero;
        map.name = "@BaseMap";

        var hero = Managers.Object.Spawn(new Vector3Int(-10, -5, 0), Hero.PrefabName);

        Camera.main.GetOrAddComponent<CameraController>().Target = hero;

        Managers.UI.ShowSceneUI<UIJoystick>();

        Managers.Object.Spawn(new Vector3Int(-10, -10, 0), Monster.PrefabName);
    }

    public override void Clear()
    {
        throw new NotImplementedException();
    }
}
