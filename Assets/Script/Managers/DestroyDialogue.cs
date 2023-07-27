using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyDialogue : MonoBehaviour
{
    GameObject Self;

    private void Awake()
    {
        Self = this.gameObject;
    }
    void Update()
    {
        if (Self.activeSelf)
        {
            Destroy(Self, 10f);

        }
    }
}
