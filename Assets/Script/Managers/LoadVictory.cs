using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadVictory : MonoBehaviour {

    public float restartDelay = 15f;
    [SerializeField] GameObject Creditos;
    [SerializeField] GameObject Victory;


    float restartTimer;

    private void Update()
    {
        restartTimer += Time.deltaTime;

        if (restartTimer >= restartDelay)
        {
            Creditos.SetActive(false);
            Victory.SetActive(true);
        }
    }

}
