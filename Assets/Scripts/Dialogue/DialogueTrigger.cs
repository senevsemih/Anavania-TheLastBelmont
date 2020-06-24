using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Movement playerMovement;
    public Dialogue dialogue;
    public GameObject dialogueBox;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            dialogueBox.SetActive(true);
            FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
            playerMovement.speed = 0;
        }
    }
}
