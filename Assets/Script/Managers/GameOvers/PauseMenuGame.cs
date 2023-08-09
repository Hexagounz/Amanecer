using System;
using UnityEngine;

public class PauseMenuGame : MonoBehaviour
{
    public static PauseMenuGame Instance;
    
    public bool gameHasEnded = false;
    public static bool GameIsPaused = false;
    [SerializeField] GameObject pausemenuUI;
    [SerializeField] GameObject GameoverUI;
    public float restartdelay = 2f;


    private void Awake()
    {
        if (Instance)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        Resume();
    }

    public void ControlPauseMenu()
    {
        Debug.Log("juego pausado");
        if (GameIsPaused)
        { 
            Resume();
        }
        else
        {
            Pause();
        }
    }


    public void EndGame()
    {
        if (gameHasEnded == false)
        {
            gameHasEnded = true;
            Debug.Log("Game Over");
            GameoverUI.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    void Resume()
    {
        pausemenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }
    void Pause()
    {
        pausemenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

}
