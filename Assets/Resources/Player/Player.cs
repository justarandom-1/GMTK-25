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

    private Transform turret;
    private Grapple grapple;

    private Transform grappleTransform;
    protected override void Start()
    {
        base.Start();
        instance = this;

        turret = transform.GetChild(0);
        grappleTransform = turret.GetChild(0).GetChild(0);
        grapple = grappleTransform.gameObject.GetComponent<Grapple>();
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

        rb.linearVelocity = movementVector * speed;


        if(movementVector.x == 0 && movementVector.y == 0){
            // animator.SetBool("isMoving", false);
            audioSource.Stop();
        }
        else if(!audioSource.isPlaying)    
            audioSource.Play();

        // animator.SetBool("isMoving", true);

    }

    public void OnFire(InputValue value){
        if(grapple.getState() == 0 && value.Get<float>() == 1){
            grapple.fire(angle);            
        }

        if(grapple.getState() == 1 && value.Get<float>() == 0){
            grapple.retract();  
        }
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


        if(grapple.getState() == 0)
        {
            float a = rotationSpeed * Time.deltaTime;
            angle += a;

            if(angle > 360)
                angle -= 360;
        }
        else if((grappleTransform.position - transform.position).magnitude > 0.25f)
        {
            angle = LevelManager.VectorToAngle(grappleTransform.position - transform.position);
        }

        turret.rotation = Quaternion.Euler(0.0f, 0.0f, angle - 90);

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
