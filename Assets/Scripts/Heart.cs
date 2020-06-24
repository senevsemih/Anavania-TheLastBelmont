using UnityEngine;

public class Heart : MonoBehaviour
{
    private readonly int upHealth = 10;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.GetComponent<Health>().Add(upHealth);
            Destroy(gameObject);
        }
    }
}
