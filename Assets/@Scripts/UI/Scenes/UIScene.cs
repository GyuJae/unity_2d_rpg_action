using Scripts.UI;

public class UIScene : UIBase
{
    protected override void Awake()
    {
        base.Awake();

        Managers.UI.SetCanvas(gameObject, false);
    }
}
