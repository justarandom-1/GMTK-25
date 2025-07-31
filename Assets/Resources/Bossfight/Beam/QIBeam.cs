using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QIBeam : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] int damage;

    [SerializeField] AudioClip SFX;

    public float angle;

    // private float range;
    private Vector2 path;

    private Transform start;

    private Vector2 center;

    private Vector2[] targets = new Vector2[4];

    private Transform[] vertices = new Transform[5];

    private int state = 0;

    private lr_LineController line;
    private AudioSource audioSource;

    List<GameObject> hits = new List<GameObject>();

    void Start()
    {
        
    }

    public void Initialize(Vector2 parentPos, Transform s, float a, bool hasted = false)
    {
        center = parentPos;

        angle = a;

        audioSource = GetComponent<AudioSource>();

        line = GetComponent<lr_LineController>();

        if (hasted)
            speed *= 2;

        vertices[0] = start = s;

        vertices[1] = transform.GetChild(0);

        for (int i = 2; i < vertices.Length; i++)
        {
            vertices[i] = vertices[i - 1].GetChild(0);
        }

        getPath();

        Debug.Log(path);

        float minInc = 1f / (6 * targets.Length);

        float maxInc = 1f / (2 * targets.Length);

        float section = Random.Range(minInc, maxInc);

        Vector2 normal = LevelManager.Deg2Vec(angle + 90);

        for (int i = 0; i < targets.Length - 2; i++)
        {
            targets[i] = center + path * section + normal * Random.Range(0.5f, 1) * Mathf.Pow(-1, i);
            section += Random.Range(minInc, maxInc);

            Debug.Log(targets[i]);
        }

        targets[targets.Length - 2] = center + path * Random.Range(section, section + maxInc);

        Debug.Log(targets[targets.Length - 2]);

        targets[targets.Length - 1] = center + path;

        Debug.Log(targets[targets.Length - 1]);

        // var allTowers = FindObjectsByType<TowerController>(FindObjectsSortMode.None);

        // for(int i = 0; i < allTowers.Length; i++)
        // {
        //     if(allTowers[i].gameObject == LevelManager.instance.Base && (allTowers.Length > 3 && (numOGTowers > 1  || Random.Range(0, 2) == 0)))
        //         continue;
        //     addToList(allTowers[i].gameObject.transform);
        // }

        // shortenTargets();


        line.SetUpLine(vertices);

        state = 0;

        reachedTarget();

    }

    // void shortenTargets()
    // {
    //     int i = 0;

    //     for(; i < 4; i++)
    //     {
    //         if(targets[i] == null)
    //             break;
    //     }

    //     Transform[] t  = new Transform[i];

    //     for(int j = 0; j < i; j++)
    //         t[j] = targets[j];

    //     targets = t;
    // }

    public void reachedTarget(bool terminate = false)
    {
        state += 1;

        if (state >= targets.Length + 1 || terminate)
        {
            Destroy(gameObject);
            return;
        }

        LevelManager.instance.PlayAudio(SFX);

        vertices[state].gameObject.GetComponent<Vertex>().Initialize(targets[state - 1], this, speed);

        vertices[state].SetParent(transform);
    }


    void FixedUpdate()
    {
        // if(targets[0] == null)
        //     Destroy(gameObject);
    }

    public void hit(GameObject target)
    {
        Player.instance.takeDamage(damage);
        // Destroy(gameObject);
    }

    private void getPath()
    {
        path = LevelManager.Deg2Vec(angle);

        if (path.x == 0)
        {
            path *= (Mathf.Abs((center.y - (5.25f * Mathf.Sign(path.y)))/ path.y));
            return;
        }

        if (path.y == 0)
        {
            path *= (Mathf.Abs((center.x - (9f * Mathf.Sign(path.x))) / path.x));
            return;
        }
        path *= Mathf.Min((Mathf.Abs((center.x - (9f * Mathf.Sign(path.x))) / path.x)),
                          (Mathf.Abs((center.y - (5.25f * Mathf.Sign(path.y))) / path.y)));
    }
}
