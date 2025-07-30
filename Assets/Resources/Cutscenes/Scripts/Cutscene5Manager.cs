using UnityEngine;
using UnityEngine.UI;
using Yarn.Unity;
using UnityEngine.SceneManagement;

public class Cutscene5Manager : CutsceneManager
{

    [SerializeField] GameObject extraAudio;

    protected override void Start()
    {
        base.Start();
    }


    public override void Increment()
    {
        if (skipping) return;
        
        if (!lv.currentStopToken.CanInterrupt && stage < 36)
        {
            stage++;
            switch (stage)
            {
                case 1:
                    objects_[1].Play("fadeIn");
                    objects_[2].Play("tankFall2");
                    objects_[3].Play("dustCloud");
                    extraAudio.GetComponent<AudioSource>().Play();
                    break;
                case 2:
                    dr.StartDialogue("Cutscene5_");
                    objects_[2].Play("Inactive");
                    objects_[3].Play("Inactive");
                    objects_[0].Play("fadeIn");
                    extraAudio.GetComponent<AudioSource>().Stop();
                    break;
                case 6:
                    lv.UserRequestedViewAdvancement();
                    objects_[0].Play("playerShiftLeft1");
                    objects_[4].Play("fadeInDelayed");
                    break;
                case 7:
                    if (dr.IsDialogueRunning)
                        dr.Stop();
                    dr.StartDialogue("Cutscene5_1");
                    objects_[4].Play("dogApproach");
                    break;
                case 10:
                    lv.UserRequestedViewAdvancement();
                    break;
                case 15:
                    lv.UserRequestedViewAdvancement();
                    objects_[5].Play("fadeInSlow");
                    objects_[6].Play("fadeInSlow");
                    objects_[7].Play("fadeInSlow");
                    break;
                case 16:
                    if (dr.IsDialogueRunning)
                        dr.Stop();
                    dr.StartDialogue("Cutscene5_2");
                    break;
                case 18:
                    lv.UserRequestedViewAdvancement();
                    Debug.Log("A");
                    objects_[6].Play("dogSurprise");
                    objects_[8].Play("shipLanding");
                    objects_[9].Play("fadeInDelayed1");
                    objects_[10].Play("shipLanding1");
                    audioSource.PlayOneShot(sounds[2]);
                    break;
                case 19:
                    if (dr.IsDialogueRunning)
                        dr.Stop();
                    dr.StartDialogue("Cutscene5_3");
                    objects_[9].Play("Active");
                    objects_[10].Play("Active");
                    objects_[11].Play("fadeIn");
                    clip = 1;
                    break;
                case 20:
                    lv.UserRequestedViewAdvancement();
                    clip = 0;
                    objects_[12].Play("fadeIn");
                    objects_[11].Play("pedroShiftLeft");
                    break;
                case 21:
                    objects_[11].Play("Inactive");
                    goto case 26;
                case 26:
                case 31:
                case 34:
                    lv.UserRequestedViewAdvancement();
                    clip = 1;
                    objects_[12].Play("Idle");
                    objects_[13].Play("Talk");
                    break;



                case 24:
                case 29:
                case 32:
                    lv.UserRequestedViewAdvancement();
                    clip = 0;
                    objects_[12].Play("Talk");
                    objects_[13].Play("Idle");
                    break;

                case 33:
                    lv.UserRequestedViewAdvancement();
                    audioSource.Stop();
                    break;


                case 35:
                    lv.UserRequestedViewAdvancement();
                    GameObject.Find("Black").GetComponent<Animator>().Play("shown");
                    audioSource.PlayOneShot(sounds[3]);
                    break;


                case 36:
                    SceneManager.LoadScene("Menu");
                    return;
                default:
                    lv.UserRequestedViewAdvancement();
                    break;
            }
        }
        else
        {
            lv.UserRequestedViewAdvancement();
        }
        talking = 0;

    }
}
