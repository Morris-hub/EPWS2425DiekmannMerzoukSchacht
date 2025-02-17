using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player; // Spieler-Objekt
    public Vector3 offset;   // Abstand zwischen Kamera und Spieler

    public float sensitivity = 2.0f; // Maus-Sensitivität
    public float minY = -40f; // Begrenzung für vertikale Rotation
    public float maxY = 80f;

    private float rotationY = 0f;
    private float rotationX = 0f;

    void Start()
    {
        if (offset == Vector3.zero)
        {
            offset = transform.position - player.position;  // Initialer Abstand
        }

        Cursor.lockState = CursorLockMode.Locked; // Maus verstecken & sperren
    }

    void LateUpdate()
    {
        // Kamera nur bewegen, wenn die linke Maustaste gedrückt wird
        if (Input.GetMouseButton(0))  // 0 steht für die linke Maustaste
        {
            // Mausbewegung erfassen
            rotationX += Input.GetAxis("Mouse X") * sensitivity;
            rotationY -= Input.GetAxis("Mouse Y") * sensitivity;
            rotationY = Mathf.Clamp(rotationY, minY, maxY); // Begrenzung der vertikalen Rotation
        }

        // Kamera-Rotation setzen
        Quaternion rotation = Quaternion.Euler(rotationY, rotationX, 0);
        
        // Kamera-Position hinter dem Spieler halten
        transform.position = player.position + rotation * offset;

        // Die Kamera bleibt hinter dem Spieler und folgt seiner Rotation
        transform.LookAt(player.position); // Optional: Kamera immer auf Spieler gerichtet
    }
}
