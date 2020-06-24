using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostTrail : MonoBehaviour
{
    public GameObject ghost;

    [Space]
    [Header("Stats")]
    public float ghostDelay;
    public float destroyGhostTime;

    [Space]
    public bool makeGhost = false;

    private float ghostDelaySeconds;

    void Start()
    {
        ghostDelaySeconds = ghostDelay;
    }

    void Update()
    {
        if (makeGhost)
        {
            if (ghostDelaySeconds > 0)
            {
                ghostDelaySeconds -= Time.deltaTime;
            }
            else
            {
                GameObject currentGhost = Instantiate(ghost, transform.position, transform.rotation);
                Sprite currenSprite = GetComponent<SpriteRenderer>().sprite;
                currentGhost.GetComponent<SpriteRenderer>().sprite = currenSprite;
                currentGhost.transform.localScale = this.transform.localScale;
                ghostDelaySeconds = ghostDelay;
                Destroy(currentGhost, destroyGhostTime);
            }
        }
    }
}
