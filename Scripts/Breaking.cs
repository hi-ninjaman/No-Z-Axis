using System.Threading.Tasks;
using System.Collections;
using UnityEngine;

public class Breaking : MonoBehaviour
{
    private bool toggling;
    private BoxCollider2D box;
    

    void Start()
    {
        box = GetComponent<BoxCollider2D>();
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!toggling)
        {
            StartCoroutine(ToggleCollider());
        }
    }

    IEnumerator ToggleCollider()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        toggling = true;
        
        yield return new WaitForSeconds(1f);
        box.enabled = false;
        Color color = sr.color;
        color.a = 0.5f;
        sr.color = color;
        Debug.Log("disable");

        yield return new WaitForSeconds(3f);
        box.enabled = true;
        color.a = 1f;
        sr.color = color;
        Debug.Log("enable");

        toggling = false;
    }
    /*
    private async void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (toggling) return;
            toggling = true;

            await Task.Delay(1000);
            if (box == null) return;
            box.enabled = false;

            await Task.Delay(1000);
            if (box == null) return;
            box.enabled = false;

        }
    }
    */
}
