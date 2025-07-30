using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Yarn.Unity;


public class CutsceneManager : LevelManager
{
    [SerializeField] protected GameObject[] objects;

    [SerializeField] protected AudioClip[] sounds;

    protected int clip = 0;
    public int talking = 0;

    protected List<Animator> objects_;
    public int stage = 0;

    protected LineView lv;

    protected DialogueRunner dr;

    protected bool skipping;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        base.Start();
        lv = GetComponent<LineView>();

        objects_ = new List<Animator>();

        for (int i = 0; i < objects.Length; i++)
        {
            objects_.Add(objects[i].GetComponent<Animator>());
        }

        nextScene = "Menu";

        dr = GameObject.Find("Dialogue System").GetComponent<DialogueRunner>();
    }

    protected override void Update()
    {
        state2();
    }
    public virtual void Increment()
    {
        lv.UserRequestedViewAdvancement();
        stage++;
    }

    public void DialogueAudio()
    {
        if(clip == -1)
            return;
        if(talking == 0)
            audioSource.PlayOneShot(sounds[clip], dialogueVol);

        talking = (talking + 1) % 3;
    }

    public void SkipScene()
    {
        audioSource.PlayOneShot(Resources.Load<AudioClip>("SFX/select"));

        skipping = true;
        EndLevel();
    }
}
