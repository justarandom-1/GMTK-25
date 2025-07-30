using UnityEngine;

public class explosiveBullet : Bullet
{
    [SerializeField] GameObject explosion;
    private AudioSource audioSource;

    protected override void end()
    {
        GameObject newAttack = Instantiate(explosion, transform.position, Quaternion.Euler(0.0f, 0.0f, Random.Range(0, 360)));
        if(audioSource != null)
            audioSource.PlayOneShot(GetComponent<AudioSource>().clip);
        base.end();
    }

    public void setup(AudioSource a)
    {
        audioSource = a;
    }

    // // Update is called once per frame
    // protected override void Update()
    // {
    //     base.Update();


    // }
}
