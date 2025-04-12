using System;
using System.Collections.Generic;
using System.Linq;
using Scripts.UI;
using UnityEngine;
using UnityEngine.UI;

public class UIManager
{
    readonly Dictionary<string, UIPopup> _popups = new();

    readonly Stack<UIPopup> _popupStack = new();
    int _pupupOrder = 100;

    public UIScene SceneUI { set; get; }

    public GameObject Root
    {
        get
        {
            var root = GameObject.Find("@UI_Root");
            if (root == null)
                root = new GameObject { name = "@UI_Root" };
            return root;
        }
    }

    public void CacheAllPopups()
    {
        var list = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(assembly => assembly.GetTypes())
            .Where(type => type.IsSubclassOf(typeof(UIPopup)));

        foreach (var type in list)
        {
            CachePopupUI(type);
        }

        CloseAllPopupUI();
    }

    public Canvas SetCanvas(GameObject go, bool sort = true, int sortOrder = 0)
    {
        var canvas = Utils.GetOrAddComponent<Canvas>(go);
        if (canvas != null)
        {
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            canvas.overrideSorting = true;
        }

        var cs = go.GetOrAddComponent<CanvasScaler>();
        if (cs != null)
        {
            cs.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
            cs.referenceResolution = new Vector2(2640, 1080);
        }

        go.GetOrAddComponent<GraphicRaycaster>();

        if (sort)
        {
            canvas.sortingOrder = _pupupOrder;
            _pupupOrder++;
        }

        return canvas;
    }

    public T GetSceneUI<T>() where T : UIBase
    {
        return SceneUI as T;
    }

    public T MakeWorldSpaceUI<T>(Transform parent = null, string name = null) where T : UIBase
    {
        if (string.IsNullOrEmpty(name))
            name = typeof(T).Name;

        var go = Managers.Resource.Instantiate($"{name}");
        if (parent != null)
            go.transform.SetParent(parent);

        var canvas = go.GetOrAddComponent<Canvas>();
        canvas.renderMode = RenderMode.WorldSpace;
        canvas.worldCamera = Camera.main;

        return Utils.GetOrAddComponent<T>(go);
    }

    public T MakeSubItem<T>(Transform parent = null, string name = null, bool pooling = false)
        where T : UIBase
    {
        if (string.IsNullOrEmpty(name))
            name = typeof(T).Name;

        var go = Managers.Resource.Instantiate($"{name}", parent, pooling);
        go.transform.SetParent(parent);
        return Utils.GetOrAddComponent<T>(go);
    }

    public T ShowSceneUI<T>(string name = null) where T : UIScene
    {
        if (string.IsNullOrEmpty(name))
            name = typeof(T).Name;

        var go = Managers.Resource.Instantiate($"{name}");
        var sceneUI = Utils.GetOrAddComponent<T>(go);
        SceneUI = sceneUI;

        go.transform.SetParent(Root.transform);

        return sceneUI;
    }

    public void CachePopupUI(Type type)
    {
        var name = type.Name;

        if (_popups.TryGetValue(name, out var popup) == false)
        {
            var go = Managers.Resource.Instantiate(name, Root.transform);
            popup = go.GetComponent<UIPopup>();
            _popups[name] = popup;
        }

        _popupStack.Push(popup);
    }

    public T ShowPopupUI<T>(string name = null) where T : UIPopup
    {
        if (string.IsNullOrEmpty(name))
            name = typeof(T).Name;

        if (_popups.TryGetValue(name, out var popup) == false)
        {
            var go = Managers.Resource.Instantiate(name);
            popup = Utils.GetOrAddComponent<T>(go);
            _popups[name] = popup;
        }

        _popupStack.Push(popup);

        popup.transform.SetParent(Root.transform);
        popup.gameObject.SetActive(true);
        _pupupOrder++;
        popup.UICanvas.sortingOrder = _pupupOrder;

        return popup as T;
    }

    public void ClosePopupUI(UIPopup popup)
    {
        if (_popupStack.Count == 0)
            return;

        if (_popupStack.Peek() != popup)
        {
            Debug.Log("Close Popup Failed!");
            return;
        }

        // TODO
        // Managers.Sound.PlayPopupClose();
        ClosePopupUI();
    }

    public void ClosePopupUI()
    {
        if (_popupStack.Count == 0)
            return;

        var popup = _popupStack.Pop();
        popup.gameObject.SetActive(false);
        _pupupOrder--;
    }

    public void CloseAllPopupUI()
    {
        while (_popupStack.Count > 0)
            ClosePopupUI();
    }

    public int GetPopupCount()
    {
        return _popupStack.Count;
    }

    public void Clear()
    {
        CloseAllPopupUI();
        Time.timeScale = 1;
        SceneUI = null;
    }
}
