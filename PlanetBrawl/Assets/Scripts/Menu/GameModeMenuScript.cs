using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class GameModeMenuScript : MonoBehaviour, ISelectHandler, IDeselectHandler
{

    Color highlightedColor = new Color(.0f, 0.0f, 0.0f, 0.7f);
    Color nothighlightedColor = new Color(0f, 0f, 0f, 0.5f);


    public void OnSelect(BaseEventData eventData)
    {
        transform.parent.GetComponent<Image>().color = highlightedColor;
        Camera.main.transform.position = new Vector3(transform.parent.position.x, transform.parent.position.y, transform.parent.position.z - 1);
    }

    public void OnDeselect(BaseEventData data)
    {
        transform.parent.GetComponent<Image>().color = nothighlightedColor;

    }
}
