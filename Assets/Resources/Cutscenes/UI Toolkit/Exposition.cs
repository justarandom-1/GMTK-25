using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class Exposition : DocumentTemplate
{
    TypewriterLabel narration, narration2, narration3, narration4;
    VisualElement storyboardImage, storyboardImage2, storyboardImage3, storyboardImage4;
    VisualElement container;
    protected override void generateContent()
    {
        narration = Create<TypewriterLabel>("narration", "n1");
        narration2 = Create<TypewriterLabel>("narration", "n2");
        narration3 = Create<TypewriterLabel>("narration", "n3");
        narration4 = Create<TypewriterLabel>("narration", "n4");

        storyboardImage = Create("sb-image", "i1");
        storyboardImage2 = Create("sb-image", "i2");
        storyboardImage3 = Create("sb-image", "i3");
        storyboardImage4 = Create("sb-image", "i4");
        container = Create("container");

        root.Add(container);
        container.Add(narration);
        container.Add(storyboardImage);
        container.Add(narration2);
        container.Add(storyboardImage2);
        container.Add(narration3);
        container.Add(storyboardImage3);
        container.Add(narration4);
        container.Add(storyboardImage4);

        storyboardImage3.style.opacity=0;
        storyboardImage4.style.opacity=0;


        if(Application.isPlaying)
        {
            narration.setNewText("Now you may be wondering how we got to this point.");
            narration2.setNewText("Well, sometime during the year 2027, humanity happened to discover the presence of alien dogs on the Moon.");
            narration3.setNewText("What was even weirder was that they happened to be in some kind of farming structure. With insufficient resources to confidently investigate, humanity chose to merely spectate from a distance.");
            narration4.setNewText("But then, the unthinkable occurred. An asteroid struck the Moon, causing chaos to unfold on the farm. The dogs escaped, and many came to rampage on Earth...");
            StartCoroutine(narration.autoIncrement(animationOne, 0.05f));
        }
    }

    protected override void nextSceneRequested()
    {
        SceneManager.LoadSceneAsync("OpeningScene_1");
    }

    public void zoomOut(Action onCompleted)
    {
        container.experimental.animation.Start(10, 5, 500, (e, v) => e.style.scale = new StyleScale(new Scale(new Vector2(v/10f, v/10f)))).OnCompleted(onCompleted);
    }
   
    public void animationOne()
    {
        zoomOut(animationTwo);
    }
    public void animationTwo()
    {
        container.experimental.animation.Start(0, -960, 500, (e,v)=> e.style.left = v).OnCompleted(animationThree);
    }
    public void animationThree()
    {
        container.experimental.animation.Start(5, 10, 500, (e, v) => {e.style.scale = new StyleScale(new Scale(new Vector2(v/10f, v/10f))); e.style.left = -960 + (((v-5)/10)*-1536);}).OnCompleted(animationFour); 
    }
    public void animationFour()
    {
        StartCoroutine(narration2.autoIncrement(animationFive, 0.05f));
    }
    public void animationFive()
    {
        storyboardImage3.style.opacity=100;
        container.experimental.animation.Start(10, 5, 500, (e, v) => {e.style.scale = new StyleScale(new Scale(new Vector2(v/10f, v/10f))); e.style.left = -960 + (((v-5)/5)*-768); Debug.Log(e.style.left);}).OnCompleted(animationSix); 
    }
    public void animationSix()
    {
        container.experimental.animation.Start(0, -480, 500, (e,v)=> e.style.top = v).OnCompleted(animationSeven);
    }
    public void animationSeven()
    {
        container.experimental.animation.Start(5, 10, 500, (e, v) => {e.style.scale = new StyleScale(new Scale(new Vector2(v/10f, v/10f))); e.style.left = -960 + (((v-5)/10)*-1536); e.style.top=-480 + (((v-5)/10)*-1032);}).OnCompleted(animationEight); 
    }
    public void animationEight()
    {
        StartCoroutine(narration3.autoIncrement(animationNine, 0.05f));
    }
    public void animationNine()
    {
        storyboardImage4.style.opacity=100;
        storyboardImage2.style.opacity=0;
        narration2.style.opacity=0;
        container.experimental.animation.Start(10, 5, 500, (e, v) => {e.style.scale = new StyleScale(new Scale(new Vector2(v/10f, v/10f))); e.style.left = -960 + (((v-5)/5)*-768); e.style.top = -480 + (((v-5)/5)*-516);}).OnCompleted(animationTen); 
    }
    public void animationTen()
    {
        container.experimental.animation.Start(-960, -1920, 500, (e,v)=> e.style.left = v).OnCompleted(animationEleven);
    }
    public void animationEleven()
    {
        container.experimental.animation.Start(5, 10, 500, (e, v) => {e.style.scale = new StyleScale(new Scale(new Vector2(v/10f, v/10f))); e.style.left = -1920 + (((v-5)/10)*-3072); e.style.top=-480 + (((v-5)/10)*-1032);}).OnCompleted(animationTwelve); 
    }
    public void animationTwelve()
    {
        StartCoroutine(narration4.autoIncrement(transition, 0.05f));
    }
    public void transition()
    {
        Debug.Log("transition");
        contentCompleted=true;
        transitionScene();
    }
}
