using UnityEngine;



public class MovingPlatform : MonoBehaviour
{
    public Transform[] points;
    public float speed = 1f;
    public GameObject Button;
    public Transform detectionPoint;
    public int detectionRadius;
    public LayerMask playerLayer;
    public bool Begin;
    public bool ignore;
    
    private int i;

    void Start()
    {
        transform.position = points[0].position;
        Begin = false;
    }

    // Update is called once per frame
    void Update()
    {
        Collider2D sensed = Physics2D.OverlapCircle(detectionPoint.transform.position, detectionRadius, playerLayer);

        if(Vector2.Distance(transform.position , points[i].position)< 0.1f) 
        {
            i++;
            if (i == points.Length)
            {
                i = 0;
            }   
        }
        if ((sensed != null && sensed.CompareTag("Player")) || ignore == true)
        {
            Begin = true;
        }
        if (Begin == true)
        {
            transform.position = Vector2.MoveTowards(transform.position, points[i].position, (speed * Time.deltaTime));
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            collision.transform.SetParent(transform);
        }

    }

        void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            collision.transform.SetParent(null);
        }

    }
}
