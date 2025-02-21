using UnityEngine;

public class Movement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float turnSpeed = 720f;
    public Transform cameraTransform; // Referenz zur Kamera
    public float collisionCheckDistance = 0.5f; // Distanz für Kollisionserkennung
    public LayerMask collisionMask; // Maske für Objekte, mit denen man kollidieren kann

    void Update()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        // Kamerarichtung holen
        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;

        // Y-Werte ignorieren (damit die Bewegung nicht nach oben/unten geht)
        forward.y = 0;
        right.y = 0;

        forward.Normalize();
        right.Normalize();

        // Bewegung relativ zur Kamera berechnen
        Vector3 moveDirection = forward * moveZ + right * moveX;
        moveDirection.Normalize(); // Sicherstellen, dass die Richtung normalisiert ist

        if (moveDirection.magnitude >= 0.1f)
        {
            // Prüfen, ob vor dem Spieler ein Hindernis ist, bevor die Bewegung ausgeführt wird
            if (!IsBlocked(moveDirection))
            {
                // Berechne den Zielwinkel für die Drehung
                float targetAngle = Mathf.Atan2(moveDirection.x, moveDirection.z) * Mathf.Rad2Deg;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSpeed, 0.1f);

                // Drehe den Spieler zur Bewegungsrichtung
                transform.rotation = Quaternion.Euler(0f, angle, 0f);

                // Bewege den Spieler vorwärts
                transform.Translate(moveDirection * moveSpeed * Time.deltaTime, Space.World);
            }
        }
        else
        {
            // Spieler bewegt sich nicht -> -90° um die X-Achse drehen
            transform.rotation = Quaternion.Euler(-90f, transform.eulerAngles.y, transform.eulerAngles.z);
        }
    }

    // Prüft, ob der Weg vor dem Spieler blockiert ist
    bool IsBlocked(Vector3 moveDirection)
    {
        RaycastHit hit;
        Vector3 rayStart = transform.position;
        Vector3 rayEnd = transform.position + moveDirection * collisionCheckDistance;

        // Zeige den Raycast im Editor
        Debug.DrawRay(rayStart, moveDirection * collisionCheckDistance, Color.red); // Zeigt den Ray als rote Linie

        if (Physics.Raycast(rayStart, moveDirection, out hit, collisionCheckDistance, collisionMask))
        {
            Debug.Log("Hindernis");
            return true; // Hindernis gefunden
        }
        return false; // Kein Hindernis
    }
}