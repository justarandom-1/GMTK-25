using UnityEngine;
using UnityEngine.UI;
using Yarn.Unity;

public class Cutscene3Manager : CutsceneManager
{

    [SerializeField] GameObject extraAudio;

    protected override void Start()
    {
        base.Start();
    }


    public override void Increment()
    {
        if (skipping) return;
        
        if (!lv.currentStopToken.CanInterrupt && stage < 21)
        {
            stage++;

            switch (stage)
            {
                case 1:
                    dr.StartDialogue("Cutscene3_");
                    objects_[0].Play("fadeIn");
                    break;
                case 2:
                    lv.UserRequestedViewAdvancement();
                    objects_[1].Play("fadeIn");
                    objects_[2].Play("fadeIn");
                    clip = 1;
                    break;
                case 6:
                    lv.UserRequestedViewAdvancement();
                    clip = 0;
                    objects_[1].Play("fadeOut");
                    objects_[2].Play("fadeOut");
                    objects_[3].Play("Active");
                    extraAudio.GetComponent<AudioSource>().Play();
                    break;
                case 11:
                    lv.UserRequestedViewAdvancement();
                    audioSource.PlayOneShot(sounds[2]);
                    objects_[3].Play("InactiveDelayed");
                    objects_[4].Play("Shot");
                    extraAudio.GetComponent<AudioSource>().Stop();
                    break;
                case 12:
                    if (dr.IsDialogueRunning)
                        dr.Stop();
                    dr.StartDialogue("Cutscene3_1");
                    objects_[5].Play("trumpEnterLeft");
                    clip = 1;
                    RectTransform t = objects_[0].gameObject.GetComponent<RectTransform>();
                    Vector3 scale = t.localScale;
                    t.localScale = new Vector3(scale.x * -1, scale.y, scale.z);
                    objects_[0].Play("Idle");
                    break;
                case 16:
                    lv.UserRequestedViewAdvancement();
                    objects_[0].Play("Talk");
                    objects_[5].Play("Idle");
                    clip = 0;
                    break;
                case 20:
                    lv.UserRequestedViewAdvancement();
                    clip = 1;
                    objects_[5].Play("Talk");
                    objects_[0].Play("Idle");
                    break;
                case 21:
                    lv.UserRequestedViewAdvancement();
                    EndLevel();
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
