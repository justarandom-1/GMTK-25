using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class GameEntity: MonoBehaviour
{
    [SerializeField] protected int maxHP;

    protected int hp;

    protected Rigidbody2D rb;
    protected AudioSource audioSource;

    protected SpriteRenderer spriteRenderer;

    protected virtual void Start()
    {
        hp = maxHP;
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public virtual void takeDamage(int dmg, bool b = true)
    {
        hp = Mathf.Max(0, hp - dmg);
    }

    public int getHP()
    {
        return hp;
    }

    public int getMaxHP()
    {
        return maxHP;
    }

    public float getHealth()
    {
        return (float) hp / maxHP;
    }

    public Vector2 getPosition()
    {
        return transform.position;
    }
}

public class Player : GameEntity
{
    // Start is called before the first frame update
    public static Player instance;
    public static Vector2 MousePosition;

    [SerializeField] Vector2 xRange;

    [SerializeField] Vector2 yRange;

    [SerializeField] float speed;

    [SerializeField] float rotationSpeed;

    [SerializeField] Vector2 movementVector;
    [SerializeField] AudioClip hitSFX;

    // [SerializeField] Vector2 f;
    private float angle = 90;

    protected override void Start()
    {
        base.Start();
        instance = this;
    }

    public void hit(Vector2 other)
    {
        Vector2 force = ((Vector2)transform.position - other).normalized;
        rb.AddForce(force * 250);
    }

    public override void takeDamage(int dmg, bool playSound = true)
    {
        if(playSound)
            audioSource.PlayOneShot(hitSFX, 0.8f);
        base.takeDamage(dmg);
        LevelManager.instance.updateHealth();

        if(hp == 0)
        {
            GetComponent<PlayerInput>().enabled = false;
            rb.linearVelocity = new Vector2(0, 0);
            LevelManager.instance.Lose();
        }
    }

    void OnMove(InputValue value)
    {
        movementVector = value.Get<Vector2>();

        // if (movementVector.x < 0)
        //     Debug.Log("a"); 

        // if (transform.position.x < xRange[0])
        //     Debug.Log("b"); 

        // if ((movementVector.x < 0 && transform.position.x < xRange[0]) ||
        //     (movementVector.x > 0 && transform.position.x > xRange[1]))
        // {
        //     Debug.Log("off");
        //     movementVector = new Vector2(0, movementVector.y);
        // }

        // if ((movementVector.y < 0 && transform.position.y < yRange[0]) ||
        //     (movementVector.y > 0 && transform.position.y > yRange[1]))
        // {
        //     movementVector = new Vector2(movementVector.x, 0);
        // }

        rb.linearVelocity = movementVector * speed;


        if(movementVector.x == 0 && movementVector.y == 0){
            // animator.SetBool("isMoving", false);
            audioSource.Stop();
        }
        else if(!audioSource.isPlaying)    
            audioSource.Play();

        // animator.SetBool("isMoving", true);

    }

    public Vector2 isInBoundary()
    {
        float a = 1;
        
        float b = 1;
        if(transform.position.x < xRange[0] || transform.position.x > xRange[1])
            a = 0;

        if(transform.position.y < yRange[0] || transform.position.y > yRange[1])
            b = 0;
        return new Vector2(a, b);
    }

    // Update is called once per frame
    void Update()
    {
        if(hp == 0) return;


        // MousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Vector2 distance = MousePosition - (Vector2)transform.position;

        // if(distance.magnitude > 0.1f)
        // {
        //     f = distance.normalized * speed;
        //     rb.AddForce(f);
        // }


        // if(Input.GetMouseButtonDown(0))
        // {
        // }
    }

    
}
