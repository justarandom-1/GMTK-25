using UnityEngine;
using UnityEngine.UI;
using Yarn.Unity;

public class OpeningSceneManager1 : CutsceneManager
{

    protected override void Start()
    {
        base.Start();
        nextScene = "Level1";
    }

 

    public override void Increment()
    {
        if (skipping) return;
        
        if (!lv.currentStopToken.CanInterrupt && stage < 22)
        {
            stage++;

            switch (stage)
            {
                case 1:
                    dr.StartDialogue("Opening2");
                    objects_[0].Play("fadeIn");
                    break;
                case 4:
                    objects_[1].Play("fadeIn");
                    break;
                case 5:
                    objects_[1].Play("fadeOut");
                    lv.UserRequestedViewAdvancement();
                    break;
                case 7:
                    RectTransform t = objects_[0].gameObject.GetComponent<RectTransform>();
                    Vector3 scale = t.localScale;
                    t.localScale = new Vector3(scale.x * -1, scale.y, scale.z);
                    lv.UserRequestedViewAdvancement();
                    break;
                case 8:
                    objects_[2].Play("fadeIn");
                    objects_[3].Play("fadeIn");
                    objects_[4].Play("tankEntrance");
                    lv.UserRequestedViewAdvancement();
                    break;
                case 14:
                    lv.UserRequestedViewAdvancement();
                    break;
                case 16:
                    audioSource.Stop();
                    lv.UserRequestedViewAdvancement();
                    break;
                case 22:
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
