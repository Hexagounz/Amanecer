using UnityEngine;
using UnityEngine.SceneManagement;

public class GameoverCastle : MonoBehaviour {

    public PlayerHealth LevanaHealth;
    public float restartDelay = 5f;

    float restartTimer;

    void Update()
    {

        if (LevanaHealth.currentHealth <= 0)
        {
            restartTimer += Time.deltaTime;

            if (restartTimer >= restartDelay)
            {
                SceneManager.LoadScene("GAMEOVER_CASTLE");
            }
        }
    }
}
