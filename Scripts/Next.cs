using UnityEngine;

public class Finish : MonoBehaviour
{
    public string Scene;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("Game should end now");
            UnityEngine.SceneManagement.SceneManager.LoadScene(Scene);
        }
    }
}
