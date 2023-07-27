using UnityEngine;
using UnityEngine.SceneManagement;

public class Victory : MonoBehaviour {

    public EnemyHealth Koutsos;
    public float restartDelay = 5f;

    float restartTimer;

    void Update()
    {
        if (Koutsos.currentHealth <= 0)
        {
            restartTimer += Time.deltaTime;

            if (restartTimer >= restartDelay)
            {
                SceneManager.LoadScene("Creditos");
            }
        }
    }
}
