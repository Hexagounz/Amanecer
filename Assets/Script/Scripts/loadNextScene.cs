using UnityEngine;
using UnityEngine.SceneManagement;

public class loadNextScene : MonoBehaviour {

    public void LoadByIndex(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }
}
