using UnityEngine;

public class Coin : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.TryGetComponent<Player>(out Player player))
        {
            Destroy(gameObject);
        }
    }
}
