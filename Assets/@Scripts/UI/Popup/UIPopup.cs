using Scripts.UI;
using UnityEngine;

public class UIPopup : UIBase
{
    public Canvas UICanvas;

    protected override void Awake()
    {
        base.Awake();

        UICanvas = Managers.UI.SetCanvas(gameObject);
        //PopupOpenAnimation(gameObject);
    }

    public virtual void ClosePopupUI()
    {
        Managers.UI.ClosePopupUI(this);
    }
}
