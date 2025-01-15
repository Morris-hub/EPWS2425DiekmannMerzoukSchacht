using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class NPC : MonoBehaviour
{
    public Transform player;  // Referenz auf den Spieler
    public float detectionRange = 10f;  // Reichweite, in der der NPC den Spieler bemerkt

    public Material idleMaterial;  // Material, wenn der NPC nichts macht
    public Material activeMaterial; // Material, wenn der Spieler in der Reichweite vom NPC ist
    public Material interactionMaterial; // Material, wenn der Spieler interagiert

    public char npcLetter;  // Buchstabe, den der NPC repräsentiert (für die Reihenfolge)

    private Renderer npcRenderer; // Referenz auf den Renderer des NPCs
    private bool playerInRange = false; // Ist der Spieler in Reichweite
    private void Interact()
    {
        // Hole den nächsten Dialog für diesen NPC
        string dialogue = GameManager.Instance.GetNextDialogue(npcLetter);
        Debug.Log($"NPC {npcLetter} sagt: {dialogue}");

        // Überprüfe, ob die Dialogreihenfolge abgeschlossen ist
        if (GameManager.Instance.IsComplete())
        {
            Debug.Log("Alle Dialoge abgeschlossen! Du hast die richtige Reihenfolge eingehalten.");
        }
    }
 void Start()
    {
        // Renderer-Komponente holen
        npcRenderer = GetComponent<Renderer>();
        // Setze das Anfangsmaterial (idle)
        npcRenderer.material = idleMaterial;
    }
    private void Update()
    {
         // Abstand zwischen NPC und dem Spieler
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // Wenn der Spieler innerhalb der Reichweite ist
        if (distanceToPlayer <= detectionRange)
        {
            if (!playerInRange)
            {
                playerInRange = true;
                npcRenderer.material = activeMaterial;  // Ändere das Material
            }

            // Spieler drückt die Interaktionstaste
            if (Input.GetKeyDown(KeyCode.E))
            {
                Interact();
            }
        }
        else
        {
            // Wenn der Spieler außerhalb der Reichweite ist
            if (playerInRange)
            {
                playerInRange = false;
                npcRenderer.material = idleMaterial;  // Ändere das Material auf idle
            }
        }
    
    }
}

  /*  public Transform player;  // Referenz auf den Spieler
    public float detectionRange = 10f;  // Reichweite, in der der NPC den Spieler bemerkt

    public Material idleMaterial;  // Material, wenn der NPC nichts macht
    public Material activeMaterial; // Material, wenn der Spieler in der Reichweite vom NPC ist
    public Material interactionMaterial; // Material, wenn der Spieler interagiert

    public char npcLetter;  // Buchstabe, den der NPC repräsentiert (für die Reihenfolge)

    private Renderer npcRenderer; // Referenz auf den Renderer des NPCs
    private bool playerInRange = false; // Ist der Spieler in Reichweite

    void Start()
    {
        // Renderer-Komponente holen
        npcRenderer = GetComponent<Renderer>();
        // Setze das Anfangsmaterial (idle)
        npcRenderer.material = idleMaterial;
    }

    void Update()
    {
        // Abstand zwischen NPC und dem Spieler
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // Wenn der Spieler innerhalb der Reichweite ist
        if (distanceToPlayer <= detectionRange)
        {
            if (!playerInRange)
            {
                playerInRange = true;
                npcRenderer.material = activeMaterial;  // Ändere das Material
            }

            // Spieler drückt die Interaktionstaste
            if (Input.GetKeyDown(KeyCode.E))
            {
                Interact();
            }
        }
        else
        {
            // Wenn der Spieler außerhalb der Reichweite ist
            if (playerInRange)
            {
                playerInRange = false;
                npcRenderer.material = idleMaterial;  // Ändere das Material auf idle
            }
        }
    }

    void Interact()
    {
        // Überprüfen, ob dieser NPC in der Reihenfolge der nächste ist
        if (GameManager.Instance.IsNext(npcLetter))
        {
            npcRenderer.material = interactionMaterial;  // Material für erfolgreiche Interaktion
            Debug.Log($"NPC {npcLetter}: Richtige Reihenfolge! Fortschritt gespeichert.");
        }
        else
        {
            Debug.Log($"NPC {npcLetter}: Falsche Reihenfolge. Versuche es erneut.");
        }

        // Überprüfen, ob die Reihenfolge komplett ist
        if (GameManager.Instance.IsComplete())
        {
            Debug.Log("Reihenfolge abgeschlossen. Du hast alle NPCs in der richtigen Reihenfolge angesprochen!");
        }
    }
}
*/