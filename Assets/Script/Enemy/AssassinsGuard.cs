using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssassinsGuard : MonoBehaviour
{
    public Action<AssassinsGuard> OnDeath;

    private void OnDestroy()
    {
        OnDeath?.Invoke(this);
    }
}
