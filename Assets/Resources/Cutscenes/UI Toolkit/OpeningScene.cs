using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using Yarn;

public class OpeningScene : DocumentTemplate
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void generateContent()
    {
        Debug.Log("Content being generated");
        VisualElement backgroundCanvas = Create("backgroundCanvas");
        YarnspinnerLabel dialogueBox = Create<YarnspinnerLabel>("DialogueBox");
        VisualElement protagonist = Create("protagonist");

        root.Add(backgroundCanvas);
        root.Add(protagonist);
        root.Add(dialogueBox);
    }



    protected override void nextSceneRequested()
    {
        SceneManager.LoadSceneAsync("OpeningScene2");
    }
}
