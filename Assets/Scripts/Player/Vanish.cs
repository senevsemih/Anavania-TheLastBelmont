using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vanish : MonoBehaviour
{
    private Material material;

    public bool isVanishing = false;
    private float fade = 1f;

    void Start()
    {
        material = GetComponent<SpriteRenderer>().material;
    }
    void Update()
    {
        HasVanish();
    }
    public void HasVanish()
    {
        if (isVanishing)
        {
            fade -= Time.deltaTime;

            if(fade <=  0)
            {
                fade = 0f;
                isVanishing = false;
            }

            material.SetFloat("_Fade", fade);
        }
    }
}
