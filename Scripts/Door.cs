using UnityEngine;

public class Door : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerMovement player = collision.GetComponent<PlayerMovement>();
        if (collision.gameObject.tag == "Player")
        {
            if (player.keys >= 1)
            {
                Destroy(gameObject);
            }
        }
    }
}
