using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PopUpMessage : MonoBehaviour
{
    public Text Title;
    public Text Message;
    public Text YesButtonText;
    public Button YesButton;
    public Button BackgroundButton;

    public static PopUpMessage Show(string title, string message, string ok = "Ok", UnityAction onClose = null)
    {
        var popUpPrefab = Resources.Load("Prefabs/PopUp");

        if (!popUpPrefab)
        {
            Debug.LogError("Failed to load popup prefab at 'Assets/Resources/Prefabs/PopUp'");
        }

        var popUpObject = Instantiate(popUpPrefab) as GameObject;

        if (!popUpObject)
        {
            Debug.LogError("Failed to instantiate a popUp!");
        }

        var popup = popUpObject.GetComponent<PopUpMessage>();

        popup.Title.text = title;
        popup.Message.text = message;
        popup.YesButtonText.text = ok;

        UnityAction destroy = () => Destroy(popup.gameObject);

        if (onClose != null)
        {
            popup.YesButton.onClick.AddListener(onClose);
            popup.BackgroundButton.onClick.AddListener(onClose);
        }

        popup.YesButton.onClick.AddListener(destroy);
        popup.BackgroundButton.onClick.AddListener(destroy);

        AddToCanvas(popup);

        return popup;
    }

    private static void AddToCanvas(Component gobject)
    {
        var canvases = FindObjectsOfType<Canvas>();

        // Get the first screen overlay canvas we can find,
        // If one is not found get the first canvas of any type,
        // If no canvas is found, create a new one
        var canvas = canvases.FirstOrDefault(c => c.renderMode == RenderMode.ScreenSpaceOverlay) ??
                     canvases.FirstOrDefault() ?? new GameObject("Canvas").AddComponent<Canvas>();

        if (canvas)
        {
            var rect = gobject.GetComponent<RectTransform>();

            rect.SetParent(canvas.transform);
            rect.localScale = Vector3.one;
            rect.anchorMin = Vector2.zero;
            rect.anchorMax = Vector2.one;
            rect.pivot = Vector2.one * .5f;
            rect.sizeDelta = Vector2.zero;
            rect.anchoredPosition = Vector3.zero;
        }
    }
}
