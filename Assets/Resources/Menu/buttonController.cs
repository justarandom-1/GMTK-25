using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using TMPro;

public class buttonController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    new private RectTransform transform;
    private Vector3 scale;

    private Button button;
    // Start is called before the first frame update
    void Start()
    {
        transform = GetComponent<RectTransform>();
        scale = transform.localScale;
        button = GetComponent<Button>();

        GetComponent<Image>().alphaHitTestMinimumThreshold = 0.5f;

    }

    public void OnPointerEnter(PointerEventData pointerEventData)
	{
        if(button.interactable){
            transform.localScale = scale * 1.1F;
        }
	}

    public void OnPointerExit(PointerEventData pointerEventData)
	{
        if(button.interactable){
            transform.localScale = scale;
        }
	}

    void Update(){
        if(!button.interactable && transform.localScale != scale){
            transform.localScale = scale;
        }
    }
}
