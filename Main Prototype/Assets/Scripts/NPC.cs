using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public Transform player;  // Referenz auf den Spieler
    public float detectionRange = 10f;  // Reichweite in der der NPC den Spieler bemerkt

    public Material idleMaterial;  // Material, wenn der NPC nichts macht
    public Material playerInReachMaterial; // Material wenn der Spiele in der Reichweite vom NPC ist
    private Renderer npcRenderer; // Referenz auf den Renderer des NPC's
    private bool playerInRange = false; // Ist der Spieler in Reicheweite

    void Start()
    {
        // Renderer-Komponente holen
        npcRenderer = GetComponent<Renderer>();
        // Setze das Anfangsmaterial (idle)
        npcRenderer.material = idleMaterial;
    }

    void Update()
    {
        // Abstand zwischen Gegner und dem Spieler
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // Wenn der Spieler innerhalb der Reichweite ist
        if (distanceToPlayer <= detectionRange)
        {
            if (!playerInRange)
            {
                playerInRange = true;
                npcRenderer.material = playerInReachMaterial;  // Ändere das Material auf das Verfolgungsmaterial
            }
            if (Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("Der Spieler hat den NPC angesprochen");
            }

        }
        else
        {
            // Wenn der Spieler außerhalb der Reichweite ist
            if (playerInRange)
            {
                playerInRange = false;
                npcRenderer.material = idleMaterial;  // Ändere das Material auf das ursprüngliche Material
            }
        }
    }
}
