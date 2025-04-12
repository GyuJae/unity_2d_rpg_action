using UnityEngine;
using Object = UnityEngine.Object;

public class UITitleScene : UIScene
{
    protected override void Awake()
    {
        base.Awake();

        BindObjects(typeof(GameObjects));
        BindTexts(typeof(Texts));

        StartLoadAssets();
    }

    void StartLoadAssets()
    {
        Managers.Resource.LoadAllAsync<Object>("PreLoad", (key, count, totalCount) =>
        {
            Debug.Log($"{key} {count}/{totalCount}");

            if (count == totalCount)
            {
                // Managers.Data.Init();

                GetObject((int)GameObjects.StartImage).gameObject.SetActive(true);
                GetText((int)Texts.StatusText).text = "Touch To Start";


            }
        });
    }


    enum GameObjects
    {
        StartImage
    }

    enum Texts
    {
        StatusText
    }
}
