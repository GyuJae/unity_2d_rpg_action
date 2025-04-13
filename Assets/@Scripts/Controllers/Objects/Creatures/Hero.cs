using System;
using UnityEngine;

public class Hero : Creature
{
    public const string PrefabName = "Hero";
    Vector2 moveDir = Vector2.zero;
    public override float Speed { get; protected set; } = 5.0f;

    public override ECreatureType Type { get; } = ECreatureType.Hero;

    protected override void Awake()
    {
        base.Awake();

        Managers.Game.OnMoveDirChanged -= OnMoveDirChanged;
        Managers.Game.OnMoveDirChanged += OnMoveDirChanged;
        Managers.Game.OnJoystickStateChanged -= OnJoystickStateChanged;
        Managers.Game.OnJoystickStateChanged += OnJoystickStateChanged;
    }

    void Update()
    {
        TranslateEx(moveDir * (Time.deltaTime * Speed));
    }

    void OnJoystickStateChanged(Define.ETouchEvent stickState)
    {
        switch (stickState)
        {

            case Define.ETouchEvent.PointerUp:
                State = CreatureState.Idle;
                break;
            case Define.ETouchEvent.PointerDown:
                State = CreatureState.Move;
                break;
            case Define.ETouchEvent.Click:
            case Define.ETouchEvent.Pressed:
            case Define.ETouchEvent.BeginDrag:
            case Define.ETouchEvent.Drag:
            case Define.ETouchEvent.EndDrag:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(stickState), stickState, null);
        }
    }

    void OnMoveDirChanged(Vector2 dir)
    {
        moveDir = dir;
    }
}
