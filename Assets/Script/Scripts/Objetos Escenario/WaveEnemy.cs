using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveEnemy : MonoBehaviour
{
    public Action<WaveEnemy> OnDeath;
    private void OnDestroy()
    {
        OnDeath?.Invoke(this);
    }
}
