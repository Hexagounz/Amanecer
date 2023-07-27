using UnityEngine;
using UnityEngine.SceneManagement;

public class GameoverCity : MonoBehaviour {

    [SerializeField] PlayerHealth LevanaHealth;
    [SerializeField] PlayerHealth SoleilHealth;
    public float restartDelay = 5f;

    float restartTimer;

    void Update()
    {
        if (LevanaHealth.currentHealth <= 0 || SoleilHealth.currentHealth <= 0)
        {
            restartTimer += Time.deltaTime;

            if (restartTimer >= restartDelay)
            {
                SceneManager.LoadScene("GAMEOVER_CITY");
            }
        }
    }
}
