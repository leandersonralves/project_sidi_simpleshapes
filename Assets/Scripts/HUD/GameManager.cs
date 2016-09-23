using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    private string nextSceneLoad = string.Empty;
    Background background;

    void Awake()
    {
        if (GetComponentInChildren<Background>() == null)
        {
            Debug.LogError("Not found Background component in " + name);
            Destroy(this);
        }

        background = GetComponentInChildren<Background>();
    }

    public void SetSoundState(bool isEnable)
    {
        AudioListener.pause = !isEnable;
    }

    public void LoadScene(string sceneName)
	{
		Debug.Log ("Load Scene");
        nextSceneLoad = sceneName;
        background.Fade();
        background.onCompleteFadeIn = delegate () { SceneManager.LoadScene(sceneName); };
    }

    public void Quit ()
    {
        Application.Quit();
    }
}
