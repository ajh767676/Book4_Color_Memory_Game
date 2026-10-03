using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class SaveMemory : MonoBehaviour
{
    public IEnumerator SendResult(
        string playerName,
        int colors,
        float seconds)
    {
        string url =
            "http://localhost/saveMemory.php?name=" +
            UnityWebRequest.EscapeURL(playerName) +
            "&colors=" + colors +
            "&seconds=" + seconds.ToString("F2");

        using (UnityWebRequest request = UnityWebRequest.Get(url))
        {
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                Debug.Log("Database result: " +
                          request.downloadHandler.text);
            }
            else
            {
                Debug.LogError("Database error: " +
                               request.error);
            }
        }
    }
}