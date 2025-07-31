using UnityEngine;

public class BossFight : LevelManager
{

    private float[] intervals = {0.5f, 1.5f, 1.5f};

    private int state = 0;

    private GameObject QIinstance;

    private float timer;

    [SerializeField] GameObject beamAttack;

    [SerializeField] GameObject beamTeleport;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        base.Start();  
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (state < intervals.Length && timer > intervals[state])
        {
            switch (state)
            {
                case 0:
                    GameObject beam1 = Instantiate(beamAttack, new Vector3(Player.instance.getPosition().x, -5.5f, -2), Quaternion.identity);
                    beam1.GetComponent<QIBeam>().Initialize(beam1.transform.position, beam1.transform, 90);
                    break;

                case 1:
                    QIinstance = Instantiate(beamTeleport, new Vector3(7.5f, Player.instance.getPosition().y, -2), Quaternion.identity);
                    QIinstance.GetComponent<QI>().Initialize(180);
                    break;

                case 2:
                    if (QIinstance != null)
                        return;
                    QIinstance = Instantiate(beamTeleport, new Vector3(-7.5f, Player.instance.getPosition().y, -2), Quaternion.identity);
                    QIinstance.GetComponent<QI>().Initialize(0);
                    break;
            }

            state++;

        }
    }
}
