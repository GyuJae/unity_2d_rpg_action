using UnityEngine;

public class CameraController : MonoBehaviour
{
    public BaseObject Target { get; set; }

    void Awake()
    {
        if (Camera.main != null)
            Camera.main.orthographicSize = 15.0f;
    }

    void LateUpdate()
    {
        if (Target == null)
            return;

        var targetPosition = new Vector3(Target.CenterPosition.x, Target.CenterPosition.y, -10f);
        transform.position = targetPosition;
    }
}
