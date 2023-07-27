using UnityEngine;
using UnityEngine.SceneManagement;

public class Gameover2 : MonoBehaviour {

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
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }            
        }
    }
}
