using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DragLv3 : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    // Start is called before the first frame update

    private RectTransform rectTran1;
    private CanvasGroup cvGr;
    public Image imgTrue;
    Vector2 v2;

    private void Awake()
    {
        rectTran1 = GetComponent<RectTransform>();
        cvGr = GetComponent<CanvasGroup>();
        v2 = new Vector2(rectTran1.anchoredPosition.x, rectTran1.anchoredPosition.y);
    }
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        //Debug.Log("1");
        //throw new System.NotImplementedException();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        cvGr.blocksRaycasts = false;
        //Debug.Log("2");
        //throw new System.NotImplementedException();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        //throw new System.NotImplementedException();
        if (imgTrue != null)
        {
            if (rectTran1.anchoredPosition != imgTrue.rectTransform.anchoredPosition)
            {
                rectTran1.anchoredPosition = v2;
            }
            else if (rectTran1.anchoredPosition == imgTrue.rectTransform.anchoredPosition)
            {
                //this.GetComponent<Image>().raycastTarget = false;
            }
        }
        cvGr.blocksRaycasts = true;
    }

    public void OnDrag(PointerEventData eventData)
    {
        //Debug.Log("4");
        rectTran1.anchoredPosition += eventData.delta;
        //throw new System.NotImplementedException();
    }
}
