using UnityEngine;

public class FinalBoss : Dog
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected int mode = 0;

    [SerializeField] GameObject rings;
    [SerializeField] GameObject explosives;

    [SerializeField] GameObject spawner;

    protected int shots = 0;

    protected float fireAngle = 0;

    protected GameObject largeBall;

    void selectNextMode()
    {
        mode = 2 * (1 - mode / 2) + Random.Range(0, 2);

        timer = 2.25f;

        shots = 0;

        fireRate = 1.5f;
    }

    protected override void grab()
    {
        base.grab();
        LevelManager.instance.Win();
    }

    protected override void Attack()
    {
        switch(mode)
        {
            case 0:               
                if(shots >= 6)
                {
                    selectNextMode();
                    return;
                }
                Instantiate(attack, new Vector3(transform.position.x, transform.position.y, attack.transform.position.z), Quaternion.Euler(0.0f, 0.0f, Random.Range(0, 360)));
                audioSource.PlayOneShot(fireSFX);
                break;
            case 1:
                if(shots >= 15)
                {
                    selectNextMode();
                    return;
                }
                else
                {
                    timer = 0.4f;
                }

                fireAngle = Random.Range(0, 360);

                Instantiate(rings, new Vector3(transform.position.x, transform.position.y, attack.transform.position.z), Quaternion.Euler(0.0f, 0.0f, fireAngle));

                if(shots % 2 == 0)
                    audioSource.PlayOneShot(fireSFX);

                break;

            case 2:
                switch(shots)
                {
                    case 0:
                        fireAngle = Random.Range(0, 360);
                        timer = 1;
                        break;
                    case 1:
                        fireAngle += 30;
                        fireRate = 0.5f;
                        break;
                    default:
                        timer = 1;
                        if(largeBall == null){
                            selectNextMode();
                        }
                        return;
                }

                largeBall = Instantiate(explosives, new Vector3(transform.position.x, transform.position.y, attack.transform.position.z), Quaternion.Euler(0.0f, 0.0f, fireAngle));
                largeBall.transform.GetChild(0).gameObject.GetComponent<explosiveBullet>().setup(audioSource);
                audioSource.PlayOneShot(fireSFX);
                break;
            case 3:
                if(shots == 0){

                    fireAngle = LevelManager.VectorToAngle(Player.instance.getPosition() - (Vector2)transform.position);

                    largeBall = Instantiate(spawner, new Vector3(transform.position.x, transform.position.y, attack.transform.position.z), Quaternion.Euler(0.0f, 0.0f, fireAngle));
                    audioSource.PlayOneShot(fireSFX);

                    fireRate = 0.5f;
                }

                if(largeBall == null){
                    selectNextMode();
                    return;
                }
                break;
        }

        // if(shots == 0)
        // {
        //     fireAngle = LevelManager.VectorToAngle(Player.instance.getPosition() - (Vector2)transform.position);
        // }

        // shots += 1;

        // Instantiate(attack, new Vector3(transform.position.x, transform.position.y, attack.transform.position.z), Quaternion.Euler(0.0f, 0.0f, fireAngle));
        
        // if(shots == 4)
        // {
        //     shots = 0;
        // }
        // else
        // {
        //     timer = 0.15f;
        // }

        shots++;
    }
}
