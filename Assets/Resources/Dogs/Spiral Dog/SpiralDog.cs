using UnityEngine;

public class SpiralDog : Dog
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected float fireAngle = 0;
    protected float initialAngle;

    protected override void activate() {
        initialAngle = fireAngle % 135;
        base.activate();
    }


    protected override void Attack()
    {
        Instantiate(attack, new Vector3(transform.position.x, transform.position.y, attack.transform.position.z), Quaternion.Euler(0.0f, 0.0f, fireAngle));
        
        if(fireAngle % 135 == initialAngle)
            audioSource.PlayOneShot(fireSFX);
        
        fireAngle += 16.875f;

    }
}
