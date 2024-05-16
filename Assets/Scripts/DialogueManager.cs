using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{

    public TMP_Text nameText;
    public TMP_Text dialogueText;
    public GameObject dialoguePrefab;
    //public Animator animator;

    //private Queue<string> sentences;

    // Use this for initialization
    void Start()
    {
        //sentences = new Queue<string>();
    }

    public void StartDialogue(Dialogue dialogue,int indexdialog)
    {
        //animator.SetBool("IsOpen", true);
        dialoguePrefab.SetActive(true);
        nameText.text = dialogue.name;

        //sentences.Clear();

        /*foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
            DisplayNextSentence();
        }*/
        Debug.LogError(indexdialog);

        DisplayNextSentence(dialogue.sentences[indexdialog]);
    }

    public void DisplayNextSentence(string dialogue)
    {

        //string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(dialogue));
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null;
        }
        StartCoroutine(EndDialogue());
    }

    IEnumerator EndDialogue()
    {
        yield return new WaitForSeconds(3f);
        dialoguePrefab.SetActive (false);
        //animator.SetBool("IsOpen", false);
    }

}
