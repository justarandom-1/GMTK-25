using UnityEngine;

public class FocusedDog : Dog
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected int shots = 0;

    protected float fireAngle = 0;

    protected override void activate() {
        shots = 0;
        base.activate();
    }

    protected override void Attack()
    {
        if(shots == 0)
        {
            fireAngle = LevelManager.VectorToAngle(Player.instance.getPosition() - (Vector2)transform.position);
        }

        shots += 1;

        Instantiate(attack, new Vector3(transform.position.x, transform.position.y, attack.transform.position.z), Quaternion.Euler(0.0f, 0.0f, fireAngle));
        
        if(shots == 4)
        {
            shots = 0;
        }
        else
        {
            timer = 0.15f;
        }

        audioSource.PlayOneShot(fireSFX);
    }
}
