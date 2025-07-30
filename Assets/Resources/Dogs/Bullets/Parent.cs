using UnityEngine;

public class Parent : MonoBehaviour
{
    [SerializeField] float delay;
    [SerializeField] float rotateSpeed;
    private Vector3 scale;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        scale = transform.localScale;

    }

    // Update is called once per frame
    void Update()
    {
        if(delay > 0){

            delay -= Time.deltaTime;

            if(delay <= 0)
            {
                AudioSource audioSource = GetComponent<AudioSource>();
                if(audioSource != null)
                    audioSource.Play();

                for(int i = 0; i < transform.childCount; i++)
                {
                    transform.GetChild(i).gameObject.SetActive(true);
                }
            }
        }

        if(transform.childCount == 0)
            Destroy(gameObject);


        if(transform.parent != null)
            transform.localScale = new Vector3(Mathf.Sign(transform.parent.localScale.x) * scale.x, scale.y, scale.z);

        transform.Rotate(new Vector3(0, 0, rotateSpeed * Time.deltaTime));
    }
}
