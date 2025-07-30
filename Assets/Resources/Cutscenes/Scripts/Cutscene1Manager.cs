using UnityEngine;
using UnityEngine.UI;
using Yarn.Unity;

public class Cutscene1Manager : CutsceneManager
{

    protected override void Start()
    {
        base.Start();
    }

 

    public override void Increment()
    {
        if (skipping) return;
        
        if (!lv.currentStopToken.CanInterrupt && stage < 9)
        {
            stage++;

            switch (stage)
            {
                case 1:
                    dr.StartDialogue("Cutscene1");
                    clip = 1;
                    objects_[1].Play("fadeIn");
                    break;
                case 2:
                    lv.UserRequestedViewAdvancement();
                    clip = 0;
                    objects_[0].Play("fadeIn");
                    objects_[1].Play("Idle");
                    break;
                case 3:
                    objects_[1].gameObject.transform.GetChild(0).gameObject.SetActive(true);
                    objects_[0].gameObject.transform.GetChild(2).gameObject.SetActive(true);
                    goto case 5;
                case 8:
                    objects_[0].gameObject.transform.GetChild(2).gameObject.SetActive(true);
                    goto case 5;
                case 5:
                    lv.UserRequestedViewAdvancement();
                    objects_[0].Play("Idle");
                    objects_[1].Play("Talk");
                    clip = 1;
                    break;
                case 6:
                    lv.UserRequestedViewAdvancement();
                    objects_[0].gameObject.transform.GetChild(1).gameObject.SetActive(true);
                    objects_[0].gameObject.transform.GetChild(2).gameObject.SetActive(false);
                    clip = 1;
                    break;
                case 7:
                    objects_[0].gameObject.transform.GetChild(0).gameObject.SetActive(true);
                    objects_[0].gameObject.transform.GetChild(1).gameObject.SetActive(false);
                    goto case 4;
                case 4:
                    lv.UserRequestedViewAdvancement();
                    objects_[1].Play("Idle");
                    objects_[0].Play("Talk");
                    clip = 0;
                    break;
                case 9:
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
