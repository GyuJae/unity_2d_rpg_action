using System;
using Unity.VisualScripting;
using UnityEngine;
using static Define;
using Random = UnityEngine.Random;

public sealed class GameScene : BaseScene
{
    public override ESceneKind SceneKind { get; } = ESceneKind.Game;

    protected override void Awake()
    {
        base.Awake();

        var map = Managers.Resource.Instantiate("BaseMap");
        map.transform.position = Vector3.zero;
        map.name = "@BaseMap";

        for (var i = 0; i < 5; i++)
        {
            Managers.Object.Spawn<Hero>(new Vector3Int(-10 + Random.Range(-5, 5), -5 + Random.Range(-5, 5), 0),
                HERO_KNIGHT_ID);
        }

        var camp = Managers.Object.Spawn<HeroCamp>(new Vector3Int(-10, -5, 0), 0);
        Camera.main.GetOrAddComponent<CameraController>().Target = camp;

        Managers.UI.ShowSceneUI<UIJoystick>();

        // Managers.Object.Spawn(new Vector3Int(-10, -10, 0), Monster.PrefabName);
    }

    public override void Clear()
    {
        throw new NotImplementedException();
    }
}
