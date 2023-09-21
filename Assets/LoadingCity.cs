using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingCity : MonoBehaviour
{
    public float restartDelay = 2f;

    float restartTimer;

    private void Update()
    {
        restartTimer += Time.deltaTime;

        if (restartTimer >= restartDelay)
        {
            SceneManager.LoadScene("Level City");
        }
    }
}
