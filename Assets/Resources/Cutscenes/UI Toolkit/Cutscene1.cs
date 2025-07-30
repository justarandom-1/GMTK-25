using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class Cutscene1 : DocumentTemplate
{
    public int nodesCompleted;

    protected override void generateContent()
    {
        VisualElement backgroundCanvas = Create("backgroundCanvas");
        YarnspinnerLabel dialogueBox = Create<YarnspinnerLabel>("DialogueBox");
        VisualElement protagonist = Create("protagonist");
        VisualElement medalAwarder = Create("medal");

        root.Add(backgroundCanvas);
        root.Add(protagonist);
        root.Add(medalAwarder);
        root.Add(dialogueBox);
    }


    protected override void nextSceneRequested()
    {
        SceneManager.LoadSceneAsync("Menu");
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
}
