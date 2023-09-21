using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeLevelVictoryForest : MonoBehaviour
{
    public EnemyHealth Tank;
    public float restartDelay = 5f;

    float restartTimer;

    void Update()
    {
        if (Tank.currentHealth <= 0)
        {
            restartTimer += Time.deltaTime;

            if (restartTimer >= restartDelay)
            {
                SceneManager.LoadScene("Loading_City");
            }
        }
    }
}
