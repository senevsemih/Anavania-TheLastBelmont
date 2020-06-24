using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngelHealth : MonoBehaviour
{
    private Health health;

    private void Start()
    {
        health = GetComponent<Health>();
    }
    private void Update()
    {
        if(health.CurrentHealth <= 0)
        {
            Destroy(gameObject, 0.5f);
        }
    }
}
