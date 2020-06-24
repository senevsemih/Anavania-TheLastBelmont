using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpgradeCollectible : MonoBehaviour
{
    public TMP_Text upgradeCounterText;
    public float upgradeCounter;
    public Material glowAttack;
    public SpriteRenderer playerMaterial;

    void Start()
    {
        upgradeCounter = 0;
        SetCount();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerMaterial.material = glowAttack;
            Destroy(gameObject);
            upgradeCounter++;
            SetCount();
        }
    }
    void SetCount()
    {
        upgradeCounterText.text = " X " + upgradeCounter.ToString();

        if(upgradeCounter >= 3)
        {
            playerMaterial.material = glowAttack;
        }
    }
}
