using UnityEngine;
using UnityEngine.UI;
using Yarn.Unity;

public class Cutscene2Manager : CutsceneManager
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
                    dr.StartDialogue("Cutscene2_");
                    objects_[2].Play("fadeIn");
                    break;
                case 2:
                    clip = 1;
                    lv.UserRequestedViewAdvancement();
                    objects_[1].Play("officerEnterRight");
                    objects_[2].Play("playerShiftLeft");
                    break;
                case 4:
                    clip = 0;
                    lv.UserRequestedViewAdvancement();
                    objects_[0].Play("Talk");
                    objects_[2].Play("Inactive");
                    objects_[1].Play("Idle");
                    break;
                case 5:
                    objects_[3].Play("fadeInSlow");
                    lv.UserRequestedViewAdvancement();
                    break;
                case 6:
                    if (dr.IsDialogueRunning)
                        dr.Stop();
                    dr.StartDialogue("Cutscene2_1");
                    objects_[4].Play("fadeIn");
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
