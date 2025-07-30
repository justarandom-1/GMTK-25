using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class Cutscene3 : DocumentTemplate
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
     protected override void generateContent()
    {
        VisualElement backgroundCanvas = Create("backgroundCanvas3");
        YarnspinnerLabel dialogueBox = Create<YarnspinnerLabel>("DialogueBox");
        VisualElement protagonist = Create("protagonist3");
        VisualElement protestors = Create("protestors");
        VisualElement tv = Create("tv");
        VisualElement dog = Create("dog");
    
        
        root.Add(backgroundCanvas);
        root.Add(protagonist);
        root.Add(protestors);
        root.Add(tv);
        root.Add(dog);
        root.Add(dialogueBox);
    }

    protected override void nextSceneRequested()
    {
        SceneManager.LoadSceneAsync("Menu");
    }
}
