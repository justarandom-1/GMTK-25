using UnityEngine;
using UnityEngine.SceneManagement;

public class Cutscene4 : DocumentTemplate
{
    protected override void generateContent()
    {
        transitionScene();
    }

    protected override void nextSceneRequested()
    {
        SceneManager.LoadSceneAsync("Menu");
    }
}
