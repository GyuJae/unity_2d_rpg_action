using System;
using UnityEngine;
using static Define;

public class GameManager
{
    ETouchEvent joystickState;

    Vector2 moveDir;
    public ETouchEvent JoystickState
    {
        get { return joystickState; }
        set
        {
            joystickState = value;
            OnJoystickStateChanged?.Invoke(value);

        }
    }
    public Vector2 MoveDir
    {
        get { return moveDir; }
        set
        {
            moveDir = value;
            OnMoveDirChanged?.Invoke(value);
        }
    }

    public event Action<Vector2> OnMoveDirChanged;
    public event Action<ETouchEvent> OnJoystickStateChanged;
}
