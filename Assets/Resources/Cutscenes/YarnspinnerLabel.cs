using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

[UxmlElement]
public partial class YarnspinnerLabel : TypewriterLabel
{
    public static event Action dialogueAnimationFinished;

    public YarnspinnerLabel()
    {
        text = "Placeholder text before typewriter starts running";
        registerCallbacks();
    }
    public YarnspinnerLabel(string text = "Placeholder text before typewriter starts running")
    {
        this.text=text;
        registerCallbacks();
    }

    private void registerCallbacks()
    {
        RegisterCallback<DetachFromPanelEvent>(deregister);
        RegisterCallback<AttachToPanelEvent>(register);
    }
    private void register(AttachToPanelEvent evt)
    {
        ToolkitDialogueViewer.changedLine+=newLine;
        ToolkitDialogueViewer.lineInterrupted+=interruptLine;
    }
    private void deregister(DetachFromPanelEvent evt)
    {
        ToolkitDialogueViewer.changedLine-=newLine;
        ToolkitDialogueViewer.lineInterrupted-=interruptLine;
        UnregisterCallback<AttachToPanelEvent>(register);
        UnregisterCallback<DetachFromPanelEvent>(deregister);
    }
    

    public IEnumerator newLine(string line)
    {
        Debug.Log("new line invoked");
        setNewText(line);
        while(incrementing)
        {
            increment();
            yield return new WaitForSeconds(0.03f);
        }
    }

    public void interruptLine()
    {
        displayFull();
    }

    public override void onAnimationFinished()
    {
        dialogueAnimationFinished?.Invoke();
    }
}
