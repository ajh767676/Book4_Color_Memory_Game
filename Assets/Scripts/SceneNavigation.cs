using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneNavigation : MonoBehaviour
{
    public void OpenPreferences()
    {
        SceneManager.LoadScene("preferences");
    }

    public void OpenIntro()
    {
        SceneManager.LoadScene("intro");
    }

    public void OpenGame()
    {
        SceneManager.LoadScene("chapter2");
    }

    public void OpenExit()
    {
        SceneManager.LoadScene("exit");
    }
}