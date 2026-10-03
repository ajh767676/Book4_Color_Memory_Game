using UnityEngine;
using TMPro;

public class LoseScreen : MonoBehaviour
{
    void Start()
    {
        GameObject.Find("scoreUI")
            .GetComponent<TMP_Text>().text =
            "Score: " + PlayerPrefs.GetInt("score");
    }
}