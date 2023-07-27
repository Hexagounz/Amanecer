using System.Collections.Generic;
using UnityEngine;

public class HealingTutorial : MonoBehaviour
{
    [SerializeField] GameObject healthTutorial;
    PlayerAbilities playerAbilities;
    public static bool tutorialdone;

    private void Start()
    {
        playerAbilities = GetComponent<PlayerAbilities>();
        tutorialdone = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("HealingTutorial") && tutorialdone == false)
        {
            healthTutorial.SetActive(true);
        }
    }
    private void Update()
    {
        if(playerAbilities.healingCast == true && healthTutorial.activeInHierarchy)
        {
            Debug.Log("i've finished");
            healthTutorial.SetActive(false);
            tutorialdone = true;
        }
        else
        {
            tutorialdone = false;
        }
    }
}
