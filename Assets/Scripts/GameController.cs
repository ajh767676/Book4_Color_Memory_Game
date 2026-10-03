using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.InputSystem;

public class GameController : MonoBehaviour
{
    public TMP_Text playerNameText;
    public TMP_Text timerText;
    public GameObject pauseText;

    private float elapsedTime;
    private float timeLimit;
    private bool isPaused;
    private bool gameEnded;
    private SaveMemory saveMemory;

    void Start()
    {
        saveMemory = GetComponent<SaveMemory>();
        Time.timeScale = 1;

        string playerName =
            PlayerPrefs.GetString("PlayerName", "Player");

        timeLimit =
            PlayerPrefs.GetFloat("TimeLimit", 30);

        playerNameText.text = "Player: " + playerName;
        timerText.text = "Time: 0.0";

        pauseText.SetActive(false);
        elapsedTime = 0;
        isPaused = false;
        gameEnded = false;
    }

    void Update()
    {
        if (Keyboard.current != null &&
            Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            TogglePause();
        }

        if (!isPaused && !gameEnded)
        {
            elapsedTime += Time.deltaTime;

            timerText.text =
                "Time: " + elapsedTime.ToString("F1");

            if (elapsedTime >= timeLimit)
            {
                EndGame("Time ran out!");
            }
        }
    }

    public void TogglePause()
    {
        isPaused = !isPaused;
        pauseText.SetActive(isPaused);

        if (isPaused)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }

    public void ExitGame()
    {
        EndGame("Game ended early.");
    }

    public void CompleteGame()
    {
        if (!gameEnded)
        {
            StartCoroutine(SaveAndExit());
        }
    }

    IEnumerator SaveAndExit()
    {
        gameEnded = true;
        Time.timeScale = 1;

        string playerName =
            PlayerPrefs.GetString("PlayerName", "Player");

        int colors =
            PlayerPrefs.GetInt("NumberOfColors", 4);

        PlayerPrefs.SetString(
            "ResultMessage",
            "You completed the game!"
        );

        PlayerPrefs.SetFloat("FinalTime", elapsedTime);
        PlayerPrefs.SetInt("FinalColors", colors);
        PlayerPrefs.Save();

        yield return saveMemory.SendResult(
            playerName,
            colors,
            elapsedTime
        );

        SceneManager.LoadScene("exit");
    }

    void EndGame(string message)
    {
        gameEnded = true;
        Time.timeScale = 1;

        PlayerPrefs.SetString("ResultMessage", message);
        PlayerPrefs.SetFloat("FinalTime", elapsedTime);

        PlayerPrefs.SetInt(
            "FinalColors",
            PlayerPrefs.GetInt("NumberOfColors", 4)
        );

        PlayerPrefs.Save();

        SceneManager.LoadScene("exit");
    }
}