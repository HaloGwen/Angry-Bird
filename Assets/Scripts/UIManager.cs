using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public GameObject panel;
    private float remainMonster;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        remainMonster = FindObjectsOfType<Monster>().Length;
        scoreText.text = "Remain: " + remainMonster;
    }
    public void Pause()
    {
        panel.SetActive(true);
        Time.timeScale = 0;
    }
    public void Resume()
    {
        panel.SetActive(false);
        Time.timeScale = 1;
    }
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;
    }
    public void Quit()
    {
        Application.Quit();
    }
    public void Home()
    {
        SceneManager.LoadScene(0);
    }
}
