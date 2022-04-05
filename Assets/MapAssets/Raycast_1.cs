using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Raycast_1 : MonoBehaviour
{
    public GameObject itemMove;
    void Start()
    {
        
    }
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Vector2 posRay = Raycast1();

            if (itemMove != null && posRay != Vector2.zero)
                itemMove.transform.position = posRay;
        }    
    }

    public Vector2 Raycast1()
    {
        RaycastHit hit;
        //Debug.Log("mouse :" + Input.mousePosition.x + "-" + Input.mousePosition.y);
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            //Debug.DrawRay(hit.point, Vector2.forward, Color.red, 20f);
            //Debug.Log("hit : " + hit.transform.tag);
            //Debug.Log("ray : " + ray.ToString());
            if (hit.transform.tag == "itemMove")
            {
                Debug.Log("ray : " + ray.ToString());
                itemMove = hit.transform.gameObject;
            }
            if(hit.transform.tag == "BgPnLv4")
            {
                Debug.Log("bg :" + hit.point);
                return hit.point;
            }
            return Vector2.zero;
        }

        return Vector2.zero;
    }    
}
