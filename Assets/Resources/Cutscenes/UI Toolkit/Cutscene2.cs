// using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class Cutscene2 : DocumentTemplate
{
    protected override void generateContent()
    {
        VisualElement backgroundCanvas = Create("backgroundCanvas2");
        YarnspinnerLabel dialogueBox = Create<YarnspinnerLabel>("DialogueBox");
        VisualElement protagonist = Create("protagonist2");
        
        root.Add(backgroundCanvas);
        root.Add(protagonist);
        root.Add(dialogueBox);
    }

    protected override void nextSceneRequested()
    {
        SceneManager.LoadSceneAsync("Menu");
    }
}
