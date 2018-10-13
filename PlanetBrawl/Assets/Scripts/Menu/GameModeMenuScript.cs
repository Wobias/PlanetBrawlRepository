using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class GameModeMenuScript : MonoBehaviour, ISelectHandler, IDeselectHandler
{

    private ScrollRect scrollRect;
    Color highlightedColor = new Color(.0f, 0.0f, 0.0f, 0.7f);
    Color nothighlightedColor = new Color(0f, 0f, 0f, 0.5f);


    private RectTransform rect;



    public void OnSelect(BaseEventData eventData)
    {
        transform.parent.GetComponent<Image>().color = highlightedColor;
       // scrollRect.horizontalNormalizedPosition -= 0.01f;
    }

    public void OnDeselect(BaseEventData data)
    {
        transform.parent.GetComponent<Image>().color = nothighlightedColor;

    }
}
