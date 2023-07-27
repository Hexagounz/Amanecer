using UnityEngine;

public class MenusinGame : MonoBehaviour
{
    public bool gameHasEnded = false;
    public static bool GameIsPaused = false;
    [SerializeField] GameObject pausemenuUI;
    [SerializeField] GameObject GameoverUI;
    public float restartdelay = 2f;

    private void Start()
    {
        Resume();
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused){ Resume();
            }
            else{ Pause();
            }
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
