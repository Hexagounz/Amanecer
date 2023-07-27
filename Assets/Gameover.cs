using UnityEngine;  

public class Gameover : MonoBehaviour
{
    bool gameHasEnded = false;

    public float restartdelay = 2f;

    public void EndGame()
    {
        if (gameHasEnded == false)
        {
            gameHasEnded = true;
            Debug.Log("Game Over");
            //Invoke("Restart", restartdelay);
        }
    }



}
