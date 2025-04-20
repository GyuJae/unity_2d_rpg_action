using System;

public class Hero : Creature
{
    public override ECreatureType Type { get; } = ECreatureType.Hero;

    protected override void Awake()
    {
        base.Awake();

        Managers.Game.OnJoystickStateChanged -= OnJoystickStateChanged;
        Managers.Game.OnJoystickStateChanged += OnJoystickStateChanged;
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
}
