using UnityEngine;

public class BasicDog : Dog
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    protected int fireAngle = 0;

    protected override void Attack()
    {
        Instantiate(attack, new Vector3(transform.position.x, transform.position.y, attack.transform.position.z), Quaternion.Euler(0.0f, 0.0f, fireAngle));
        fireAngle = 30 - fireAngle;
        audioSource.PlayOneShot(fireSFX);
    }
}
