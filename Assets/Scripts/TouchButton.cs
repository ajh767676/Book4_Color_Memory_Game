using UnityEngine;
using UnityEngine.EventSystems;

public class TouchButton : MonoBehaviour
{
    public void touchButton()
    {
        int colorNumber =
            int.Parse(EventSystem.current.currentSelectedGameObject.tag);

        GameObject.Find("gameManager")
            .GetComponent<ManageAudioGame>()
            .submitColor(colorNumber);
    }
}