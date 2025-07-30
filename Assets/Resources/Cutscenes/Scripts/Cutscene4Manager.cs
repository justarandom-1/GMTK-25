using UnityEngine;
using UnityEngine.UI;
using Yarn.Unity;

public class Cutscene4Manager : CutsceneManager
{
    [SerializeField] GameObject extraAudio;

    protected override void Start()
    {
        base.Start();
    }


    public override void Increment()
    {
        if (skipping) return;

        if (!lv.currentStopToken.CanInterrupt && stage < 49)
        {
            stage++;

            switch (stage)
            {
                case 1:
                    dr.StartDialogue("Cutscene4_");
                    objects_[0].Play("fadeIn");
                    break;
                case 2:
                    lv.UserRequestedViewAdvancement();
                    clip = 1;
                    objects_[1].Play("fadeIn");
                    objects_[0].Play("Idle");
                    break;
                case 31:
                    if (!extraAudio.GetComponent<AudioSource>().isPlaying)
                        extraAudio.GetComponent<AudioSource>().Play();
                    goto case 41;
                case 7:
                case 10:
                case 14:
                case 25:
                case 38:
                case 41:
                    lv.UserRequestedViewAdvancement();
                    clip = 0;
                    objects_[1].Play("Idle");
                    objects_[0].Play("Talk");
                    break;
                case 11:
                    objects_[2].Play("fadeIn");
                    goto case 43;
                case 27:
                    objects_[3].Play("Idle");
                    goto case 43;
                case 8:
                case 15:
                case 32:
                case 39:
                case 43:
                    lv.UserRequestedViewAdvancement();
                    clip = 1;
                    objects_[0].Play("Idle");
                    objects_[1].Play("Talk");
                    break;

                case 16:
                    lv.UserRequestedViewAdvancement();
                    objects_[4].Play("fadeIn");
                    extraAudio.GetComponent<AudioSource>().loop = false;
                    break;

                case 19:
                    lv.UserRequestedViewAdvancement();
                    objects_[4].Play("fadeOut");
                    extraAudio.GetComponent<AudioSource>().loop = true;
                    if (!extraAudio.GetComponent<AudioSource>().isPlaying)
                        extraAudio.GetComponent<AudioSource>().Play();
                    break;

                case 26:
                    lv.UserRequestedViewAdvancement();
                    objects_[0].Play("Idle");
                    objects_[1].Play("Idle");
                    objects_[3].Play("fadeIn");
                    clip = 3;
                    extraAudio.GetComponent<AudioSource>().loop = false;
                    break;

                case 30:
                    extraAudio.GetComponent<AudioSource>().loop = true;
                    extraAudio.GetComponent<AudioSource>().PlayDelayed(3.75f);
                    goto default;

                case 45:
                    extraAudio.GetComponent<AudioSource>().loop = false;
                    goto default;

                case 46:
                    lv.UserRequestedViewAdvancement();
                    objects_[5].Play("fadeIn");
                    objects_[6].Play("Shuttle");
                    audioSource.PlayOneShot(sounds[4], dialogueVol);
                    break;

                case 48:
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
