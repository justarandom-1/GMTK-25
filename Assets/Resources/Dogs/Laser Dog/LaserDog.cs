using UnityEngine;

public class LaserDog : Dog
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] float rotateSpeed;
    protected int shots = 0;

    protected float fireAngle = 0;

    private Transform lasers;

    private Animator laserAnimator;


    protected override void Start()
    {
        base.Start();
        lasers = attack.transform;
        laserAnimator = attack.GetComponent<Animator>();
    }

    protected override void grab()
    {
        base.grab();
        Destroy(lasers.gameObject);
    }

    protected override void activate() {
        base.activate();
        laserAnimator.Play("LaserFire");
        audioSource.PlayOneShot(fireSFX);
    }

    protected override void deactivate() {
        base.deactivate();
        laserAnimator.Play("LasersRetract");
    }

    protected override void Attack()
    {
        lasers.Rotate(new Vector3(0, 0, rotateSpeed * Time.deltaTime));
    }
}
