using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lr_LineController : MonoBehaviour
{

    private LineRenderer lr;
    [SerializeField] List<GameObject> points;

    private Transform a;
    private Transform b;
    // Start is called before the first frame update
    void Awake()
    {
        lr = GetComponent<LineRenderer>();
        lr.enabled = false;

        a = points[0].transform;
        b = points[1].transform;
    }

    // Update is called once per frame
    void Update()
    {
        if(!lr.enabled)
            return;

        lr.SetPosition(0, a.position);
        lr.SetPosition(1, b.position);
    }
}
