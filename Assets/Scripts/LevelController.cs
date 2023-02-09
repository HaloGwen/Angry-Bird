using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    Monster[] _monsters;
    private AudioSource audioSource;
    public AudioClip winSFX;
    private bool isPlay;
    public bool isGameover;
    public GameObject panel;
    private void OnEnable()
    {
        _monsters = FindObjectsOfType<Monster>();
    }
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        isPlay = false;
        isGameover = false;
    }
    // Update is called once per frame
    void Update()
    {
        if (MonstersAreAllDead())
        {
            if (isPlay == false)
            {
                audioSource.PlayOneShot(winSFX);
                isPlay = true;
            }
            isGameover = true;
            panel.SetActive(true);
        }
    }

    public void GoToNextLevel()
    {
        if(SceneManager.GetActiveScene().buildIndex != SceneManager.sceneCount - 1) { 
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
    private bool MonstersAreAllDead()
    {
        foreach(var monster in _monsters)
        {
            if (monster.gameObject.activeSelf)
            {
                return false;
            }
        }
        return true;
    }
}
