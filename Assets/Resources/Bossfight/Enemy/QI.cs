using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
// using Pathfinding;


public class QI : GameEntity
{
    // [SerializeField] protected float speed;
    // [SerializeField] protected int value;
    // protected bool isMoving;

    // protected Transform Base;

    // protected float nextWayPointDistance = 1f;

    // protected Path path;
    // protected int currentWaypoint = 0;
    // bool reachedTarget = false;

    // Seeker seeker;

    protected float xDirection;

    protected float angle;

    protected Animator animator;

    [SerializeField] protected GameObject projectilePrefab;


    // [SerializeField] float fireRate;

    // [SerializeField] float teleportRate;

    // [SerializeField] AudioClip teleportSFX;
    private Transform hand;

    [SerializeField] float timer;

    // private float teleportTimer = 0;

    private GameObject beam = null;

    private int state;

    // [SerializeField] List<Vector2> teleportCoordinates;

    // int prevTeleport = 3;

    // private List<GameObject> allTowers = new List<GameObject>();

    // [SerializeField] protected GameObject skeletonPrefab;

    // [SerializeField] protected float spawnRate;

    // [SerializeField] protected float spawnTimer;

    // [SerializeField] protected GameObject soul;

    // [SerializeField] GameObject starsPrefab;

    // [SerializeField] protected GameObject crocodilePrefab;

    // private Shield shield;
    // private int summonedCrocodiles = 0;

    // private bool usedWave = false;
    // private bool usedStars = false;

    // private bool justUsedSpecial = false;

    public static float hasteTimer = 0;

    [SerializeField] float hasteDuration;

    // Start is called before the first frame update
    public void Initialize(float a = -1)
    {
        base.Start();

        animator = GetComponent<Animator>();

        hand = transform.GetChild(1);

        state = 0;

        angle = a;

        if (angle == -1)
            angle = LevelManager.Vec2Deg(Player.instance.getPosition() - (Vector2)transform.position);

        xDirection = Mathf.Sign(LevelManager.Deg2Vec(angle).x);

        transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x) * -1 * xDirection, transform.localScale.y, transform.localScale.z);

        if (hasteTimer > 0)
            haste();

        // var data = FindObjectsByType<TowerController>(FindObjectsSortMode.None);

            // foreach(TowerController t in data)
            //     allTowers.Add(t.gameObject);

            // shield = transform.GetChild(9).gameObject.GetComponent<Shield>();
    }

    // public override void Push(Vector2 other)
    // {
    //     if(shield.getState() == 1)
    //         return;
    //     LevelManager.instance.PlaySound(hitSFX, 0.4f);
    //     Vector2 force = ((Vector2)transform.position - other).normalized;
    //     rb.AddForce(force * 100);
    //     UpdatePath();
    // }

    // protected override void kill()
    // {

    //     Instantiate(soul, new Vector3(transform.position.x, transform.position.y, -2), Quaternion.identity);

    //     base.kill();
    // }

    // protected override void OnTriggerEnter2D(Collider2D other)
    // {
    //     if(shield.getState() == 1)
    //         return;
    //     base.OnTriggerEnter2D(other);
    // }


    // protected override void OnCollisionEnter2D(Collision2D collision)
    // {
    //     if(shield.getState() == 1)
    //         return;
    //     base.OnCollisionEnter2D(collision);
    // }


    // public override void takeDamage(int dmg)
    // {
    //     if(shield.getState() != 1)
    //         base.takeDamage(dmg);
    // }

    protected void haste()
    {
        animator.speed = 3;
        hasteTimer = hasteDuration;
    }

    protected void unHaste()
    {
        animator.speed = 1;
    }

    protected void FixedUpdate()
    {

        // allTowers.RemoveAll(item => item == null);

        if(Player.instance == null)
            Destroy(gameObject);

        // xDirection = xDirection / Mathf.Abs(xDirection);


        if(state == 2)
        {
            if(animator.GetCurrentAnimatorStateInfo(0).IsName("MidTeleport")){
                Destroy(gameObject);
            }

            return;
        }

        // teleportTimer = Mathf.Max(teleportTimer - Time.deltaTime, 0);

        if(state == 1 && beam == null)
        {
            // rb.linearVelocity = new Vector2(0, 0);
            state = 2;
            animator.Play("Teleport");           
            
            return;
        }

        if(animator.GetCurrentAnimatorStateInfo(0).IsName("Attacking") && timer == -1){
            if(hasteTimer == 0)
                timer = 1;
            else
                timer = 0.1f;
            FireProjectile();
        }

        if((!animator.GetCurrentAnimatorStateInfo(0).IsName("QI") && !animator.GetCurrentAnimatorStateInfo(0).IsName("Attack")))
        {
            // rb.linearVelocity = new Vector2(0, 0);
            return;
        }

        if(hasteTimer > 0)
        {
            hasteTimer = Mathf.Max(hasteTimer - Time.deltaTime, 0);
            if (hasteTimer == 0)
            {
                unHaste();
            }
        }

        // if(path != null)
        // {
        //     if(currentWaypoint >= path.vectorPath.Count){
        //         reachedTarget = true;
        //         return;
        //     }

        //     Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;

        //     Vector2 force = direction * speed * Time.deltaTime;

        //     rb.AddForce(force);

        //     float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);

        //     if (distance < nextWayPointDistance)
        //         currentWaypoint++;
        // }

        if(timer > 0)
        {
            timer = Mathf.Max(timer - Time.deltaTime, 0);
        }
        else if(animator.GetCurrentAnimatorStateInfo(0).IsName("QI") && beam == null)
        {
            timer = -1;
            animator.Play("Attack");
            beam = hand.gameObject;
        }

        // if(curHealth < maxHealth / 2)
        //     spawnTimer = Mathf.Max(spawnTimer - Time.deltaTime, 0);  
    }

    protected void FireProjectile()
    {
        state = 1;
        beam = Instantiate(projectilePrefab, hand.position, Quaternion.identity);
        beam.GetComponent<QIBeam>().Initialize(transform.position, hand, angle, hasteTimer > 0);
    }
}
