// GameManager.cs
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public string targetWord = "ABCACBABCBCAA"; // Vorgabe für die Reihenfolge
    private int currentIndex = 0; // Fortschritt im Wort

    private Dictionary<char, List<string>> npcDialogues; // Buchstabe, Dialoge
    private Dictionary<char, int> npcDialogueIndex; // Fortschritt für jeden NPC

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            npcDialogues = new Dictionary<char, List<string>>();
            npcDialogueIndex = new Dictionary<char, int>(); // Initialize
            LoadDialogueData(); // Lade die Dialoge aus der JSON-Datei
        }
        else
        {
            Destroy(gameObject);
        }
    }

    /// Lädt die Dialogdaten aus der JSON-Datei.
    private void LoadDialogueData()
    {
        string path = Path.Combine(Application.streamingAssetsPath, "dialogues.json");

        if (File.Exists(path))
        {
            string jsonText = File.ReadAllText(path);
            DialogueCollection collection = JsonUtility.FromJson<DialogueCollection>(jsonText);

            foreach (var npc in collection.npcDialogues)
            {
                if (!string.IsNullOrEmpty(npc.letter) && npc.dialogues != null)
                {
                    npcDialogues[npc.letter[0]] = npc.dialogues;
                    npcDialogueIndex[npc.letter[0]] = 0; // Setze den Start-Index für jeden NPC
                }
            }

            Debug.Log("Dialogdaten erfolgreich geladen.");
        }
        else
        {
            Debug.LogError("Dialog JSON nicht gefunden!");
        }
    }

    /// Gibt den nächsten Dialog für den aktuellen NPC zurück.
    public string GetNextDialogue(char npcLetter, string npcName, string notInTurnMessage)
    {
        // Prüfe, ob der NPC in der Reihenfolge korrekt ist
        if (currentIndex < targetWord.Length && targetWord[currentIndex] == npcLetter)
        {
            // Prüfe, ob Dialoge für diesen NPC existieren
            if (npcDialogues.ContainsKey(npcLetter))
            {
                List<string> dialogues = npcDialogues[npcLetter];

                // Prüfe, ob es noch einen Dialog gibt
                if (npcDialogueIndex[npcLetter] < dialogues.Count)
                {
                    string dialogue = npcName + ": " + dialogues[npcDialogueIndex[npcLetter]];
                    npcDialogueIndex[npcLetter]++; // Fortschritt für diesen NPC
                    currentIndex++; // Fortschritt im Wort
                    return dialogue;
                }
                else
                {
                    return $"Keine weiteren Dialoge für NPC {npcLetter}.";
                }
            }
            else
            {
                return $"Dialoge für NPC {npcLetter} nicht gefunden.";
            }
        }
        else
        {
            // Rückgabe des individuellen Satzes, wenn der NPC nicht an der Reihe ist
            return notInTurnMessage;
        }
    }

    /// Überprüft, ob die Dialogreihenfolge abgeschlossen ist.
    public bool IsComplete()
    {
        return currentIndex >= targetWord.Length;
    }

    // Öffentliche Methode, um den aktuellen Index zurückzugeben
    public int GetCurrentIndex()
    {
        return currentIndex;
    }

    public Dictionary<char, List<string>> GetNpcDialogues()
    {
        return npcDialogues;
    }

    public string GetTargetWord()
    {
        return targetWord;
    }
}