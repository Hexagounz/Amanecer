using System.Collections.Generic;
using UnityEngine;

public class EventGal1 : MonoBehaviour {

    public GameObject SecondWave;
    public GameObject Dialogue;
    public GameObject Barrier;

    [SerializeField] private GameObject definitiveTutorialTrigger;
    
    [SerializeField] private List<GameObject> mapEnemys;

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
            definitiveTutorialTrigger.SetActive(true);
            
            Destroy(Dialogue.gameObject, 2f);
            foreach (var enemy in mapEnemys)
            {
                enemy.gameObject.SetActive(true);
            }
        }
    }
}
