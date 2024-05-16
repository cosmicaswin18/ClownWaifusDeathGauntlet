using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    [SerializeField] private float speed = 2f;
    [SerializeField] private AudioSource sawSoundEffect;

    private void Update()
    {
        transform.Rotate(0, 0, 360 * speed * Time.deltaTime);
    }

    private void OnBecameVisible()
    {
        sawSoundEffect.Play();
    }

    private void OnBecameInvisible()
    {
        sawSoundEffect.Stop();
    }
}
