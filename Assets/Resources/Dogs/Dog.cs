using UnityEngine;

public class Dog : GameEntity
{
    [SerializeField] protected int power;

    [SerializeField] protected float fireRate;
    [SerializeField] protected float speed;
    [SerializeField] protected GameObject attack;
    protected float timer;
    protected int state = 0;
    protected Grapple grapple;
    protected float dir;
    protected float initialDir;
    protected DogHP healthbar;
    protected AudioClip fireSFX;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        base.Start();
        timer = 1;
        initialDir = transform.localScale.x;

        healthbar = transform.GetChild(0).gameObject.GetComponent<DogHP>();

        fireSFX = Resources.Load<AudioClip>("SFX/firing");
    }

    public override void takeDamage(int dmg, bool b = true)
    {
        base.takeDamage(dmg);
        healthbar.UpdateHP();
    }

    void OnTriggerEnter2D (Collider2D other)
    {
        if (other.gameObject.CompareTag("Grapple")){
            grapple = other.gameObject.GetComponent<Grapple>();
            if(grapple.getState() == 0 || grapple.hasBeenHit(gameObject))
                return;

            grapple.hit(gameObject);

            grapple.retract();

            if(hp <= 1){
                grab();
            }
            else{
                takeDamage(1);
            }
        }

        if (other.gameObject.CompareTag("Player") && state != 2){
            Player.instance.hit(transform.position);
            Player.instance.takeDamage(power);
        }
    }

    protected virtual void grab()
    {
        state = 2;
        Destroy(rb);
        transform.SetParent(grapple.gameObject.transform);
        Destroy(transform.GetChild(0).gameObject);
    }

    public void capture()
    {
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if(state == 2)
        {
            if(grapple.getState() == 0)
            {
                capture();
            }
            return;

            // Vector3 pos = transform.localPosition;
            // transform.localPosition = new Vector3(incrementDistance(pos.x, 4 * Time.deltaTime), 
            //                                       incrementDistance(pos.y, 4 * Time.deltaTime),
            //                                       pos.z);
        }

        Vector2 distance = Player.instance.getPosition() - (Vector2)transform.position;

        dir = Mathf.Sign(distance.x);

        transform.localScale = new Vector3(dir * initialDir, transform.localScale.y, transform.localScale.z);

        if((distance.magnitude < 10 || spriteRenderer.isVisible) && state < 2)
        {
            if(state == 0)
                activate();
                
            timer = Mathf.Max(0, timer - Time.deltaTime);

            if(timer == 0){
                timer = fireRate;
                Attack();
            }

            rb.linearVelocity = distance.normalized * speed;
        }
        else{
            if(state != 0)
                deactivate();
            if(rb != null)
                rb.linearVelocity = new Vector2(0, 0);
        }
    }

    float incrementDistance(float x, float i)
    {
        if(x > 0)
        {
            return Mathf.Max(x - i, 0);
        }
        return Mathf.Min(x + i, 0);
    }

    protected virtual void Attack() {}

    protected virtual void activate() {
        state = 1;
    }

    protected virtual void deactivate() {
        state = 0;
    }
}
