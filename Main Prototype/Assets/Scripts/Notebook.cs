using UnityEngine;
using System.Collections.Generic;

public class Notebook : MonoBehaviour
{
    [SerializeField] private NotebookUI notebookUI;

    private void Start()
    {
        // Finde NotebookUI, falls nicht zugewiesen
        if (notebookUI == null)
        {
            notebookUI = FindObjectOfType<NotebookUI>();
            if (notebookUI == null)
            {
                Debug.LogError("NotebookUI nicht gefunden!");
            }
        }
    }

    // Sammelt Dialogeinträge und übergibt sie an die UI
    public List<string> GetNotebookEntries()
    {
        if (GameManager.Instance == null)
        {
            Debug.LogError("GameManager-Instance nicht gefunden!");
            return new List<string>();
        }

        List<string> entries = new List<string>();
        string targetWord = GameManager.Instance.GetTargetWord();
        int currentIndex = GameManager.Instance.GetCurrentIndex();
        Dictionary<char, List<string>> npcDialogues = GameManager.Instance.GetNpcDialogues();

        // Tracking-Dictionary für NPC-Dialog-Positionen
        Dictionary<char, int> npcDialoguePosition = new Dictionary<char, int>();

        for (int i = 0; i < currentIndex; i++)
        {
            char npcLetter = targetWord[i];

            if (npcDialogues.TryGetValue(npcLetter, out var dialogues))
            {
                // Initialisiere die Position für den NPC
                if (!npcDialoguePosition.ContainsKey(npcLetter))
                {
                    npcDialoguePosition[npcLetter] = 0;
                }

                int dialogueIndex = npcDialoguePosition[npcLetter];

                // Suche nach dem NPC anhand des Buchstabens
                NPC npc = FindNPCByLetter(npcLetter);
                string npcName = npc != null ? npc.npcName : $"NPC {npcLetter}";  // Fallback auf Buchstaben, falls NPC nicht gefunden wird

                if (dialogueIndex < dialogues.Count)
                {
                    entries.Add($"{npcName}: {dialogues[dialogueIndex]}");
                    npcDialoguePosition[npcLetter]++;
                }
                else
                {
                    entries.Add($"{npcName}: Kein weiterer Dialog verfügbar.");
                }
            }
            else
            {
                entries.Add($"NPC {npcLetter}: Keine Dialoge gefunden.");
            }
        }

        return entries;
    }

    // Methode, um NPC anhand des Buchstabens zu finden
    private NPC FindNPCByLetter(char letter)
    {
        NPC[] npcs = FindObjectsOfType<NPC>();
        foreach (NPC npc in npcs)
        {
            if (npc.npcLetter == letter)
            {
                return npc;
            }
        }
        return null; // Falls kein NPC gefunden wurde
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (notebookUI != null)
            {
                notebookUI.ToggleNotebook();
            }
            else
            {
                Debug.LogError("NotebookUI ist nicht zugewiesen!");
            }
        }
    }
}
