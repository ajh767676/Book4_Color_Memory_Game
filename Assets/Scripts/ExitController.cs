using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ExitController : MonoBehaviour
{
    public TMP_Text resultText;

    void Start()
    {
        string playerName =
            PlayerPrefs.GetString("PlayerName", "Player");

        int colors =
            PlayerPrefs.GetInt("FinalColors", 4);

        float finalTime =
            PlayerPrefs.GetFloat("FinalTime", 0);

        string message =
            PlayerPrefs.GetString("ResultMessage", "Game finished.");

        resultText.text =
            message + "\n" +
            "Player: " + playerName + "\n" +
            "Colors: " + colors + "\n" +
            "Time: " + finalTime.ToString("F1") + " seconds";
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene("preferences");
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("intro");
    }
}