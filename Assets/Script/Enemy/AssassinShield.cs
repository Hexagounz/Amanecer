using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssassinShield : MonoBehaviour
{
    [SerializeField] private EnemyHealth assassinsHealth;

    [SerializeField] private List<ParticleSystem> _shieldParticles;

    [SerializeField] private List<AssassinsGuard> _assassinsGuards;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < _assassinsGuards.Count; i++)
        {
            _assassinsGuards[i].OnDeath += TestShield;
        }
    }

    private void TestShield(AssassinsGuard guard)
    {
        if (_assassinsGuards.Contains(guard))
        {
            _assassinsGuards.Remove(guard);
        }

        if (_assassinsGuards.Count == 0)
        {
            for (int i = 0; i < _shieldParticles.Count; i++)
            {
                _shieldParticles[i].Stop();
            }

            assassinsHealth.isInmune = false;
        }
    }
}
