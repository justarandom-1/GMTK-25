using UnityEngine;

public class spawnerBullet : Bullet
{
    [SerializeField] GameObject attack;

    [SerializeField] float fireRate;

    private float fireTimer;

    private float fireAngle;
    private int totalShots;

    protected override void Start()
    {
        base.Start();
        fireAngle = Random.Range(0, 360);
        fireTimer = fireRate;
    }

    protected override void OnTriggerEnter2D (Collider2D other)
    {
        if (other.gameObject.CompareTag("Player")){
            Player.instance.takeDamage(power);
        }
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

        fireTimer -= Time.deltaTime;

        if(fireTimer <= 0)
        {
            totalShots++;

            if (totalShots % 2 == 0)
                GetComponent<AudioSource>().Play();

            fireTimer = fireRate;
            Instantiate(attack, transform.position + (Vector3)direction * 0.75f, Quaternion.Euler(0.0f, 0.0f, fireAngle));
        }

        timer -= Time.deltaTime;

        if(!spriteRenderer.isVisible && timer < -6)
        {
            end();
        }        
    }
}
