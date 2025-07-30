using UnityEngine;

public class LaserField : MonoBehaviour
{
    private Transform parent;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        parent = transform.parent;
        transform.SetParent(null);
    }

    // Update is called once per frame
    void Update()
    {
        if(parent == null)
        {
            Destroy(gameObject);
            return;
        }
        transform.position = parent.position;
    }
}
