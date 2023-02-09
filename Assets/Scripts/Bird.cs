using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{
    [SerializeField] float _launchForce = 500;
    [SerializeField] float _maxDragDistance = 5;
    public LevelController levelController;
    public AudioClip flySFX;
    public AudioClip resetBird;
    private AudioSource audioSource;
    private Vector2 startPosition;
    private Rigidbody2D m_rb;
    private SpriteRenderer m_sr;
    private bool _canLaunch;
    private bool isCollider;
    private void Awake()
    {
        m_rb = GetComponent<Rigidbody2D>();
        m_sr = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
        levelController = FindObjectOfType<LevelController>();
    }
    void Start()
    {
        startPosition = m_rb.position;
        m_rb.isKinematic = true;
        _canLaunch = true;
        isCollider = false;
    }
    void OnMouseDown()
    {
        if(_canLaunch == true && levelController.isGameover == false) {
            m_sr.color = Color.red;
        }
    }
    void OnMouseUp()
    {
        if(_canLaunch == true && levelController.isGameover == false)
        {
            Vector2 currentPosition = m_rb.position;
            Vector2 direction = startPosition - currentPosition;
            direction.Normalize();
            m_rb.isKinematic = false;
            m_rb.AddForce(direction * _launchForce);
            audioSource.PlayOneShot(flySFX);
            m_sr.color = Color.white;
            _canLaunch = false;
        }
        
    }
    void OnMouseDrag()
    {
        if (_canLaunch == true && levelController.isGameover == false) { 
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 desiredPosition = mousePosition;
        
            float distance = Vector2.Distance(desiredPosition, startPosition);
            if(distance > _maxDragDistance)
            {
                Vector2 direction = desiredPosition - startPosition;
                direction.Normalize();
                desiredPosition = startPosition + (direction * _maxDragDistance);
            }
            if (desiredPosition.x > startPosition.x)
            {
                desiredPosition.x = startPosition.x;
            }
            m_rb.position = desiredPosition;
        }
    }
    IEnumerator ResetAfterDelay()
    {

        yield return new WaitForSeconds(3);
        audioSource.PlayOneShot(resetBird);
        if(levelController.isGameover == false)
        {
            m_rb.position = startPosition;
        }
        m_rb.isKinematic = true;
        m_rb.velocity = Vector2.zero;
        _canLaunch = true;
        isCollider = false;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(isCollider == false) { 
            StartCoroutine(ResetAfterDelay());
            isCollider = true;
        }
    }
}
