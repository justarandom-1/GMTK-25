using System;
using Unity.VisualScripting;
using UnityEngine;

public class KeyboardListener : MonoBehaviour
{
    //mostly to check if we already exist
    public static KeyboardListener instance;
    public static Action continueClicked;

    private void Awake()
    {
        if(instance != null)
        {
            Destroy(gameObject);
            return;
        }   

        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)||Input.GetKeyDown(KeyCode.Return))
        {
            continueClicked?.Invoke();
        }   
    }
}
