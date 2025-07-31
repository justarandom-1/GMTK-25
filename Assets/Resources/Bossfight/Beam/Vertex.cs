using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vertex : MonoBehaviour
{    
    float speed;

    [SerializeField] int state = 0;

    Rigidbody2D rb;

    Vector2 target;

    QIBeam parent;

    [SerializeField] Vector2 direction;
    
    // Start is called before the first frame update
    void Start()
    {
    }

    public void Initialize(Vector2 t, QIBeam p, float s = 15)
    {
        state = 1;
        target = t;
        parent = p;
        speed = s;
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if(state == 1)
        {
            Vector2 distance = target - (Vector2)transform.position;
            direction = distance.normalized;

            rb.linearVelocity = speed * direction;

            if(distance.magnitude < 0.25f){
                transform.position = target;
                state = 2;
                rb.linearVelocity = new Vector2(0, 0);          
                parent.reachedTarget();
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // if(collision.CompareTag("Environment"))
        //     Destroy(gameObject);

        if (collision.CompareTag("Player") && parent != null)
        {
            parent.hit(collision.gameObject);
        }
    }

    
}
