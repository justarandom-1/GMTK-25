using UnityEngine;
using Yarn.Unity;

public class OpeningSceneManager : CutsceneManager
{

    protected override void Start()
    {
        base.Start();
        nextScene = "Level1";
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
                    dr.StartDialogue("Opening1");
                    objects_[0].Play("fadeIn");
                    break;
                case 9:
                    lv.UserRequestedViewAdvancement();
                    nextScene = "OpeningScene_2";
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
