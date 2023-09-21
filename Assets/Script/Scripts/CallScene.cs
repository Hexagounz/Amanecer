using UnityEngine;
using UnityEngine.SceneManagement;

public class CallScene : MonoBehaviour
{

    public void LoadByIndex()
    {
        SceneManager.LoadScene("Loading_Forest");
    }

    public void QuitGame()
    {
        Debug.Log("QUIT!");
        Application.Quit();
    }
    public void Restart()
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);
    }
}
