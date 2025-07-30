using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;


// This file is a demonstration of how to build a simple Dialogue View that
// presents lines, by subclassing DialogueViewBase and overriding certain
// important methods.

// Before using this class, you should first get familiar with using the
// built-in Line View and Options List View, which come built-in to Yarn
// Spinner. 

// This file also includes a class called 'Tween', which handles some animation
// work. While it's not actually making any use of Yarn Spinner APIs, it might
// be of interest.

// Import the Yarn.Unity namespace so we get access to Yarn classes.
using Yarn.Unity;

// SimpleSpeechBubbleView is a Dialogue View that shows text in a box that
// animates its size up and down.
public class ToolkitDialogueViewer : DialogueViewBase
{

    // The current coroutine that's playing out a scaling animation. When this
    // is not null, we're in the middle of an animation.
    Coroutine currentAnimation;

    // Stores a reference to the method to call when the user wants to advance
    // the line.
    Action advanceHandler = null;

    public static event Func<string, IEnumerator> changedLine;
    public static event Action lineInterrupted;

    private void OnEnable()
    {
        YarnspinnerLabel.dialogueAnimationFinished+=animationFinished;
        KeyboardListener.continueClicked+=UserRequestedViewAdvancement;
    }
    private void OnDisable()
    {
        YarnspinnerLabel.dialogueAnimationFinished-=animationFinished; 
        KeyboardListener.continueClicked-=UserRequestedViewAdvancement;       
    }
    public void animationFinished()
    {
        if(currentAnimation!=null)
        {
            StopCoroutine(currentAnimation);
            currentAnimation = null;    
        }
    }
    // RunLine receives a localized line, and is in charge of displaying it to
    // the user. When the view is done with the line, it should call
    // onDialogueLineFinished.
    //
    // Unless the line gets interrupted, the Dialogue Runner will wait until all
    // views have called their onDialogueLineFinished, before telling them to
    // dismiss the line and proceeding on to the next one. This means that if
    // you want to keep a line on screen for a while, simply don't call
    // onDialogueLineFinished until you're ready.
    public override void RunLine(LocalizedLine dialogueLine, Action onDialogueLineFinished)
    {
        // We shouldn't do anything if we're not active.
        if (gameObject.activeInHierarchy == false)
        {
            // This line view isn't active; it should immediately report that
            // it's finished presenting.
            onDialogueLineFinished();
            return;
        }

        Debug.Log($"{this.name} running line {dialogueLine.TextID}");

        advanceHandler = requestInterrupt;

        //animations here
        if(changedLine==null)
        {
            StartCoroutine(waitForLabel(dialogueLine, onDialogueLineFinished));
        }
        else
        {
            currentAnimation = StartCoroutine(changedLine?.Invoke(dialogueLine.Text.Text));
        }
    }

    public IEnumerator waitForLabel(LocalizedLine dialogueLine, Action onDialogueLineFinished)
    {
        yield return new WaitUntil(()=>changedLine!=null);
        Debug.Log("reached point");
        currentAnimation = StartCoroutine(changedLine?.Invoke(dialogueLine.Text.Text));
    }
   
    public override void InterruptLine(LocalizedLine dialogueLine, Action onDialogueLineFinished)
    {
        if (gameObject.activeInHierarchy == false)
        {
            // This line view isn't active; it should immediately report that
            // it's finished presenting.
            onDialogueLineFinished();
            return;
        }

        Debug.Log($"{this.name} was interrupted while presenting {dialogueLine.TextID}");

        // If we're in the middle of an animation, stop it.
        if (currentAnimation != null)
        {
            lineInterrupted?.Invoke();
        }
        else
        {
            advanceHandler = null;
            onDialogueLineFinished();
        }
    }

    // DismissLine is called when the dialogue runner has instructed us to get
    // rid of the line. This is our view's opportunity to do whatever animations
    // we need to to get rid of the line. When we're done, we call
    // onDismissalComplete. When all line views have called their
    // onDismissalComplete, the dialogue runner moves on to the next line.
    public override void DismissLine(Action onDismissalComplete)
    {
        if (gameObject.activeInHierarchy == false)
        {
            onDismissalComplete();
            return;
        }

        Debug.Log($"{this.name} dismissing line");

        // If we have an animation running, stop it.
        if (currentAnimation != null)
        {
            StopCoroutine(currentAnimation);
            currentAnimation = null;
        }

        //animations can come b4 this
        onDismissalComplete();
    }

    // RunOptions is called when the Dialogue Runner needs to show options. It
    // receives an array containing the options, and a method to run when an
    // option has been selected. 
    //
    // This view only displays lines, not options. (We've found it useful to
    // break up the line views based on role - so, one view for lines, another
    // view for options.)
    //
    // public override void RunOptions(DialogueOption[] dialogueOptions,
    // Action<int> onOptionSelected)
    // {
    // }

    public override void UserRequestedViewAdvancement()
    {
        advanceHandler?.Invoke();
    }
}
