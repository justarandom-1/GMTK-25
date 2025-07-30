using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using Yarn;

public class OpeningScene2 : DocumentTemplate
{
    int nodesCompleted=0;
    VisualElement protagonist, dog, tank;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void generateContent()
    {
        VisualElement backgroundCanvas = Create("backgroundCanvas2");
        YarnspinnerLabel dialogueBox = Create<YarnspinnerLabel>("DialogueBox");
        protagonist = Create("protagonist2");
        dog = Create("dog");
        tank = Create("tank");
        

        root.Add(backgroundCanvas);
        root.Add(protagonist);
        root.Add(dog);
        root.Add(tank);
        root.Add(dialogueBox);
        dog.style.opacity=0;
        protagonist.style.opacity = 0;
        tank.style.opacity=0;
    }

    public void incrementNodesCompleted()
    {
        nodesCompleted++;
        if(nodesCompleted==1)
        {
            dog.experimental.animation.Start(0, 1, 300, (e, v)=>e.style.opacity = new StyleFloat(v));
            protagonist.experimental.animation.Start(0, 1, 300, (e, v)=>e.style.opacity = new StyleFloat(v));
        }
        if(nodesCompleted==2)
        {
            tank.experimental.animation.Start(0, 1, 300, (e, v)=>e.style.opacity = new StyleFloat(v)).OnCompleted(slowTankMove);
        }
    }

    private void slowTankMove()
    {
        tank.experimental.animation.Start(-300, -100, 12000, (e,v)=>e.style.left=v);
    }

    protected override void nextSceneRequested()
    {
        SceneManager.LoadSceneAsync("Menu");
    }
}
