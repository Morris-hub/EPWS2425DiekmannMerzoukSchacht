using UnityEngine;

public class NPC : MonoBehaviour
{
    public Transform player;
    public float detectionRange = 10f;
    public Material idleMaterial;
    public Material activeMaterial;
    public Material interactionMaterial;
    public char npcLetter;
    public string npcName;

    [TextArea]
    public string notInTurnMessage = "Ich bin nicht an der Reihe."; // Individueller Satz

    private Renderer npcRenderer;
    private bool playerInRange = false;
    private bool isInDialogue = false;
    private Dialogue dialogueSystem;

    void Start()
    {
        npcRenderer = GetComponent<Renderer>();
        npcRenderer.material = idleMaterial;
        dialogueSystem = FindObjectOfType<Dialogue>();

        if (string.IsNullOrEmpty(npcName))
        {
            npcName = "NPC " + npcLetter;
        }

        if (dialogueSystem != null)
        {
            dialogueSystem.onDialogueEnd.AddListener(OnDialogueEnded);
        }
    }

    private void OnDialogueEnded()
    {
        isInDialogue = false;
        npcRenderer.material = playerInRange ? activeMaterial : idleMaterial;
    }

    private void Interact()
    {
        if (!isInDialogue && dialogueSystem != null)
        {
            string dialogMessage = GameManager.Instance.GetNextDialogue(npcLetter, npcName, notInTurnMessage);
            isInDialogue = true;
            npcRenderer.material = interactionMaterial;
            dialogueSystem.lines = new string[] { dialogMessage };
            dialogueSystem.StartDialogue();
            Debug.Log($"Dialog Message: {dialogMessage}");
        }
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= detectionRange)
        {
            if (!playerInRange && !isInDialogue)
            {
                playerInRange = true;
                npcRenderer.material = activeMaterial;
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
                npcRenderer.material = idleMaterial;
            }
        }
    }

    void OnDestroy()
    {
        if (dialogueSystem != null)
        {
            dialogueSystem.onDialogueEnd.RemoveListener(OnDialogueEnded);
        }
    }
}