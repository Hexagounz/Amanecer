using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingForest : MonoBehaviour {
    
    public float restartDelay = 2f;

    float restartTimer;

    private void Update()
    {
        restartTimer += Time.deltaTime;

        if (restartTimer >= restartDelay)
        {
            SceneManager.LoadScene("Level Forest (2021)");
        }
    }

}
