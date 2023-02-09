using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[SelectionBase]
public class Monster : MonoBehaviour
{
    [SerializeField] Sprite deadSprite;
    [SerializeField] ParticleSystem m_ps;
    private bool _hasDied;
    private bool ShouldDieFromCollision(Collision2D collision)
    {
        if (_hasDied)
        {
            return false;
        }
        Bird bird = collision.gameObject.GetComponent<Bird>();
        if (bird != null)
            return true;
        if(collision.contacts[0].normal.y < -0.0001)
        {
            return true;
        }
        return false;
    }
    IEnumerator Die()
    {
        _hasDied = true;
        GetComponent<SpriteRenderer>().sprite = deadSprite;
        m_ps.Play();

        yield return new WaitForSeconds(1);
        gameObject.SetActive(false);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (ShouldDieFromCollision(collision))
        {
            StartCoroutine(Die());
        }
        
    }
}
