using UnityEngine;

public class HeroCamp : BaseObject
{
    Vector2 MoveDir { get; set; } = Vector2.zero;

    public float Speed { get; set; } = 5.0f;

    public Transform Pivot { get; private set; }
    public Transform Destination { get; private set; }

    public override EObjectType ObjectType { get; } = EObjectType.HeroCamp;

    protected override void Awake()
    {
        base.Awake();

        Managers.Game.OnMoveDirChanged -= HandleOnMoveDirChanged;
        Managers.Game.OnMoveDirChanged += HandleOnMoveDirChanged;

        Collider.includeLayers = 1 << (int)Define.ELayer.Obstacle;
        Collider.excludeLayers = 1 << (int)Define.ELayer.Monster | 1 << (int)Define.ELayer.Hero;

        Pivot = Utils.FindChild<Transform>(gameObject, "Pivot", true);
        Destination = Utils.FindChild<Transform>(gameObject, "Destination", true);
    }

    void Update()
    {
        transform.Translate(MoveDir * (Time.deltaTime * Speed));
    }

    void HandleOnMoveDirChanged(Vector2 dir)
    {
        MoveDir = dir;

        if (dir != Vector2.zero)
        {
            var angle = Mathf.Atan2(-dir.x, +dir.y) * 180 / Mathf.PI;
            Pivot.eulerAngles = new Vector3(0, 0, angle);
        }
    }
}
