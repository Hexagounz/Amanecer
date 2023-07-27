using UnityEngine;

public class EventGal1 : MonoBehaviour {

    public GameObject SecondWave;
    public GameObject Dialogue;
    public GameObject Barrier;

    private void Awake()
    {
        SecondWave.gameObject.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            SecondWave.gameObject.SetActive(true);
            Dialogue.gameObject.SetActive(true);
            Barrier.gameObject.SetActive(false);
        }
    }
}
