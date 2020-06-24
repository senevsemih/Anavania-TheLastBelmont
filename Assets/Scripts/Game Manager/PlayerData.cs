using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public float hp;

    public PlayerData(Health playerHealth)
    {
        hp = playerHealth.CurrentHealth;
    }
}
