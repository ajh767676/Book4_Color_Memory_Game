using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;

public class LoadMemories : MonoBehaviour
{
    public TMP_Text databaseResultsText;

    void Start()
    {
        StartCoroutine(GetMemories());
    }

    IEnumerator GetMemories()
    {
        string url = "http://localhost/getMemories.php";

        using (UnityWebRequest request = UnityWebRequest.Get(url))
        {
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                databaseResultsText.text =
                    "TOP RESULTS\n\n" +
                    request.downloadHandler.text;
            }
            else
            {
                databaseResultsText.text =
                    "Unable to load saved results.";

                Debug.LogError(request.error);
            }
        }
    }
}