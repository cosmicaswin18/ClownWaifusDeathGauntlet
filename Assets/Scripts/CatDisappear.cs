using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

public class CatDisappear : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Sprite  []spriteImage;
    [SerializeField] private float blinkInterval = 1.0f;
    void Start()
    {
        //InvokeRepeating("Disapper()", 3f, 1);
        StartCoroutine("Blink");
    }

    

    private void Disapper()
    {
        GetComponent<SpriteRenderer>().gameObject.SetActive(true);
        int Number = Random.Range(0, spriteImage.Length);
        
        GetComponent<SpriteRenderer>().sprite = spriteImage[Number];
    }

    
    IEnumerator Blink()
    {
        while (true)
        {
            GetComponent<SpriteRenderer>().enabled=true;
            yield return new WaitForSeconds(blinkInterval / 2f);
            
            GetComponent<SpriteRenderer>().enabled = false;
            yield return new WaitForSeconds(blinkInterval / 2f);
            
        }
    }


}
