using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public GameObject playerLight;
    public Movement playerMovement;
    public GameObject dialogueBox;
    public GameObject upgrade;
    public GameObject forcemaster;

    public TMP_Text nameText;
    public TMP_Text dialogueText;

    private Queue<string> sentences;

    void Start()
    {
        sentences = new Queue<string>();
    }
    public void StartDialogue(Dialogue dialogue)
    {
        nameText.text = dialogue.name;

        sentences.Clear();

        foreach(string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }
    public void DisplayNextSentence()
    {
        if(sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }
    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach(char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null;
        }
    }
    public void EndDialogue()
    {
        playerLight.SetActive(true);
        dialogueBox.SetActive(false);
        upgrade.SetActive(true);
        playerMovement.speed = 40;
        forcemaster.GetComponent<Enemy_Controller>().enabled = true;

        Debug.Log("End conversation");
    }
}
