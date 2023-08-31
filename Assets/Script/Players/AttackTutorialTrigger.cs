using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackTutorialTrigger : MonoBehaviour
{
    [SerializeField] private GameObject _attackTutorialText;
    [SerializeField] private string mechanic;

    [SerializeField] private PlayerAbilities fpAbilities;
    [SerializeField] private PlayerAbilities spAbilities;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _attackTutorialText.SetActive(true);
            switch (mechanic)
            {
                case"attack":
                    fpAbilities.canShoot = true;
                    spAbilities.canShoot = true;
                    break;
                case"heal":
                    fpAbilities.canHeal = true;
                    spAbilities.canHeal = true;
                    break;
                case "beam":
                    fpAbilities.canBlast = true;
                    spAbilities.canBlast = true;
                    break;
            }
            Destroy(gameObject, 3f);
            
        }
    }


    private void OnDestroy()
    {
        _attackTutorialText.SetActive(false);


    }
}
