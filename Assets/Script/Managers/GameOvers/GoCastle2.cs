using UnityEngine;
using UnityEngine.SceneManagement;

public class GoCastle2 : MonoBehaviour {

    public PlayerHealth SoleilHealth;
    public float restartDelay = 5f;

    float restartTimer;

    void Update()
    {
        if (SoleilHealth.currentHealth <= 0)
        {
            restartTimer += Time.deltaTime;

            if (restartTimer >= restartDelay)
            {
                SceneManager.LoadScene("GAMEOVER_CASTLE");
            }
        }
    }
}
