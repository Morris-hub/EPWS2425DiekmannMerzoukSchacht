using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Campfire : MonoBehaviour
{
    public Transform player;
        public float detectionRange = 10f;
        public Material idleMaterial;
        public Material activeMaterial;
        public Material interactionMaterial;



        private Renderer campfireRenderer;
        private bool playerInRange = false;
        private bool isInDialogue = false;
        private Dialogue dialogueSystem;

        void Start()
        {
            campfireRenderer = GetComponent<Renderer>();
            campfireRenderer.material = idleMaterial;
            dialogueSystem = FindObjectOfType<Dialogue>();
        }



        private void Interact()
        {
            SceneManager.LoadScene("Auflösung");
        }

        void Update()
        {
            float distanceToPlayer = Vector3.Distance(transform.position, player.position);

            if (distanceToPlayer <= detectionRange)
            {
                if (!playerInRange && !isInDialogue)
                {
                    playerInRange = true;
                    campfireRenderer.material = activeMaterial;
                }

                if (Input.GetKeyDown(KeyCode.E) && !isInDialogue)
                {
                    Interact();
                }
            }
            else
            {
                if (playerInRange)
                {
                    playerInRange = false;
                    campfireRenderer.material = idleMaterial;
                }
            }
        }
        
    
}
