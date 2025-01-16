using System.IO;
using UnityEngine;

[System.Serializable]
public class GameState
{
    public int currentIndex;        // Fortschritt im Wort
    public Vector3 playerPosition; // Koordinaten des Spielers
}

public class GameSaver : MonoBehaviour
{
    private string fileName = "gameState.json";
    [SerializeField] private Transform player;

    public void SaveGame()
    {
        if (player == null)
        {
            Debug.LogError("Spieler-Transform ist nicht zugewiesen!");
            return;
        }

        if (GameManager.Instance == null)
        {
            Debug.LogError("GameManager-Instance nicht gefunden!");
            return;
        }

        int currentIndex = GameManager.Instance.GetCurrentIndex(); // Index aus GameManager abrufen

        GameState state = new GameState // Spielstand speichern
        {
            currentIndex = currentIndex,
            playerPosition = player.position
        };

        string json = JsonUtility.ToJson(state, true); // JSON-String aus Objet erstellen

        // Pfad zum Assets-Ordners
        string path = Path.Combine(Application.dataPath, fileName);

        File.WriteAllText(path, json);

        Debug.Log($"Spielstand gespeichert unter: {path}"); 
    }

    private void Start()
    {
        // Aufrufen beim aktivieren des Spiels
        SaveGame();
    }
}
