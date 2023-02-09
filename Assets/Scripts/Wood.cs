using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wood : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip breakSFX;
    private bool canCollision = false;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        StartCoroutine(canCollider());
    }
    IEnumerator canCollider()
    {
        yield return new WaitForSeconds(0.5f);
        canCollision = true;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("NotObject") && canCollision == true)
        {
            audioSource.PlayOneShot(breakSFX);
        }
    }
}
