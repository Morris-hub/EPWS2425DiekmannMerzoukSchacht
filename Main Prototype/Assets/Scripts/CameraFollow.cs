using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player; // Spieler-Objekt (Würfel)
    public Vector3 offset;   // Abstand zwischen Kamera und Spieler

    void Start()
    {
        // Anfangs-Offset setzen, falls nicht im Editor eingestellt
        if (offset == Vector3.zero)
        {
            offset = transform.position - player.position;
        }
    }

    void LateUpdate()
    {
        // Kamera folgt dem Spieler mit dem definierten Offset
        transform.position = player.position + offset;
    }
}