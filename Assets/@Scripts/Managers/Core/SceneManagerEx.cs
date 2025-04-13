using UnityEngine.SceneManagement;

public class SceneManagerEx
{
    public void LoadScene(ESceneKind scene)
    {
        SceneManager.LoadScene(scene.GetName());
    }
}
