using System;
using Unity.VisualScripting;
using UnityEngine;
using Yarn.Unity;

public class KeyboardListener_ : MonoBehaviour
{
    //mostly to check if we already exist
    public static Action continueClicked;

    // private bool keyHeld = true;

    private CutsceneManager cm;

    private void Awake()
    {
        // if(instance != null)
        // {
        //     Destroy(gameObject);
        //     return;
        // }   

        // instance = this;
        // DontDestroyOnLoad(gameObject);

        cm = GetComponent<CutsceneManager>();
    }

    public void Update()
    {
        // if(!Input.anyKey){
        //     keyHeld = false;
        // }

        // if(Input.anyKey && !keyHeld){
        //     keyHeld = true;
            
        //     cm.Increment();
        // }   
        
        if(Input.anyKeyDown){           
            cm.Increment();
        }   
    }
}
