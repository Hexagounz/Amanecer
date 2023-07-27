using UnityEngine;

public class MovementTutorial : MonoBehaviour
{
    [SerializeField] GameObject TutorialMov;
    private void Awake()
    {
        TutorialMov.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        TutorialMov.SetActive(true);
    }
    private void OnTriggerExit(Collider other) //si se salio del trigger, obviamente puede caminar.
    {
        Destroy(TutorialMov.gameObject, 0.5f);
        Destroy(this.gameObject, 2f);
    }

}
