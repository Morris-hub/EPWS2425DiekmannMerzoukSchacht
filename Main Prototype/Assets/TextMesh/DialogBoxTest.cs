using UnityEngine;
using UnityEngine.UIElements;

public class DialogBoxTest : MonoBehaviour
{
    void OnEnable()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;
        var dialogBox = root.Q<VisualElement>("DialogBox");

        if (dialogBox != null)
        {
            Debug.Log("✅ DialogBox wurde gefunden!");
            dialogBox.style.display = DisplayStyle.Flex; // Falls sie versteckt ist
        }
        else
        {
            Debug.LogError("❌ DialogBox wurde NICHT gefunden!");
        }
    }
}
