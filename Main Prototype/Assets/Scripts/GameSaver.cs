
using System.IO;
using UnityEngine;

[System.Serializable]
public class GameState
{
    public int currentIndex;        // Fortschritt im Wort
    public Vector3 playerPosition;  // Koordinaten des Spielers
    public Vector3 npcAPosition;
    public Vector3 npcBPosition;
    public Vector3 npcCPosition;
    public Vector3 npcDPosition;
}

public class GameSaver : MonoBehaviour
{
    private string fileName = "gameState.json";
    [SerializeField] private Transform player;
    [SerializeField] private Transform npc_A;
    [SerializeField] private Transform npc_B;
    [SerializeField] private Transform npc_C;
    [SerializeField] private Transform npc_D;

    private void Update()
    {
        // Speichern wenn I Taste gedrückt wird
        if (Input.GetKeyDown(KeyCode.I))
        {
            SaveGame();
        }
    }

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

        GameState state = new GameState
        {
            currentIndex = currentIndex,
            playerPosition = player.position,
            npcAPosition = npc_A.position,
            npcBPosition = npc_B.position,
            npcCPosition = npc_C.position,
            npcDPosition = npc_D.position,
        };

        string json = JsonUtility.ToJson(state, true);

        string path = Path.Combine(Application.persistentDataPath, fileName); // Besserer Speicherort für Daten

        File.WriteAllText(path, json);

        Debug.Log($"Spielstand gespeichert unter: {path}");
    }
}
