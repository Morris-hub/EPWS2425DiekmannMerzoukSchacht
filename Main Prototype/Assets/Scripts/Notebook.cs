using System.Collections.Generic;
using UnityEngine;

public class Notebook : MonoBehaviour
{
    public void DisplayEntries()
{
    if (GameManager.Instance == null)
    {
        Debug.LogError("GameManager-Instance nicht gefunden!");
        return;
    }

    string targetWord = GameManager.Instance.GetTargetWord();
    int currentIndex = GameManager.Instance.GetCurrentIndex();
    Dictionary<char, List<string>> npcDialogues = GameManager.Instance.GetNpcDialogues();

    Debug.Log("Notizbuch:");
    Debug.Log($"Aktueller Fortschritt: {currentIndex}/{targetWord.Length}");
    Debug.Log($"TargetWord: {targetWord}");

    // Gehe die Buchstaben im TargetWord durch bis zum currentIndex
    Dictionary<char, int> npcDialoguePosition = new Dictionary<char, int>();

    for (int i = 0; i < currentIndex; i++)
    {
        char npcLetter = targetWord[i]; // Buchstabe des NPCs an dieser Position

        // Prüfen, ob der NPC Dialoge hat
        if (npcDialogues.TryGetValue(npcLetter, out var dialogues))
        {
            // Initialisiere die Position für den NPC, falls noch nicht vorhanden
            if (!npcDialoguePosition.ContainsKey(npcLetter))
            {
                npcDialoguePosition[npcLetter] = 0;
            }

            int dialogueIndex = npcDialoguePosition[npcLetter]; // Hole den Fortschritt dieses NPCs

            // Prüfen, ob ein Dialog für den Fortschritt existiert
            if (dialogueIndex < dialogues.Count)
            {
                Debug.Log($"NPC {npcLetter}: {dialogues[dialogueIndex]}");
                npcDialoguePosition[npcLetter]++; // Fortschritt dieses NPCs erhöhen
            }
            else
            {
                Debug.Log($"NPC {npcLetter}: Kein weiterer Dialog verfügbar.");
            }
        }
        else
        {
            Debug.Log($"NPC {npcLetter}: Keine Dialoge gefunden.");
        }
    }
}


    private void Update()
    {
        // Zeigt das Notizbuch, wenn die Taste Q gedrückt wird
        if (Input.GetKeyDown(KeyCode.Q))
        {
            DisplayEntries();
        }
    }
}
