using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DrogLv3 : MonoBehaviour, IDropHandler
{

    public Image imgX;
    private void Awake()
    {
    }
    public void OnDrop(PointerEventData eventData)
    {
        //throw new System.NotImplementedException();
        Image imgInp = eventData.pointerDrag.GetComponent<Image>();


        if (imgInp.sprite.name == imgX.sprite.name)
        {
            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
            eventData.pointerDrag.GetComponent<Image>().raycastTarget = false;
        }
        else
        { 
        }
    }
    void WinLv3()
    {
        Debug.Log("Win");

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
