using System.Collections;
using NUnit.Framework.Constraints;
using Unity.Burst.Intrinsics;
using UnityEngine;
using UnityEngine.UIElements;

public abstract class DocumentTemplate : MonoBehaviour
{
    [SerializeField]
    private UIDocument document;
    
    [SerializeField]
    private bool transitionsEnabled = true;

    [SerializeField]
    private float fadeDelay = 0.3f;

    [SerializeField]
    private float fadeDuration = 1f;

    [SerializeField]
    private StyleSheet masterStyleSheet;

    [SerializeField]
    private StyleSheet individualStyleSheet;

    public VisualElement root, blackBox;
    public bool contentCompleted=false;
    
    [SerializeField] AudioClip typingSound;

    private int talking = 0;

    private AudioSource audioSource;

    void Start()
    {
        Debug.Log("new template opened");
        StartCoroutine(Generate());

        audioSource = GetComponent<AudioSource>();
    }

    void OnValidate()
    {
        if(Application.isPlaying) return;
        StartCoroutine(Generate());
    }

    void OnEnable()
    {
        KeyboardListener.continueClicked+=transitionScene;
    }

    void OnDisable()
    {
        KeyboardListener.continueClicked-=transitionScene;
    }

    private IEnumerator Generate()
    {
        yield return null;
        
        document.rootVisualElement.Clear();
        document.rootVisualElement.styleSheets.Add(masterStyleSheet);
        if(individualStyleSheet != null)
            document.rootVisualElement.styleSheets.Add(individualStyleSheet);

        VisualElement extContainer = new VisualElement();
        
        if(transitionsEnabled && Application.isPlaying)
            extContainer.style.opacity = 0;

        extContainer.AddToClassList("ext-container");
        document.rootVisualElement.Add(extContainer);
        blackBox = Create("blackBox");
        document.rootVisualElement.Add(blackBox);

        root = extContainer;
        generateContent();
        StartCoroutine(fadeIn());
        Debug.Log("progressed");
    }

    protected void transitionScene()
    {
        if(transitionsEnabled && contentCompleted)
        {
            contentCompleted=false;
            blackBox.BringToFront();
            blackBox.experimental.animation.Start(0, 1 , (int)fadeDuration*1000, (e,v) => e.style.opacity=new StyleFloat(v));
            root.experimental.animation.Start(1, 0, (int) fadeDuration*1000, (e, v) => {e.style.opacity = new StyleFloat(v); e.style.unityBackgroundImageTintColor = new Color(0, 0, 0);}).OnCompleted(clear);
        }
        else if(contentCompleted)
            clear();
    }

    public void clear()
    {
        document.rootVisualElement.Clear();
        nextSceneRequested();
    }

    //override this method to do the funny
    protected abstract void generateContent();
    protected abstract void nextSceneRequested();

    private IEnumerator fadeIn()
    {
        if(transitionsEnabled && Application.isPlaying)
        {
            yield return new WaitForSeconds(fadeDelay);
            root.experimental.animation.Start(0, 1, (int) fadeDuration*1000, (e, v) => e.style.opacity = new StyleFloat(v));
        }
        Debug.Log("fade in attempted");
        yield return null;
    }

    public VisualElement Create(params string[] classnames)
    {
        return Create<VisualElement>(classnames);
    }

    public T Create<T>(params string[] classnames) where T : VisualElement, new()
    {
        var ele = new T();
        foreach (var classname in classnames)
        {
            ele.AddToClassList(classname);
        }
        return ele;
    }

    public void completed()
    {
        contentCompleted=true;
        transitionScene();
    }

    public void PlaySound()
    {
        if(typingSound == null || audioSource == null)
            return;
        if(talking == 0)
            audioSource.PlayOneShot(typingSound);

        talking = (talking + 1) % 3;
    }

    public void resetSound()
    {
        talking = 0;
    }
}
