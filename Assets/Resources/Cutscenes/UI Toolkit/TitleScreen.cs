using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class TitleScreen : DocumentTemplate
{
    Label glowingText;
    Label title;
    VisualElement backgroundCanvas;
    protected override void generateContent()
    {
        contentCompleted = true;
        backgroundCanvas = Create("background-canvas");
        VisualElement runningDog = Create("running-dog");
        glowingText = Create<Label>("glowing-text");
        title = Create<Label>("title");
        glowingText.text = "Click space to begin";
        title.text = "外星人";

        root.Add(backgroundCanvas);
        backgroundCanvas.Add(runningDog);
        backgroundCanvas.Add(glowingText);
        backgroundCanvas.Add(title);

        labelGlow();
    }

    protected override void nextSceneRequested()
    {
        //idk fill this in later
        SceneManager.LoadSceneAsync("Exposition");
    }

    private void labelGlow()
    {
        if(Application.isPlaying)
        {
            title.experimental.animation.Start(155, 228, 1500, (e, v) => e.style.color = new StyleColor(new Color(34.0f/255, v/255.0f, 34.0f/255)));
            glowingText.experimental.animation.Start(155, 228, 1500, (e, v) => e.style.color = new StyleColor(new Color(34.0f/255, v/255.0f, 34.0f/255))).OnCompleted(labelDarken);
        }
    }
    private void labelDarken()
    {
        if(Application.isPlaying)
        {
            title.experimental.animation.Start(228, 155, 1500, (e, v) => e.style.color = new StyleColor(new Color(34.0f/255, v/255.0f, 34.0f/255)));
            glowingText.experimental.animation.Start(228, 155, 1500, (e, v) => e.style.color = new StyleColor(new Color(34.0f/255, v/255.0f, 34.0f/255))).OnCompleted(labelGlow);
        }
    }
}
