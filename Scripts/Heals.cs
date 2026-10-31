using UnityEngine;
using UnityEngine.UI;

public class Heals : MonoBehaviour
{
    public Image healthImg;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            PlayerMovement player = collision.GetComponent<PlayerMovement>();

            if (player.health != 100)
            {
                
                player.health += 25;
                int health = player.health;
                healthImg.fillAmount = health / 100f;
                Destroy(gameObject);
                
                Debug.Log("Heal collected! Amount of health: " + health);
            }
        }
    }
}