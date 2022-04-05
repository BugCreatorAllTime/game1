using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AnimationController : MonoBehaviour
{
    private Animator selfAnim;
    
    
    // Start is called before the first frame update
    void Start()
    {
        selfAnim = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
    
        
        

        
    }
    public void turnOffAnimation(){
        
         selfAnim.enabled = false;
    }
    public void turnOnAnimation(){
        
        selfAnim.enabled=true;

    }
}
