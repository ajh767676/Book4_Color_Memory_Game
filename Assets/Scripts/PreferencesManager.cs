using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class PreferencesManager : MonoBehaviour
{
    public TMP_InputField playerNameInput;
    public TMP_Dropdown colorsDropdown;
    public Slider timeSlider;
    public TMP_Text timeValueText;

    public void UpdateTimeText(float value)
    {
        timeValueText.text = value + " seconds";
    }

    public void StartGame()
    {
        string playerName = playerNameInput.text;

        if (playerName == "")
        {
            playerName = "Player";
        }

        int numberOfColors = colorsDropdown.value + 2;
        float timeLimit = timeSlider.value;

        PlayerPrefs.SetString("PlayerName", playerName);
        PlayerPrefs.SetInt("NumberOfColors", numberOfColors);
        PlayerPrefs.SetFloat("TimeLimit", timeLimit);
        PlayerPrefs.Save();

        SceneManager.LoadScene("chapter2");
    }
}