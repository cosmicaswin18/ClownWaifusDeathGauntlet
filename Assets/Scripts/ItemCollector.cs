using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ItemCollector : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;

    [SerializeField] private AudioSource collectSoundEffect;
    [SerializeField] private AudioSource deathSoundEffect;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {


        if (collision.gameObject.CompareTag("Cherry"))
        {
            collectSoundEffect.Play();
            Destroy(collision.gameObject);
        
        }

        if (collision.gameObject.CompareTag("Alt-Cherry"))
        {
            deathSoundEffect.Play();
            Die();

        }
        if (collision.gameObject.CompareTag("Double Jump Key"))
        {
            collision.gameObject.GetComponent<DialogueTrigger>().TriggerDialogue();
            PlayerMovement.isDoubleJump = true;
            Destroy(collision.gameObject);

        }
        if (collision.gameObject.CompareTag("Dash Key"))
        {
            collision.gameObject.GetComponent<DialogueTrigger>().TriggerDialogue();
            PlayerMovement.isDash = true;
            Destroy(collision.gameObject);

        }

        if (collision.gameObject.CompareTag("Finish"))
        {
            deathSoundEffect.Play();
            Die();
            SceneManager.LoadScene("End Screen");

        }
    }

    private void Die()
    {
        rb.bodyType = RigidbodyType2D.Static;
        anim.SetTrigger("death");
    }
}
