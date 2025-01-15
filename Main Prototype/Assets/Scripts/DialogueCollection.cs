using System.Collections.Generic;

[System.Serializable]
public class NPCDialogue
{
    public string letter;          // Buchstabe des NPC
    public List<string> dialogues; // Liste der Dialoge
}

[System.Serializable]
public class DialogueCollection
{
    public List<NPCDialogue> npcDialogues; // Liste aller NPCs und Dialoge
}
