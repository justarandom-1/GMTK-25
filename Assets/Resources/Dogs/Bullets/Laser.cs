using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField] protected int power;
    [SerializeField] protected float invincibilityTime;
    

    protected SpriteRenderer spriteRenderer;

    private float timer;

    private bool isTouched = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void OnTriggerEnter2D (Collider2D other)
    {
        if (other.gameObject.CompareTag("Player")){
            isTouched = true;
            timer = 0;
        }
    }

    void OnTriggerExit2D (Collider2D other)
    {
        if (other.gameObject.CompareTag("Player")){
            isTouched = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(isTouched)
        {
            if(timer > 0)
            {
                timer -= Time.deltaTime;
            }

            if(timer <= 0)
            {
                timer = invincibilityTime;
                Player.instance.takeDamage(power);
            }
        }
    }
}
