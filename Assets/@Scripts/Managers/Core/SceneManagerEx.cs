using Scripts.Scene;
using UnityEngine.SceneManagement;

public class SceneManagerEx
{
    public void LoadScene(ESceneKind scene)
    {
        SceneManager.LoadScene(scene.GetName());
    }
}
