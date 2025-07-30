using UnityEngine;
using UnityEngine.SceneManagement;

public class menuTemp : MonoBehaviour
{

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 1; i <= 5; i++)
            if (Input.GetKeyDown("" + i))
                SceneManager.LoadScene("Level" + i.ToString());

    }
}
