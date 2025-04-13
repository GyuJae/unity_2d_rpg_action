using UnityEngine;
using UnityEngine.EventSystems;
using static Define;

public class UIJoystick : UIScene
{
    GameObject background;
    GameObject cursor;
    float radius;
    Vector2 touchPos;

    protected override void Awake()
    {
        base.Awake();

        BindObjects(typeof(GameObjects));

        background = GetObject((int)GameObjects.JoystickBg);
        cursor = GetObject((int)GameObjects.JoystickCursor);
        radius = background.GetComponent<RectTransform>().sizeDelta.y / 5;

        gameObject.BindEvent(OnPointerDown, ETouchEvent.PointerDown);
        gameObject.BindEvent(OnPointerUp, ETouchEvent.PointerUp);
        gameObject.BindEvent(OnDrag, ETouchEvent.Drag);


        gameObject.BindEvent();
    }

    void OnPointerDown(PointerEventData evt)
    {
        background.transform.position = evt.position;
        cursor.transform.position = evt.position;
        touchPos = evt.position;

        Managers.Game.JoystickState = ETouchEvent.PointerDown;
    }

    void OnPointerUp(PointerEventData evt)
    {
        cursor.transform.position = touchPos;

        Managers.Game.MoveDir = Vector2.zero;
        Managers.Game.JoystickState = ETouchEvent.PointerUp;
    }

    void OnDrag(PointerEventData eventData)
    {
        var touchDir = eventData.position - touchPos;

        var moveDist = Mathf.Min(touchDir.magnitude, radius);
        var moveDir = touchDir.normalized;
        var newPosition = touchPos + moveDir * moveDist;
        cursor.transform.position = newPosition;

        Managers.Game.MoveDir = moveDir;
        Managers.Game.JoystickState = ETouchEvent.Drag;
    }

    enum GameObjects
    {
        JoystickBg,
        JoystickCursor
    }
}
