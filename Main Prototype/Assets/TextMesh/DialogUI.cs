using UnityEngine;
using UnityEngine.UIElements;

public class DialogueUI : MonoBehaviour
{
    private Label dialogText;
    private VisualElement dialogBox;
    private Button nextButton;

    void OnEnable()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;

        // 🎯 Dialog-Box (nach unten setzen)
        dialogBox = new VisualElement();
        dialogBox.style.width = 400;
        dialogBox.style.height = 180;
        dialogBox.style.backgroundColor = new Color(0, 0, 0, 0.8f);
        dialogBox.style.borderTopLeftRadius = 5;
        dialogBox.style.borderTopRightRadius = 5;
        dialogBox.style.borderBottomLeftRadius = 5;
        dialogBox.style.borderBottomRightRadius = 5;
        dialogBox.style.alignItems = Align.Center;
        dialogBox.style.justifyContent = Justify.Center;
        
        // 🎯 Nach unten verschieben (absolute Position)
        dialogBox.style.position = Position.Absolute;
        dialogBox.style.bottom = 20; // Abstand zum unteren Rand
        dialogBox.style.left = 50;
        dialogBox.style.right = 50;

        // 🏷️ Header
        Label header = new Label("NPC Dialog");
        header.style.color = Color.white;
        header.style.fontSize = 25;
        header.style.unityTextAlign = TextAnchor.MiddleCenter;
        dialogBox.Add(header);

        // 📝 Dialog-Text
        dialogText = new Label("Drücke [W], um fortzufahren...");
        dialogText.style.color = Color.white;
        dialogText.style.fontSize = 20;
        dialogText.style.paddingLeft = 10;
        dialogText.style.paddingRight = 10;
        dialogBox.Add(dialogText);

        // 🔘 Button (nicht mehr nötig, aber als Backup)
        nextButton = new Button(() => ShowNextDialog());
        nextButton.text = "Weiter";
        nextButton.style.marginTop = 10;
        dialogBox.Add(nextButton);

        root.Add(dialogBox);
    }

    void Update()
    {
        // 🎮 "W"-Taste als Alternative für den Button
        if (Input.GetKeyDown(KeyCode.W))
        {
            ShowNextDialog();
        }
    }

    void ShowNextDialog()
    {
        dialogText.text = "Neuer Dialogtext...";
    }
}
