// Dialogue.cs
using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.Events;  // Für Events

public class Dialogue : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public string[] lines;
    public float textSpeed = 0.05f;
    private int index;
    
    public UnityEvent onDialogueEnd;  // Event wenn Dialog endet

    void Start()
    {
        if (textComponent == null)
        {
            Debug.LogError("TextComponent nicht zugewiesen!");
            return;
        }
        textComponent.text = string.Empty;
    }

    public void StartDialogue()
    {
        gameObject.SetActive(true);
        index = 0;
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        textComponent.text = string.Empty;

        if (index < lines.Length && lines[index] != null)
        {
            foreach (char c in lines[index].ToCharArray())
            {
                textComponent.text += c;
                yield return new WaitForSeconds(textSpeed);
            }
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            if (textComponent.text == lines[index])
            {
                EndDialogue();
            }
        }
    }

    void EndDialogue()
    {
        gameObject.SetActive(false);
        onDialogueEnd.Invoke();  // Sende Event
    }
}