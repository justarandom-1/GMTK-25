using System;
using System.Collections;
using System.ComponentModel.Design.Serialization;
// using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

[UxmlElement]
public partial class TypewriterLabel : Label
{
    [UxmlAttribute]
    public string sourceText;
    private int substringLength;
    protected bool incrementing=false;

    private DocumentTemplate document;

    public TypewriterLabel()
    {
        text = "Placeholder text before typewriter starts running";
    }
    public TypewriterLabel(string tempText = "Placeholder text before typewriter starts running")
    {
        text = tempText;
    }

    public void hideText()
    {
        substringLength=0;
        text = sourceText.Substring(0, substringLength) + "<alpha=#00>"+sourceText.Substring(substringLength);
        incrementing=true;
    }

    public char increment()
    {
        substringLength++;
        if(substringLength<sourceText.Length)
            text = sourceText.Substring(0, substringLength) + "<alpha=#00>"+sourceText.Substring(substringLength);
        else
            displayFull();

        if(document != null)
            document.PlaySound();

        return sourceText[substringLength - 1];        
    }

    public void displayFull()
    {
        text=sourceText;
        incrementing=false;
        onAnimationFinished();
    }

    public void setNewText(string newText)
    {
        sourceText=newText;
        hideText();

        if(GameObject.Find("UIDocument") != null)
            document = GameObject.Find("UIDocument").GetComponent<DocumentTemplate>();

    }

    public IEnumerator autoIncrement(Action onComplete = null, float speed = 0.03f)
    {
        if(document != null)
            document.resetSound();
        while(incrementing)
        {
            char c = increment();

            switch(c)
            {
                case '…':
                case '.':                
                    yield return new WaitForSeconds(speed * 5);
                    break;

                case ',':                
                    yield return new WaitForSeconds(speed * 3);
                    break;

                default:
                    yield return new WaitForSeconds(speed);
                    break;
            }

        }
        onComplete?.Invoke();
    }


    public virtual void onAnimationFinished()
    {
        //do nothing??? idk :sob: why cant i make this an abstract class 
    }
}

