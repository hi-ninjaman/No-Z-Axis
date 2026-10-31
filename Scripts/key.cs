using UnityEngine;

public class key : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            PlayerMovement player = collision.GetComponent<PlayerMovement>();
            player.keys += 1;
            Destroy(gameObject);
            int keys = player.keys;
            Debug.Log("Key Collected! Amount of keys:" + keys.ToString());
        }
    }
}
