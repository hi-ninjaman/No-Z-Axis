using UnityEngine;

public class Collector : MonoBehaviour
{
    public string itemID;
    void Start()
    {
        if (PlayerPrefs.GetInt(itemID, 0) == 1)
        {
            Destroy(gameObject);
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            PlayerMovement.coins += 1;
            PlayerPrefs.SetInt(itemID, 1);
            PlayerPrefs.Save();
            int coins = PlayerMovement.coins;
            Debug.Log("Coin Collected! Amount of coins:" + coins.ToString());
            Destroy(gameObject);
        }
    }
}
