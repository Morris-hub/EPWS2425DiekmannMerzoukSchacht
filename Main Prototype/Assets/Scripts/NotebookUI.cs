using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class NotebookUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI notebookText;
    [SerializeField] private GameObject notebookPanel;
    [SerializeField] private int maxEntriesPerPage = 5;
    [SerializeField] private Notebook notebook;

    private int currentPage = 0;
    private List<string> dialogueEntries = new List<string>();
    private bool isOpen = false;

    private void Start()
    {
        // Finde Notebook, falls nicht zugewiesen
        if (notebook == null)
        {
            notebook = FindObjectOfType<Notebook>();
            if (notebook == null)
            {
                Debug.LogError("Notebook nicht gefunden!");
            }
        }

        // Stelle sicher, dass das Panel zu Beginn geschlossen ist
        if (notebookPanel != null)
        {
            notebookPanel.SetActive(false);
        }
        else
        {
            Debug.LogError("Notebook Panel ist nicht zugewiesen!");
        }
    }

    // Öffnet oder schließt das Notizbuch
    public void ToggleNotebook()
    {
        isOpen = !isOpen;
        
        if (notebookPanel != null)
        {
            notebookPanel.SetActive(isOpen);
            
            if (isOpen)
            {
                Debug.Log("Notebook wird geöffnet!");
                RefreshEntries();
            }
            else
            {
                Debug.Log("Notebook wurde geschlossen.");
            }
        }
    }

    // Aktualisiert die Einträge aus dem Notebook
    public void RefreshEntries()
    {
        if (notebook != null)
        {
            dialogueEntries = notebook.GetNotebookEntries();
            currentPage = 0;
            UpdateNotebookUI();
        }
    }

    // Nächste Seite anzeigen
    public void NextPage()
    {
        int totalPages = Mathf.Max(1, Mathf.CeilToInt((float)dialogueEntries.Count / maxEntriesPerPage));
        if (currentPage < totalPages - 1)
        {
            currentPage++;
            UpdateNotebookUI();
        }
    }

    // Vorherige Seite anzeigen
    public void PrevPage()
    {
        if (currentPage > 0)
        {
            currentPage--;
            UpdateNotebookUI();
        }
    }

    // UI aktualisieren
    private void UpdateNotebookUI()
    {
        if (notebookText == null)
        {
            Debug.LogError("NotebookText ist nicht zugewiesen!");
            return;
        }

        int totalPages = Mathf.Max(1, Mathf.CeilToInt((float)dialogueEntries.Count / maxEntriesPerPage));
        currentPage = Mathf.Clamp(currentPage, 0, totalPages - 1);

        int startIndex = currentPage * maxEntriesPerPage;
        int endIndex = Mathf.Min(startIndex + maxEntriesPerPage, dialogueEntries.Count);

        if (dialogueEntries.Count > 0)
        {
            notebookText.text = string.Join("\n\n", dialogueEntries.GetRange(startIndex, endIndex - startIndex));
        }
        else
        {
            notebookText.text = "Keine Einträge gefunden.";
        }

        notebookText.text += $"\n\n<b>Seite {currentPage + 1}/{totalPages}</b>";
    }
}