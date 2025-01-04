using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public string targetWord = "ABCD"; // Vorgegebenes Wort 
    private int currentIndex = 0; // Fortschritt im Wort

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public bool IsNext(char npcLetter)
    {
        if (currentIndex < targetWord.Length && targetWord[currentIndex] == npcLetter)
        {
            currentIndex++;
            Debug.Log($"Richtig! Fortschritt: {currentIndex}/{targetWord.Length}");
            return true;
        }

        Debug.Log("Falscher NPC! Reihenfolge nicht korrekt.");
        return false;
    }

    public bool IsComplete()
    {
        return currentIndex >= targetWord.Length;
    }
}
