using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLife : MonoBehaviour
{
    public DialogueTrigger DeathTrigger;
    public DialogueTrigger RespwanTrigger;
    private Rigidbody2D rb;
    private Animator anim;
    private Vector3 respawnPoint;
    public Dialogue dialogue;
    
    public List<string> tags;

    bool canRestartLevel = false;
   

    [SerializeField] private AudioSource deathSoundEffect;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        respawnPoint = transform.position;
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Trap"))
        {
            deathSoundEffect.Play();
            Die();
        }

        if (collision.gameObject.CompareTag("Alt-Trap") || collision.gameObject.CompareTag("Alt-Cherry"))
        {
            collision.gameObject.GetComponent<DialogueTrigger>().TriggerDialogue();
            collision.enabled = false;
        }

        if (collision.gameObject.CompareTag("Hidden-Trap"))
        {

            collision.gameObject.GetComponentInChildren<SpriteRenderer>().enabled = true;

        }

        if (collision.gameObject.CompareTag("Respawn"))
        {

            respawnPoint = collision.transform.position;

        }
    }

    private void Die()
    {
        rb.bodyType = RigidbodyType2D.Static;
        anim.SetTrigger("death");
        //DeathTrigger.TriggerDialogue();
      
    }

    private void RestartLevel()
    {
       StartCoroutine(WaitAndRestart(2f));
    }

    IEnumerator WaitAndRestart(float waitTime)
    { 
        yield return new WaitForSeconds(waitTime);
        anim.Play("idle");
        transform.position = respawnPoint;
        Debug.LogError("dialog   ");
        //string random_dialogue = dialogue.tags[1];
        int rsndom = Random.Range(0, 6);
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue,rsndom);

        rb.bodyType = RigidbodyType2D.Dynamic;
    }
}
