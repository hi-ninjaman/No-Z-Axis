using UnityEngine;

public class Next : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("Game should end now");
            Time.timeScale = 0f;
            UnityEngine.SceneManagement.SceneManager.LoadScene("GameOver");
        }
    }
}
