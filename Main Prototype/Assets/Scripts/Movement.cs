using System.Collections;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float moveSpeed = 5f; // Geschwindigkeit der Bewegung
    public float turnSpeed = 720f; // Drehgeschwindigkeit des Spielers
    public GameObject player; // Das Spieler-Objekt (Prefab)
    public float sidestepDistance = 1f; // Seitenschritt-Distanz

    void Update()
    {
        // Bewegung in der Horizontalen und Vertikalen (x und z)
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        // Berechnung der Bewegungsrichtung
        Vector3 moveDirection = new Vector3(moveX, 0f, moveZ).normalized;

        // Wenn es eine Bewegung gibt
        if (moveDirection.magnitude >= 0.1f)
        {
            // Berechne den Winkel basierend auf der Richtung der Bewegung
            float targetAngle = Mathf.Atan2(moveDirection.x, moveDirection.z) * Mathf.Rad2Deg;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSpeed, 0.1f);

            // Drehe den Spieler mit seinem Gesicht nach vorne
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            // Bewege den Spiele Vorwärts
            transform.Translate(moveDirection * moveSpeed * Time.deltaTime, Space.World);
        }

    }

 
}
