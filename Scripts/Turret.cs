using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Turret : MonoBehaviour
{
    // identifying all aspects of the function
    public GameObject Bullet;
    public GameObject ActiveBullets;
    public Transform target;
    public Transform detectionPoint;
    public Transform BulletPoint;
    public GameObject turretBody;
    public LayerMask playerLayer;
    public float speed = 5f;
    public float detectionRadius;
    public float bulletCooldown = 0f;
    public float bulletCooldownDuration = 1f;
    public float realBulletCooldowns = 0f;


    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(detectionPoint.position, detectionRadius);
    }

    void Update()
    {
        Collider2D sensed = Physics2D.OverlapCircle(detectionPoint.transform.position, detectionRadius, playerLayer);
        bulletCooldown -= Time.deltaTime;

        if (sensed != null)
        {
            if (sensed.CompareTag("Player"))
            {
                Vector3 dir = target.position - transform.position;
                float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
                
                if (bulletCooldown <= bulletCooldownDuration)
                {
                    GameObject clone = Instantiate(Bullet, BulletPoint.transform);
                    StartCoroutine(FIRE(clone));
                    bulletCooldown = realBulletCooldowns;
                }
            }
        }
    }
    
    // bullet script!!!
    IEnumerator FIRE(GameObject obj)
    {
        float lifetime = 1f;
        float timer = 0f;
        Vector3 direction = (target.position - transform.position).normalized;

        while (timer < lifetime)
        {   
            obj.transform.SetParent(ActiveBullets.transform, true);
            obj.transform.position += direction * speed * Time.deltaTime;
            timer += Time.deltaTime;
            yield return null;
            
        }
        Destroy(obj);
    }

    // Update is called once per frame

}
